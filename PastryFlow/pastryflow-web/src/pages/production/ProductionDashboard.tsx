import React, { useEffect, useState } from 'react';
import { Card, Col, Row, Typography, Spin, Table, Tag, Badge } from 'antd';
import {
  ClockCircleOutlined,
  CheckCircleOutlined,
  CloseCircleOutlined,
} from '@ant-design/icons';
import { demandApi } from '../../api/demandApi';
import { Demand } from '../../types/demand';
import useAuth from '../../hooks/useAuth';
import dayjs from 'dayjs';

const { Title, Text } = Typography;

const statusColors: Record<string, string> = {
  Pending: 'orange',
  Approved: 'green',
  PartiallyApproved: 'blue',
  Rejected: 'red',
  Delivered: 'purple',
  Received: 'cyan',
};

const statusLabels: Record<string, string> = {
  Pending: 'Bekliyor',
  Approved: 'Onaylandı',
  PartiallyApproved: 'Kısmen Onaylandı',
  Rejected: 'Reddedildi',
  Delivered: 'Teslim Edildi',
  Received: 'Teslim Alındı',
};

const ProductionDashboard: React.FC = () => {
  const { user } = useAuth();
  const [demands, setDemands] = useState<Demand[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchDemands = async () => {
      if (!user?.branchId) return;
      try {
        const res = await demandApi.getDemands({ productionBranchId: user.branchId });
        if (res.success && res.data) {
          setDemands(res.data);
        }
      } catch {
        // silent
      } finally {
        setLoading(false);
      }
    };
    fetchDemands();
  }, [user]);

  const today = dayjs().format('YYYY-MM-DD');

  const pendingCount = demands.filter(d => d.statusName === 'Pending').length;
  const approvedToday = demands.filter(
    d => d.statusName !== 'Pending' && d.reviewedAt && dayjs(d.reviewedAt).format('YYYY-MM-DD') === today
      && (d.statusName === 'Approved' || d.statusName === 'PartiallyApproved')
  ).length;
  const rejectedToday = demands.filter(
    d => d.statusName === 'Rejected' && d.reviewedAt && dayjs(d.reviewedAt).format('YYYY-MM-DD') === today
  ).length;

  const recentDemands = demands.slice(0, 10);

  const columns = [
    {
      title: 'Talep No',
      dataIndex: 'demandNumber',
      key: 'demandNumber',
      render: (val: string) => <Text strong>{val}</Text>,
    },
    {
      title: 'Satış Noktası',
      dataIndex: 'salesBranchName',
      key: 'salesBranchName',
    },
    {
      title: 'Tarih',
      dataIndex: 'createdAt',
      key: 'createdAt',
      render: (val: string) => dayjs(val).format('DD/MM/YYYY HH:mm'),
    },
    {
      title: 'Durum',
      dataIndex: 'statusName',
      key: 'statusName',
      render: (val: string) => (
        <Tag color={statusColors[val] || 'default'}>{statusLabels[val] || val}</Tag>
      ),
    },
  ];

  if (loading) return <div style={{ textAlign: 'center', padding: '100px' }}><Spin size="large" /></div>;

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ marginBottom: 24 }}>
        <Title level={2} style={{ marginBottom: 4 }}>
          🏭 {user?.branchName || 'Üretim Paneli'}
        </Title>
        <Text type="secondary">
          {dayjs().format('DD MMMM YYYY, dddd')}
        </Text>
      </div>

      <Row gutter={[16, 16]} style={{ marginBottom: 24 }}>
        <Col xs={24} sm={8}>
          <Card
            style={{
              borderLeft: '4px solid #fa8c16',
              borderRadius: 12,
              boxShadow: '0 2px 8px rgba(0,0,0,0.08)',
            }}
          >
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
              <div>
                <Text type="secondary" style={{ fontSize: 13 }}>Bekleyen Talepler</Text>
                <Title level={2} style={{ margin: '4px 0 0', color: '#fa8c16' }}>
                  {pendingCount}
                </Title>
              </div>
              <ClockCircleOutlined style={{ fontSize: 40, color: '#fa8c16', opacity: 0.3 }} />
            </div>
          </Card>
        </Col>
        <Col xs={24} sm={8}>
          <Card
            style={{
              borderLeft: '4px solid #52c41a',
              borderRadius: 12,
              boxShadow: '0 2px 8px rgba(0,0,0,0.08)',
            }}
          >
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
              <div>
                <Text type="secondary" style={{ fontSize: 13 }}>Bugün Onaylanan</Text>
                <Title level={2} style={{ margin: '4px 0 0', color: '#52c41a' }}>
                  {approvedToday}
                </Title>
              </div>
              <CheckCircleOutlined style={{ fontSize: 40, color: '#52c41a', opacity: 0.3 }} />
            </div>
          </Card>
        </Col>
        <Col xs={24} sm={8}>
          <Card
            style={{
              borderLeft: '4px solid #ff4d4f',
              borderRadius: 12,
              boxShadow: '0 2px 8px rgba(0,0,0,0.08)',
            }}
          >
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
              <div>
                <Text type="secondary" style={{ fontSize: 13 }}>Bugün Reddedilen</Text>
                <Title level={2} style={{ margin: '4px 0 0', color: '#ff4d4f' }}>
                  {rejectedToday}
                </Title>
              </div>
              <CloseCircleOutlined style={{ fontSize: 40, color: '#ff4d4f', opacity: 0.3 }} />
            </div>
          </Card>
        </Col>
      </Row>

      <Card
        title={
          <span>
            Son Talepler{' '}
            {pendingCount > 0 && (
              <Badge count={pendingCount} style={{ marginLeft: 8, backgroundColor: '#fa8c16' }} />
            )}
          </span>
        }
        style={{ borderRadius: 12, boxShadow: '0 2px 8px rgba(0,0,0,0.08)' }}
      >
        <Table
          dataSource={recentDemands}
          columns={columns}
          rowKey="id"
          pagination={false}
          size="small"
          locale={{ emptyText: 'Henüz talep yok' }}
          scroll={{ x: 'max-content' }}
        />
      </Card>
    </div>
  );
};

export default ProductionDashboard;
