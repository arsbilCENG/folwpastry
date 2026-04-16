import React, { useEffect, useState } from 'react';
import { Table, Tag, Button, message, Space, Modal } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { demandApi } from '../../api/demandApi';
import { Demand } from '../../types/demand';
import useAuth from '../../hooks/useAuth';

const ReceiveDeliveryPage: React.FC = () => {
  const [demands, setDemands] = useState<Demand[]>([]);
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();

  const fetchDemands = async () => {
    if (!user?.branchId) return;
    setLoading(true);
    try {
      // Get demands that are Approved(2), PartiallyApproved(3) or Delivered(5)
      const res = await demandApi.getDemands({ branchId: user.branchId });
      if (res.success && res.data) {
        // Filter in frontend to avoid multiple API calls 
        const readyToReceive = res.data.filter(d => d.status === 2 || d.status === 3 || d.status === 5);
        setDemands(readyToReceive);
      }
    } catch (err) {
      message.error('Teslimatlar yüklenemedi');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchDemands();
  }, [user]);

  const handleReceive = async (demandId: string) => {
    if (!user?.id) return;
    Modal.confirm({
      title: 'Teslim Almayı Onaylıyor Musunuz?',
      content: 'Onaylanan bu ürün miktarları, bugünkü Z Raporunda (açılış stoğuna/gelenlere) yansıyacaktır.',
      onOk: async () => {
        try {
          const res = await demandApi.receiveDemand(demandId, user.id);
          if (res.success) {
            message.success('Teslimat başarıyla stoğa aktarıldı.');
            fetchDemands();
          } else {
            message.error(res.message || 'Hata');
          }
        } catch(e) {
          message.error('İşlem başarısız');
        }
      }
    });
  };

  const columns: ColumnsType<Demand> = [
    {
      title: 'Talep No',
      dataIndex: 'demandNumber',
      key: 'demandNumber',
    },
    {
      title: 'Üretim Şubesi',
      dataIndex: 'productionBranchName',
      key: 'productionBranchName',
    },
    {
      title: 'Durum',
      dataIndex: 'statusName',
      key: 'statusName',
      render: (val: string) => <Tag color="cyan">{val}</Tag>
    },
    {
      title: 'İşlem',
      key: 'action',
      render: (_, record) => (
        <Button type="primary" onClick={() => handleReceive(record.id)}>Teslim Al</Button>
      )
    }
  ];

  return (
    <div>
      <div style={{ marginBottom: 16 }}>
        <h2>Teslimat Kabul</h2>
        <p>Aşağıdaki talepler üretim şubesinden onaylanmış veya yola çıkmıştır. Şubeye ulaştığında "Teslim Al" diyerek stoğunuza ekleyebilirsiniz.</p>
      </div>
      <Table 
        columns={columns} 
        dataSource={demands} 
        rowKey="id" 
        loading={loading}
      />
    </div>
  );
};

export default ReceiveDeliveryPage;
