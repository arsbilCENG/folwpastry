import React, { useState, useEffect } from 'react';
import { Modal, Descriptions, Tag, Button, Divider, Typography, Image, Spin, Alert, Form, InputNumber, Radio, Space, Input } from 'antd';
import { useCakeOrder, useUpdateCakeOrderStatus, useCancelCakeOrder } from '../../hooks/useCakeOrders';
import { CAKE_ORDER_STATUS_CONFIG } from '../../utils/constants';
import { PaymentMethod, PAYMENT_METHOD_LABELS } from '../../types/purchase';

const { Text, Title } = Typography;

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
  
  const [deliveryModalVisible, setDeliveryModalVisible] = useState(false);
  const [deliveryForm] = Form.useForm();

  const order = orderResponse?.data;

  useEffect(() => {
    if (deliveryModalVisible && order) {
      deliveryForm.setFieldsValue({
        finalPaymentAmount: order.remainingAmount || 0,
        finalPaymentMethod: PaymentMethod.Cash
      });
    }
  }, [deliveryModalVisible, order, deliveryForm]);

  const handleUpdateStatus = async (newStatus: any, additionalData?: any) => {
    await updateStatusMutation.mutateAsync({ 
      id: orderId, 
      data: { 
        newStatus,
        ...additionalData
      } 
    });
    setDeliveryModalVisible(false);
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
          <Button 
            key="deliver" 
            type="primary" 
            style={{ background: '#52c41a', borderColor: '#52c41a' }} 
            onClick={() => setDeliveryModalVisible(true)}
          >
            Müşteriye Teslim Et
          </Button>
        );
      }
      if (order.status === 'Created' || order.status === 'SentToProduction') {
        buttons.push(
          <Button key="cancel" danger onClick={handleCancel} loading={cancelOrderMutation.isPending}>
            Siparişi İptal Et
          </Button>
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

  const formatCurrency = (amount: number) => `₺ ${amount.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}`;
  const formatDate = (dateStr?: string) => dateStr ? new Date(dateStr).toLocaleDateString('tr-TR') : '-';

  return (
    <>
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
              <Descriptions.Item label="Tarih">{formatDate(order.createdAt)}</Descriptions.Item>
              <Descriptions.Item label="Oluşturan">{order.createdByUserName}</Descriptions.Item>
              <Descriptions.Item label="Şube">{order.branchName}</Descriptions.Item>
              <Descriptions.Item label="Teslim Tarihi">
                <Text strong style={{ color: '#1890ff' }}>{formatDate(order.deliveryDate)}</Text>
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
              <Descriptions.Item label="Fiyat"><Text strong>{formatCurrency(order.price)}</Text></Descriptions.Item>
              <Descriptions.Item label="Kek Türü">{order.cakeTypeName}</Descriptions.Item>
              <Descriptions.Item label="İç Krema">{order.innerCreamName}</Descriptions.Item>
              <Descriptions.Item label="Dış Krema" span={2}>{order.outerCreamName}</Descriptions.Item>
            </Descriptions>

            <Divider>Ödeme Özeti</Divider>
            <Descriptions column={2} bordered size="small">
              <Descriptions.Item label="Toplam Fiyat" span={2}>
                <Text strong>{formatCurrency(order.price)}</Text>
              </Descriptions.Item>
              
              {order.depositAmount !== undefined && order.depositAmount > 0 && (
                <>
                  <Descriptions.Item label="Kapora">
                    <Space direction="vertical" size={0}>
                      <Text strong>{formatCurrency(order.depositAmount)} ({PAYMENT_METHOD_LABELS[order.depositPaymentMethod!]})</Text>
                      <Text type="secondary" style={{ fontSize: 12 }}>{formatDate(order.depositPaidAt)}</Text>
                    </Space>
                  </Descriptions.Item>
                  <Descriptions.Item label="Tahsil Eden">{order.depositCollectedByUserName || '-'}</Descriptions.Item>
                </>
              )}

              {order.finalPaymentAmount !== undefined && order.finalPaymentAmount > 0 && (
                <>
                  <Descriptions.Item label="Kalan Ödeme">
                    <Space direction="vertical" size={0}>
                      <Text strong>{formatCurrency(order.finalPaymentAmount)} ({PAYMENT_METHOD_LABELS[order.finalPaymentMethod!]})</Text>
                      <Text type="secondary" style={{ fontSize: 12 }}>{formatDate(order.finalPaymentPaidAt)}</Text>
                    </Space>
                  </Descriptions.Item>
                  <Descriptions.Item label="Teslim Eden">{order.statusText === 'Teslim Edildi' ? order.createdByUserName : '-'}</Descriptions.Item>
                </>
              )}

              <Descriptions.Item label="Ödeme Durumu" span={2}>
                {order.remainingAmount === 0 ? (
                  <Tag color="green">✓ Ödendi</Tag>
                ) : (
                  <Tag color="orange">! {formatCurrency(order.remainingAmount || order.price)} Kalan</Tag>
                )}
              </Descriptions.Item>
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

      {/* DELIVERY MODAL */}
      <Modal
        title={
          <Space>
            <span>🚚 Sipariş Teslimatı</span>
            <Text type="secondary" style={{ fontSize: 14 }}>{order?.orderNumber}</Text>
          </Space>
        }
        open={deliveryModalVisible}
        onCancel={() => setDeliveryModalVisible(false)}
        onOk={() => deliveryForm.submit()}
        okText="Teslim Et"
        cancelText="İptal"
        okButtonProps={{ 
          style: { background: '#52c41a', borderColor: '#52c41a' },
          loading: updateStatusMutation.isPending 
        }}
        width={400}
      >
        <Form
          form={deliveryForm}
          layout="vertical"
          onFinish={(values) => handleUpdateStatus('Delivered', values)}
        >
          <div style={{ marginBottom: 20, padding: 16, background: '#f5f5f5', borderRadius: 8 }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 8 }}>
              <Text type="secondary">Toplam Fiyat:</Text>
              <Text strong>{formatCurrency(order?.price || 0)}</Text>
            </div>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 8 }}>
              <Text type="secondary">Alınan Kapora:</Text>
              <Text strong>{formatCurrency(order?.depositAmount || 0)}</Text>
            </div>
            <Divider style={{ margin: '8px 0' }} />
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
              <Text strong>Kalan Ödeme:</Text>
              <Text strong style={{ fontSize: 18, color: '#f5222d' }}>{formatCurrency(order?.remainingAmount || 0)}</Text>
            </div>
          </div>

          {order?.remainingAmount && order.remainingAmount > 0 ? (
            <>
              <Form.Item 
                name="finalPaymentAmount" 
                label="Tahsil Edilen Tutar"
                rules={[{ required: true, message: 'Tutar giriniz' }]}
              >
                <InputNumber 
                  style={{ width: '100%' }} 
                  addonBefore="₺"
                  precision={2}
                  max={order.remainingAmount}
                  min={0}
                  onFocus={(e) => e.target.select()}
                  inputMode="numeric"
                  keyboard={false}
                />
              </Form.Item>

              <Form.Item name="finalPaymentMethod" label="Ödeme Yöntemi">
                <Radio.Group buttonStyle="solid" style={{ width: '100%' }}>
                  <Radio.Button value={PaymentMethod.Cash} style={{ width: '50%', textAlign: 'center' }}>Nakit</Radio.Button>
                  <Radio.Button value={PaymentMethod.CreditCard} style={{ width: '50%', textAlign: 'center' }}>Kart</Radio.Button>
                </Radio.Group>
              </Form.Item>
            </>
          ) : (
            <Alert message="Ödeme Tamamlanmış" description="Bu siparişin tüm ödemesi kapora ile alınmıştır." type="success" showIcon />
          )}

          <Form.Item name="statusNote" label="Teslimat Notu (Opsiyonel)">
            <Input.TextArea placeholder="Müşteri memnun kaldı, paketlendi." rows={2} />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

export default CakeOrderDetail;
