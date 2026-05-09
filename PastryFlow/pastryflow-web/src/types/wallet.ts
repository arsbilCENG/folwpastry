export enum WalletType {
  Cash = 0,
  Bank = 1,
}

export enum WalletTransactionType {
  InitialBalance = 0,
  DayClosingCash = 1,
  DayClosingBank = 2,
  PurchaseDeduction = 3,
  PurchaseRefund = 4,
  BranchToAdmin = 5,
  AdminToBranch = 6,
  ManualAdjustment = 7,
}

export interface BranchWalletDto {
  branchId: string;
  branchName: string;
  cashBalance: number;
  bankBalance: number;
  totalBalance: number;
}

export interface AdminWalletDto {
  cashBalance: number;
  bankBalance: number;
  totalBalance: number;
}

export interface WalletSummaryDto {
  branches: BranchWalletDto[];
  admin: AdminWalletDto;
  grandTotalCash: number;
  grandTotalBank: number;
  grandTotal: number;
}

export interface WalletTransactionDto {
  id: string;
  transactionDate: string;
  transactionTypeLabel: string;
  walletTypeLabel: string;
  sourceLabel?: string;
  targetLabel?: string;
  amount: number;
  description?: string;
  createdByName: string;
}

export interface SetInitialBalanceRequest {
  branchId?: string;   // undefined = AdminWallet
  cashBalance: number;
  bankBalance: number;
}

export interface TransferRequest {
  branchId: string;
  walletType: WalletType;
  amount: number;
  description?: string;
}

export interface ManualAdjustmentRequest {
  branchId?: string;   // undefined = AdminWallet
  walletType: WalletType;
  amount: number;      // pozitif = ekle, negatif = çıkar
  description: string;
}
