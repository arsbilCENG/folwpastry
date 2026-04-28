import React, { useState } from 'react';
import { 
  Typography, Button, Card, Row, Col, Tag, Segmented, Space, 
  Empty, Spin, Popconfirm 
} from 'antd';
import { ClockCircleOutlined, ShopOutlined, ExceptionOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import 'dayjs/locale/tr';
import { useCakeOrders, useUpdateCakeOrderStatus } from '../../hooks/useCakeOrders';
import { CAKE_ORDER_STATUS_CONFIG } from '../../utils/constants';
import CakeOrderDetail from '../../components/cake/CakeOrderDetail';
import useAuth from '../../hooks/useAuth';

dayjs.locale('tr');

const { Title, Text } = Typography;

const ProductionCakeOrders: React.FC = () => {
  const { user } = useAuth();
  const [statusFilter, setStatusFilter] = useState<string>('Bekleyen');
  const [detailModalVisible, setDetailModalVisible] = useState(false);
  const [selectedOrderId, setSelectedOrderId] = useState<string | null>(null);

  const { data: ordersResponse, isLoading } = useCakeOrders();
  const updateStatusMutation = useUpdateCakeOrderStatus();

  const orders = ordersResponse?.data || [];

  const filteredOrders = orders.filter(order => {
    if (statusFilter === 'Tümü') return true;
    if (statusFilter === 'Bekleyen') return order.status === 'SentToProduction';
    if (statusFilter === 'Üretimde') return order.status === 'InProduction';
    if (statusFilter === 'Hazır') return order.status === 'Ready' || order.status === 'Delivered';
    return true;
  });

  const handleOpenDetail = (id: string) => {
    setSelectedOrderId(id);
    setDetailModalVisible(true);
  };

  const handleUpdateStatus = async (id: string, newStatus: any) => {
    await updateStatusMutation.mutateAsync({ id, data: { newStatus } });
  };

  const isDeliveryClose = (deliveryDateStr: string) => {
    const delivery = dayjs(deliveryDateStr);
    const today = dayjs().startOf('day');
    const diff = delivery.diff(today, 'day');
    return diff >= 0 && diff <= 2;
  };

  return (
    <div style={{ padding: '24px' }}>
      <Row justify="space-between" align="middle" style={{ marginBottom: 24 }}>
        <Col>
          <Title level={2} style={{ margin: 0 }}>🎂 Özel Pasta Siparişleri</Title>
        </Col>
      </Row>

      <div style={{ marginBottom: 24, overflowX: 'auto', paddingBottom: 8 }}>
        <Segmented 
          options={['Tümü', 'Bekleyen', 'Üretimde', 'Hazır']} 
          value={statusFilter}
          onChange={(val) => setStatusFilter(val as string)}
          size="large"
        />
      </div>

      {isLoading ? (
        <div style={{ textAlign: 'center', padding: '50px' }}><Spin size="large" /></div>
      ) : filteredOrders.length === 0 ? (
        <Empty description={`Bu filtreye uygun sipariş bulunamadı (${statusFilter})`} />
      ) : (
        <Row gutter={[16, 16]}>
          {filteredOrders.map(order => (
            <Col xs={24} sm={12} lg={8} key={order.id}>
              <Card 
                style={{ 
                  borderRadius: 12, 
                  border: isDeliveryClose(order.deliveryDate) && order.status !== 'Ready' && order.status !== 'Delivered' 
                          ? '1px solid #ffa39e' : undefined 
                }}
                bodyStyle={{ padding: 20 }}
              >
                <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 12 }}>
                  <Text strong style={{ fontSize: 16 }}>🎂 {order.orderNumber}</Text>
                  <Tag color={CAKE_ORDER_STATUS_CONFIG[order.status]?.color || 'default'}>
                    {CAKE_ORDER_STATUS_CONFIG[order.status]?.text || order.statusText}
                  </Tag>
                </div>
                
                <div style={{ marginBottom: 8 }}>
                  <Text><ShopOutlined /> {order.branchName} • {order.servingSize} Kişilik</Text>
                </div>

                <div style={{ marginBottom: 16 }}>
                  {isDeliveryClose(order.deliveryDate) && order.status !== 'Ready' && order.status !== 'Delivered' ? (
                    <Text type="danger" strong><ClockCircleOutlined /> Teslim: {new Date(order.deliveryDate).toLocaleDateString('tr-TR')}</Text>
                  ) : (
                    <Text><ClockCircleOutlined /> Teslim: {new Date(order.deliveryDate).toLocaleDateString('tr-TR')}</Text>
                  )}
                </div>

                <div style={{ marginBottom: 16 }}>
                  <Text type="secondary" style={{ fontSize: 13 }}>
                    {order.cakeTypeName} / {order.innerCreamName} / {order.outerCreamName}
                  </Text>
                </div>

                <div style={{ borderTop: '1px solid #f0f0f0', paddingTop: 16, display: 'flex', flexWrap: 'wrap', gap: 8 }}>
                  <Button icon={<ExceptionOutlined />} onClick={() => handleOpenDetail(order.id)}>
                    Detay
                  </Button>
                  
                  {order.status === 'SentToProduction' && (
                    <Button 
                      type="primary" 
                      loading={updateStatusMutation.isPending}
                      onClick={() => handleUpdateStatus(order.id, 'InProduction')}
                      style={{ flex: 1 }}
                    >
                      🔧 Üretime Al
                    </Button>
                  )}

                  {order.status === 'InProduction' && (
                    <Popconfirm
                      title="Siparişi hazır olarak işaretlemek istiyor musunuz?"
                      onConfirm={() => handleUpdateStatus(order.id, 'Ready')}
                      okText="Evet"
                      cancelText="Hayır"
                    >
                      <Button 
                        type="primary" 
                        loading={updateStatusMutation.isPending}
                        style={{ background: '#52c41a', borderColor: '#52c41a', flex: 1 }}
                      >
                        ✅ Hazır
                      </Button>
                    </Popconfirm>
                  )}
                </div>
              </Card>
            </Col>
          ))}
        </Row>
      )}

      {selectedOrderId && (
        <CakeOrderDetail
          orderId={selectedOrderId}
          visible={detailModalVisible}
          onClose={() => setDetailModalVisible(false)}
          userRole={user?.role || 'Production'}
        />
      )}
    </div>
  );
};

export default ProductionCakeOrders;
