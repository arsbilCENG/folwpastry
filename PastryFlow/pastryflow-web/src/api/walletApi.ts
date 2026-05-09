import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type {
  WalletSummaryDto,
  BranchWalletDto,
  WalletTransactionDto,
  SetInitialBalanceRequest,
  TransferRequest,
  ManualAdjustmentRequest,
} from '../types/wallet';

export const getWalletSummary = async (): Promise<WalletSummaryDto> => {
  const response = await axiosClient.get<any, ApiResponse<WalletSummaryDto>>('/wallets/summary');
  return response.data!;
};

export const getBranchWallet = async (
  branchId: string
): Promise<BranchWalletDto> => {
  const response = await axiosClient.get<any, ApiResponse<BranchWalletDto>>(`/wallets/branch/${branchId}`);
  return response.data!;
};

export const setInitialBalance = async (
  request: SetInitialBalanceRequest
): Promise<void> => {
  await axiosClient.post('/wallets/initial-balance', request);
};

export const transferBranchToAdmin = async (
  request: TransferRequest
): Promise<void> => {
  await axiosClient.post('/wallets/transfer/branch-to-admin', request);
};

export const transferAdminToBranch = async (
  request: TransferRequest
): Promise<void> => {
  await axiosClient.post('/wallets/transfer/admin-to-branch', request);
};

export const manualAdjustment = async (
  request: ManualAdjustmentRequest
): Promise<void> => {
  await axiosClient.post('/wallets/adjustment', request);
};

export const getWalletTransactions = async (
  branchId?: string,
  startDate?: string,
  endDate?: string
): Promise<WalletTransactionDto[]> => {
  const params = new URLSearchParams();
  if (branchId) params.append('branchId', branchId);
  if (startDate) params.append('startDate', startDate);
  if (endDate) params.append('endDate', endDate);
  const response = await axiosClient.get<any, ApiResponse<WalletTransactionDto[]>>(`/wallets/transactions?${params}`);
  return response.data!;
};
