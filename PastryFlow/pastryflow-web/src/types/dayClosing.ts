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
  items: DailySummaryItem[];
  totals: DayClosingTotals;
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
