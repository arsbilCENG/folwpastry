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
  Form,
} from 'antd';
import {
  CheckOutlined,
  CloseOutlined,
  ArrowLeftOutlined,
  ExclamationCircleOutlined,
} from '@ant-design/icons';
import { useParams, useNavigate } from 'react-router-dom';
import { demandApi } from '../../api/demandApi';
import { Demand, DemandItem, ReviewDemandItemDto } from '../../types/demand';
import useAuth from '../../hooks/useAuth';
import dayjs from 'dayjs';

const { Title, Text } = Typography;
const { TextArea } = Input;

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

  if (loading) return <Spin size="large" style={{ display: 'block', marginTop: 100 }} />;
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

      <Card 
        title="Talep Edilen Ürünler" 
        style={{ borderRadius: 12 }}
        extra={isPending && (
          <Space>
            <Button onClick={handleApproveAll}>Tümünü Onayla</Button>
            <Button onClick={handleRejectAll} danger>Tümünü Reddet</Button>
          </Space>
        )}
      >
        <Table
          dataSource={items}
          columns={columns}
          rowKey="demandItemId"
          pagination={false}
          bordered
        />
        
        <Divider />
        
        <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 12 }}>
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
