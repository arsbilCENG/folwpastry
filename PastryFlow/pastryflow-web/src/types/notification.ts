export type NotificationType =
  | 'DemandCreated'
  | 'DemandApproved'
  | 'DemandRejected'
  | 'DemandPartiallyApproved'
  | 'DeliveryReady'
  | 'DeliveryReceived'
  | 'WasteRecorded'
  | 'DayClosingCorrected'
  | 'Info';

export interface NotificationDto {
  id: string;
  title: string;
  message: string;
  type: NotificationType;
  sourceEntity: string | null;
  sourceEntityId: string | null;
  sourceBranchId: string | null;
  sourceBranchName: string | null;
  isRead: boolean;
  readAt: string | null;
  createdAt: string;
  timeAgo: string;
}

export interface NotificationListDto {
  items: NotificationDto[];
  totalCount: number;
  unreadCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface NotificationFilterParams {
  pageNumber?: number;
  pageSize?: number;
  isRead?: boolean;
}
