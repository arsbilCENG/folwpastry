import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { NotificationType } from '../types/notification';

export const notificationApi = {
  getNotifications: async (params: {
    userId?: string;
    branchId?: string;
    unreadOnly?: boolean;
  }): Promise<ApiResponse<NotificationType[]>> => {
    return axiosClient.get('/notifications', { params });
  },
  getUnreadCount: async (params: {
    userId?: string;
    branchId?: string;
  }): Promise<ApiResponse<number>> => {
    return axiosClient.get('/notifications/unread-count', { params });
  },
  markAsRead: async (id: string): Promise<ApiResponse<boolean>> => {
    return axiosClient.patch(`/notifications/${id}/read`, {});
  },
};
