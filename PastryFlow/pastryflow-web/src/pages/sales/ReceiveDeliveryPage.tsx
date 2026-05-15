import React, { useEffect, useState, useMemo } from 'react';
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
  Radio,
  Progress,
  Statistic,
} from 'antd';
import {
  CheckCircleOutlined,
  SendOutlined,
  ArrowLeftOutlined,
  WarningOutlined,
  ThunderboltOutlined,
  UnorderedListOutlined,
  AppstoreOutlined,
  LeftOutlined,
  RightOutlined,
  CameraOutlined,
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
  categoryName?: string;
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
  
  // View Mode States
  const [viewMode, setViewMode] = useState<'table' | 'card'>(() => 
    (localStorage.getItem('pastryflow_input_mode') as 'table' | 'card') || 'table'
  );
  const [currentIndex, setCurrentIndex] = useState(0);
  
  const acceptMutation = useAcceptDelivery();
  const uploadPhotoMutation = useUploadRejectionPhoto();

  // Save View Mode Preference
  useEffect(() => {
    localStorage.setItem('pastryflow_input_mode', viewMode);
  }, [viewMode]);

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
      categoryName: item.categoryName,
      unitName: item.unitName,
      requestedQuantity: item.requestedQuantity,
      approvedQuantity: item.approvedQuantity || 0,
      sentQuantity: item.sentQuantity || 0,
      acceptedQuantity: item.sentQuantity || 0,
      rejectionReason: '',
      rejectionPhoto: null,
    }));
    setItems(initialItems);
    setCurrentIndex(0);
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

  const renderProductCard = () => {
    if (items.length === 0) return <Empty />;
    const item = items[currentIndex];
    if (!item) return <Empty />;

    const rejected = Math.max(0, item.sentQuantity - item.acceptedQuantity);
    const isExtra = item.acceptedQuantity > item.sentQuantity;

    return (
      <Card 
        style={{ 
          borderRadius: 20, 
          boxShadow: '0 8px 32px rgba(0,0,0,0.08)', 
          marginBottom: 24,
          border: 'none',
          overflow: 'hidden'
        }}
        bodyStyle={{ padding: '32px 24px' }}
      >
        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <Text type="secondary" style={{ fontSize: 14, display: 'block', textTransform: 'uppercase', letterSpacing: 1 }}>
            {item.categoryName || 'Kategorisiz'}
          </Text>
          <Title level={2} style={{ margin: '8px 0 4px 0', fontSize: 28 }}>{item.productName}</Title>
          <Tag color="blue">{item.unitName}</Tag>
        </div>

        <div style={{ 
          background: '#f8fafc', 
          padding: '20px', 
          borderRadius: 16, 
          marginBottom: 32,
          display: 'flex',
          justifyContent: 'space-around',
          textAlign: 'center',
          border: '1px solid #f1f5f9'
        }}>
          <div>
            <Text type="secondary" style={{ fontSize: 12 }}>TALEP</Text>
            <div style={{ fontSize: 18, fontWeight: 600 }}>{item.requestedQuantity}</div>
          </div>
          <Divider type="vertical" style={{ height: 40 }} />
          <div>
            <Text type="secondary" style={{ fontSize: 12 }}>ONAY</Text>
            <div style={{ fontSize: 18, fontWeight: 600 }}>{item.approvedQuantity}</div>
          </div>
          <Divider type="vertical" style={{ height: 40 }} />
          <div>
            <Text type="secondary" style={{ color: '#0891b2', fontSize: 12 }}>GÖNDERİLEN</Text>
            <div style={{ fontSize: 22, fontWeight: 700, color: '#0891b2' }}>{item.sentQuantity}</div>
          </div>
        </div>

        <div style={{ marginBottom: 32 }}>
          <div style={{ textAlign: 'center', marginBottom: 12 }}>
            <Text strong style={{ fontSize: 16, color: '#1e293b' }}>Kabul Edilen Miktar</Text>
          </div>
          <InputNumber 
            min={0}
            value={item.acceptedQuantity}
            onChange={(val) => handleItemChange(item.demandItemId, 'acceptedQuantity', val || 0)}
            style={{ 
              width: '100%', 
              fontSize: 24, 
              height: 64, 
              display: 'flex', 
              alignItems: 'center',
              borderRadius: 12,
              textAlign: 'center'
            }}
            onFocus={(e) => e.target.select()}
            inputMode="numeric"
            keyboard={false}
          />
          <style>{`
            .ant-input-number-input {
              text-align: center !important;
              font-weight: 600 !important;
            }
          `}</style>
        </div>

        {(rejected > 0 || isExtra) && (
          <div style={{ marginBottom: 24 }}>
             {rejected > 0 && (
                <Alert
                  message={<Text strong>{rejected} adet eksik/reddedildi</Text>}
                  type="error"
                  showIcon
                  icon={<WarningOutlined />}
                  style={{ borderRadius: 12, marginBottom: 16 }}
                />
             )}
             {isExtra && (
                <Alert
                  message={<Text strong>{item.acceptedQuantity - item.sentQuantity} adet fazla ürün</Text>}
                  type="info"
                  showIcon
                  icon={<CheckCircleOutlined />}
                  style={{ borderRadius: 12, marginBottom: 16 }}
                />
             )}

             {rejected > 0 && (
               <div style={{ background: '#fff1f0', padding: 20, borderRadius: 16, border: '1px solid #ffa39e' }}>
                  <div style={{ marginBottom: 12 }}>
                    <Text strong>Red Sebebi <Text type="danger">*</Text></Text>
                    <Input 
                      placeholder="Neden eksik/hatalı?" 
                      value={item.rejectionReason}
                      onChange={(e) => handleItemChange(item.demandItemId, 'rejectionReason', e.target.value)}
                      style={{ marginTop: 8 }}
                    />
                  </div>
                  <div>
                    <Text strong>Fotoğraf Ekle</Text>
                    <div style={{ marginTop: 8 }}>
                      <PhotoUpload 
                        value={item.rejectionPhoto}
                        onChange={(file) => handleItemChange(item.demandItemId, 'rejectionPhoto', file)}
                        placeholder="Hatalı ürünü çekin"
                      />
                    </div>
                  </div>
               </div>
             )}
          </div>
        )}

        <Row gutter={16} style={{ marginTop: 16 }}>
          <Col span={12}>
            <Button 
              size="large" 
              block 
              icon={<LeftOutlined />}
              disabled={currentIndex === 0}
              onClick={() => setCurrentIndex(prev => prev - 1)}
              style={{ height: 56, borderRadius: 12, fontWeight: 600 }}
            >
              Önceki
            </Button>
          </Col>
          <Col span={12}>
            <Button 
              type="primary" 
              size="large" 
              block 
              disabled={currentIndex === items.length - 1}
              onClick={() => setCurrentIndex(prev => prev + 1)}
              style={{ height: 56, borderRadius: 12, fontWeight: 600 }}
            >
              Sonraki <RightOutlined />
            </Button>
          </Col>
        </Row>

        <div style={{ marginTop: 24, textAlign: 'center' }}>
          <Progress 
            percent={Math.round(((currentIndex + 1) / items.length) * 100)} 
            showInfo={false}
            strokeColor="#1890ff"
            style={{ marginBottom: 8 }}
          />
          <Text type="secondary" style={{ fontSize: 13, fontWeight: 500 }}>
            {currentIndex + 1} / {items.length} Ürün
          </Text>
        </div>
      </Card>
    );
  };

  const renderModeSelector = () => (
    <div style={{ 
      display: 'flex', 
      justifyContent: 'space-between', 
      alignItems: 'center', 
      marginBottom: 20,
      background: '#fff',
      padding: '10px 16px',
      borderRadius: 14,
      boxShadow: '0 2px 8px rgba(0,0,0,0.04)',
      border: '1px solid #f0f0f0'
    }}>
      <Radio.Group 
        value={viewMode} 
        onChange={e => setViewMode(e.target.value)}
        optionType="button"
        buttonStyle="solid"
      >
        <Radio.Button value="table"><UnorderedListOutlined /> Liste</Radio.Button>
        <Radio.Button value="card"><AppstoreOutlined /> Kart</Radio.Button>
      </Radio.Group>
      <Space>
        <Text type="secondary" style={{ fontSize: 13 }}>
          {items.filter(i => i.acceptedQuantity !== i.sentQuantity).length} Farklı Ürün
        </Text>
      </Space>
    </div>
  );

  if (selectedDemand) {
    const totalAccepted = items.reduce((acc, curr) => acc + curr.acceptedQuantity, 0);
    const totalRejected = items.reduce((acc, curr) => acc + Math.max(0, curr.sentQuantity - curr.acceptedQuantity), 0);

    const columns = [
      {
        title: 'Ürün Bilgisi',
        key: 'product',
        fixed: 'left' as const,
        width: 250,
        render: (_: any, item: ItemState) => (
          <div>
            <Text type="secondary" style={{ fontSize: 11, display: 'block' }}>{item.categoryName || 'Genel'}</Text>
            <Text strong>{item.productName}</Text>
            <Tag size="small" style={{ marginLeft: 4 }}>{item.unitName}</Tag>
          </div>
        )
      },
      {
        title: 'Gönderilen',
        dataIndex: 'sentQuantity',
        width: 100,
        align: 'center' as const,
        render: (val: number) => <Text strong style={{ color: '#13c2c2', fontSize: 16 }}>{val}</Text>
      },
      {
        title: 'Kabul Edilen',
        key: 'accepted',
        width: 150,
        align: 'center' as const,
        render: (_: any, item: ItemState) => (
          <InputNumber
            min={0}
            value={item.acceptedQuantity}
            onChange={(val) => handleItemChange(item.demandItemId, 'acceptedQuantity', val || 0)}
            onFocus={(e) => e.target.select()}
            inputMode="numeric"
            keyboard={false}
            style={{ width: '100%' }}
          />
        )
      },
      {
        title: 'Durum / Red',
        key: 'status',
        width: 150,
        render: (_: any, item: ItemState) => {
          const rejected = Math.max(0, item.sentQuantity - item.acceptedQuantity);
          const isExtra = item.acceptedQuantity > item.sentQuantity;
          if (rejected > 0) return <Tag color="error">{rejected} Red</Tag>;
          if (isExtra) return <Tag color="processing">+{item.acceptedQuantity - item.sentQuantity} Fazla</Tag>;
          return <Tag color="success">Tamam</Tag>;
        }
      },
      {
        title: 'Red Sebebi & Fotoğraf',
        key: 'rejection',
        width: 400,
        render: (_: any, item: ItemState) => {
          const rejected = Math.max(0, item.sentQuantity - item.acceptedQuantity);
          if (rejected <= 0) return null;
          return (
            <Space direction="vertical" style={{ width: '100%' }}>
              <Input 
                placeholder="Red sebebi..." 
                value={item.rejectionReason}
                onChange={(e) => handleItemChange(item.demandItemId, 'rejectionReason', e.target.value)}
                status={!item.rejectionReason.trim() ? 'error' : ''}
              />
              <PhotoUpload 
                value={item.rejectionPhoto}
                onChange={(file) => handleItemChange(item.demandItemId, 'rejectionPhoto', file)}
                size="small"
              />
            </Space>
          );
        }
      }
    ];

    return (
      <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24, flexWrap: 'wrap', gap: 16 }}>
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

        {renderModeSelector()}

        {viewMode === 'card' ? (
          <div style={{ maxWidth: 600, margin: '0 auto' }}>
            {renderProductCard()}
          </div>
        ) : (
          <Card style={{ borderRadius: 16, border: '1px solid #f0f0f0', overflow: 'hidden' }} bodyStyle={{ padding: 0 }}>
            <Table 
              dataSource={items}
              columns={columns}
              rowKey="demandItemId"
              pagination={false}
              scroll={{ x: 'max-content' }}
              className="receive-delivery-table"
            />
          </Card>
        )}

        <Card style={{ marginTop: 24, borderRadius: 16, background: '#fff', boxShadow: '0 -4px 12px rgba(0,0,0,0.03)', border: '1px solid #f0f0f0' }}>
          <Row justify="space-between" align="middle" gutter={[16, 16]}>
            <Col xs={24} md={12}>
              <Space direction="vertical" size={0}>
                <Text type="secondary">Toplam {items.length} kalem ürün</Text>
                <Space size="large">
                  <Statistic 
                    title={<Text style={{ fontSize: 12 }}>TOPLAM KABUL</Text>} 
                    value={totalAccepted} 
                    valueStyle={{ color: '#52c41a', fontSize: 24, fontWeight: 'bold' }} 
                  />
                  {totalRejected > 0 && (
                    <Statistic 
                      title={<Text style={{ fontSize: 12 }}>TOPLAM RED</Text>} 
                      value={totalRejected} 
                      valueStyle={{ color: '#ff4d4f', fontSize: 24, fontWeight: 'bold' }} 
                    />
                  )}
                </Space>
              </Space>
            </Col>
            <Col xs={24} md={12} style={{ textAlign: 'right' }}>
              <Space>
                <Button size="large" onClick={() => setSelectedDemand(null)} style={{ borderRadius: 10 }}>İptal</Button>
                <Button 
                  type="primary" 
                  size="large" 
                  icon={<CheckCircleOutlined />} 
                  onClick={handleSubmit}
                  loading={acceptMutation.isPending || uploadPhotoMutation.isPending}
                  style={{ background: '#52c41a', borderColor: '#52c41a', height: 52, padding: '0 40px', fontWeight: 'bold', borderRadius: 12 }}
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
      <Card bordered={false} style={{ borderRadius: 20, marginBottom: 32, background: 'linear-gradient(135deg, #f0f9ff 0%, #e0f2fe 100%)', border: '1px solid #bae6fd' }}>
        <Row align="middle" gutter={24}>
          <Col>
            <div style={{ background: '#fff', padding: 16, borderRadius: 16, boxShadow: '0 4px 12px rgba(0,0,0,0.05)' }}>
              <SendOutlined style={{ fontSize: 32, color: '#0ea5e9' }} />
            </div>
          </Col>
          <Col>
            <Title level={2} style={{ margin: 0 }}>📦 Sevkiyat Kabul</Title>
            <Text type="secondary" style={{ fontSize: 16 }}>
              Üretimden gelen ürünleri kontrol edin ve stoklarınıza dahil edin.
            </Text>
          </Col>
        </Row>
      </Card>

      <div style={{ maxWidth: 800 }}>
        {loading ? (
          <div style={{ textAlign: 'center', padding: 50 }}><Spin size="large" tip="Sevkiyatlar getiriliyor..." /></div>
        ) : demands.length === 0 ? (
          <Empty 
            image={Empty.PRESENTED_IMAGE_SIMPLE}
            description="Şu an bekleyen sevkiyat bulunmuyor." 
            style={{ padding: 60, background: '#fff', borderRadius: 20, border: '1px dashed #d9d9d9' }} 
          />
        ) : (
          <div style={{ display: 'grid', gap: 16 }}>
            {demands.map(demand => (
              <Card 
                key={demand.id} 
                hoverable 
                style={{ borderRadius: 16, border: '1px solid #f0f0f0' }}
                onClick={() => handleSelectDemand(demand)}
              >
                <Row justify="space-between" align="middle" gutter={16}>
                  <Col flex="auto">
                    <Space size="middle">
                      <div style={{ background: '#f0fdf4', padding: 12, borderRadius: 12 }}>
                        <ThunderboltOutlined style={{ fontSize: 20, color: '#22c55e' }} />
                      </div>
                      <div>
                        <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                          <Text strong style={{ fontSize: 18 }}>Talep #{demand.demandNumber}</Text>
                          <Tag color="cyan">Yolda</Tag>
                        </div>
                        <Text type="secondary">{demand.productionBranchName} • {demand.items.length} Kalem Ürün</Text>
                        <div style={{ fontSize: 12, color: '#94a3b8', marginTop: 4 }}>
                          {dayjs(demand.createdAt).format('DD MMMM YYYY • HH:mm')}
                        </div>
                      </div>
                    </Space>
                  </Col>
                  <Col>
                    <Button type="primary" shape="round" size="large" style={{ fontWeight: 600 }}>
                      Teslim Al →
                    </Button>
                  </Col>
                </Row>
              </Card>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default ReceiveDeliveryPage;
