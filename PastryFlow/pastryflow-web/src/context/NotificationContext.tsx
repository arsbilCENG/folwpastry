import React, { createContext, useContext, useEffect, useState, useCallback, useRef } from 'react';
import { notification as antNotification } from 'antd';
import {
  BellOutlined,
  CheckCircleOutlined,
  CloseCircleOutlined,
  ExclamationCircleOutlined,
  InfoCircleOutlined,
  CarOutlined,
} from '@ant-design/icons';
import signalRService from '../services/signalRService';
import notificationSound from '../utils/notificationSound';
import { notificationApi } from '../api/notificationApi';
import useAuth from '../hooks/useAuth';
import type { NotificationDto, NotificationType } from '../types/notification';

interface NotificationContextType {
  unreadCount: number;
  refreshUnreadCount: () => void;
  markAsRead: (id: string) => Promise<void>;
  markAllAsRead: () => Promise<void>;
  isConnected: boolean;
}

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

// Bildirim tipine göre icon ve renk
const getNotificationConfig = (type: NotificationType) => {
  switch (type) {
    case 'DemandCreated':
      return { icon: <BellOutlined style={{ color: '#1890ff' }} />, antType: 'info' as const };
    case 'DemandApproved':
      return { icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />, antType: 'success' as const };
    case 'DemandRejected':
      return { icon: <CloseCircleOutlined style={{ color: '#f5222d' }} />, antType: 'error' as const };
    case 'DemandPartiallyApproved':
      return { icon: <ExclamationCircleOutlined style={{ color: '#faad14' }} />, antType: 'warning' as const };
    case 'DeliveryReady':
      return { icon: <CarOutlined style={{ color: '#13c2c2' }} />, antType: 'info' as const };
    case 'DeliveryReceived':
      return { icon: <CheckCircleOutlined style={{ color: '#52c41a' }} />, antType: 'success' as const };
    case 'WasteRecorded':
      return { icon: <ExclamationCircleOutlined style={{ color: '#faad14' }} />, antType: 'warning' as const };
    case 'DayClosingCorrected':
      return { icon: <InfoCircleOutlined style={{ color: '#722ed1' }} />, antType: 'info' as const };
    default:
      return { icon: <BellOutlined style={{ color: '#1890ff' }} />, antType: 'info' as const };
  }
};

export const NotificationProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { user, token } = useAuth();
  const [unreadCount, setUnreadCount] = useState(0);
  const [isConnected, setIsConnected] = useState(false);
  const connectionCheckInterval = useRef<any>(null);

  // Okunmamış bildirim sayısını çek
  const refreshUnreadCount = useCallback(async () => {
    if (!token) return;
    try {
      const count = await notificationApi.getUnreadCount();
      setUnreadCount(count);
    } catch {
      // Sessizce devam et
    }
  }, [token]);

  // Bildirim okundu işaretle
  const markAsRead = useCallback(async (id: string) => {
    try {
      await notificationApi.markAsRead(id);
      setUnreadCount(prev => Math.max(0, prev - 1));
    } catch {
      // Sessizce devam et
    }
  }, []);

  // Tümünü okundu işaretle
  const markAllAsRead = useCallback(async () => {
    try {
      await notificationApi.markAllAsRead();
      setUnreadCount(0);
    } catch {
      // Sessizce devam et
    }
  }, []);

  // SignalR bağlantısı kur
  useEffect(() => {
    if (!token || !user) return;

    const startConnection = async () => {
      await signalRService.start(token);
      setIsConnected(signalRService.isConnected());

      // Browser notification izni iste (ilk login'de)
      notificationSound.requestPermission();
    };

    startConnection();

    // Bağlantı durumu kontrolü
    connectionCheckInterval.current = setInterval(() => {
      setIsConnected(signalRService.isConnected());
    }, 5000);

    return () => {
      signalRService.stop();
      if (connectionCheckInterval.current) {
        clearInterval(connectionCheckInterval.current);
      }
    };
  }, [token, user]);

  // Gelen bildirimleri dinle
  useEffect(() => {
    if (!token) return;

    const unsubscribe = signalRService.onNotification((notification: NotificationDto) => {
      // 1. Unread count artır
      setUnreadCount(prev => prev + 1);

      // 2. Ses çal
      notificationSound.play();

      // 3. Ant Design notification göster (in-app toast)
      const config = getNotificationConfig(notification.type);
      antNotification[config.antType]({
        message: notification.title,
        description: notification.message,
        icon: config.icon,
        placement: 'topRight',
        duration: 6,
        onClick: () => {
          // Bildirimi okundu işaretle
          markAsRead(notification.id);
        },
      });

      // 4. Browser masaüstü bildirimi göster
      notificationSound.showBrowserNotification(notification.title, notification.message);
    });

    return unsubscribe;
  }, [token, markAsRead]);

  // İlk yüklemede unread count çek
  useEffect(() => {
    if (token) {
      refreshUnreadCount();
    }
  }, [token, refreshUnreadCount]);

  // Periyodik unread count yenileme (60 saniyede bir)
  useEffect(() => {
    if (!token) return;
    
    const interval = setInterval(refreshUnreadCount, 60000);
    return () => clearInterval(interval);
  }, [token, refreshUnreadCount]);

  return (
    <NotificationContext.Provider
      value={{
        unreadCount,
        refreshUnreadCount,
        markAsRead,
        markAllAsRead,
        isConnected,
      }}
    >
      {children}
    </NotificationContext.Provider>
  );
};

export const useNotifications = (): NotificationContextType => {
  const context = useContext(NotificationContext);
  if (!context) {
    throw new Error('useNotifications must be used within a NotificationProvider');
  }
  return context;
};
