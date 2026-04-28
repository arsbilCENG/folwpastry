export interface DailySummaryItem {
  productId: string;
  productName: string;
  categoryName: string;
  unit: string;
  openingStock: number;
  receivedFromDemands: number;
  incomingTransfer: number;
  outgoingTransfer: number;
  dayWaste: number;
  endOfDayCount: number;
  carryOver: number;
  endOfDayWaste: number;
  calculatedSales: number;
}

export interface DayClosingTotals {
  totalSales: number;
  totalWaste: number;
  totalCarryOver: number;
}

export interface DayClosingSummary {
  branchName: string;
  date: string;
  isClosed: boolean;
  
  // Kasa Sayımı
  expectedCashAmount: number | null;
  cashAmount: number | null;
  posAmount: number | null;
  totalCounted: number | null;
  cashDifference: number | null;
  differenceNote: string | null;
  
  // Fotoğraflar
  receiptPhotoUrl: string | null;
  counterPhotoUrl: string | null;

  items: DailySummaryItem[];
  totals: DayClosingTotals;
}

export interface CashCountDto {
  cashAmount: number;
  posAmount: number;
  differenceNote?: string;
}

export interface ExpectedCashDto {
  expectedAmount: number;
  productsWithPrice: number;
  productsWithoutPrice: number;
  items: ExpectedCashItemDto[];
}

export interface ExpectedCashItemDto {
  productName: string;
  calculatedSales: number;
  unitPrice: number | null;
  salesValue: number | null;
}

export interface CountItemDto {
  productId: string;
  endOfDayCount: number;
}

export interface CountInputDto {
  branchId: string;
  date: string;
  items: CountItemDto[];
}

export interface CarryOverItemDto {
  productId: string;
  carryOverQuantity: number;
}

export interface CarryOverInputDto {
  branchId: string;
  date: string;
  items: CarryOverItemDto[];
}
