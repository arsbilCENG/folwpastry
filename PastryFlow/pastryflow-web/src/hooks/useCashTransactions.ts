import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { cashTransactionApi, adminCashTransactionApi } from '../api/cashTransactionApi';
import type { CreateCashTransactionDto } from '../types/cashTransaction';
import { message } from 'antd';

export const useCashTransactions = (params?: {
  page?: number;
  pageSize?: number;
  startDate?: string;
  endDate?: string;
}) => {
  return useQuery({
    queryKey: ['cash-transactions', params],
    queryFn: () => cashTransactionApi.getTransactions(params),
  });
};

export const useBranchCashSummary = (date?: string) => {
  return useQuery({
    queryKey: ['cash-summary', date],
    queryFn: () => cashTransactionApi.getSummary(date),
    refetchInterval: 60000, // Her dakika yenile
  });
};

export const useAdminCashTransactions = (params?: any) => {
  return useQuery({
    queryKey: ['admin-cash-transactions', params],
    queryFn: () => adminCashTransactionApi.getAllTransactions(params),
  });
};

export const useAdminCashSummaries = (date?: string) => {
  return useQuery({
    queryKey: ['admin-cash-summaries', date],
    queryFn: () => adminCashTransactionApi.getAllSummaries(date),
  });
};

export const useCreateCashTransaction = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateCashTransactionDto) =>
      adminCashTransactionApi.createTransaction(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin-cash-transactions'] });
      queryClient.invalidateQueries({ queryKey: ['admin-cash-summaries'] });
      queryClient.invalidateQueries({ queryKey: ['cash-summary'] });
      message.success('Kasa hareketi kaydedildi.');
    },
    onError: (error: any) => {
      message.error(error?.response?.data?.message ?? 'İşlem gerçekleştirilemedi.');
    },
  });
};

export const useDeleteCashTransaction = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => adminCashTransactionApi.deleteTransaction(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin-cash-transactions'] });
      queryClient.invalidateQueries({ queryKey: ['admin-cash-summaries'] });
      message.success('Kayıt silindi.');
    },
  });
};
