// Eski rapor endpoint'leri / sekmeleri için tipler (üretim ve bileşenler)

export interface DailyReportDto {
  date: string;
  branchId: string;
  branchName: string;
  items: DailySalesItemDto[];
  totalCalculatedSales: number;
  totalWaste: number;
  totalSalesValue: number | null;
  counterSalesRevenue: number;
  totalPurchaseExpense: number;
  cashPurchaseExpense: number;
  cardPurchaseExpense: number;
  totalCashDeposits: number;
  totalCashWithdrawals: number;
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

export interface PurchaseReportItemDetail {
  itemName: string;
  quantity: number;
  unit: string;
  unitPrice: number;
  totalPrice: number;
}

export interface PurchaseReportItem {
  purchaseId: string;
  purchaseNumber: string;
  purchaseDate: string;
  branchName: string;
  paymentMethodLabel: string;
  totalAmount: number;
  notes?: string;
  items: PurchaseReportItemDetail[];
}

export interface PurchaseReport {
  startDate: string;
  endDate: string;
  branchName?: string;
  totalExpense: number;
  cashExpense: number;
  cardExpense: number;
  purchases: PurchaseReportItem[];
}

export interface CashTransactionReportItem {
  transactionId: string;
  transactionDate: string;
  branchName: string;
  transactionTypeLabel: string;
  methodLabel: string;
  amount: number;
  description?: string;
  createdByName: string;
}

export interface CashTransactionReport {
  startDate: string;
  endDate: string;
  branchName?: string;
  totalDeposits: number;
  totalWithdrawals: number;
  netFlow: number;
  transactions: CashTransactionReportItem[];
}

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
