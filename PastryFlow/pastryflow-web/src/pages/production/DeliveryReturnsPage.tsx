import React, { useState } from 'react';
import {
  Table,
  Tag,
  Button,
  Typography,
  Card,
  Space,
  DatePicker,
  Image,
} from 'antd';
import {
  RollbackOutlined,
  CheckOutlined,
  EyeOutlined,
} from '@ant-design/icons';
import dayjs from 'dayjs';
import { useDeliveryReturns, useAcknowledgeReturn } from '../../hooks/useDeliveryReturns';
import useAuth from '../../hooks/useAuth';

const { Title, Text } = Typography;
const { RangePicker } = DatePicker;

const DeliveryReturnsPage: React.FC = () => {
  const { user } = useAuth();
  const [dates, setDates] = useState<[dayjs.Dayjs, dayjs.Dayjs] | null>(null);
  
  const startDate = dates ? dates[0].format('YYYY-MM-DD') : undefined;
  const endDate = dates ? dates[1].format('YYYY-MM-DD') : undefined;

  const { data: returns, isLoading } = useDeliveryReturns(user?.branchId ?? undefined, startDate, endDate);
  const acknowledgeMutation = useAcknowledgeReturn();

  const handleAcknowledge = async (id: string) => {
    try {
      await acknowledgeMutation.mutateAsync(id);
    } catch {
      // Handled
    }
  };

  const columns = [
    {
      title: 'Tarih',
      dataIndex: 'createdAt',
      key: 'createdAt',
      width: 150,
      render: (val: string) => dayjs(val).format('DD.MM.YYYY HH:mm'),
    },
    {
      title: 'Gönderen Şube',
      dataIndex: 'fromBranchName',
      key: 'fromBranchName',
      render: (text: string) => <Text strong>{text}</Text>
    },
    {
      title: 'Ürün',
      dataIndex: 'productName',
      key: 'productName',
      render: (text: string, record: any) => (
        <Space direction="vertical" size={0}>
          <Text strong>{text}</Text>
          <Text type="secondary" style={{ fontSize: 11 }}>{record.categoryName}</Text>
        </Space>
      ),
    },
    {
      title: 'Miktar',
      key: 'quantity',
      render: (_: any, record: any) => (
        <Text strong style={{ color: '#ff4d4f' }}>{record.quantity} {record.unitType}</Text>
      ),
    },
    {
      title: 'Red Nedeni',
      dataIndex: 'reason',
      key: 'reason',
      render: (text: string) => <Text type="danger">{text}</Text>
    },
    {
      title: 'Fotoğraf',
      dataIndex: 'photoUrl',
      key: 'photoUrl',
      align: 'center' as const,
      render: (url: string) => url ? (
        <Image
          src={url}
          alt="İade"
          width={40}
          height={40}
          style={{ objectFit: 'cover', borderRadius: 4 }}
          preview={{ mask: <EyeOutlined /> }}
        />
      ) : <Text type="secondary">-</Text>,
    },
    {
      title: 'Durum',
      dataIndex: 'status',
      key: 'status',
      render: (status: string) => (
        <Tag color={status === 'Created' ? 'orange' : 'green'}>
          {status === 'Created' ? 'Yeni' : 'Görüldü'}
        </Tag>
      ),
    },
    {
      title: 'İşlem',
      key: 'action',
      render: (_: any, record: any) => (
        record.status === 'Created' ? (
          <Button
            type="primary"
            size="small"
            icon={<CheckOutlined />}
            onClick={() => handleAcknowledge(record.id)}
            loading={acknowledgeMutation.isPending}
          >
            Görüldü
          </Button>
        ) : null
      ),
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
          <RollbackOutlined style={{ marginRight: 12 }} />
          Sevkiyat İadeleri
        </Title>
        <Space wrap>
          <RangePicker 
            onChange={(vals) => setDates(vals as [dayjs.Dayjs, dayjs.Dayjs])} 
            placeholder={['Başlangıç', 'Bitiş']}
            style={{ width: '100%', maxWidth: 300 }}
          />
        </Space>
      </div>

      <Card bordered={false} style={{ borderRadius: 12 }}>
        <Table
          dataSource={returns}
          columns={columns}
          rowKey="id"
          loading={isLoading}
          bordered
          pagination={{ pageSize: 15 }}
          locale={{ emptyText: 'İade kaydı bulunamadı' }}
          scroll={{ x: 'max-content' }}
        />
      </Card>
    </div>
  );
};

export default DeliveryReturnsPage;
