import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { message } from 'antd';
import {
  adminDashboardApi,
  adminUsersApi,
  adminCategoriesApi,
  adminProductsApi,
  adminBranchesApi,
  adminDayClosingApi
} from '../api/adminApi';
import type {
  AdminDashboardDto,
  UserListDto,
  UpdateUserDto,
  ResetPasswordDto,
  UserFilterParams,
  CategoryListDto,
  CreateCategoryDto,
  UpdateCategoryDto,
  ProductListDto,
  CreateProductDto,
  UpdateProductDto,
  ProductFilterParams,
  BranchListDto,
  UpdateBranchDto,
  DayClosingCorrectionDto,
  PagedResult,
} from '../types/admin';

// ============ DASHBOARD ============
export const useAdminDashboard = (date?: string) => {
  return useQuery<AdminDashboardDto>({
    queryKey: ['admin', 'dashboard', date],
    queryFn: () => adminDashboardApi.getDashboard(date),
    refetchInterval: 60000, // 1 dakikada bir yenile
  });
};

// ============ USERS ============
export const useAdminUsers = (filters: UserFilterParams = {}) => {
  return useQuery<PagedResult<UserListDto>>({
    queryKey: ['admin', 'users', filters],
    queryFn: () => adminUsersApi.getAll(filters),
  });
};

export const useAdminUser = (id: string) => {
  return useQuery<UserListDto>({
    queryKey: ['admin', 'users', id],
    queryFn: () => adminUsersApi.getById(id),
    enabled: !!id,
  });
};

export const useUpdateUser = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateUserDto }) =>
      adminUsersApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'users'] });
      message.success('Kullanıcı başarıyla güncellendi');
    },
    onError: (error: any) => {
      message.error(error.message || 'Kullanıcı güncellenirken hata oluştu');
    },
  });
};

export const useResetPassword = () => {
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: ResetPasswordDto }) =>
      adminUsersApi.resetPassword(id, data),
    onSuccess: () => {
      message.success('Şifre başarıyla sıfırlandı');
    },
    onError: (error: any) => {
      message.error(error.message || 'Şifre sıfırlanırken hata oluştu');
    },
  });
};

export const useDeleteUser = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => adminUsersApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'users'] });
      message.success('Kullanıcı başarıyla silindi');
    },
    onError: (error: any) => {
      message.error(error.message || 'Kullanıcı silinirken hata oluştu');
    },
  });
};

// ============ CATEGORIES ============
export const useAdminCategories = () => {
  return useQuery<CategoryListDto[]>({
    queryKey: ['admin', 'categories'],
    queryFn: () => adminCategoriesApi.getAll(),
  });
};

export const useCreateCategory = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateCategoryDto) => adminCategoriesApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'categories'] });
      message.success('Kategori başarıyla oluşturuldu');
    },
    onError: (error: any) => {
      message.error(error.message || 'Kategori oluşturulurken hata oluştu');
    },
  });
};

export const useUpdateCategory = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateCategoryDto }) =>
      adminCategoriesApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'categories'] });
      message.success('Kategori başarıyla güncellendi');
    },
    onError: (error: any) => {
      message.error(error.message || 'Kategori güncellenirken hata oluştu');
    },
  });
};

export const useDeleteCategory = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => adminCategoriesApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'categories'] });
      message.success('Kategori başarıyla silindi');
    },
    onError: (error: any) => {
      message.error(error.message || 'Kategori silinirken hata oluştu');
    },
  });
};

// ============ PRODUCTS ============
export const useAdminProducts = (filters: ProductFilterParams = {}) => {
  return useQuery<PagedResult<ProductListDto>>({
    queryKey: ['admin', 'products', filters],
    queryFn: () => adminProductsApi.getAll(filters),
  });
};

export const useCreateProduct = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateProductDto) => adminProductsApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'products'] });
      message.success('Ürün başarıyla oluşturuldu');
    },
    onError: (error: any) => {
      message.error(error.message || 'Ürün oluşturulurken hata oluştu');
    },
  });
};

export const useUpdateProduct = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateProductDto }) =>
      adminProductsApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'products'] });
      message.success('Ürün başarıyla güncellendi');
    },
    onError: (error: any) => {
      message.error(error.message || 'Ürün güncellenirken hata oluştu');
    },
  });
};

export const useDeleteProduct = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => adminProductsApi.delete(id),
    onSuccess: (response: any) => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'products'] });
      if (response.message && (response.message.includes('UYARI') || response.message.includes('WARNING'))) {
        message.warning(response.message);
      } else {
        message.success('Ürün başarıyla silindi');
      }
    },
    onError: (error: any) => {
      message.error(error.message || 'Ürün silinirken hata oluştu');
    },
  });
};

// ============ BRANCHES ============
export const useAdminBranches = () => {
  return useQuery<BranchListDto[]>({
    queryKey: ['admin', 'branches'],
    queryFn: () => adminBranchesApi.getAll(),
  });
};

export const useUpdateBranch = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateBranchDto }) =>
      adminBranchesApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['admin', 'branches'] });
      message.success('Şube başarıyla güncellendi');
    },
    onError: (error: any) => {
      message.error(error.message || 'Şube güncellenirken hata oluştu');
    },
  });
};

// ============ DAY CLOSING CORRECTION ============
export const useAdminCorrectDayClosing = () => {
    const queryClient = useQueryClient();
    return useMutation({
      mutationFn: ({ dayClosingId, data }: { dayClosingId: string; data: DayClosingCorrectionDto }) =>
        adminDayClosingApi.correct(dayClosingId, data),
      onSuccess: () => {
        queryClient.invalidateQueries({ queryKey: ['admin', 'day-closing'] });
        message.success('Gün sonu düzeltmesi başarıyla kaydedildi');
      },
      onError: (error: any) => {
        message.error(error.message || 'Düzeltme kaydedilirken hata oluştu');
      },
    });
  };
  
export const useAdminDayClosings = (branchId?: string, date?: string) => {
  return useQuery<any>({
    queryKey: ['admin', 'day-closing', branchId, date],
    queryFn: () => adminDayClosingApi.getDayClosings(branchId, date),
  });
};
