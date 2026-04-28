export interface CurrentStock {
  productId: string;
  productName: string;
  categoryName: string;
  unit: number;
  unitName: string;
  openingStock: number;
  receivedFromDemands: number;
  incomingTransfer: number;
  outgoingTransfer: number;
  dayWaste: number;
  currentStock: number;
}

export interface Stock {
  id: string;
  branchId: string;
  productId: string;
  productName: string;
  quantity: number;
  unitName: string;
  lastUpdated: string;
}
