import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { message } from 'antd';
import { cakeOrderApi, cakeOptionApi, adminCakeOptionApi } from '../api/cakeOrderApi';
import type {
  CreateCustomCakeOrderDto,
  UpdateCakeOrderStatusDto,
  CreateCakeOptionDto,
  UpdateCakeOptionDto,
  CakeOptionType,
} from '../types/cakeOrder';

// ============ CAKE ORDERS ============
export const useCakeOrders = (status?: string) => {
  return useQuery({
    queryKey: ['cake-orders', status],
    queryFn: () => cakeOrderApi.getAll(status),
  });
};

export const useCakeOrder = (id: string) => {
  return useQuery({
    queryKey: ['cake-orders', id],
    queryFn: () => cakeOrderApi.getById(id),
    enabled: !!id,
  });
};

export const useCreateCakeOrder = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateCustomCakeOrderDto) => cakeOrderApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['cake-orders'] });
      message.success('Pasta siparişi oluşturuldu');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Sipariş oluşturulurken hata oluştu');
    },
  });
};

export const useUpdateCakeOrderStatus = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateCakeOrderStatusDto }) =>
      cakeOrderApi.updateStatus(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['cake-orders'] });
      message.success('Sipariş durumu güncellendi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Durum güncellenirken hata oluştu');
    },
  });
};

export const useUploadCakeReferencePhoto = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, photo }: { id: string; photo: File }) =>
      cakeOrderApi.uploadReferencePhoto(id, photo),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['cake-orders'] });
      message.success('Referans fotoğrafı yüklendi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Fotoğraf yüklenirken hata oluştu');
    },
  });
};

export const useCancelCakeOrder = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => cakeOrderApi.cancel(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['cake-orders'] });
      message.success('Sipariş iptal edildi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Sipariş iptal edilirken hata oluştu');
    },
  });
};

// ============ CAKE OPTIONS ============
export const useCakeOptions = (optionType?: CakeOptionType) => {
  return useQuery({
    queryKey: ['cake-options', optionType],
    queryFn: () => cakeOptionApi.getAll(optionType),
  });
};

// ============ ADMIN CAKE OPTIONS ============
export const useAdminCakeOptions = (optionType?: CakeOptionType) => {
  return useQuery({
    queryKey: ['admin', 'cake-options', optionType],
    queryFn: () => adminCakeOptionApi.getAll(optionType),
  });
};

export const useCreateCakeOption = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateCakeOptionDto) => adminCakeOptionApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'cake-options'] });
      queryClient.invalidateQueries({ queryKey: ['cake-options'] });
      message.success('Seçenek oluşturuldu');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Seçenek oluşturulurken hata oluştu');
    },
  });
};

export const useUpdateCakeOption = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateCakeOptionDto }) =>
      adminCakeOptionApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'cake-options'] });
      queryClient.invalidateQueries({ queryKey: ['cake-options'] });
      message.success('Seçenek güncellendi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Seçenek güncellenirken hata oluştu');
    },
  });
};

export const useDeleteCakeOption = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => adminCakeOptionApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'cake-options'] });
      queryClient.invalidateQueries({ queryKey: ['cake-options'] });
      message.success('Seçenek silindi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Seçenek silinirken hata oluştu');
    },
  });
};
