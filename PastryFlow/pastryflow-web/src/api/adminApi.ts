import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
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
export const adminDashboardApi = {
  getDashboard: async (date?: string): Promise<AdminDashboardDto> => {
    const params = date ? { date } : {};
    const res = await axiosClient.get<any, ApiResponse<AdminDashboardDto>>('/admin/dashboard', { params });
    return res.data!; // Using non-null assertion to satisfy strict types
  },
};

// ============ USERS ============
export const adminUsersApi = {
  getAll: async (filters: UserFilterParams = {}): Promise<PagedResult<UserListDto>> => {
    const res = await axiosClient.get<any, ApiResponse<PagedResult<UserListDto>>>('/admin/users', { params: filters });
    return res.data!;
  },
  
  getById: async (id: string): Promise<UserListDto> => {
    const res = await axiosClient.get<any, ApiResponse<UserListDto>>(`/admin/users/${id}`);
    return res.data!;
  },
  
  update: async (id: string, data: UpdateUserDto): Promise<ApiResponse<UserListDto>> => {
    return axiosClient.put(`/admin/users/${id}`, data);
  },
  
  resetPassword: async (id: string, data: ResetPasswordDto): Promise<ApiResponse<string>> => {
    return axiosClient.put(`/admin/users/${id}/reset-password`, data);
  },
  
  delete: async (id: string): Promise<ApiResponse<string>> => {
    return axiosClient.delete(`/admin/users/${id}`);
  },
};

// ============ CATEGORIES ============
export const adminCategoriesApi = {
  getAll: async (): Promise<CategoryListDto[]> => {
    const res = await axiosClient.get<any, ApiResponse<CategoryListDto[]>>('/admin/categories');
    return res.data!;
  },
  
  getById: async (id: string): Promise<CategoryListDto> => {
    const res = await axiosClient.get<any, ApiResponse<CategoryListDto>>(`/admin/categories/${id}`);
    return res.data!;
  },
  
  create: async (data: CreateCategoryDto): Promise<ApiResponse<CategoryListDto>> => {
    return axiosClient.post('/admin/categories', data);
  },
  
  update: async (id: string, data: UpdateCategoryDto): Promise<ApiResponse<CategoryListDto>> => {
    return axiosClient.put(`/admin/categories/${id}`, data);
  },
  
  delete: async (id: string): Promise<ApiResponse<string>> => {
    return axiosClient.delete(`/admin/categories/${id}`);
  },
};

// ============ PRODUCTS ============
export const adminProductsApi = {
  getAll: async (filters: ProductFilterParams = {}): Promise<PagedResult<ProductListDto>> => {
    const res = await axiosClient.get<any, ApiResponse<PagedResult<ProductListDto>>>('/admin/products', { params: filters });
    return res.data!;
  },
  
  getById: async (id: string): Promise<ProductListDto> => {
    const res = await axiosClient.get<any, ApiResponse<ProductListDto>>(`/admin/products/${id}`);
    return res.data!;
  },
  
  create: async (data: CreateProductDto): Promise<ApiResponse<ProductListDto>> => {
    return axiosClient.post('/admin/products', data);
  },
  
  update: async (id: string, data: UpdateProductDto): Promise<ApiResponse<ProductListDto>> => {
    return axiosClient.put(`/admin/products/${id}`, data);
  },
  
  delete: async (id: string): Promise<ApiResponse<string>> => {
    return axiosClient.delete(`/admin/products/${id}`);
  },
};

// ============ BRANCHES ============
export const adminBranchesApi = {
  getAll: async (): Promise<BranchListDto[]> => {
    const res = await axiosClient.get<any, ApiResponse<BranchListDto[]>>('/admin/branches');
    return res.data!;
  },
  
  getById: async (id: string): Promise<BranchListDto> => {
    const res = await axiosClient.get<any, ApiResponse<BranchListDto>>(`/admin/branches/${id}`);
    return res.data!;
  },
  
  update: async (id: string, data: UpdateBranchDto): Promise<ApiResponse<BranchListDto>> => {
    return axiosClient.put(`/admin/branches/${id}`, data);
  },
};

// ============ DAY CLOSING CORRECTION ============
export const adminDayClosingApi = {
  correct: async (dayClosingId: string, data: DayClosingCorrectionDto): Promise<ApiResponse<any>> => {
    return axiosClient.put(`/admin/day-closing/${dayClosingId}/correct`, data);
  },
  
  getDayClosings: async (branchId?: string, date?: string): Promise<any> => {
    const params: Record<string, string> = {};
    if (branchId) params.branchId = branchId;
    if (date) params.date = date;
    const res = await axiosClient.get<any, ApiResponse<any>>('/admin/day-closing', { params });
    return res.data!;
  },
};
