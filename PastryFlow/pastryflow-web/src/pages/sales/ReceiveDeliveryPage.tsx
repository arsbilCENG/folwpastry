import React, { useEffect, useState } from 'react';
import {
  Table,
  Tag,
  Button,
  message,
  Space,
  Modal,
  Typography,
  Card,
  Input,
  InputNumber,
  Row,
  Col,
  Divider,
  Empty,
  Spin,
  Alert,
  Popconfirm,
} from 'antd';
import {
  CheckCircleOutlined,
  SendOutlined,
  ArrowLeftOutlined,
  WarningOutlined,
  ThunderboltOutlined,
} from '@ant-design/icons';
import dayjs from 'dayjs';
import { demandApi } from '../../api/demandApi';
import { useAcceptDelivery, useUploadRejectionPhoto } from '../../hooks/useDemands';
import { Demand, AcceptDeliveryDto } from '../../types/demand';
import useAuth from '../../hooks/useAuth';
import PhotoUpload from '../../components/common/PhotoUpload';

const { Title, Text } = Typography;

interface ItemState {
  demandItemId: string;
  productName: string;
  unitName: string;
  requestedQuantity: number;
  approvedQuantity: number;
  sentQuantity: number;
  acceptedQuantity: number;
  rejectionReason: string;
  rejectionPhoto: File | null;
}

