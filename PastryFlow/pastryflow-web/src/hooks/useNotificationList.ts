import { useQuery } from '@tanstack/react-query';
import { notificationApi } from '../api/notificationApi';
import type { NotificationFilterParams } from '../types/notification';

export const useNotificationList = (params: NotificationFilterParams = {}, enabled = true) => {
  return useQuery({
    queryKey: ['notifications', params],
    queryFn: () => notificationApi.getAll(params),
    enabled,
  });
};
