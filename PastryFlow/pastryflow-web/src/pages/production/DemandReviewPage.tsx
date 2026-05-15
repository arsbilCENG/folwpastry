import React, { useEffect, useState } from 'react';
import {
  Card,
  Table,
  Typography,
  InputNumber,
  Radio,
  Input,
  Button,
  Space,
  Tag,
  Descriptions,
  Spin,
  message,
  Divider,
  Progress,
  Empty,
  Statistic,
  Row,
  Col
} from 'antd';
import {
  CheckOutlined,
  ArrowLeftOutlined,
  ExclamationCircleOutlined,
  UnorderedListOutlined,
  AppstoreOutlined,
  LeftOutlined,
  RightOutlined
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import { demandApi } from '../../api/demandApi';
import { Demand, DemandItem } from '../../types/demand';
import useAuth from '../../hooks/useAuth';
import dayjs from 'dayjs';

const { Title, Text } = Typography;

interface ItemReviewState {
  demandItemId: string;
  productName: string;
  categoryName: string;
  unit: string;
  requestedQuantity: number;
  approvedQuantity: number;
  status: 'Approved' | 'Rejected';
  rejectionReason: string;
}

const DemandReviewPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { user } = useAuth();
  
  const [demand, setDemand] = useState<Demand | null>(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [items, setItems] = useState<ItemReviewState[]>([]);

  // View Mode States
  const [viewMode, setViewMode] = useState<'table' | 'card'>(() => 
    (localStorage.getItem('pastryflow_input_mode') as 'table' | 'card') || 'table'
  );
  const [currentProductIndex, setCurrentProductIndex] = useState(0);

  useEffect(() => {
    const fetchDemand = async () => {
      if (!id) return;
      try {
        const res = await demandApi.getDemand(id);
        if (res.success && res.data) {
          setDemand(res.data);
          
          // Check if already processed (Support both number 1 and string 'Pending')
          const isActuallyPending = res.data.status === 1 || res.data.status === 'Pending';
          if (!isActuallyPending) {
             message.warning('Bu talep daha önce incelenmiş.');
          }

          setItems(
            res.data.items.map((item: DemandItem) => ({
              demandItemId: item.id,
              productName: item.productName,
              categoryName: item.categoryName || 'Diğer',
              unit: item.unitName,
              requestedQuantity: item.requestedQuantity,
              approvedQuantity: item.approvedQuantity !== null ? item.approvedQuantity : item.requestedQuantity,
              // DemandItemStatus.Rejected = 3
              status: (item.status === 3 || item.status === 'Rejected') ? 'Rejected' : 'Approved', 
              rejectionReason: item.rejectionReason || '',
            }))
          );
        } else {
          message.error(res.message || 'Talep bulunamadı.');
          navigate('/production/demands');
        }
      } catch {
        message.error('Talep yüklenemedi.');
        navigate('/production/demands');
      } finally {
        setLoading(false);
      }
    };
    fetchDemand();
  }, [id, navigate]);

  // Save View Mode Preference
  useEffect(() => {
    localStorage.setItem('pastryflow_input_mode', viewMode);
  }, [viewMode]);

  const updateItem = (itemId: string, field: keyof ItemReviewState, value: any) => {
    setItems(prev =>
      prev.map(item => (item.demandItemId === itemId ? { ...item, [field]: value } : item))
    );
  };

  const handleApproveAll = () => {
    setItems(prev =>
      prev.map(item => ({
        ...item,
        status: 'Approved',
        approvedQuantity: item.requestedQuantity,
        rejectionReason: '',
      }))
    );
    message.success('Tüm ürünler onaylandı olarak işaretlendi.');
  };

  const handleRejectAll = () => {
    setItems(prev =>
      prev.map(item => ({
        ...item,
        status: 'Rejected',
        approvedQuantity: 0,
      }))
    );
    message.info('Tüm ürünler reddedildi olarak işaretlendi. Red sebeplerini giriniz.');
  };

  const handleSave = async () => {
    if (!user?.id || !id) return;

    // Validation
    const invalidItems = items.filter(item => {
      if (item.status === 'Approved') {
        const isQuantityValid = item.approvedQuantity > 0 && item.approvedQuantity <= item.requestedQuantity;
        const isReduced = item.approvedQuantity < item.requestedQuantity;
        const hasReason = !!item.rejectionReason.trim();
        
        return !isQuantityValid || (isReduced && !hasReason);
      } else {
        return !item.rejectionReason.trim();
      }
    });

    if (invalidItems.length > 0) {
      message.error(`${invalidItems.length} kalemde eksik veya hatalı bilgi var. Eksik miktarlar için sebep girmeyi unutmayın.`);
      return;
    }

    const reviewDto = {
      reviewedByUserId: user.id,
      items: items.map(item => ({
        demandItemId: item.demandItemId,
        status: item.status,
        approvedQuantity: item.status === 'Approved' ? item.approvedQuantity : 0,
        rejectionReason: item.rejectionReason.trim() || undefined,
      })),
    };

    setSaving(true);
    try {
      const res = await demandApi.reviewDemand(id, reviewDto);
      if (res.success) {
        message.success('Talep başarıyla incelendi');
        navigate('/production/demands');
      } else {
        message.error(res.message || 'Kaydedilemedi.');
      }
    } catch {
      message.error('Bağlantı hatası.');
    } finally {
      setSaving(false);
    }
  };

  const columns = [
    {
      title: 'Ürün Adı',
      dataIndex: 'productName',
      key: 'productName',
      render: (text: string) => <Text strong>{text}</Text>,
    },
    {
      title: 'Kategori',
      dataIndex: 'categoryName',
      key: 'categoryName',
      render: (text: string) => <Tag>{text}</Tag>,
    },
    {
      title: 'Birim',
      dataIndex: 'unit',
      key: 'unit',
    },
    {
      title: 'İstenen Miktar',
      dataIndex: 'requestedQuantity',
      key: 'requestedQuantity',
    },
    {
      title: 'Onay Miktarı',
      key: 'approvedQuantity',
      render: (_: any, record: ItemReviewState) => (
        <Space direction="vertical" style={{ width: '100%' }}>
          <InputNumber
            min={0}
            max={record.requestedQuantity}
            value={record.approvedQuantity}
            onChange={(val) => updateItem(record.demandItemId, 'approvedQuantity', val || 0)}
            disabled={record.status === 'Rejected'}
            status={record.status === 'Approved' && (record.approvedQuantity <= 0 || record.approvedQuantity > record.requestedQuantity) ? 'error' : ''}
            style={{ width: '100%' }}
            onFocus={(e) => e.target.select()}
            inputMode="numeric"
            keyboard={false}
          />
          {record.status === 'Approved' && record.approvedQuantity > record.requestedQuantity && (
            <Text type="danger" style={{ fontSize: '11px' }}>Miktarı aşamaz!</Text>
          )}
        </Space>
      ),
    },
    {
      title: 'Durum',
      key: 'status',
      render: (_: any, record: ItemReviewState) => (
        <Radio.Group
          value={record.status}
          onChange={(e) => updateItem(record.demandItemId, 'status', e.target.value)}
          buttonStyle="solid"
          size="small"
        >
          <Radio.Button value="Approved">Onayla</Radio.Button>
          <Radio.Button value="Rejected">Reddet</Radio.Button>
        </Radio.Group>
      ),
    },
    {
      title: 'Sebep / Not',
      key: 'rejectionReason',
      render: (_: any, record: ItemReviewState) => {
        const isReduced = record.status === 'Approved' && record.approvedQuantity < record.requestedQuantity;
        const isRejected = record.status === 'Rejected';
        const showReason = isReduced || isRejected;
        
        return (
          <div style={{ visibility: showReason ? 'visible' : 'hidden' }}>
            <Input
              placeholder={isReduced ? "Azaltma sebebi..." : "Red sebebi..."}
              value={record.rejectionReason}
              onChange={(e) => updateItem(record.demandItemId, 'rejectionReason', e.target.value)}
              status={showReason && !record.rejectionReason.trim() ? 'error' : ''}
            />
          </div>
        );
      },
    },
  ];

  const renderCardMode = () => {
    if (items.length === 0) return <Empty description="İncelenecek ürün bulunamadı" />;
    const record = items[currentProductIndex];
    if (!record) return <Empty />;

    const isReduced = record.status === 'Approved' && record.approvedQuantity < record.requestedQuantity;
    const isRejected = record.status === 'Rejected';
    const showReason = isReduced || isRejected;

    return (
      <Card 
        style={{ borderRadius: 16, boxShadow: '0 4px 20px rgba(0,0,0,0.08)', marginBottom: 24 }}
        bodyStyle={{ padding: '24px 16px' }}
      >
        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <Tag color="blue" style={{ marginBottom: 8, borderRadius: 4 }}>{record.categoryName}</Tag>
          <Title level={3} style={{ margin: 0, fontSize: 22 }}>{record.productName}</Title>
          <Text type="secondary" style={{ fontSize: 14 }}>{record.unit}</Text>
        </div>

        <div style={{ 
          background: '#f5f5f5', 
          padding: '16px', 
          borderRadius: 12, 
          marginBottom: 24,
          display: 'flex',
          justifyContent: 'center',
          gap: 24
        }}>
          <Statistic 
            title="İstenen Miktar" 
            value={record.requestedQuantity} 
            valueStyle={{ fontSize: 20, fontWeight: 600 }}
          />
        </div>

        <div style={{ marginBottom: 24 }}>
          <div style={{ textAlign: 'center', marginBottom: 8 }}>
            <Text strong style={{ fontSize: 16 }}>Onay Miktarı</Text>
          </div>
          <InputNumber 
            min={0}
            max={record.requestedQuantity}
            value={record.approvedQuantity}
            onChange={(val) => updateItem(record.demandItemId, 'approvedQuantity', val || 0)}
            disabled={record.status === 'Rejected'}
            style={{ 
              width: '100%', 
              fontSize: 28, 
              height: 64, 
              display: 'flex', 
              alignItems: 'center',
              borderRadius: 12,
            }}
            onFocus={(e) => e.target.select()}
            inputMode="numeric"
            keyboard={false}
            placeholder="0.00"
          />
        </div>

        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <div style={{ marginBottom: 8 }}><Text strong>İşlem</Text></div>
          <Radio.Group
            value={record.status}
            onChange={(e) => updateItem(record.demandItemId, 'status', e.target.value)}
            buttonStyle="solid"
            size="large"
            style={{ width: '100%', display: 'flex' }}
          >
            <Radio.Button value="Approved" style={{ flex: 1, textAlign: 'center' }}>Onayla</Radio.Button>
            <Radio.Button value="Rejected" style={{ flex: 1, textAlign: 'center' }}>Reddet</Radio.Button>
          </Radio.Group>
        </div>

        {showReason && (
          <div style={{ marginBottom: 24 }}>
            <div style={{ marginBottom: 8 }}><Text strong>{isReduced ? "Azaltma Sebebi" : "Red Sebebi"} <Text type="danger">*</Text></Text></div>
            <Input
              placeholder={isReduced ? "Neden daha az onayladınız?" : "Neden reddettiniz?"}
              value={record.rejectionReason}
              onChange={(e) => updateItem(record.demandItemId, 'rejectionReason', e.target.value)}
              status={!record.rejectionReason.trim() ? 'error' : ''}
              size="large"
            />
          </div>
        )}

        <Row gutter={12}>
          <Col span={12}>
            <Button 
              size="large" 
              block 
              icon={<LeftOutlined />}
              disabled={currentProductIndex === 0}
              onClick={() => setCurrentProductIndex(prev => prev - 1)}
              style={{ height: 50, borderRadius: 10 }}
            >
              Önceki
            </Button>
          </Col>
          <Col span={12}>
            <Button 
              type="primary" 
              size="large" 
              block 
              icon={<RightOutlined />}
              disabled={currentProductIndex === items.length - 1}
              onClick={() => setCurrentProductIndex(prev => prev + 1)}
              style={{ height: 50, borderRadius: 10 }}
            >
              Sonraki
            </Button>
          </Col>
        </Row>

        <div style={{ marginTop: 20, textAlign: 'center' }}>
          <Progress 
            percent={Math.round(((currentProductIndex + 1) / items.length) * 100)} 
            showInfo={false}
            size="small"
            strokeColor="#1890ff"
          />
          <div style={{ marginTop: 8 }}>
            <Text type="secondary" style={{ fontSize: 12 }}>
              {currentProductIndex + 1} / {items.length} Ürün
            </Text>
          </div>
        </div>
      </Card>
    );
  };

  const renderModeSelector = () => (
    <div style={{ 
      display: 'flex', 
      justifyContent: 'space-between', 
      alignItems: 'center', 
      marginBottom: 16,
      background: '#fff',
      padding: '8px 16px',
      borderRadius: 12,
      border: '1px solid #f0f0f0'
    }}>
      <Radio.Group 
        value={viewMode} 
        onChange={e => setViewMode(e.target.value)}
        optionType="button"
        buttonStyle="solid"
        size="middle"
      >
        <Radio.Button value="table"><UnorderedListOutlined /> Tablo</Radio.Button>
        <Radio.Button value="card"><AppstoreOutlined /> Kart</Radio.Button>
      </Radio.Group>
      <Text type="secondary" style={{ fontSize: 13 }}>
        {items.length} Ürün İnceleniyor
      </Text>
    </div>
  );

  if (loading) return <div style={{ textAlign: 'center', padding: '100px' }}><Spin size="large" /></div>;
  if (!demand) return null;

  const isPending = demand.status === 1 || demand.status === 'Pending';

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ marginBottom: 24 }}>
        <Button icon={<ArrowLeftOutlined />} onClick={() => navigate('/production/demands')}>Geri Dön</Button>
      </div>

      <Title level={2}>Talep İnceleme</Title>

      <Card style={{ marginBottom: 24, borderRadius: 12 }} bordered={false}>
        <Descriptions title="Talep Detayları" bordered column={{ xs: 1, sm: 2 }}>
          <Descriptions.Item label="Talep No">{demand.demandNumber}</Descriptions.Item>
          <Descriptions.Item label="Tarih">{dayjs(demand.createdAt).format('DD.MM.YYYY HH:mm')}</Descriptions.Item>
          <Descriptions.Item label="Satış Şubesi">{demand.salesBranchName}</Descriptions.Item>
          <Descriptions.Item label="Durum">
             <Tag color={isPending ? 'orange' : 'blue'}>{demand.statusName}</Tag>
          </Descriptions.Item>
          <Descriptions.Item label="Notlar" span={2}>{demand.notes || '-'}</Descriptions.Item>
        </Descriptions>
      </Card>

      <div style={{ marginBottom: 16 }}>
        {renderModeSelector()}
      </div>

      <Card 
        title="Talep Edilen Ürünler" 
        style={{ borderRadius: 12 }}
        extra={isPending && (
          <Space wrap>
            <Button size="small" onClick={handleApproveAll}>Tümünü Onayla</Button>
            <Button size="small" onClick={handleRejectAll} danger>Tümünü Reddet</Button>
          </Space>
        )}
      >
        {viewMode === 'card' ? (
          renderCardMode()
        ) : (
          <Table
            dataSource={items}
            columns={columns}
            rowKey="demandItemId"
            pagination={false}
            bordered
            scroll={{ x: 'max-content' }}
          />
        )}
        
        <Divider />
        
        <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 12, flexWrap: 'wrap' }}>
          <Button size="large" onClick={() => navigate('/production/demands')}>İptal</Button>
          {isPending && (
            <Button 
              type="primary" 
              size="large" 
              icon={<CheckOutlined />} 
              loading={saving}
              onClick={handleSave}
              style={{ minWidth: 150 }}
            >
              Kaydet
            </Button>
          )}
        </div>
      </Card>
      
      {!isPending && (
         <div style={{ marginTop: 16 }}>
            <Tag icon={<ExclamationCircleOutlined />} color="warning">
               Bu talep zaten işlenmiş, değişiklik yapamazsınız.
            </Tag>
         </div>
      )}
    </div>
  );
};

export default DemandReviewPage;
