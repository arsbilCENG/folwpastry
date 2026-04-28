import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { message } from 'antd';
import { dayClosingApi } from '../api/dayClosingApi';
import { CashCountDto } from '../types/dayClosing';

export const useExpectedCash = (branchId: string, date: string, enabled = true) => {
  return useQuery({
    queryKey: ['day-closing', 'expected-cash', branchId, date],
    queryFn: () => dayClosingApi.getExpectedCash(branchId, date),
    enabled: enabled && !!branchId && !!date,
  });
};

export const useSubmitCashCount = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ dayClosingId, data }: { dayClosingId: string; data: CashCountDto }) =>
      dayClosingApi.submitCashCount(dayClosingId, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['day-closing'] });
      message.success('Kasa sayımı kaydedildi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Kasa sayımı kaydedilirken hata oluştu');
    },
  });
};

export const useUploadReceiptPhoto = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ dayClosingId, photo }: { dayClosingId: string; photo: File }) =>
      dayClosingApi.uploadReceiptPhoto(dayClosingId, photo),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['day-closing'] });
      message.success('Fiş fotoğrafı yüklendi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Fiş fotoğrafı yüklenirken hata oluştu');
    },
  });
};

export const useUploadCounterPhoto = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ dayClosingId, photo }: { dayClosingId: string; photo: File }) =>
      dayClosingApi.uploadCounterPhoto(dayClosingId, photo),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['day-closing'] });
      message.success('Tezgah fotoğrafı yüklendi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Tezgah fotoğrafı yüklenirken hata oluştu');
    },
  });
};
