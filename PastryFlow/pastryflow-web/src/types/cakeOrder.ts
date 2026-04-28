// ============ CAKE OPTION ============
export interface CakeOptionDto {
  id: string;
  name: string;
  optionType: CakeOptionType;
  sortOrder: number;
  isActive: boolean;
}

export type CakeOptionType = 'CakeType' | 'InnerCream' | 'OuterCream';

export interface CreateCakeOptionDto {
  name: string;
  optionType: CakeOptionType;
  sortOrder: number;
}

export interface UpdateCakeOptionDto {
  name: string;
  sortOrder: number;
  isActive: boolean;
}

// ============ CUSTOM CAKE ORDER ============
export interface CustomCakeOrderDto {
  id: string;
  orderNumber: string;
  branchId: string;
  branchName: string;
  productionBranchId: string;
  productionBranchName: string;
  customerName: string | null;
  customerPhone: string | null;
  deliveryDate: string;
  servingSize: number;
  cakeTypeId: string;
  cakeTypeName: string;
  innerCreamId: string;
  innerCreamName: string;
  outerCreamId: string;
  outerCreamName: string;
  description: string;
  referencePhotoUrl: string | null;
  price: number;
  status: CustomCakeOrderStatus;
  statusText: string;
  statusNote: string | null;
  statusChangedAt: string | null;
  createdByUserName: string;
  createdAt: string;
}

export type CustomCakeOrderStatus =
  | 'Created'
  | 'SentToProduction'
  | 'InProduction'
  | 'Ready'
  | 'Delivered'
  | 'Cancelled';

export interface CreateCustomCakeOrderDto {
  customerName?: string;
  customerPhone?: string;
  deliveryDate: string;
  servingSize: number;
  cakeTypeId: string;
  innerCreamId: string;
  outerCreamId: string;
  description: string;
  price: number;
}

export interface UpdateCakeOrderStatusDto {
  newStatus: CustomCakeOrderStatus;
  statusNote?: string;
}
