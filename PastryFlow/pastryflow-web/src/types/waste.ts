export interface Waste {
  id: string;
  branchId: string;
  branchName: string;
  productId: string;
  productName: string;
  quantity: number;
  wasteType: number;
  wasteTypeName: string;
  photoPath: string | null;
  notes: string | null;
  date: string;
  createdByUserId: string;
  createdAt: string;
}
