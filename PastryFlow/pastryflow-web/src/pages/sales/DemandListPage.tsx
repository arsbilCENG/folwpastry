import React, { useEffect, useState } from 'react';
import { Table, Tag, Button, message, Space, Modal, Typography, Descriptions, Divider } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { demandApi } from '../../api/demandApi';
import { Demand, DemandItem } from '../../types/demand';
import useAuth from '../../hooks/useAuth';

const { Text, Title } = Typography;

const DemandListPage: React.FC = () => {
  const [demands, setDemands] = useState<Demand[]>([]);
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();
  
  const [selectedDemand, setSelectedDemand] = useState<Demand | null>(null);
  const [detailLoading, setDetailLoading] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const fetchDemands = async () => {
    if (!user?.branchId) return;
    setLoading(true);
    try {
      const res = await demandApi.getDemands({ branchId: user.branchId });
      if (res.success && res.data) {
        setDemands(res.data);
      }
    } catch (err) {
      message.error('Talepler yüklenemedi');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchDemands();
  }, [user]);

  const showDetails = async (id: string) => {
    setDetailLoading(true);
    setIsModalOpen(true);
    try {
      const res = await demandApi.getDemand(id);
      if (res.success && res.data) {
        setSelectedDemand(res.data);
      } else {
        message.error(res.message || 'Detaylar yüklenemedi');
        setIsModalOpen(false);
      }
    } catch (err) {
      message.error('Bağlantı hatası');
      setIsModalOpen(false);
    } finally {
      setDetailLoading(false);
    }
  };

  const getStatusColor = (status: number) => {
    switch (status) {
      case 1: return 'blue';    // Pending
      case 2: return 'green';   // Approved
      case 3: return 'cyan';    // PartiallyApproved
      case 4: return 'red';     // Rejected
      case 5: return 'purple';  // Delivered
      case 6: return 'success'; // Received
      default: return 'default';
    }
  };

  const getItemStatusTag = (status: number) => {
    switch (status) {
      case 1: return <Tag color="blue">Beklemede</Tag>;
      case 2: return <Tag color="green">Onaylandı</Tag>;
      case 3: return <Tag color="red">Reddedildi</Tag>;
      default: return <Tag>{status}</Tag>;
    }
  };

  const columns: ColumnsType<Demand> = [
    {
      title: 'Talep No',
      dataIndex: 'demandNumber',
      key: 'demandNumber',
      render: (text, record) => <Text strong style={{ color: '#1677ff', cursor: 'pointer' }} onClick={() => showDetails(record.id)}>{text}</Text>
    },
    {
      title: 'Üretim Şubesi',
      dataIndex: 'productionBranchName',
      key: 'productionBranchName',
    },
    {
      title: 'Tarih',
      dataIndex: 'createdAt',
      key: 'createdAt',
      render: val => dayjs(val).format('DD.MM.YYYY HH:mm')
    },
    {
      title: 'Durum',
      dataIndex: 'statusName',
      key: 'statusName',
      render: (val: string, record) => (
        <Tag color={getStatusColor(record.status)}>{val}</Tag>
      )
    },
    {
      title: 'İşlem',
      key: 'action',
      render: (_, record) => (
        <Button size="small" onClick={() => showDetails(record.id)}>Detay</Button>
      )
    }
  ];

  const itemColumns = [
    {
      title: 'Ürün Adı',
      dataIndex: 'productName',
      key: 'productName',
      render: (text: string) => <Text strong>{text}</Text>
    },
    {
      title: 'Birim',
      dataIndex: 'unitName',
      key: 'unitName',
    },
    {
      title: 'İstenen',
      dataIndex: 'requestedQuantity',
      key: 'requestedQuantity',
    },
    {
      title: 'Onaylanan',
      dataIndex: 'approvedQuantity',
      key: 'approvedQuantity',
      render: (val: number | null) => val !== null ? <Text strong color="green">{val}</Text> : '-'
    },
    {
      title: 'Durum',
      dataIndex: 'statusName',
      key: 'statusName',
      render: (_: any, record: DemandItem) => getItemStatusTag(record.status)
    },
    {
      title: 'Sebep / Not',
      dataIndex: 'rejectionReason',
      key: 'rejectionReason',
      render: (val: string | null) => val ? <Text type="danger">{val}</Text> : '-'
    }
  ];

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ marginBottom: 16 }}>
        <Title level={2}>Taleplerim</Title>
      </div>
      <Table 
        columns={columns} 
        dataSource={demands} 
        rowKey="id" 
        loading={loading}
        bordered
      />

      <Modal
        title={
          <span>
            Talep Detayı: <Text copyable>{selectedDemand?.demandNumber}</Text>
          </span>
        }
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={[
          <Button key="close" onClick={() => setIsModalOpen(false)}>Kapat</Button>
        ]}
        width={1000}
        loading={detailLoading}
      >
        {selectedDemand && (
          <>
            <Descriptions bordered size="small" column={{ xs: 1, sm: 2 }}>
              <Descriptions.Item label="Talep Numarası">{selectedDemand.demandNumber}</Descriptions.Item>
              <Descriptions.Item label="Tarih">{dayjs(selectedDemand.createdAt).format('DD.MM.YYYY HH:mm')}</Descriptions.Item>
              <Descriptions.Item label="Üretim Şubesi">{selectedDemand.productionBranchName}</Descriptions.Item>
              <Descriptions.Item label="Durum">
                <Tag color={getStatusColor(selectedDemand.status)}>{selectedDemand.statusName}</Tag>
              </Descriptions.Item>
              {selectedDemand.notes && (
                <Descriptions.Item label="Notlar" span={2}>{selectedDemand.notes}</Descriptions.Item>
              )}
            </Descriptions>

            <Divider orientation={"left" as any}>Talep Edilen Ürünler</Divider>
            
            <Table 
              dataSource={selectedDemand.items}
              columns={itemColumns}
              pagination={false}
              rowKey="id"
              size="small"
              bordered
            />
          </>
        )}
      </Modal>
    </div>
  );
};

export default DemandListPage;
