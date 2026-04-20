export interface NotificationType {
  id: string;
  userId: string | null;
  branchId: string | null;
  title: string;
  message: string;
  isRead: boolean;
  relatedEntityType: string | null;
  relatedEntityId: string | null;
  createdAt: string;
}
