export interface DeliveryReturnDto {
  id: string;
  demandId: string;
  demandItemId: string;
  productId: string;
  productName: string;
  categoryName: string;
  unitType: string;
  fromBranchId: string;
  fromBranchName: string;
  toBranchId: string;
  toBranchName: string;
  quantity: number;
  reason: string;
  photoUrl: string | null;
  status: string;
  createdAt: string;
}
