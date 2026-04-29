import React, { useEffect, useState, useCallback } from 'react';
import {
  Table,
  Tag,
  Button,
  Typography,
  Space,
  Select,
  Badge,
  message,
  Tooltip,
} from 'antd';
import {
  EyeOutlined,
  CheckCircleOutlined,
  SendOutlined,
} from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { demandApi } from '../../api/demandApi';
import { Demand } from '../../types/demand';
import useAuth from '../../hooks/useAuth';
import dayjs from 'dayjs';
import { DEMAND_STATUS_CONFIG } from '../../utils/constants';

const { Title, Text } = Typography;
const { Option } = Select;

const IncomingDemandsPage: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [demands, setDemands] = useState<Demand[]>([]);
  const [loading, setLoading] = useState(true);
  const [statusFilter, setStatusFilter] = useState<string>('All');

  const fetchDemands = useCallback(async () => {
    if (!user?.branchId) return;
    setLoading(true);
    try {
      const params: any = { productionBranchId: user.branchId };
      if (statusFilter !== 'All') params.status = statusFilter;
      
      const res = await demandApi.getDemands(params);
      if (res.success && res.data) {
        setDemands(res.data);
      }
    } catch {
      message.error('Talepler yüklenemedi.');
    } finally {
      setLoading(false);
    }
  }, [user?.branchId, statusFilter]);

  useEffect(() => {
    fetchDemands();
  }, [fetchDemands]);

  const pendingCount = demands.filter(d => d.status === 0 || d.status === 'Pending').length;

  const columns = [
    {
      title: 'Talep No',
      dataIndex: 'demandNumber',
      key: 'demandNumber',
      render: (val: string) => <Text strong style={{ color: '#1677ff' }}>{val}</Text>,
    },
    {
      title: 'Satış Şubesi',
      dataIndex: 'salesBranchName',
      key: 'salesBranchName',
    },
    {
      title: 'Tarih',
      dataIndex: 'createdAt',
      key: 'createdAt',
      render: (val: string) => dayjs(val).format('DD.MM.YY HH:mm'),
    },
    {
      title: 'Kalem',
      key: 'itemCount',
      render: (_: any, record: Demand) => `${record.items?.length || 0} Ürün`,
    },
    {
      title: 'Durum',
      dataIndex: 'statusName',
      key: 'statusName',
      render: (val: string) => {
        const config = DEMAND_STATUS_CONFIG[val] || { color: 'default', text: val };
        return <Tag color={config.color}>{config.text}</Tag>;
      },
    },
    {
      title: 'İşlem',
      key: 'action',
      render: (_: any, record: Demand) => {
        const status = record.status;
        const isPending = status === 0 || status === 'Pending';
        const canShip = status === 1 || status === 'Approved' || status === 3 || status === 'PartiallyApproved';
        const isShipped = status === 4 || status === 'Shipped';
        
        return (
          <Space size="middle">
            <Tooltip title={isPending ? 'Talebi İncele' : 'Talebi Görüntüle'}>
              <Button
                type={isPending ? 'primary' : 'default'}
                icon={isPending ? <CheckCircleOutlined /> : <EyeOutlined />}
                size="small"
                onClick={() => navigate(`/production/demands/${record.id}`)}
              >
                {isPending ? 'İncele' : 'Görüntüle'}
              </Button>
            </Tooltip>
            
            {canShip && (
              <Tooltip title="Sevkiyatı Hazırla ve Gönder">
                <Button
                  type="primary"
                  icon={<SendOutlined />}
                  size="small"
                  onClick={() => navigate(`/production/ship-demand/${record.id}`)}
                  style={{ backgroundColor: '#13c2c2', borderColor: '#13c2c2' }}
                >
                  Gönder
                </Button>
              </Tooltip>
            )}

            {isShipped && (
              <Tag color="cyan" icon={<SendOutlined />}>Gönderildi</Tag>
            )}
          </Space>
        );
      },
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ 
        display: 'flex', 
        justifyContent: 'space-between', 
        alignItems: 'center', 
        marginBottom: 24,
        flexWrap: 'wrap',
        gap: 16
      }}>
        <Title level={2} style={{ margin: 0 }}>
          Gelen Talepler{' '}
          {pendingCount > 0 && statusFilter === 'All' && (
            <Badge count={pendingCount} style={{ marginLeft: 8, backgroundColor: '#fa8c16' }} />
          )}
        </Title>
        <Space wrap>
          <Text type="secondary">Filtrele:</Text>
          <Select
            value={statusFilter}
            onChange={setStatusFilter}
            style={{ width: 220 }}
          >
            <Option value="All">Tümü</Option>
            <Option value="Pending">Bekleyenler</Option>
            <Option value="Approved">Onaylananlar</Option>
            <Option value="PartiallyApproved">Kısmen Onaylananlar</Option>
            <Option value="Shipped">Gönderilenler</Option>
            <Option value="Delivered">Yoldakiler</Option>
            <Option value="Received">Teslim Alınanlar</Option>
            <Option value="Rejected">Reddedilenler</Option>
          </Select>
          <Button onClick={fetchDemands}>Tazele</Button>
        </Space>
      </div>

      <Table
        dataSource={demands}
        columns={columns}
        rowKey="id"
        loading={loading}
        bordered
        locale={{ emptyText: 'Gösterilecek talep bulunamadı' }}
        pagination={{ pageSize: 15 }}
        scroll={{ x: 'max-content' }}
      />
    </div>
  );
};

export default IncomingDemandsPage;
