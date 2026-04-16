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
