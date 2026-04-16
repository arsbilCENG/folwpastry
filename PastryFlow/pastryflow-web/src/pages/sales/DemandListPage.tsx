import React, { useEffect, useState } from 'react';
import { Table, Tag, Button, message, Space, Modal } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { demandApi } from '../../api/demandApi';
import { Demand } from '../../types/demand';
import useAuth from '../../hooks/useAuth';

const DemandListPage: React.FC = () => {
  const [demands, setDemands] = useState<Demand[]>([]);
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();
  
  const [selectedDemand, setSelectedDemand] = useState<Demand | null>(null);
  const [isModalVisible, setIsModalVisible] = useState(false);

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

  const showDetails = (record: Demand) => {
    setSelectedDemand(record);
    setIsModalVisible(true);
  };

  const columns: ColumnsType<Demand> = [
    {
      title: 'Talep No',
      dataIndex: 'demandNumber',
      key: 'demandNumber',
      render: (text, record) => <a onClick={() => showDetails(record)}>{text}</a>
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
      render: (val: string, record) => {
        let color = 'blue';
        if (record.status === 1) color = 'gold'; // Pending
        if (record.status === 2 || record.status === 3) color = 'cyan'; // Approved
        if (record.status === 4) color = 'red'; // Rejected
        if (record.status === 5) color = 'purple'; // Delivered
        if (record.status === 6) color = 'green'; // Received
        return <Tag color={color}>{val}</Tag>;
      }
    },
    {
      title: 'İşlem',
      key: 'action',
      render: (_, record) => (
        <Button size="small" onClick={() => showDetails(record)}>Detay</Button>
      )
    }
  ];

  return (
    <div>
      <div style={{ marginBottom: 16 }}>
        <h2>Taleplerim</h2>
      </div>
      <Table 
        columns={columns} 
        dataSource={demands} 
        rowKey="id" 
        loading={loading}
      />

      <Modal
        title={`Talep Detayı - ${selectedDemand?.demandNumber}`}
        visible={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={[
          <Button key="close" onClick={() => setIsModalVisible(false)}>Kapat</Button>
        ]}
        width={800}
      >
        <div style={{ marginBottom: 16 }}>
          <p><strong>Zaman:</strong> {dayjs(selectedDemand?.createdAt).format('DD.MM.YYYY HH:mm')}</p>
          <p><strong>Durum:</strong> {selectedDemand?.statusName}</p>
          <p><strong>Üretim Şubesi:</strong> {selectedDemand?.productionBranchName}</p>
        </div>
        <Table 
          dataSource={selectedDemand?.items || []}
          pagination={false}
          rowKey="id"
          columns={[
            { title: 'Ürün', dataIndex: 'productName' },
            { title: 'Birim', dataIndex: 'unitName' },
            { title: 'İstenen', dataIndex: 'requestedQuantity' },
            { title: 'Onaylanan', dataIndex: 'approvedQuantity', render: val => val !== null ? val : '-' },
            { title: 'Durum', dataIndex: 'statusName' }
          ]}
        />
      </Modal>
    </div>
  );
};

export default DemandListPage;
