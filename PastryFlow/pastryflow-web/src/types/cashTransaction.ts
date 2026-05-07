import { PaymentMethod } from './purchase';

export enum TransactionType {
  AdminWithdrawal = 0,
  AdminDeposit = 1,
}

export const TRANSACTION_TYPE_LABELS: Record<TransactionType, string> = {
  [TransactionType.AdminWithdrawal]: 'Para Çekme',
  [TransactionType.AdminDeposit]: 'Para Yatırma',
};

export const TRANSACTION_TYPE_COLORS: Record<TransactionType, string> = {
  [TransactionType.AdminWithdrawal]: 'red',
  [TransactionType.AdminDeposit]: 'green',
};

export interface CashTransactionDto {
  id: string;
  branchId: string;
  branchName: string;
  transactionDate: string;
  transactionType: TransactionType;
  amount: number;
  method: PaymentMethod;
  description: string;
  createdByUserName: string;
  createdAt: string;
}

export interface CreateCashTransactionDto {
  branchId: string;
  transactionDate: string;
  transactionType: TransactionType;
  amount: number;
  method: PaymentMethod;
  description: string;
}

export interface BranchCashSummaryDto {
  branchId: string;
  branchName: string;
  openingCashBalance: number;
  todayCashSales: number;
  todayCashPurchases: number;
  todayWithdrawals: number;
  todayDeposits: number;
  expectedCashBalance: number;
  lastCountedCash?: number;
  lastUpdated?: string;
}
