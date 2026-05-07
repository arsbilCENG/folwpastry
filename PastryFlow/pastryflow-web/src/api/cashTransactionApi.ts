import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type {
  CashTransactionDto,
  CreateCashTransactionDto,
  BranchCashSummaryDto
} from '../types/cashTransaction';
import { PagedResult } from '../types/api';

export const cashTransactionApi = {
  // Şube: kendi hareketlerini gör
  getTransactions: (params?: {
    page?: number;
    pageSize?: number;
    startDate?: string;
    endDate?: string;
  }): Promise<ApiResponse<PagedResult<CashTransactionDto>>> =>
    axiosClient.get('/cash-transactions', { params }),

  // Şube: kasa özeti
  getSummary: (date?: string): Promise<ApiResponse<BranchCashSummaryDto>> =>
    axiosClient.get('/cash-transactions/summary',
      { params: { date } }),
};

export const adminCashTransactionApi = {
  // Admin: Para çek/yatır
  createTransaction: (data: CreateCashTransactionDto): Promise<ApiResponse<CashTransactionDto>> =>
    axiosClient.post('/admin/cash-transactions', data),

  // Admin: Tüm hareketler
  getAllTransactions: (params?: {
    page?: number;
    pageSize?: number;
    branchId?: string;
    startDate?: string;
    endDate?: string;
  }): Promise<ApiResponse<PagedResult<CashTransactionDto>>> =>
    axiosClient.get('/admin/cash-transactions', { params }),

  // Admin: Tüm şube özetleri
  getAllSummaries: (date?: string): Promise<ApiResponse<BranchCashSummaryDto[]>> =>
    axiosClient.get('/admin/cash-transactions/summaries',
      { params: { date } }),

  // Admin: Sil
  deleteTransaction: (id: string): Promise<ApiResponse<boolean>> =>
    axiosClient.delete(`/admin/cash-transactions/${id}`),
};
