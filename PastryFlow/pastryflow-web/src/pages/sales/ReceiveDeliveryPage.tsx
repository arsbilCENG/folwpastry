import React, { useEffect, useState } from 'react';
import { Table, Tag, Button, message, Space, Modal, Typography, Card } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { CheckCircleOutlined, CarOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import { demandApi } from '../../api/demandApi';
import { Demand } from '../../types/demand';
import useAuth from '../../hooks/useAuth';

const { Title, Text } = Typography;

const ReceiveDeliveryPage: React.FC = () => {
  const [demands, setDemands] = useState<Demand[]>([]);
  const [loading, setLoading] = useState(false);
  const { user } = useAuth();

  const fetchDemands = async () => {
    if (!user?.branchId) return;
    setLoading(true);
    try {
      // Get demands for the current sales branch
      const res = await demandApi.getDemands({ branchId: user.branchId });
      if (res.success && res.data) {
        // Filter for Approved(2), PartiallyApproved(3) or Delivered(5)
        // These are the demands that are ready to be "received" into stock.
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
  }, [user?.branchId]);

  const handleReceive = async (demand: Demand) => {
    if (!user?.id) return;
    
    Modal.confirm({
      title: 'Teslimatı Onaylıyor Musunuz?',
      icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />,
      content: (
        <div style={{ marginTop: 16 }}>
          <p><strong>{demand.demandNumber}</strong> numaralı talepten gelen ürünler stoğunuza eklenecektir.</p>
          <p>Onaylanan kalemler için günlük stok özetiniz otomatik olarak güncellenecek.</p>
          <p style={{ fontWeight: 'bold' }}>Bu işlemi onaylıyor musunuz?</p>
        </div>
      ),
      okText: 'Evet, Teslim Aldım',
      cancelText: 'Vazgeç',
      okButtonProps: { style: { backgroundColor: '#52c41a', borderColor: '#52c41a' } },
      onOk: async () => {
        try {
          const res = await demandApi.receiveDemand(demand.id, user.id);
          if (res.success) {
            message.success('Teslimat başarıyla kabul edildi ve stok güncellendi.');
            fetchDemands();
          } else {
            message.error(res.message || 'Hata oluştu.');
          }
        } catch(e) {
          message.error('Bağlantı hatası.');
        }
      }
    });
  };

  const columns: ColumnsType<Demand> = [
    {
      title: 'Talep No',
      dataIndex: 'demandNumber',
      key: 'demandNumber',
      render: (val) => <Text strong color="blue">{val}</Text>
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
      render: (val) => dayjs(val).format('DD.MM.YYYY HH:mm')
    },
    {
      title: 'Durum',
      dataIndex: 'statusName',
      key: 'statusName',
      render: (val: string, record) => {
         const color = record.status === 5 ? 'purple' : 'blue';
         const icon = record.status === 5 ? <CarOutlined /> : null;
         return <Tag color={color} icon={icon}>{val}</Tag>;
      }
    },
    {
      title: 'İşlem',
      key: 'action',
      render: (_, record) => (
        <Button 
          type="primary" 
          icon={<CheckCircleOutlined />}
          onClick={() => handleReceive(record)}
          style={{ backgroundColor: '#52c41a', borderColor: '#52c41a' }}
        >
          Teslim Aldım
        </Button>
      )
    }
  ];

  return (
    <div style={{ padding: '24px' }}>
      <Card bordered={false} style={{ borderRadius: 12, marginBottom: 24, background: 'linear-gradient(135deg, #f0f2f5 0%, #e6f7ff 100%)' }}>
        <Title level={2}>Teslimat Kabul</Title>
        <Text type="secondary">
          Üretim şubesi tarafından onaylanan veya yola çıkan ürünleri buradan teslim alabilirsiniz. 
          "Teslim Aldım" butonuna tıkladığınızda ürünler otomatik olarak dükkan stoğunuza yansıyacaktır.
        </Text>
      </Card>

      <Table 
        columns={columns} 
        dataSource={demands} 
        rowKey="id" 
        loading={loading}
        bordered
        locale={{ emptyText: 'Bekleyen teslimat bulunamadı.' }}
      />
    </div>
  );
};

export default ReceiveDeliveryPage;
