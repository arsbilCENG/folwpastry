export type DemandStatus = 
  | 'Pending' 
  | 'Approved' 
  | 'Rejected' 
  | 'PartiallyApproved' 
  | 'Shipped'
  | 'Delivered'
  | 'Received' 
  | 'Cancelled';

export interface DemandItem {
  id: string;
  productId: string;
  productName: string;
  categoryName?: string;
  unit: number;
  unitName: string;
  requestedQuantity: number;
  approvedQuantity: number | null;
  status: string | number;
  statusName: string;
  rejectionReason: string | null;

  // Gönderim (Mutfak)
  sentQuantity: number | null;
  sentAt: string | null;
  
  // Kabul (Tezgah)
  acceptedQuantity: number | null;
  rejectedQuantity: number | null;
  deliveryRejectionReason?: string | null; // Note: Backend has deliveryRejectionReason
  rejectionPhotoUrl: string | null;
  acceptedAt: string | null;
}

export interface Demand {
  id: string;
  demandNumber: string;
  salesBranchId: string;
  salesBranchName: string;
  productionBranchId: string;
  productionBranchName: string;
  status: string | number;
  statusName: string;
  notes: string | null;
  createdAt: string;
  createdByUserId: string;
  reviewedAt: string | null;
  shippedAt?: string | null;
  deliveredAt: string | null;
  receivedAt: string | null;
  receivedByUserId: string | null;
  items: DemandItem[];
}

export interface CreateDemandItemDto {
  productId: string;
  requestedQuantity: number;
}

export interface CreateDemandDto {
  salesBranchId: string;
  productionBranchId: string;
  notes?: string;
  items: CreateDemandItemDto[];
}

export interface ReviewDemandItemDto {
  demandItemId: string;
  status: 'Approved' | 'Rejected';
  approvedQuantity?: number;
  rejectionReason?: string;
}

export interface ReviewDemandDto {
  reviewedByUserId: string;
  items: ReviewDemandItemDto[];
}

export interface DeliverDemandDto {
  driverUserId?: string;
}

export interface ReceiveDemandDto {
  receivedByUserId: string;
}

export interface ShipDemandDto {
  items: ShipDemandItemDto[];
}

export interface ShipDemandItemDto {
  demandItemId: string;
  sentQuantity: number;
}

export interface AcceptDeliveryDto {
  items: AcceptDeliveryItemDto[];
}

export interface AcceptDeliveryItemDto {
  demandItemId: string;
  acceptedQuantity: number;
  rejectionReason?: string;
}
