import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { message } from 'antd';
import * as walletApi from '../api/walletApi';
import type {
  SetInitialBalanceRequest,
  TransferRequest,
  ManualAdjustmentRequest,
} from '../types/wallet';

export const useWalletSummary = () =>
  useQuery({
    queryKey: ['walletSummary'],
    queryFn: walletApi.getWalletSummary,
    refetchInterval: 60_000, // 1 dakikada bir yenile
  });

export const useBranchWallet = (branchId?: string) =>
  useQuery({
    queryKey: ['branchWallet', branchId],
    queryFn: () => walletApi.getBranchWallet(branchId!),
    enabled: !!branchId,
  });

export const useWalletTransactions = (
  branchId?: string,
  startDate?: string,
  endDate?: string
) =>
  useQuery({
    queryKey: ['walletTransactions', branchId, startDate, endDate],
    queryFn: () =>
      walletApi.getWalletTransactions(branchId, startDate, endDate),
  });

export const useSetInitialBalance = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (request: SetInitialBalanceRequest) =>
      walletApi.setInitialBalance(request),
    onSuccess: () => {
      message.success('Başlangıç bakiyesi kaydedildi');
      queryClient.invalidateQueries({ queryKey: ['walletSummary'] });
    },
    onError: () => message.error('Bakiye kaydedilemedi'),
  });
};

export const useTransferBranchToAdmin = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (request: TransferRequest) =>
      walletApi.transferBranchToAdmin(request),
    onSuccess: () => {
      message.success('Para transferi tamamlandı');
      queryClient.invalidateQueries({ queryKey: ['walletSummary'] });
      queryClient.invalidateQueries({ queryKey: ['walletTransactions'] });
    },
    onError: (error: any) => {
      const msg = error?.response?.data?.message ?? 'Transfer gerçekleştirilemedi';
      message.error(msg);
    },
  });
};

export const useTransferAdminToBranch = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (request: TransferRequest) =>
      walletApi.transferAdminToBranch(request),
    onSuccess: () => {
      message.success('Para transferi tamamlandı');
      queryClient.invalidateQueries({ queryKey: ['walletSummary'] });
      queryClient.invalidateQueries({ queryKey: ['walletTransactions'] });
    },
    onError: (error: any) => {
      const msg = error?.response?.data?.message ?? 'Transfer gerçekleştirilemedi';
      message.error(msg);
    },
  });
};

export const useManualAdjustment = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (request: ManualAdjustmentRequest) =>
      walletApi.manualAdjustment(request),
    onSuccess: () => {
      message.success('Bakiye güncellendi');
      queryClient.invalidateQueries({ queryKey: ['walletSummary'] });
      queryClient.invalidateQueries({ queryKey: ['walletTransactions'] });
    },
    onError: (error: any) => {
      const msg = error?.response?.data?.message ?? 'Güncelleme gerçekleştirilemedi';
      message.error(msg);
    },
  });
};
