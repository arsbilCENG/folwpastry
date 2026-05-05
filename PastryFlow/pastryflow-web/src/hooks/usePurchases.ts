import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { purchaseApi, adminPurchaseApi } from '../api/purchaseApi';
import type { CreatePurchaseDto } from '../types/purchase';
import { message } from 'antd';

export const usePurchases = (params?: {
  page?: number;
  pageSize?: number;
  startDate?: string;
  endDate?: string;
}) => {
  return useQuery({
    queryKey: ['purchases', params],
    queryFn: () => purchaseApi.getPurchases(params),
  });
};

export const usePurchase = (id: string) => {
  return useQuery({
    queryKey: ['purchases', id],
    queryFn: () => purchaseApi.getPurchaseById(id),
    enabled: !!id,
  });
};

export const useCreatePurchase = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: CreatePurchaseDto) => purchaseApi.createPurchase(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['purchases'] });
      queryClient.invalidateQueries({ queryKey: ['stock'] });
    },
    onError: (error: any) => {
      message.error(error?.message || 'Satın alım oluşturulamadı.');
    },
  });
};

export const useUploadReceiptPhoto = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, photo }: { id: string; photo: File }) =>
      purchaseApi.uploadReceiptPhoto(id, photo),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['purchases'] });
    },
    onError: (error: any) => {
      message.error(error?.message || 'Fotoğraf yüklenemedi.');
    },
  });
};

export const useDeletePurchase = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => purchaseApi.deletePurchase(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['purchases'] });
      queryClient.invalidateQueries({ queryKey: ['stock'] });
      message.success('Satın alım silindi.');
    },
    onError: (error: any) => {
      message.error(error?.message || 'Silinemedi.');
    },
  });
};

export const useAdminPurchases = (params?: {
  page?: number;
  pageSize?: number;
  branchId?: string | null;

  startDate?: string;
  endDate?: string;
}) => {
  return useQuery({
    queryKey: ['admin-purchases', params],
    queryFn: () => adminPurchaseApi.getAllPurchases(params),
  });
};
