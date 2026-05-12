// ─── Günlük Özet ───────────────────────────────────────────

export interface DailyProductSaleDto {
  categoryName: string;
  productName: string;
  unit: string;
  soldQuantity: number;
  unitPrice?: number;
  revenue: number;
  isCounter: boolean;
}

export interface DailyWasteDto {
  productName: string;
  categoryName: string;
  quantity: number;
  unit: string;
  reason?: string;
  wasteTypeLabel: string;
}

export interface DailySummaryReport {
  date: string;
  branchName: string;
  isClosed: boolean;
  productSalesRevenue: number;
  counterSalesRevenue: number;
  totalSalesRevenue: number;
  totalPurchaseExpense: number;
  cashPurchaseExpense: number;
  cardPurchaseExpense: number;
  expectedCashAmount: number;
  actualCashAmount: number;
  posAmount: number;
  cashDifference: number;
  wasteItemCount: number;
  totalWasteQuantity: number;
  productSales: DailyProductSaleDto[];
  wastes: DailyWasteDto[];
}

// ─── Dönem Raporu ──────────────────────────────────────────

export interface PeriodDailyRow {
  date: string;
  productSalesRevenue: number;
  counterSalesRevenue: number;
  totalSalesRevenue: number;
  purchaseExpense: number;
  cashDifference: number;
}

export interface PeriodProductSummary {
  categoryName: string;
  productName: string;
  unit: string;
  totalSoldQuantity: number;
  totalRevenue: number;
  isCounter: boolean;
}

export interface PeriodSummaryReport {
  startDate: string;
  endDate: string;
  branchName: string;
  closedDayCount: number;
  totalSalesRevenue: number;
  totalCounterRevenue: number;
  totalPurchaseExpense: number;
  totalCashDifference: number;
  totalWasteQuantity: number;
  dailyRows: PeriodDailyRow[];
  productSummaries: PeriodProductSummary[];
}

// ─── Yönetim Paneli ────────────────────────────────────────

export interface BranchComparisonDto {
  branchName: string;
  totalSalesRevenue: number;
  totalPurchaseExpense: number;
  totalCashDifference: number;
  netRevenue: number;
  closedDayCount: number;
}

export interface BranchWalletSummaryDto {
  branchName: string;
  cashBalance: number;
  bankBalance: number;
  totalBalance: number;
}

export interface WalletMovementDto {
  transactionDate: string;
  branchName: string;
  transactionTypeLabel: string;
  walletTypeLabel: string;
  amount: number;
  description?: string;
  createdByName: string;
}

export interface ManagementReport {
  startDate: string;
  endDate: string;
  branchComparisons: BranchComparisonDto[];
  walletBalances: BranchWalletSummaryDto[];
  walletMovements: WalletMovementDto[];
  grandTotalRevenue: number;
  grandTotalExpense: number;
  grandTotalCashBalance: number;
  grandTotalBankBalance: number;
}

// ─── Dönem seçici ──────────────────────────────────────────

export type PeriodPreset =
  | 'this_week'
  | 'this_month'
  | 'last_month'
  | 'custom';
