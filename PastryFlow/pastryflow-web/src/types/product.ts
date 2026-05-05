export enum TrackingType {
  Production = 'Production',
  Purchased = 'Purchased',
  Counter = 'Counter',
}

export const TRACKING_TYPE_LABELS: Record<TrackingType, string> = {
  [TrackingType.Production]: 'Üretim',
  [TrackingType.Purchased]: 'Satın Alım',
  [TrackingType.Counter]: 'Sayaç',
};

export const TRACKING_TYPE_COLORS: Record<TrackingType, string> = {
  [TrackingType.Production]: 'blue',
  [TrackingType.Purchased]: 'green',
  [TrackingType.Counter]: 'orange',
};

export interface Product {
  id: string;
  name: string;
  categoryId: string;
  categoryName: string;
  productionBranchId: string | null;
  productionBranchName: string | null;
  productType: number;
  productTypeName: string;
  trackingType: TrackingType;
  trackingTypeName: string;
  unit: number;
  unitName: string;
  unitPrice: number | null;
  isActive: boolean;
}

export interface CategoryWithProducts {
  id: string;
  name: string;
  sortOrder: number;
  products: Product[];
}
