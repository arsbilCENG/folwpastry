import React, { useState } from 'react';
import { List, Card, Typography, Tag, Button, Space, Segmented, Empty, Skeleton } from 'antd';
import {
  BellOutlined,
  CheckCircleOutlined,
  CloseCircleOutlined,
  ExclamationCircleOutlined,
  InfoCircleOutlined,
  CarOutlined,
  ClockCircleOutlined,
  CheckOutlined,
} from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { useNotifications } from '../../context/NotificationContext';
import { useNotificationList } from '../../hooks/useNotificationList';
import useAuth from '../../hooks/useAuth';
import type { NotificationDto, NotificationType } from '../../types/notification';

const { Title, Text } = Typography;

const getNotificationIcon = (type: NotificationType) => {
  switch (type) {
    case 'DemandCreated':
      return <BellOutlined style={{ color: '#1890ff', fontSize: 24 }} />;
    case 'DemandApproved':
      return <CheckCircleOutlined style={{ color: '#52c41a', fontSize: 24 }} />;
    case 'DemandRejected':
      return <CloseCircleOutlined style={{ color: '#f5222d', fontSize: 24 }} />;
    case 'DemandPartiallyApproved':
      return <ExclamationCircleOutlined style={{ color: '#faad14', fontSize: 24 }} />;
    case 'DeliveryReady':
      return <CarOutlined style={{ color: '#13c2c2', fontSize: 24 }} />;
    case 'DeliveryReceived':
      return <CheckCircleOutlined style={{ color: '#52c41a', fontSize: 24 }} />;
    case 'WasteRecorded':
      return <ExclamationCircleOutlined style={{ color: '#faad14', fontSize: 24 }} />;
    case 'DayClosingCorrected':
      return <InfoCircleOutlined style={{ color: '#722ed1', fontSize: 24 }} />;
    default:
      return <BellOutlined style={{ color: '#1890ff', fontSize: 24 }} />;
  }
};

const NotificationList: React.FC = () => {
  const { markAsRead, markAllAsRead } = useNotifications();
  const [filter, setFilter] = useState<'all' | 'unread' | 'read'>('all');
  const [page, setPage] = useState(1);
  const pageSize = 20;
  const navigate = useNavigate();
  const { user } = useAuth();

  const { data, isLoading, refetch } = useNotificationList({
    pageNumber: page,
    pageSize,
    isRead: filter === 'all' ? undefined : filter === 'read',
  });

  const getNotificationRoute = (notification: NotificationDto): string | null => {
    const role = user?.role;
    const roleStr = role ? String(role) : '';
    
    switch (notification.type) {
      case 'DemandCreated':
        if (role === 'Production' || roleStr === '1') {
          return `/production/demands/${notification.sourceEntityId}`;
        }
        return null;
        
      case 'DemandApproved':
      case 'DemandRejected':
      case 'DemandPartiallyApproved':
        if (role === 'Sales' || roleStr === '2') {
          return `/sales/demands`;
        }
        return null;
        
      case 'DeliveryReady':
        if (role === 'Sales' || roleStr === '2') {
          return `/sales/receive-delivery`;
        }
        return null;
        
      case 'DeliveryReceived':
        if (role === 'Production' || roleStr === '1') {
          return `/production/demands`;
        }
        return null;
        
      case 'WasteRecorded':
        if (role === 'Admin' || roleStr === '0') {
          return `/admin/reports`;
        }
        return null;
        
      case 'DayClosingCorrected':
        if (role === 'Sales' || roleStr === '2') {
          return `/sales/day-closing`;
        }
        return null;
        
      default:
        return null;
    }
  };

  const handleMarkAllAsRead = async () => {
    await markAllAsRead();
    refetch();
  };

  const handleNotificationClick = async (item: NotificationDto) => {
    if (!item.isRead) {
      await markAsRead(item.id);
      refetch();
    }
    
    const route = getNotificationRoute(item);
    if (route) {
      navigate(route);
    }
  };

  const handleMarkAsReadOnly = async (id: string) => {
    await markAsRead(id);
    refetch();
  };

  return (
    <div style={{ padding: '24px', maxWidth: '1000px', margin: '0 auto' }}>
      <Card
        title={
          <Space>
            <BellOutlined />
            <span>Bildirimler</span>
          </Space>
        }
        extra={
          <Button type="link" onClick={handleMarkAllAsRead} disabled={data?.unreadCount === 0}>
            Tümünü Okundu İşaretle
          </Button>
        }
      >
        <div style={{ marginBottom: 20 }}>
          <Text type="secondary" style={{ marginRight: 12 }}>Filtrele:</Text>
          <Segmented
            options={[
              { label: 'Tümü', value: 'all' },
              { label: 'Okunmamış', value: 'unread' },
              { label: 'Okunmuş', value: 'read' },
            ]}
            value={filter}
            onChange={(value) => {
              setFilter(value as any);
              setPage(1);
            }}
          />
        </div>

        {isLoading ? (
          <Skeleton active avatar paragraph={{ rows: 4 }} />
        ) : (
          <List
            itemLayout="horizontal"
            dataSource={data?.items || []}
            locale={{ emptyText: <Empty description="Henüz bildiriminiz bulunmuyor" /> }}
            pagination={{
              current: page,
              pageSize: pageSize,
              total: data?.totalCount || 0,
              onChange: (p) => setPage(p),
              showSizeChanger: false,
              position: 'bottom',
              align: 'center',
            }}
            renderItem={(item) => (
              <List.Item
                style={{
                  backgroundColor: item.isRead ? 'transparent' : '#f0f7ff',
                  padding: '16px 24px',
                  borderRadius: '8px',
                  marginBottom: '8px',
                  transition: 'background-color 0.3s',
                  cursor: 'pointer',
                  border: '1px solid #f0f0f0',
                }}
                onClick={() => handleNotificationClick(item)}
                actions={[
                  !item.isRead && (
                    <Button
                      type="text"
                      icon={<CheckOutlined />}
                      onClick={(e) => {
                        e.stopPropagation();
                        handleMarkAsReadOnly(item.id);
                      }}
                    >
                      Okundu
                    </Button>
                  ),
                ].filter(Boolean) as React.ReactNode[]}
              >
                <List.Item.Meta
                  avatar={getNotificationIcon(item.type)}
                  title={
                    <Space>
                      <Text strong>{item.title}</Text>
                      {!item.isRead && <Tag color="blue">Yeni</Tag>}
                      {item.sourceBranchName && <Tag color="default">{item.sourceBranchName}</Tag>}
                    </Space>
                  }
                  description={
                    <div>
                      <div style={{ marginBottom: 4 }}>{item.message}</div>
                      <Space style={{ fontSize: '12px', color: '#8c8c8c' }}>
                        <ClockCircleOutlined />
                        {item.timeAgo}
                      </Space>
                    </div>
                  }
                />
              </List.Item>
            )}
          />
        )}
      </Card>
    </div>
  );
};

export default NotificationList;
