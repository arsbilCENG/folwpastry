import React, { useEffect, useState } from 'react';
import { Row, Col, Card, Statistic, Button, Typography, Space, Spin, message } from 'antd';
import { PlusCircleOutlined, WarningOutlined, CalculatorOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';
import { stockApi } from '../../api/stockApi';
import { demandApi } from '../../api/demandApi';
import { wasteApi } from '../../api/wasteApi';
import { dayClosingApi } from '../../api/dayClosingApi';

const { Title, Text } = Typography;

const SalesDashboard: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  
  const [stockCount, setStockCount] = useState(0);
  const [pendingDemands, setPendingDemands] = useState(0);
  const [wasteCount, setWasteCount] = useState(0);
  const [isDayClosed, setIsDayClosed] = useState<boolean | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      if (!user?.branchId) return;
      try {
        const today = new Date().toISOString().split('T')[0];
        
        const [stockRes, demandRes, wasteRes, closingRes] = await Promise.all([
          stockApi.getCurrentStock(user.branchId, today),
          demandApi.getDemands({ branchId: user.branchId, status: 'Pending', date: today }), // 'Pending' or 1 as string
          wasteApi.getWastes(user.branchId, today),
          dayClosingApi.getSummary(user.branchId, today)
        ]);

        if (stockRes.success && stockRes.data) {
          setStockCount(stockRes.data.filter(s => s.currentStock > 0).length);
        }

        if (demandRes.success && demandRes.data) {
          setPendingDemands(demandRes.data.length);
        }
        
        if (wasteRes.success && wasteRes.data) {
          setWasteCount(wasteRes.data.length);
        }

        if (closingRes.success && closingRes.data) {
          setIsDayClosed(closingRes.data.isClosed);
        } else {
          setIsDayClosed(false);
        }

      } catch (error) {
        message.warning('Bazı veriler yüklenirken hata oluştu.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [user]);

  if (loading) return <div style={{ textAlign: 'center', padding: '50px' }}><Spin size="large" /></div>;

  return (
    <div>
      <div style={{ marginBottom: 24 }}>
        <Title level={3}>Hoş Geldiniz, {user?.fullName}</Title>
        <Text type="secondary">{user?.branchName} Şubesi Kontrol Paneli</Text>
      </div>

      <Row gutter={[16, 16]}>
        <Col xs={24} sm={12} lg={6}>
          <Card bordered={false}>
            <Statistic title="Bugünkü Aktif Stok Kalemi" value={stockCount} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card bordered={false}>
            <Statistic title="Bekleyen Talepler" value={pendingDemands} valueStyle={{ color: pendingDemands > 0 ? '#cf1322' : '#3f8600' }} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card bordered={false}>
            <Statistic title="Bugünkü Zayiat Kaydı" value={wasteCount} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card bordered={false} style={{ background: isDayClosed ? '#f6ffed' : '#fffbe6' }}>
            <Statistic 
              title="Gün Durumu" 
              value={isDayClosed ? "KAPALI" : "AÇIK"} 
              valueStyle={{ color: isDayClosed ? '#3f8600' : '#faad14', fontWeight: 'bold' }} 
            />
          </Card>
        </Col>
      </Row>

      <Title level={4} style={{ marginTop: 32 }}>Hızlı İşlemler</Title>
      <Row gutter={[16, 16]}>
        <Col xs={24} sm={8}>
          <Button 
            type="primary" 
            icon={<PlusCircleOutlined />} 
            size="large" 
            block 
            style={{ height: 80, fontSize: 16 }}
            onClick={() => navigate('/sales/demands/create')}
            disabled={isDayClosed === true}
          >
            Talep Oluştur
          </Button>
        </Col>
        <Col xs={24} sm={8}>
          <Button 
            icon={<WarningOutlined />} 
            size="large" 
            block 
            danger
            style={{ height: 80, fontSize: 16 }}
            onClick={() => navigate('/sales/wastes/add')}
            disabled={isDayClosed === true}
          >
            Zayiat Ekle
          </Button>
        </Col>
        <Col xs={24} sm={8}>
          <Button 
            type="default" 
            icon={<CalculatorOutlined />} 
            size="large" 
            block 
            style={{ height: 80, fontSize: 16, borderColor: '#faad14', color: '#faad14' }}
            onClick={() => navigate('/sales/day-closing')}
            disabled={isDayClosed === true}
          >
            Gün Sonu Sayımı
          </Button>
        </Col>
      </Row>
    </div>
  );
};

export default SalesDashboard;
