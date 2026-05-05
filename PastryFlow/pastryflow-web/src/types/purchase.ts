export enum PaymentMethod {
  Cash = 0,
  CreditCard = 1,
}

export const PAYMENT_METHOD_LABELS: Record<PaymentMethod, string> = {
  [PaymentMethod.Cash]: 'Nakit',
  [PaymentMethod.CreditCard]: 'Kart',
};

export const PAYMENT_METHOD_COLORS: Record<PaymentMethod, string> = {
  [PaymentMethod.Cash]: 'green',
  [PaymentMethod.CreditCard]: 'blue',
};

export interface PurchaseItemDto {
  id: string;
  productId?: string;
  productName?: string;
  itemName: string;
  quantity: number;
  unit: string;
  unitPrice: number;
  totalPrice: number;
  affectsStock: boolean;
}

export interface PurchaseDto {
  id: string;
  purchaseNumber: string;
  branchId: string;
  branchName: string;
  purchaseDate: string;
  paymentMethod: PaymentMethod;
  totalAmount: number;
  receiptPhotoUrl?: string;
  notes?: string;
  createdByUserName: string;
  createdAt: string;
  items: PurchaseItemDto[];
}

export interface CreatePurchaseItemDto {
  productId?: string;
  itemName: string;
  quantity: number;
  unit: string;
  unitPrice: number;
}

export interface CreatePurchaseDto {
  purchaseDate: string;
  paymentMethod: PaymentMethod;
  notes?: string;
  items: CreatePurchaseItemDto[];
}
