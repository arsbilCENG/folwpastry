export interface DemandItem {
  id: string;
  productId: string;
  productName: string;
  categoryName?: string;
  unit: number;
  unitName: string;
  requestedQuantity: number;
  approvedQuantity: number | null;
  status: number;
  statusName: string;
  rejectionReason: string | null;
}

export interface Demand {
  id: string;
  demandNumber: string;
  salesBranchId: string;
  salesBranchName: string;
  productionBranchId: string;
  productionBranchName: string;
  status: number;
  statusName: string;
  notes: string | null;
  createdAt: string;
  createdByUserId: string;
  reviewedAt: string | null;
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
