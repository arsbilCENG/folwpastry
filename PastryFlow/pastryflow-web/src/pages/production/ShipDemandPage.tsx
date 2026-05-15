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
  Radio,
  Progress,
  Empty,
  Statistic,
  Row,
  Col,
  Tag,
} from 'antd';
import { 
  SendOutlined, 
  ArrowLeftOutlined, 
  UnorderedListOutlined, 
  AppstoreOutlined, 
  LeftOutlined, 
  RightOutlined 
} from '@ant-design/icons';
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

  // View Mode States
  const [viewMode, setViewMode] = useState<'table' | 'card'>(() => 
    (localStorage.getItem('pastryflow_input_mode') as 'table' | 'card') || 'table'
  );
  const [currentProductIndex, setCurrentProductIndex] = useState(0);
  
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

  // Save View Mode Preference
  useEffect(() => {
    localStorage.setItem('pastryflow_input_mode', viewMode);
  }, [viewMode]);

  const handleShip = async () => {
    if (!demand) return;

    const items = demand.items
      .filter(item => (sentQuantities[item.id] || 0) > 0)
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

  const renderCardMode = () => {
    if (!demand || demand.items.length === 0) return <Empty description="Ürün bulunamadı" />;
    const record = demand.items[currentProductIndex];
    if (!record) return <Empty />;

    const isRejected = !record.approvedQuantity || record.approvedQuantity <= 0;

    return (
      <Card 
        style={{ borderRadius: 16, boxShadow: '0 4px 20px rgba(0,0,0,0.08)', marginBottom: 24 }}
        bodyStyle={{ padding: '24px 16px' }}
      >
        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <Tag color="blue" style={{ marginBottom: 8, borderRadius: 4 }}>{record.categoryName}</Tag>
          <Title level={3} style={{ margin: 0, fontSize: 22 }}>{record.productName}</Title>
          <Text type="secondary" style={{ fontSize: 14 }}>{record.unitName}</Text>
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
            title="Talep" 
            value={record.requestedQuantity} 
            valueStyle={{ fontSize: 18, fontWeight: 500 }}
          />
          <Statistic 
            title="Onaylanan" 
            value={record.approvedQuantity || 0} 
            valueStyle={{ fontSize: 20, fontWeight: 600, color: '#52c41a' }}
          />
        </div>

        <div style={{ marginBottom: 32 }}>
          <div style={{ textAlign: 'center', marginBottom: 8 }}>
            <Text strong style={{ fontSize: 16 }}>Gönderilecek Miktar</Text>
          </div>
          <InputNumber 
            min={0}
            disabled={isRejected}
            value={sentQuantities[record.id] || 0}
            onChange={(val) => setSentQuantities(prev => ({ ...prev, [record.id]: val || 0 }))}
            style={{ 
              width: '100%', 
              fontSize: 28, 
              height: 64, 
              display: 'flex', 
              alignItems: 'center',
              borderRadius: 12,
            }}
            status={sentQuantities[record.id] > (record.approvedQuantity || 0) ? 'warning' : ''}
            onFocus={(e) => e.target.select()}
            inputMode="numeric"
            keyboard={false}
            placeholder="0.00"
          />
          {sentQuantities[record.id] > (record.approvedQuantity || 0) && (
            <div style={{ textAlign: 'center', marginTop: 8 }}>
              <Text type="warning" style={{ fontSize: 12 }}>Onaylanan miktardan fazla gönderiyorsunuz.</Text>
            </div>
          )}
        </div>

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
              disabled={currentProductIndex === demand.items.length - 1}
              onClick={() => setCurrentProductIndex(prev => prev + 1)}
              style={{ height: 50, borderRadius: 10 }}
            >
              Sonraki
            </Button>
          </Col>
        </Row>

        <div style={{ marginTop: 20, textAlign: 'center' }}>
          <Progress 
            percent={Math.round(((currentProductIndex + 1) / demand.items.length) * 100)} 
            showInfo={false}
            size="small"
            strokeColor="#13c2c2"
          />
          <div style={{ marginTop: 8 }}>
            <Text type="secondary" style={{ fontSize: 12 }}>
              {currentProductIndex + 1} / {demand.items.length} Ürün
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
        {demand?.items.length} Ürün Hazırlanıyor
      </Text>
    </div>
  );

  if (loading) return <div style={{ textAlign: 'center', padding: '100px' }}><Spin size="large" /></div>;
  if (!demand) return <div style={{ padding: 24 }}><Alert message="Talep bulunamadı" type="error" /></div>;

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
            onFocus={(e) => e.target.select()}
            inputMode="numeric"
            keyboard={false}
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
        <div style={{ 
          display: 'flex', 
          justifyContent: 'space-between', 
          alignItems: 'flex-start', 
          marginBottom: 24,
          flexWrap: 'wrap',
          gap: 16
        }}>
          <div>
            <Title level={3} style={{ margin: 0 }}>📦 Sevkiyat Hazırla — {demand.demandNumber}</Title>
            <div style={{ marginTop: 8, display: 'flex', flexWrap: 'wrap', gap: 12 }}>
              <Text type="secondary">Talep Eden: <strong>{demand.salesBranchName}</strong></Text>
              <Divider type="vertical" className="hidden-mobile" />
              <Text type="secondary">Tarih: {dayjs(demand.createdAt).format('DD.MM.YYYY')}</Text>
            </div>
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

        <div style={{ marginBottom: 16 }}>
          {renderModeSelector()}
        </div>

        {viewMode === 'card' ? (
          renderCardMode()
        ) : (
          <Table
            dataSource={demand.items}
            columns={columns}
            rowKey="id"
            pagination={false}
            bordered
            rowClassName={(record) => (!record.approvedQuantity || record.approvedQuantity <= 0 ? 'row-disabled' : '')}
            scroll={{ x: 'max-content' }}
          />
        )}

        <Divider />

        <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
          <Space wrap>
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
        @media (max-width: 576px) {
          .hidden-mobile {
            display: none;
          }
        }
      `}} />
    </div>
  );
};

export default ShipDemandPage;
