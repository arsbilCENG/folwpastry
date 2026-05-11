export enum TransferStatus {
  Shipped = 0,
  Received = 1,
  Cancelled = 2,
}

export const TransferStatusLabel: Record<TransferStatus, string> = {
  [TransferStatus.Shipped]: 'Yolda',
  [TransferStatus.Received]: 'Teslim Alındı',
  [TransferStatus.Cancelled]: 'İptal Edildi',
};

export const TransferStatusColor: Record<TransferStatus, string> = {
  [TransferStatus.Shipped]: 'blue',
  [TransferStatus.Received]: 'green',
  [TransferStatus.Cancelled]: 'red',
};

export interface TransferItemDto {
  productId: string;
  productName: string;
  categoryName: string;
  unit: string;
  quantity: number;
}

export interface TransferDto {
  id: string;
  transferNumber: string;
  senderBranchId: string;
  senderBranchName: string;
  receiverBranchId: string;
  receiverBranchName: string;
  status: TransferStatus;
  statusLabel: string;
  shippedAt: string;
  receivedAt?: string;
  cancelledAt?: string;
  notes?: string;
  cancellationReason?: string;
  createdByName: string;
  receivedByName?: string;
  items: TransferItemDto[];
}

export interface CreateTransferItemRequest {
  productId: string;
  quantity: number;
}

export interface CreateTransferRequest {
  receiverBranchId: string;
  notes?: string;
  items: CreateTransferItemRequest[];
}

export interface CancelTransferRequest {
  cancellationReason: string;
}
