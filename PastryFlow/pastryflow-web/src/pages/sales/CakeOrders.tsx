import React, { useState } from 'react';
import { 
  Typography, Button, Card, Row, Col, Tag, Segmented, Space, 
  Empty, Spin, Modal, Form, Input, DatePicker, InputNumber, Select, Alert 
} from 'antd';
import { PlusOutlined, InfoCircleOutlined, ClockCircleOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import 'dayjs/locale/tr';
import { useCakeOrders, useCreateCakeOrder, useUploadCakeReferencePhoto, useCakeOptions } from '../../hooks/useCakeOrders';
import { CAKE_ORDER_STATUS_CONFIG } from '../../utils/constants';
import CakeOrderDetail from '../../components/cake/CakeOrderDetail';
import PhotoUpload from '../../components/common/PhotoUpload';
import useAuth from '../../hooks/useAuth';

dayjs.locale('tr');

const { Title, Text } = Typography;
const { TextArea } = Input;
const { Option } = Select;

const CakeOrders: React.FC = () => {
  const { user } = useAuth();
  const [statusFilter, setStatusFilter] = useState<string>('Tümü');
  const [detailModalVisible, setDetailModalVisible] = useState(false);
  const [selectedOrderId, setSelectedOrderId] = useState<string | null>(null);
  
  const [createModalVisible, setCreateModalVisible] = useState(false);
  const [form] = Form.useForm();
  const [referencePhoto, setReferencePhoto] = useState<File | null>(null);

  // Queries
  const apiFilter = statusFilter === 'Tümü' ? undefined :
                    statusFilter === 'Bekleyen' ? 'Created' :
                    statusFilter === 'Üretimde' ? 'InProduction' :
                    statusFilter === 'Hazır' ? 'Ready' :
                    statusFilter === 'Teslim' ? 'Delivered' : undefined;

  const { data: ordersResponse, isLoading } = useCakeOrders();
  const createMutation = useCreateCakeOrder();
  const uploadPhotoMutation = useUploadCakeReferencePhoto();

  // Options Queries
  const { data: cakeTypesRes } = useCakeOptions('CakeType');
  const { data: innerCreamsRes } = useCakeOptions('InnerCream');
  const { data: outerCreamsRes } = useCakeOptions('OuterCream');

  const orders = ordersResponse?.data || [];

  const filteredOrders = orders.filter(order => {
    if (statusFilter === 'Tümü') return true;
    if (statusFilter === 'Bekleyen') return order.status === 'Created' || order.status === 'SentToProduction';
    if (statusFilter === 'Üretimde') return order.status === 'InProduction';
    if (statusFilter === 'Hazır') return order.status === 'Ready';
    if (statusFilter === 'Teslim') return order.status === 'Delivered';
    return true;
  });

  const handleOpenDetail = (id: string) => {
    setSelectedOrderId(id);
    setDetailModalVisible(true);
  };

  const handleCreateSubmit = async (values: any) => {
    try {
      const dto = {
        customerName: values.customerName,
        customerPhone: values.customerPhone,
        deliveryDate: values.deliveryDate.format('YYYY-MM-DD'),
        servingSize: values.servingSize,
        cakeTypeId: values.cakeTypeId,
        innerCreamId: values.innerCreamId,
        outerCreamId: values.outerCreamId,
        description: values.description,
        price: values.price,
      };

      const res = await createMutation.mutateAsync(dto);
      const createdOrderId = res.data?.id;

      if (createdOrderId && referencePhoto) {
        await uploadPhotoMutation.mutateAsync({ id: createdOrderId, photo: referencePhoto });
      }

      setCreateModalVisible(false);
      form.resetFields();
      setReferencePhoto(null);
    } catch (e) {
      // Error handled by mutation
    }
  };

  const isDeliveryClose = (deliveryDateStr: string) => {
    const delivery = dayjs(deliveryDateStr);
    const today = dayjs().startOf('day');
    const diff = delivery.diff(today, 'day');
    return diff >= 0 && diff <= 2;
  };

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ 
        display: 'flex', 
        justifyContent: 'space-between', 
        alignItems: 'center', 
        marginBottom: 24,
        flexWrap: 'wrap',
        gap: 12
      }}>
        <Title level={2} style={{ margin: 0 }}>🎂 Özel Pasta Siparişleri</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={() => setCreateModalVisible(true)} size="large">
          Yeni Sipariş
        </Button>
      </div>

      <div style={{ marginBottom: 24, overflowX: 'auto', paddingBottom: 8 }}>
        <Segmented 
          options={['Tümü', 'Bekleyen', 'Üretimde', 'Hazır', 'Teslim']} 
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
                hoverable 
                onClick={() => handleOpenDetail(order.id)}
                style={{ 
                  borderRadius: 12, 
                  border: isDeliveryClose(order.deliveryDate) && order.status !== 'Delivered' && order.status !== 'Cancelled' 
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
                  <Space size="large" wrap>
                    <Text><InfoCircleOutlined /> {order.servingSize} Kişilik</Text>
                    {isDeliveryClose(order.deliveryDate) && order.status !== 'Delivered' && order.status !== 'Cancelled' ? (
                      <Text type="danger" strong><ClockCircleOutlined /> Teslim: {new Date(order.deliveryDate).toLocaleDateString('tr-TR')}</Text>
                    ) : (
                      <Text><ClockCircleOutlined /> Teslim: {new Date(order.deliveryDate).toLocaleDateString('tr-TR')}</Text>
                    )}
                  </Space>
                </div>

                <div style={{ marginBottom: 16 }}>
                  <Text type="secondary" style={{ fontSize: 13 }}>
                    {order.cakeTypeName} / {order.innerCreamName} / {order.outerCreamName}
                  </Text>
                </div>

                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', borderTop: '1px solid #f0f0f0', paddingTop: 12 }}>
                  <Text strong style={{ fontSize: 16 }}>₺ {order.price.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}</Text>
                  <Button type="link" style={{ padding: 0 }}>Detay →</Button>
                </div>
              </Card>
            </Col>
          ))}
        </Row>
      )}

      {/* CREATE ORDER MODAL */}
      <Modal
        title="🎂 Yeni Özel Pasta Siparişi"
        open={createModalVisible}
        onCancel={() => setCreateModalVisible(false)}
        footer={null}
        width={700}
        destroyOnClose
      >
        <Form 
          form={form} 
          layout="vertical" 
          onFinish={handleCreateSubmit}
          initialValues={{ servingSize: 10, price: 0 }}
        >
          <div style={{ marginBottom: 16 }}>
            <Text type="secondary" strong>Müşteri Bilgileri (Opsiyonel)</Text>
          </div>
          <Row gutter={16}>
            <Col xs={24} md={12}>
              <Form.Item name="customerName" label="Müşteri Adı">
                <Input placeholder="Ad Soyad" />
              </Form.Item>
            </Col>
            <Col xs={24} md={12}>
              <Form.Item name="customerPhone" label="Telefon">
                <Input placeholder="05XX XXX XX XX" />
              </Form.Item>
            </Col>
          </Row>

          <div style={{ marginBottom: 16 }}>
            <Text type="secondary" strong>Sipariş Detayları</Text>
          </div>
          <Row gutter={16}>
            <Col xs={24} md={12}>
              <Form.Item 
                name="deliveryDate" 
                label="Teslim Tarihi" 
                rules={[{ required: true, message: 'Teslim tarihi zorunludur' }]}
              >
                <DatePicker 
                  style={{ width: '100%' }} 
                  format="DD.MM.YYYY" 
                  disabledDate={(current) => current && current < dayjs().startOf('day')}
                  placeholder="Tarih Seçin"
                />
              </Form.Item>
            </Col>
            <Col xs={24} md={12}>
              <Form.Item 
                name="servingSize" 
                label="Kişi Sayısı" 
                rules={[{ required: true, message: 'Kişi sayısı zorunludur' }]}
              >
                <InputNumber min={1} style={{ width: '100%' }} />
              </Form.Item>
            </Col>
          </Row>

          <div style={{ marginBottom: 16 }}>
            <Text type="secondary" strong>Pasta Seçimleri</Text>
          </div>
          <Row gutter={16}>
            <Col xs={24} md={8}>
              <Form.Item 
                name="cakeTypeId" 
                label="Kek Türü" 
                rules={[{ required: true, message: 'Zorunlu' }]}
              >
                <Select placeholder="Seçiniz">
                  {cakeTypesRes?.data?.filter(o => o.isActive).map(o => (
                    <Option key={o.id} value={o.id}>{o.name}</Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
            <Col xs={24} md={8}>
              <Form.Item 
                name="innerCreamId" 
                label="İç Krema" 
                rules={[{ required: true, message: 'Zorunlu' }]}
              >
                <Select placeholder="Seçiniz">
                  {innerCreamsRes?.data?.filter(o => o.isActive).map(o => (
                    <Option key={o.id} value={o.id}>{o.name}</Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
            <Col xs={24} md={8}>
              <Form.Item 
                name="outerCreamId" 
                label="Dış Krema" 
                rules={[{ required: true, message: 'Zorunlu' }]}
              >
                <Select placeholder="Seçiniz">
                  {outerCreamsRes?.data?.filter(o => o.isActive).map(o => (
                    <Option key={o.id} value={o.id}>{o.name}</Option>
                  ))}
                </Select>
              </Form.Item>
            </Col>
          </Row>

          <Row gutter={16}>
            <Col xs={24} md={12}>
              <Form.Item 
                name="price" 
                label="Fiyat" 
                rules={[{ required: true, message: 'Fiyat zorunludur' }]}
              >
                <InputNumber addonBefore="₺" precision={2} min={0.01} style={{ width: '100%' }} />
              </Form.Item>
            </Col>
          </Row>

          <Form.Item 
            name="description" 
            label="Açıklama" 
            rules={[{ required: true, message: 'Açıklama zorunludur' }]}
          >
            <TextArea 
              rows={4} 
              showCount 
              maxLength={2000} 
              placeholder="Pastanın üzeri ve şeritleri toz pembe olacak. Kenarlarına pembe kelebekler konulacak..."
            />
          </Form.Item>

          <Form.Item label="Referans Fotoğraf (Opsiyonel)">
            <PhotoUpload 
              value={referencePhoto} 
              onChange={(f) => setReferencePhoto(f as File)} 
              placeholder="Görsel ekleyin"
            />
          </Form.Item>

          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 24 }}>
            <Button onClick={() => setCreateModalVisible(false)}>İptal</Button>
            <Button type="primary" htmlType="submit" loading={createMutation.isPending || uploadPhotoMutation.isPending}>
              🎂 Sipariş Oluştur
            </Button>
          </div>
        </Form>
      </Modal>

      {/* DETAIL MODAL */}
      {selectedOrderId && (
        <CakeOrderDetail
          orderId={selectedOrderId}
          visible={detailModalVisible}
          onClose={() => setDetailModalVisible(false)}
          userRole={user?.role || 'Sales'}
        />
      )}
    </div>
  );
};

export default CakeOrders;
