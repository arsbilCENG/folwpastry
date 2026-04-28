import React, { useState } from 'react';
import { Badge, Popover, List, Button, Typography, Space, Skeleton, Empty, Divider } from 'antd';
import { BellOutlined, CheckOutlined, RightOutlined } from '@ant-design/icons';
import { useNavigate, useLocation } from 'react-router-dom';
import { useNotifications } from '../../context/NotificationContext';
import { useNotificationList } from '../../hooks/useNotificationList';
import useAuth from '../../hooks/useAuth';
import type { NotificationDto, NotificationType } from '../../types/notification';
import {
  CheckCircleOutlined,
  CloseCircleOutlined,
  ExclamationCircleOutlined,
  InfoCircleOutlined,
  CarOutlined,
} from '@ant-design/icons';

const { Text, Title } = Typography;

interface NotificationBellProps {
  style?: React.CSSProperties;
}

const getNotificationIcon = (type: NotificationType): React.ReactNode => {
  switch (type) {
    case 'DemandCreated': return <BellOutlined style={{ color: '#1890ff' }} />;
    case 'DemandApproved': return <CheckCircleOutlined style={{ color: '#52c41a' }} />;
    case 'DemandRejected': return <CloseCircleOutlined style={{ color: '#f5222d' }} />;
    case 'DemandPartiallyApproved': return <ExclamationCircleOutlined style={{ color: '#faad14' }} />;
    case 'DeliveryReady': return <CarOutlined style={{ color: '#13c2c2' }} />;
    case 'DeliveryReceived': return <CheckCircleOutlined style={{ color: '#52c41a' }} />;
    case 'WasteRecorded': return <ExclamationCircleOutlined style={{ color: '#faad14' }} />;
    case 'DayClosingCorrected': return <InfoCircleOutlined style={{ color: '#722ed1' }} />;
    default: return <BellOutlined style={{ color: '#1890ff' }} />;
  }
};

const NotificationBell: React.FC<NotificationBellProps> = ({ style }) => {
  const { unreadCount, markAsRead, markAllAsRead } = useNotifications();
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();
  const { user } = useAuth();

  const { data, isLoading, refetch } = useNotificationList({ pageSize: 10 }, open);

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

  const getNotificationPath = () => {
    const segments = location.pathname.split('/');
    const roleBase = segments[1] || 'dashboard'; // fallback
    return `/${roleBase}/notifications`;
  };

  const handleNotificationClick = async (item: NotificationDto) => {
    if (!item.isRead) {
      await markAsRead(item.id);
      refetch();
    }
    
    const route = getNotificationRoute(item);
    if (route) {
      navigate(route);
      setOpen(false);
    }
  };

  const handleMarkAllRead = async () => {
    await markAllAsRead();
    refetch();
  };

  const content = (
    <div style={{ width: 380 }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
        <Title level={5} style={{ margin: 0 }}>Bildirimler</Title>
        <Button 
          type="link" 
          size="small" 
          onClick={handleMarkAllRead}
          disabled={unreadCount === 0}
          icon={<CheckOutlined />}
        >
          Tümünü Okundu İşaretle
        </Button>
      </div>
      
      <Divider style={{ margin: '8px 0' }} />
 
      <div style={{ maxHeight: 400, overflowY: 'auto' }}>
        {isLoading ? (
          <Skeleton active avatar paragraph={{ rows: 2 }} />
        ) : data?.items.length === 0 ? (
          <Empty description="Henüz bildiriminiz yok" image={Empty.PRESENTED_IMAGE_SIMPLE} />
        ) : (
          <List
            itemLayout="horizontal"
            dataSource={data?.items}
            renderItem={(item) => (
              <List.Item
                style={{
                  padding: '12px 8px',
                  cursor: 'pointer',
                  backgroundColor: item.isRead ? 'transparent' : '#f0f7ff',
                  transition: 'background-color 0.3s',
                  borderRadius: 4,
                  borderBottom: '1px solid #f0f0f0'
                }}
                onClick={() => handleNotificationClick(item)}
              >
                <List.Item.Meta
                  avatar={getNotificationIcon(item.type)}
                  title={
                    <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                      <Text strong={!item.isRead}>{item.title}</Text>
                      <Text type="secondary" style={{ fontSize: 11 }}>{item.timeAgo}</Text>
                    </div>
                  }
                  description={
                    <div>
                      <Text type="secondary" ellipsis={{ tooltip: item.message }} style={{ fontSize: 13 }}>
                        {item.message}
                      </Text>
                    </div>
                  }
                />
              </List.Item>
            )}
          />
        )}
      </div>
 
      <Divider style={{ margin: '8px 0' }} />
      
      <div style={{ textAlign: 'center' }}>
        <Button 
          type="link" 
          block 
          onClick={() => {
            setOpen(false);
            navigate(getNotificationPath());
          }}
          icon={<RightOutlined />}
        >
          Tüm Bildirimleri Gör
        </Button>
      </div>
    </div>
  );
 
  return (
    <Popover
      content={content}
      trigger="click"
      open={open}
      onOpenChange={setOpen}
      placement="bottomRight"
      overlayInnerStyle={{ padding: 12 }}
    >
      <Badge count={unreadCount} overflowCount={99} size="small" offset={[-2, 2]}>
        <Button
          type="text"
          icon={<BellOutlined style={{ fontSize: 20 }} />}
          style={{ ...style, display: 'flex', alignItems: 'center', justifyContent: 'center' }}
        />
      </Badge>
    </Popover>
  );
};
 
export default NotificationBell;
