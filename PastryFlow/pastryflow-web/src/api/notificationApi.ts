import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type { NotificationListDto, NotificationFilterParams } from '../types/notification';

export const notificationApi = {
  getAll: async (params: NotificationFilterParams = {}): Promise<NotificationListDto> => {
    const res = await axiosClient.get<any, ApiResponse<NotificationListDto>>('/notifications', { params });
    return res.data!;
  },

  getUnreadCount: async (): Promise<number> => {
    const res = await axiosClient.get<any, ApiResponse<number>>('/notifications/unread-count');
    return res.data!;
  },

  markAsRead: async (id: string): Promise<ApiResponse<any>> => {
    return axiosClient.put(`/notifications/${id}/read`);
  },

  markAllAsRead: async (): Promise<ApiResponse<any>> => {
    return axiosClient.put('/notifications/read-all');
  },
};
