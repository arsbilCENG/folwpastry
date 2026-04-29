import React, { useEffect, useState } from 'react';
import { Table, Tag, Button, message, Space, Modal, Typography, Descriptions, Divider, Image, Card } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { demandApi } from '../../api/demandApi';
import { Demand, DemandItem } from '../../types/demand';
import useAuth from '../../hooks/useAuth';
import { DEMAND_STATUS_CONFIG } from '../../utils/constants';

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

  const getItemStatusTag = (status: any) => {
    switch (status) {
      case 0:
      case 'Pending': return <Tag color="orange">Beklemede</Tag>;
      case 1:
      case 'Approved': return <Tag color="green">Onaylandı</Tag>;
      case 2:
      case 'Rejected': return <Tag color="red">Reddedildi</Tag>;
      case 3:
      case 'PartiallyApproved': return <Tag color="blue">Kısmen Onaylandı</Tag>;
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
      render: (val: string) => {
        const config = DEMAND_STATUS_CONFIG[val] || { color: 'default', text: val };
        return <Tag color={config.color}>{config.text}</Tag>;
      }
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
      title: 'Talep',
      dataIndex: 'requestedQuantity',
      key: 'requestedQuantity',
    },
    {
      title: 'Onay',
      dataIndex: 'approvedQuantity',
      key: 'approvedQuantity',
      render: (val: number | null) => val !== null ? <Text strong color="green">{val}</Text> : '-'
    },
    {
      title: 'Gönderilen',
      dataIndex: 'sentQuantity',
      key: 'sentQuantity',
      render: (val: number | null) => val !== null ? <Text strong style={{ color: '#13c2c2' }}>{val}</Text> : '-'
    },
    {
      title: 'Kabul',
      dataIndex: 'acceptedQuantity',
      key: 'acceptedQuantity',
      render: (val: number | null) => val !== null ? <Text strong style={{ color: '#52c41a' }}>{val}</Text> : '-'
    },
    {
      title: 'Durum',
      dataIndex: 'statusName',
      key: 'statusName',
      render: (_: any, record: DemandItem) => getItemStatusTag(record.status)
    },
    {
      title: 'Not / Red Nedeni',
      key: 'rejection',
      render: (_: any, record: DemandItem) => (
        <Space direction="vertical" size={0}>
          {record.rejectionReason && <Text type="danger" style={{ fontSize: 12 }}>Onay Red: {record.rejectionReason}</Text>}
          {record.deliveryRejectionReason && <Text type="warning" style={{ fontSize: 12 }}>Teslim Red: {record.deliveryRejectionReason}</Text>}
          {record.rejectionPhotoUrl && (
            <Image src={record.rejectionPhotoUrl} width={30} height={30} style={{ borderRadius: 4, marginTop: 4 }} />
          )}
        </Space>
      )
    }
  ];

  return (
    <div style={{ padding: '24px' }}>
      <Card bordered={false} style={{ borderRadius: 12, marginBottom: 24 }}>
        <Title level={2}>Taleplerim</Title>
        <Text type="secondary">Verdiğiniz siparişlerin durumunu ve üretim detaylarını buradan takip edebilirsiniz.</Text>
      </Card>

      <Table 
        columns={columns} 
        dataSource={demands} 
        rowKey="id" 
        loading={loading}
        bordered
        style={{ background: '#fff', borderRadius: 8, overflow: 'hidden' }}
        scroll={{ x: 'max-content' }}
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
          <div style={{ marginTop: 16 }}>
            <Descriptions bordered size="small" column={{ xs: 1, sm: 2 }}>
              <Descriptions.Item label="Talep Numarası">{selectedDemand.demandNumber}</Descriptions.Item>
              <Descriptions.Item label="Tarih">{dayjs(selectedDemand.createdAt).format('DD.MM.YYYY HH:mm')}</Descriptions.Item>
              <Descriptions.Item label="Üretim Şubesi">{selectedDemand.productionBranchName}</Descriptions.Item>
              <Descriptions.Item label="Durum">
                <Tag color={DEMAND_STATUS_CONFIG[selectedDemand.statusName]?.color}>{selectedDemand.statusName}</Tag>
              </Descriptions.Item>
              {selectedDemand.shippedAt && (
                <Descriptions.Item label="Gönderim Tarihi">{dayjs(selectedDemand.shippedAt).format('DD.MM.YYYY HH:mm')}</Descriptions.Item>
              )}
              {selectedDemand.receivedAt && (
                <Descriptions.Item label="Teslim Alım Tarihi">{dayjs(selectedDemand.receivedAt).format('DD.MM.YYYY HH:mm')}</Descriptions.Item>
              )}
              {selectedDemand.notes && (
                <Descriptions.Item label="Notlar" span={2}>{selectedDemand.notes}</Descriptions.Item>
              )}
            </Descriptions>

            <Divider orientation={"left" as any}>Ürün Detayları</Divider>
            
            <Table 
              dataSource={selectedDemand.items}
              columns={itemColumns}
              pagination={false}
              rowKey="id"
              size="small"
              bordered
              scroll={{ x: 'max-content' }}
            />
          </div>
        )}
      </Modal>
    </div>
  );
};

export default DemandListPage;
