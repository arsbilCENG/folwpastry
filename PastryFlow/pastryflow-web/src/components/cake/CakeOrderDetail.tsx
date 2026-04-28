import React from 'react';
import { Modal, Descriptions, Tag, Button, Popconfirm, Divider, Typography, Image, Spin, Alert } from 'antd';
import { useCakeOrder, useUpdateCakeOrderStatus, useCancelCakeOrder } from '../../hooks/useCakeOrders';
import { CAKE_ORDER_STATUS_CONFIG } from '../../utils/constants';

const { Text } = Typography;

interface CakeOrderDetailProps {
  orderId: string;
  visible: boolean;
  onClose: () => void;
  userRole: string;
}

const CakeOrderDetail: React.FC<CakeOrderDetailProps> = ({ orderId, visible, onClose, userRole }) => {
  const { data: orderResponse, isLoading } = useCakeOrder(orderId);
  const updateStatusMutation = useUpdateCakeOrderStatus();
  const cancelOrderMutation = useCancelCakeOrder();

  const order = orderResponse?.data;

  const handleUpdateStatus = async (newStatus: any) => {
    await updateStatusMutation.mutateAsync({ id: orderId, data: { newStatus } });
    onClose();
  };

  const handleCancel = async () => {
    await cancelOrderMutation.mutateAsync(orderId);
    onClose();
  };

  const renderFooter = () => {
    if (!order) return [<Button key="close" onClick={onClose}>Kapat</Button>];

    const buttons = [];

    // Sales Actions
    if (userRole === 'Sales' || userRole === 'Satış') {
      if (order.status === 'Ready') {
        buttons.push(
          <Popconfirm
            key="deliver"
            title="Siparişi müşteriye teslim ettiniz mi?"
            onConfirm={() => handleUpdateStatus('Delivered')}
            okText="Evet"
            cancelText="Hayır"
          >
            <Button type="primary" style={{ background: '#52c41a', borderColor: '#52c41a' }} loading={updateStatusMutation.isPending}>
              Müşteriye Teslim Edildi
            </Button>
          </Popconfirm>
        );
      }
      if (order.status === 'Created' || order.status === 'SentToProduction') {
        buttons.push(
          <Popconfirm
            key="cancel"
            title="Siparişi iptal etmek istediğinize emin misiniz?"
            onConfirm={handleCancel}
            okText="Evet, İptal Et"
            cancelText="Hayır"
            okButtonProps={{ danger: true }}
          >
            <Button danger loading={cancelOrderMutation.isPending}>İptal Et</Button>
          </Popconfirm>
        );
      }
    }

    // Production Actions
    if (userRole === 'Production' || userRole === 'Mutfak') {
      if (order.status === 'SentToProduction') {
        buttons.push(
          <Button key="produce" type="primary" onClick={() => handleUpdateStatus('InProduction')} loading={updateStatusMutation.isPending}>
            Üretime Al
          </Button>
        );
      }
      if (order.status === 'InProduction') {
        buttons.push(
          <Button key="ready" type="primary" style={{ background: '#52c41a', borderColor: '#52c41a' }} onClick={() => handleUpdateStatus('Ready')} loading={updateStatusMutation.isPending}>
            Hazır
          </Button>
        );
      }
    }

    buttons.push(<Button key="close" onClick={onClose}>Kapat</Button>);
    return buttons;
  };

  return (
    <Modal
      title={`🎂 Sipariş Detayı — ${order?.orderNumber || ''}`}
      open={visible}
      onCancel={onClose}
      footer={renderFooter()}
      width={700}
    >
      {isLoading || !order ? (
        <div style={{ textAlign: 'center', padding: 40 }}><Spin /></div>
      ) : (
        <div>
          <Descriptions column={2} bordered size="small">
            <Descriptions.Item label="Sipariş No">{order.orderNumber}</Descriptions.Item>
            <Descriptions.Item label="Tarih">{new Date(order.createdAt).toLocaleDateString('tr-TR')}</Descriptions.Item>
            <Descriptions.Item label="Oluşturan">{order.createdByUserName}</Descriptions.Item>
            <Descriptions.Item label="Şube">{order.branchName}</Descriptions.Item>
            <Descriptions.Item label="Teslim Tarihi">
              <Text strong style={{ color: '#1890ff' }}>{new Date(order.deliveryDate).toLocaleDateString('tr-TR')}</Text>
            </Descriptions.Item>
            <Descriptions.Item label="Durum">
              <Tag color={CAKE_ORDER_STATUS_CONFIG[order.status]?.color || 'default'}>
                {CAKE_ORDER_STATUS_CONFIG[order.status]?.text || order.statusText}
              </Tag>
            </Descriptions.Item>
          </Descriptions>

          <Divider>Müşteri Bilgileri</Divider>
          <Descriptions column={2} bordered size="small">
            <Descriptions.Item label="Ad Soyad">{order.customerName || '-'}</Descriptions.Item>
            <Descriptions.Item label="Telefon">{order.customerPhone || '-'}</Descriptions.Item>
          </Descriptions>

          <Divider>Pasta Detayları</Divider>
          <Descriptions column={2} bordered size="small">
            <Descriptions.Item label="Kişi Sayısı"><Text strong>{order.servingSize}</Text></Descriptions.Item>
            <Descriptions.Item label="Fiyat"><Text strong>₺ {order.price.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}</Text></Descriptions.Item>
            <Descriptions.Item label="Kek Türü">{order.cakeTypeName}</Descriptions.Item>
            <Descriptions.Item label="İç Krema">{order.innerCreamName}</Descriptions.Item>
            <Descriptions.Item label="Dış Krema" span={2}>{order.outerCreamName}</Descriptions.Item>
          </Descriptions>

          <Divider>Açıklama</Divider>
          <div style={{ padding: '12px', background: '#fafafa', borderRadius: '4px', border: '1px solid #f0f0f0' }}>
            <Text>{order.description}</Text>
          </div>

          {order.referencePhotoUrl && (
            <>
              <Divider>Referans Fotoğraf</Divider>
              <div style={{ textAlign: 'center' }}>
                <Image 
                  src={order.referencePhotoUrl} 
                  alt="Referans Fotoğraf" 
                  style={{ maxHeight: 300, objectFit: 'contain', borderRadius: '8px' }} 
                />
              </div>
            </>
          )}

          {order.statusNote && order.status === 'Cancelled' && (
            <Alert message="İptal Nedeni" description={order.statusNote} type="error" showIcon style={{ marginTop: 16 }} />
          )}
        </div>
      )}
    </Modal>
  );
};

export default CakeOrderDetail;
