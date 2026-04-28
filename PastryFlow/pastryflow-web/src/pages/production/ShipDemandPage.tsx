import React, { useEffect, useState } from 'react';
import {
  Table,
  Button,
  Typography,
  Space,
  InputNumber,
  Card,
  message,
  Divider,
  Alert,
  Spin,
  Breadcrumb,
} from 'antd';
import { SendOutlined, ArrowLeftOutlined } from '@ant-design/icons';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { demandApi } from '../../api/demandApi';
import { useShipDemand } from '../../hooks/useDemands';
import { Demand, ShipDemandDto } from '../../types/demand';
import dayjs from 'dayjs';

const { Title, Text } = Typography;

const ShipDemandPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [demand, setDemand] = useState<Demand | null>(null);
  const [loading, setLoading] = useState(true);
  const [sentQuantities, setSentQuantities] = useState<Record<string, number>>({});
  
  const shipMutation = useShipDemand();

  useEffect(() => {
    const fetchDemand = async () => {
      if (!id) return;
      try {
        const res = await demandApi.getDemand(id);
        if (res.success && res.data) {
          setDemand(res.data);
          // Set default sent quantities to approved quantities
          const initialSent: Record<string, number> = {};
          res.data.items.forEach(item => {
            if (item.approvedQuantity && item.approvedQuantity > 0) {
              initialSent[item.id] = item.approvedQuantity;
            }
          });
          setSentQuantities(initialSent);
        }
      } catch {
        message.error('Talep detayları yüklenemedi.');
      } finally {
        setLoading(false);
      }
    };
    fetchDemand();
  }, [id]);

  const handleShip = async () => {
    if (!demand) return;

    const items = demand.items
      .filter(item => sentQuantities[item.id] > 0)
      .map(item => ({
        demandItemId: item.id,
        sentQuantity: sentQuantities[item.id],
      }));

    if (items.length === 0) {
      message.warning('En az bir ürün için gönderim miktarı girmelisiniz.');
      return;
    }

    const dto: ShipDemandDto = { items };
    
    try {
      await shipMutation.mutateAsync({ demandId: demand.id, data: dto });
      navigate('/production/demands');
    } catch {
      // Error handled by mutation
    }
  };

  if (loading) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;
  if (!demand) return <Alert message="Talep bulunamadı" type="error" />;

  const columns = [
    {
      title: 'Ürün Adı',
      dataIndex: 'productName',
      key: 'productName',
      render: (text: string, record: any) => (
        <Space direction="vertical" size={0}>
          <Text strong>{text}</Text>
          <Text type="secondary" style={{ fontSize: 12 }}>{record.categoryName}</Text>
        </Space>
      ),
    },
    {
      title: 'Birim',
      dataIndex: 'unitName',
      key: 'unitName',
    },
    {
      title: 'Talep',
      dataIndex: 'requestedQuantity',
      key: 'requestedQuantity',
      align: 'center' as const,
    },
    {
      title: 'Onaylanan',
      dataIndex: 'approvedQuantity',
      key: 'approvedQuantity',
      align: 'center' as const,
      render: (val: number) => <Text strong color="green">{val || 0}</Text>
    },
    {
      title: 'Gönderilecek Miktar',
      key: 'sentQuantity',
      width: 150,
      render: (_: any, record: any) => {
        const isRejected = !record.approvedQuantity || record.approvedQuantity <= 0;
        return (
          <InputNumber
            min={0}
            disabled={isRejected}
            value={sentQuantities[record.id] || 0}
            onChange={(val) => setSentQuantities(prev => ({ ...prev, [record.id]: val || 0 }))}
            style={{ width: '100%' }}
            status={sentQuantities[record.id] > (record.approvedQuantity || 0) ? 'warning' : ''}
          />
        );
      },
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <Breadcrumb style={{ marginBottom: 16 }}>
        <Breadcrumb.Item><Link to="/production/demands">Gelen Talepler</Link></Breadcrumb.Item>
        <Breadcrumb.Item>Sevkiyat Hazırla</Breadcrumb.Item>
      </Breadcrumb>

      <Card bordered={false} style={{ borderRadius: 12, boxShadow: '0 4px 12px rgba(0,0,0,0.05)' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: 24 }}>
          <div>
            <Title level={3} style={{ margin: 0 }}>📦 Sevkiyat Hazırla — {demand.demandNumber}</Title>
            <Space split={<Divider type="vertical" />}>
              <Text type="secondary">Talep Eden: <strong>{demand.salesBranchName}</strong></Text>
              <Text type="secondary">Tarih: {dayjs(demand.createdAt).format('DD.MM.YYYY')}</Text>
            </Space>
          </div>
          <Button icon={<ArrowLeftOutlined />} onClick={() => navigate(-1)}>Geri Dön</Button>
        </div>

        <Alert
          message="Bilgilendirme"
          description="Gönderilecek miktar onaylanan miktardan farklı olabilir. Örneğin hamur fazla çıktıysa veya eksik ürün varsa gerçek gönderilen miktarı yazınız."
          type="info"
          showIcon
          style={{ marginBottom: 24 }}
        />

        <Table
          dataSource={demand.items}
          columns={columns}
          rowKey="id"
          pagination={false}
          bordered
          rowClassName={(record) => (!record.approvedQuantity || record.approvedQuantity <= 0 ? 'row-disabled' : '')}
        />

        <Divider />

        <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
          <Space>
            <Button size="large" onClick={() => navigate(-1)}>Vazgeç</Button>
            <Button
              type="primary"
              size="large"
              icon={<SendOutlined />}
              onClick={handleShip}
              loading={shipMutation.isPending}
              style={{ backgroundColor: '#13c2c2', borderColor: '#13c2c2', height: 45, padding: '0 32px' }}
            >
              Sevkiyatı Gönder
            </Button>
          </Space>
        </div>
      </Card>

      <style dangerouslySetInnerHTML={{ __html: `
        .row-disabled {
          background-color: #fafafa;
          color: #bfbfbf;
        }
        .row-disabled .ant-typography {
          color: #bfbfbf !important;
        }
      `}} />
    </div>
  );
};

export default ShipDemandPage;
