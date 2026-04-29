import React, { useState, useMemo } from 'react';
import { 
  Row, Col, Card, Statistic, Table, DatePicker, 
  Tag, Spin, Empty, Typography, Space, Tooltip 
} from 'antd';
import { 
  DashboardOutlined,
  HourglassOutlined, 
  CheckCircleOutlined, 
  CloseCircleOutlined, 
  DeleteOutlined,
  CalendarOutlined,
  ShopOutlined,
  ClockCircleOutlined,
  InfoCircleOutlined
} from '@ant-design/icons';
import dayjs from 'dayjs';
import { useAdminDashboard } from '../../hooks/useAdmin';
import { useOutletContext } from 'react-router-dom';
import type { AdminOutletContext, BranchSummaryDto } from '../../types/admin';
import { formatCurrency } from '../../utils/formatters';

const { Title } = Typography;

const AdminDashboard: React.FC = () => {
  const [date, setDate] = useState<dayjs.Dayjs>(dayjs());
  const { selectedBranchId } = useOutletContext<AdminOutletContext>();
  
  const { data: dashboard, isLoading } = useAdminDashboard(date.format('YYYY-MM-DD'));

  const filteredSummaries = useMemo(() => {
    if (!dashboard?.branchSummaries) return [];
    if (!selectedBranchId) return dashboard.branchSummaries;
    return dashboard.branchSummaries.filter(s => s.branchId === selectedBranchId);
  }, [dashboard, selectedBranchId]);

  const columns = [
    {
      title: 'Şube',
      dataIndex: 'branchName',
      key: 'branchName',
      render: (text: string, record: BranchSummaryDto) => (
        <Space>
          <ShopOutlined />
          <Typography.Text strong>{text}</Typography.Text>
        </Space>
      ),
    },
    {
      title: 'Tip',
      dataIndex: 'branchType',
      key: 'branchType',
      render: (type: string) => (
        <Tag color={type === 'Production' ? 'blue' : 'green'}>
          {type === 'Production' ? 'Üretim' : 'Satış'}
        </Tag>
      ),
    },
    {
      title: 'Talep (B/O)',
      key: 'demands',
      render: (_: any, record: BranchSummaryDto) => (
        <Space>
          <Tooltip title="Bekleyen Talepler">
            <Tag color="orange">{record.pendingDemandCount}</Tag>
          </Tooltip>
          /
          <Tooltip title="Onaylanan Talepler">
            <Tag color="green">{record.approvedDemandCount}</Tag>
          </Tooltip>
        </Space>
      ),
    },
    {
      title: 'Stok (Çeşit)',
      dataIndex: 'totalProductsInStock',
      key: 'totalProductsInStock',
      render: (count: number) => <Typography.Text>{count} Ürün</Typography.Text>,
    },
    {
      title: 'Zayiat',
      dataIndex: 'totalWasteQuantity',
      key: 'totalWasteQuantity',
      render: (qty: number) => <Typography.Text type={qty > 0 ? 'danger' : 'secondary'}>{qty > 0 ? `${qty} Birim` : '-'}</Typography.Text>,
    },
    {
      title: 'Durum',
      key: 'status',
      render: (_: any, record: BranchSummaryDto) => (
        <Space size="small">
          {record.isDayOpened ? (
            <Tooltip title="Gün Açık">
              <CheckCircleOutlined style={{ color: '#52c41a' }} />
            </Tooltip>
          ) : (
            <Tooltip title="Gün Henüz Açılmadı">
              <ClockCircleOutlined style={{ color: '#faad14' }} />
            </Tooltip>
          )}
          {record.isDayClosed ? (
            <Tooltip title="Gün Kapatıldı">
              <Tag color="success">Kapalı</Tag>
            </Tooltip>
          ) : (
             <Tag color="processing">Açık</Tag>
          )}
        </Space>
      ),
    },
  ];

  if (isLoading && !dashboard) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;

  return (
    <div style={{ padding: '24px' }}>
      <Row gutter={[16, 16]} justify="space-between" align="middle" style={{ marginBottom: 24 }}>
        <Col xs={24} sm={12}>
          <Title level={3} style={{ margin: 0 }}>
            <DashboardOutlined /> Yönetim Paneli
          </Title>
        </Col>
        <Col xs={24} sm={12} style={{ textAlign: 'right' }}>
          <DatePicker 
            value={date} 
            onChange={(val) => val && setDate(val)} 
            allowClear={false}
            format="DD.MM.YYYY"
            style={{ width: '100%', maxWidth: 200 }}
          />
        </Col>
      </Row>

      <Row gutter={[16, 16]} style={{ marginBottom: 24 }}>
        <Col xs={24} sm={12} md={6} lg={4}>
          <Card bordered={false} className="stat-card">
            <Statistic 
              title="Bekleyen Talepler" 
              value={dashboard?.totalPendingDemands} 
              prefix={<HourglassOutlined style={{ color: '#faad14' }} />} 
            />
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6} lg={4}>
          <Card bordered={false} className="stat-card">
            <Statistic 
              title="Onaylananlar" 
              value={dashboard?.totalApprovedDemands} 
              prefix={<CheckCircleOutlined style={{ color: '#52c41a' }} />} 
            />
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6} lg={4}>
          <Card bordered={false} className="stat-card">
            <Statistic 
              title="Reddedilenler" 
              value={dashboard?.totalRejectedDemands} 
              prefix={<CloseCircleOutlined style={{ color: '#ff4d4f' }} />} 
            />
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6} lg={4}>
          <Card bordered={false} className="stat-card">
            <Statistic 
              title="Zayiat Toplam" 
              value={dashboard?.totalWasteToday} 
              suffix="Birim"
              prefix={<DeleteOutlined style={{ color: '#ff7875' }} />} 
            />
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6} lg={4}>
          <Card bordered={false} className="stat-card">
            <Statistic 
              title="Açık Şubeler" 
              value={dashboard?.branchesOpenToday} 
              prefix={<ShopOutlined style={{ color: '#1677ff' }} />} 
            />
          </Card>
        </Col>
        <Col xs={24} sm={12} md={6} lg={4}>
          <Card bordered={false} className="stat-card">
            <Statistic 
              title="Kapananlar" 
              value={dashboard?.branchesClosedToday} 
              prefix={<CheckCircleOutlined style={{ color: '#722ed1' }} />} 
            />
          </Card>
        </Col>
      </Row>

      <Card title={
        <Space style={{ flexWrap: 'wrap' }}>
          <CalendarOutlined />
          <span>Şube Bazlı Günlük Özet</span>
          <Tooltip title="Filtrelenmiş şube verileri listelenmektedir.">
            <InfoCircleOutlined style={{ fontSize: 14, color: '#8c8c8c' }} />
          </Tooltip>
        </Space>
      } bordered={false}>
        <Table 
          columns={columns} 
          dataSource={filteredSummaries} 
          rowKey="branchId"
          pagination={false}
          locale={{ emptyText: <Empty description="Veri bulunamadı" /> }}
          scroll={{ x: 'max-content' }}
        />
      </Card>

      <style>{`
        .stat-card {
          box-shadow: 0 1px 2px rgba(0,0,0,0.03);
          transition: all 0.3s;
          border-radius: 8px;
        }
        .stat-card:hover {
          transform: translateY(-2px);
          box-shadow: 0 4px 12px rgba(0,0,0,0.05);
        }
      `}</style>
    </div>
  );
};

export default AdminDashboard;
