import React, { useEffect, useState, useCallback } from 'react';
import {
  Table,
  Tag,
  Button,
  Typography,
  Space,
  Select,
  Badge,
  Modal,
  message,
  Tooltip,
} from 'antd';
import {
  EyeOutlined,
  CarOutlined,
  CheckCircleOutlined,
} from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { demandApi } from '../../api/demandApi';
import { Demand } from '../../types/demand';
import useAuth from '../../hooks/useAuth';
import dayjs from 'dayjs';

const { Title, Text } = Typography;
const { Option } = Select;

const statusColors: Record<string, string> = {
  Pending: 'orange',
  Approved: 'green',
  PartiallyApproved: 'blue',
  Rejected: 'red',
  Delivered: 'purple',
  Received: 'success',
};

const statusLabels: Record<string, string> = {
  Pending: 'Bekliyor',
  Approved: 'Onaylandı',
  PartiallyApproved: 'Kısmen Onaylandı',
  Rejected: 'Reddedildi',
  Delivered: 'Teslim Edildi',
  Received: 'Teslim Alındı',
};

const IncomingDemandsPage: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [demands, setDemands] = useState<Demand[]>([]);
  const [loading, setLoading] = useState(true);
  const [statusFilter, setStatusFilter] = useState<string>('All');
  const [deliverLoading, setDeliverLoading] = useState<string | null>(null);

  const fetchDemands = useCallback(async () => {
    if (!user?.branchId) return;
    setLoading(true);
    try {
      // Logic fix: Explicitly use productionBranchId for this page
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

  const pendingCount = demands.filter(d => d.status === 1).length;

  const handleDeliver = (demand: Demand) => {
    Modal.confirm({
      title: 'Şoföre Teslim Onayı',
      icon: <CarOutlined style={{ color: '#722ed1' }} />,
      content: (
        <div style={{ marginTop: 16 }}>
          <p><strong>{demand.demandNumber}</strong> numaralı talebi şoföre teslim etmek istediğinize emin misiniz?</p>
          <p>Satış Şubesi: <strong>{demand.salesBranchName}</strong></p>
          <p style={{ fontSize: '12px', color: '#666' }}>Bu işlemden sonra talep "Teslim Edildi" durumuna geçecektir.</p>
        </div>
      ),
      okText: 'Evet, Teslim Et',
      cancelText: 'Vazgeç',
      okButtonProps: { style: { backgroundColor: '#722ed1' } },
      onOk: async () => {
        setDeliverLoading(demand.id);
        try {
          const res = await demandApi.deliverDemand(demand.id, {});
          if (res.success) {
            message.success('Talep şoföre başarıyla teslim edildi.');
            fetchDemands();
          } else {
            message.error(res.message || 'Teslim işlemi başarısız.');
          }
        } catch {
          message.error('Bağlantı hatası.');
        } finally {
          setDeliverLoading(null);
        }
      },
    });
  };

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
      render: (val: string, record: Demand) => (
        <Tag color={statusColors[val] || 'default'}>{statusLabels[val] || val}</Tag>
      ),
    },
    {
      title: 'İşlem',
      key: 'action',
      render: (_: any, record: Demand) => (
        <Space size="middle">
          <Tooltip title={record.status === 1 ? 'Talebi İncele' : 'Talebi Görüntüle'}>
            <Button
              type={record.status === 1 ? 'primary' : 'default'}
              icon={record.status === 1 ? <CheckCircleOutlined /> : <EyeOutlined />}
              size="small"
              onClick={() => navigate(`/production/demands/${record.id}`)}
              style={record.status !== 1 ? { color: '#8c8c8c' } : {}}
            >
              {record.status === 1 ? 'İncele' : 'Görüntüle'}
            </Button>
          </Tooltip>
          
          {(record.status === 2 || record.status === 3) && (
            <Tooltip title="Şoföre Teslim Et">
              <Button
                type="primary"
                icon={<CarOutlined />}
                size="small"
                loading={deliverLoading === record.id}
                onClick={(e) => {
                  e.stopPropagation();
                  handleDeliver(record);
                }}
                style={{ backgroundColor: '#722ed1', borderColor: '#722ed1' }}
              >
                Teslim Et
              </Button>
            </Tooltip>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}>
        <Title level={2} style={{ margin: 0 }}>
          Gelen Talepler{' '}
          {pendingCount > 0 && statusFilter === 'All' && (
            <Badge count={pendingCount} style={{ marginLeft: 8, backgroundColor: '#fa8c16' }} />
          )}
        </Title>
        <Space>
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
            <Option value="Rejected">Reddedilenler</Option>
            <Option value="Delivered">Teslim Edilenler</Option>
            <Option value="Received">Teslim Alınanlar</Option>
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
      />
    </div>
  );
};

export default IncomingDemandsPage;
