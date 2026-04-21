// ============ DAILY SALES REPORT ============
export interface DailySalesReportDto {
  date: string;
  branchId: string;
  branchName: string;
  items: DailySalesItemDto[];
  totalCalculatedSales: number;
  totalWaste: number;
  totalSalesValue: number | null;
}

export interface DailySalesItemDto {
  productId: string;
  productName: string;
  categoryName: string;
  unitType: string;
  openingStock: number;
  receivedFromDemand: number;
  receivedFromTransfer: number;
  sentByTransfer: number;
  wasteQuantity: number;
  endOfDayCount: number;
  calculatedSales: number;
  unitPrice: number | null;
  salesValue: number | null;
}

// ============ WASTE SUMMARY REPORT ============
export interface WasteSummaryReportDto {
  startDate: string;
  endDate: string;
  branchId: string | null;
  branchName: string | null;
  items: WasteSummaryItemDto[];
  totalWasteQuantity: number;
  totalEstimatedLoss: number | null;
}

export interface WasteSummaryItemDto {
  productId: string;
  productName: string;
  categoryName: string;
  unitType: string;
  totalQuantity: number;
  wasteCount: number;
  estimatedLoss: number | null;
}

// ============ DEMAND SUMMARY REPORT ============
export interface DemandSummaryReportDto {
  startDate: string;
  endDate: string;
  items: DemandSummaryItemDto[];
  totalDemands: number;
  totalApproved: number;
  totalRejected: number;
  approvalRate: number;
}

export interface DemandSummaryItemDto {
  date: string;
  fromBranchId: string;
  fromBranchName: string;
  toBranchId: string;
  toBranchName: string;
  totalItems: number;
  approvedItems: number;
  rejectedItems: number;
  status: string;
}

// ============ BRANCH COMPARISON REPORT ============
export interface BranchComparisonReportDto {
  startDate: string;
  endDate: string;
  metric: string;
  items: BranchComparisonItemDto[];
}

export interface BranchComparisonItemDto {
  branchId: string;
  branchName: string;
  dailyData: DailyMetricDto[];
  total: number;
}

export interface DailyMetricDto {
  date: string;
  value: number;
}

// ============ FILTER PARAMS ============
export interface DailySalesFilterParams {
  date: string;
  branchId?: string;
}

export interface WasteSummaryFilterParams {
  startDate: string;
  endDate: string;
  branchId?: string;
  categoryId?: string;
}

export interface DemandSummaryFilterParams {
  startDate: string;
  endDate: string;
  branchId?: string;
}

export interface BranchComparisonFilterParams {
  startDate: string;
  endDate: string;
  metric: 'sales' | 'waste' | 'demand';
}
