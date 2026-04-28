import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type {
  CustomCakeOrderDto,
  CreateCustomCakeOrderDto,
  UpdateCakeOrderStatusDto,
  CakeOptionDto,
  CreateCakeOptionDto,
  UpdateCakeOptionDto,
  CakeOptionType,
} from '../types/cakeOrder';

// ============ CAKE ORDERS ============
export const cakeOrderApi = {
  getAll: async (status?: string): Promise<ApiResponse<CustomCakeOrderDto[]>> => {
    const params = status ? { status } : {};
    return axiosClient.get('/custom-cake-orders', { params });
  },

  getById: async (id: string): Promise<ApiResponse<CustomCakeOrderDto>> => {
    return axiosClient.get(`/custom-cake-orders/${id}`);
  },

  create: async (data: CreateCustomCakeOrderDto): Promise<ApiResponse<CustomCakeOrderDto>> => {
    return axiosClient.post('/custom-cake-orders', data);
  },

  updateStatus: async (id: string, data: UpdateCakeOrderStatusDto): Promise<ApiResponse<CustomCakeOrderDto>> => {
    return axiosClient.put(`/custom-cake-orders/${id}/status`, data);
  },

  uploadReferencePhoto: async (id: string, photo: File): Promise<ApiResponse<string>> => {
    const formData = new FormData();
    formData.append('photo', photo);
    return axiosClient.post(`/custom-cake-orders/${id}/reference-photo`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    });
  },

  cancel: async (id: string): Promise<ApiResponse<boolean>> => {
    return axiosClient.put(`/custom-cake-orders/${id}/cancel`);
  },
};

// ============ CAKE OPTIONS (Public) ============
export const cakeOptionApi = {
  getAll: async (optionType?: CakeOptionType): Promise<ApiResponse<CakeOptionDto[]>> => {
    const params = optionType ? { optionType } : {};
    return axiosClient.get('/cake-options', { params });
  },
};

// ============ ADMIN CAKE OPTIONS ============
export const adminCakeOptionApi = {
  getAll: async (optionType?: CakeOptionType): Promise<ApiResponse<CakeOptionDto[]>> => {
    const params = optionType ? { optionType } : {};
    return axiosClient.get('/admin/cake-options', { params });
  },

  create: async (data: CreateCakeOptionDto): Promise<ApiResponse<CakeOptionDto>> => {
    return axiosClient.post('/admin/cake-options', data);
  },

  update: async (id: string, data: UpdateCakeOptionDto): Promise<ApiResponse<CakeOptionDto>> => {
    return axiosClient.put(`/admin/cake-options/${id}`, data);
  },

  delete: async (id: string): Promise<ApiResponse<boolean>> => {
    return axiosClient.delete(`/admin/cake-options/${id}`);
  },
};