const ReceiveDeliveryPage: React.FC = () => {
  const { user } = useAuth();
  const [demands, setDemands] = useState<Demand[]>([]);
  const [loading, setLoading] = useState(false);
  const [selectedDemand, setSelectedDemand] = useState<Demand | null>(null);
  const [items, setItems] = useState<ItemState[]>([]);
  
  const acceptMutation = useAcceptDelivery();
  const uploadPhotoMutation = useUploadRejectionPhoto();

  const fetchDemands = async () => {
    if (!user?.branchId) return;
    setLoading(true);
    try {
      const res = await demandApi.getDemands({ branchId: user.branchId });
      if (res.success && res.data) {
        const readyToReceive = res.data.filter(d => 
          d.status === 4 || d.status === 'Shipped' || 
          d.status === 5 || d.status === 'Delivered'
        );
        setDemands(readyToReceive);
      }
    } catch (err) {
      message.error('Sevkiyatlar yüklenemedi');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchDemands();
  }, [user?.branchId]);

  const handleSelectDemand = (demand: Demand) => {
    setSelectedDemand(demand);
    const initialItems = demand.items.map(item => ({
      demandItemId: item.id,
      productName: item.productName,
      unitName: item.unitName,
      requestedQuantity: item.requestedQuantity,
      approvedQuantity: item.approvedQuantity || 0,
      sentQuantity: item.sentQuantity || 0,
      acceptedQuantity: item.sentQuantity || 0,
      rejectionReason: '',
      rejectionPhoto: null,
    }));
    setItems(initialItems);
  };

  const handleItemChange = (itemId: string, field: keyof ItemState, value: any) => {
    setItems(prev => prev.map(item => 
      item.demandItemId === itemId ? { ...item, [field]: value } : item
    ));
  };

  const handleQuickAccept = async () => {
    if (!selectedDemand) return;
    
    const dto: AcceptDeliveryDto = {
      items: items.map(item => ({
        demandItemId: item.demandItemId,
        acceptedQuantity: item.sentQuantity,
      })),
    };

    try {
      await acceptMutation.mutateAsync({ demandId: selectedDemand.id, data: dto });
      message.success('Tüm ürünler başarıyla kabul edildi.');
      setSelectedDemand(null);
      fetchDemands();
    } catch {
      // Handled by mutation
    }
  };

  const handleSubmit = async () => {
    if (!selectedDemand) return;

    for (const item of items) {
      const rejected = item.sentQuantity - item.acceptedQuantity;
      if (rejected > 0 && !item.rejectionReason.trim()) {
        message.error(`${item.productName} için red sebebi yazılmalıdır.`);
        return;
      }
    }

    const dto: AcceptDeliveryDto = {
      items: items.map(item => ({
        demandItemId: item.demandItemId,
        acceptedQuantity: item.acceptedQuantity,
        rejectionReason: item.sentQuantity > item.acceptedQuantity ? item.rejectionReason : undefined,
      })),
    };

    try {
      await acceptMutation.mutateAsync({ demandId: selectedDemand.id, data: dto });
      
      const photoUploads = items
        .filter(item => item.rejectionPhoto && item.sentQuantity > item.acceptedQuantity)
        .map(item => uploadPhotoMutation.mutateAsync({
          demandId: selectedDemand.id,
          itemId: item.demandItemId,
          photo: item.rejectionPhoto!,
        }));

      if (photoUploads.length > 0) {
        await Promise.allSettled(photoUploads);
      }

      setSelectedDemand(null);
      fetchDemands();
    } catch {
      // Error handled
    }
  };

  if (selectedDemand) {
    const totalAccepted = items.reduce((acc, curr) => acc + curr.acceptedQuantity, 0);
    const totalRejected = items.reduce((acc, curr) => acc + Math.max(0, curr.sentQuantity - curr.acceptedQuantity), 0);

    return (
      <div style={{ padding: '24px', maxWidth: 1000, margin: '0 auto' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}>
          <Space>
            <Button icon={<ArrowLeftOutlined />} onClick={() => setSelectedDemand(null)} />
            <div>
              <Title level={3} style={{ margin: 0 }}>Sevkiyat Kabul — {selectedDemand.demandNumber}</Title>
              <Text type="secondary">Gönderen: {selectedDemand.productionBranchName} • {dayjs(selectedDemand.createdAt).format('DD.MM.YYYY')}</Text>
            </div>
          </Space>
          
          <Popconfirm
            title="Tümünü Kabul Et"
            description="Tüm ürünleri gönderilen miktarlar üzerinden kabul etmek istiyor musunuz?"
            onConfirm={handleQuickAccept}
            okText="Evet"
            cancelText="Hayır"
          >
            <Button 
              type="dashed" 
              icon={<ThunderboltOutlined />} 
              style={{ color: '#faad14', borderColor: '#faad14' }}
            >
              Hızlı Kabul (Tümünü Onayla)
            </Button>
          </Popconfirm>
        </div>

        {items.map(item => {
          const rejected = Math.max(0, item.sentQuantity - item.acceptedQuantity);
          const isExtra = item.acceptedQuantity > item.sentQuantity;

          return (
            <Card key={item.demandItemId} style={{ marginBottom: 16, borderRadius: 12, border: rejected > 0 ? '1px solid #ffa39e' : isExtra ? '1px solid #91d5ff' : '1px solid #f0f0f0' }}>
              <Row gutter={24} align="middle">
                <Col xs={24} sm={12}>
                  <Text strong style={{ fontSize: 16 }}>{item.productName}</Text>
                  <Tag style={{ marginLeft: 8 }}>{item.unitName}</Tag>
                  <div style={{ marginTop: 8 }}>
                    <Space size="large">
                      <Text type="secondary">Talep: <strong>{item.requestedQuantity}</strong></Text>
                      <Text type="secondary">Onay: <strong>{item.approvedQuantity}</strong></Text>
                      <Text type="secondary" style={{ color: '#13c2c2' }}>Gönderilen: <strong>{item.sentQuantity}</strong></Text>
                    </Space>
                  </div>
                </Col>
                <Col xs={24} sm={12} style={{ textAlign: 'right' }}>
                  <Space direction="vertical" align="end" style={{ width: '100%' }}>
                    <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'flex-end', width: '100%' }}>
                      <Text style={{ marginRight: 12 }}>Kabul Edilen:</Text>
                      <InputNumber
                        min={0}
                        value={item.acceptedQuantity}
                        onChange={(val) => handleItemChange(item.demandItemId, 'acceptedQuantity', val || 0)}
                        size="large"
                        style={{ width: 120 }}
                      />
                    </div>
                    {rejected > 0 && <Tag color="error" icon={<WarningOutlined />}>{rejected} adet reddedildi</Tag>}
                    {isExtra && <Tag color="processing">+{item.acceptedQuantity - item.sentQuantity} fazla ürün</Tag>}
                    {rejected === 0 && !isExtra && <Tag color="success" icon={<CheckCircleOutlined />}>Tamamı kabul edildi</Tag>}
                  </Space>
                </Col>
              </Row>

              {rejected > 0 && (
                <>
                  <Divider style={{ margin: '16px 0' }} />
                  <Row gutter={16}>
                    <Col xs={24} md={12}>
                      <div style={{ marginBottom: 8 }}><Text strong>Red Sebebi</Text> <Text type="danger">*</Text></div>
                      <Input 
                        placeholder="Ürün neden reddedildi?" 
                        value={item.rejectionReason}
                        onChange={(e) => handleItemChange(item.demandItemId, 'rejectionReason', e.target.value)}
                        status={!item.rejectionReason.trim() ? 'error' : ''}
                      />
                    </Col>
                    <Col xs={24} md={12}>
                      <div style={{ marginBottom: 8 }}><Text strong>Red Fotoğrafı (Önerilir)</Text></div>
                      <PhotoUpload 
                        value={item.rejectionPhoto}
                        onChange={(file) => handleItemChange(item.demandItemId, 'rejectionPhoto', file)}
                        placeholder="Hatalı ürünü çekin"
                        style={{ border: '1px solid #d9d9d9', borderRadius: 8, padding: 8, background: '#fafafa' }}
                      />
                    </Col>
                  </Row>
                </>
              )}
            </Card>
          );
        })}

        <Card style={{ marginTop: 24, borderRadius: 12, background: '#fafafa', boxShadow: '0 -4px 12px rgba(0,0,0,0.03)' }}>
          <Row justify="space-between" align="middle">
            <Col>
              <Space direction="vertical" size={0}>
                <Text type="secondary">Toplam {items.length} kalem ürün</Text>
                <Space size="large">
                  <Text strong>Toplam Kabul: <span style={{ color: '#52c41a' }}>{totalAccepted}</span></Text>
                  {totalRejected > 0 && <Text strong>Toplam Red: <span style={{ color: '#ff4d4f' }}>{totalRejected}</span></Text>}
                </Space>
              </Space>
            </Col>
            <Col>
              <Space>
                <Button size="large" onClick={() => setSelectedDemand(null)}>İptal</Button>
                <Button 
                  type="primary" 
                  size="large" 
                  icon={<CheckCircleOutlined />} 
                  onClick={handleSubmit}
                  loading={acceptMutation.isPending || uploadPhotoMutation.isPending}
                  style={{ background: '#52c41a', borderColor: '#52c41a', height: 48, padding: '0 40px', fontWeight: 'bold' }}
                >
                  Teslim Al ve Onayla
                </Button>
              </Space>
            </Col>
          </Row>
        </Card>
      </div>
    );
  }

  return (
    <div style={{ padding: '24px' }}>
      <Card bordered={false} style={{ borderRadius: 12, marginBottom: 24, background: 'linear-gradient(135deg, #f0f2f5 0%, #e6f7ff 100%)' }}>
        <Title level={2}>📦 Sevkiyat Kabul</Title>
        <Text type="secondary">
          Üretimden yola çıkan sevkiyatları buradan kontrol ederek teslim alabilirsiniz. 
          Eksik veya hatalı ürünleri red miktarını girerek bildirebilirsiniz.
        </Text>
      </Card>

      <div style={{ maxWidth: 800 }}>
        {loading ? (
          <div style={{ textAlign: 'center', padding: 50 }}><Spin size="large" /></div>
        ) : demands.length === 0 ? (
          <Empty description="Şu an bekleyen sevkiyat bulunmuyor." style={{ padding: 40, background: '#fff', borderRadius: 12 }} />
        ) : (
          demands.map(demand => (
            <Card 
              key={demand.id} 
              hoverable 
              style={{ marginBottom: 16, borderRadius: 12 }}
              onClick={() => handleSelectDemand(demand)}
            >
              <Row justify="space-between" align="middle">
                <Col>
                  <Space size="middle">
                    <div style={{ background: '#e6f7ff', padding: 12, borderRadius: 10 }}>
                      <SendOutlined style={{ fontSize: 24, color: '#1890ff' }} />
                    </div>
                    <div>
                      <Title level={4} style={{ margin: 0 }}>Talep #{demand.demandNumber}</Title>
                      <Text type="secondary">{demand.productionBranchName} • {demand.items.length} Kalem Ürün</Text>
                      <br />
                      <Text type="secondary" style={{ fontSize: 12 }}>{dayjs(demand.createdAt).format('DD.MM.YYYY HH:mm')}</Text>
                    </div>
                  </Space>
                </Col>
                <Col>
                  <Space direction="vertical" align="end">
                    <Tag color="cyan">Yolda / Gönderildi</Tag>
                    <Button type="primary" shape="round">Teslim Al →</Button>
                  </Space>
                </Col>
              </Row>
            </Card>
          ))
        )}
      </div>
    </div>
  );
};

export default ReceiveDeliveryPage;
