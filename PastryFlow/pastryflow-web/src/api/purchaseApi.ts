import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type { PurchaseDto, CreatePurchaseDto } from '../types/purchase';
import { PagedResult } from '../types/admin'; // Assuming PagedResult is here as seen in adminApi.ts

export const purchaseApi = {
  getPurchases: async (params?: {
    page?: number;
    pageSize?: number;
    startDate?: string;
    endDate?: string;
  }): Promise<PagedResult<PurchaseDto>> => {
    const res = await axiosClient.get<any, ApiResponse<PagedResult<PurchaseDto>>>('/purchases', { params });
    return res.data!;
  },

  getPurchaseById: async (id: string): Promise<PurchaseDto> => {
    const res = await axiosClient.get<any, ApiResponse<PurchaseDto>>(`/purchases/${id}`);
    return res.data!;
  },

  createPurchase: async (data: CreatePurchaseDto): Promise<PurchaseDto> => {
    const res = await axiosClient.post<any, ApiResponse<PurchaseDto>>('/purchases', data);
    return res.data!;
  },

  uploadReceiptPhoto: async (id: string, photo: File): Promise<PurchaseDto> => {
    const formData = new FormData();
    formData.append('photo', photo);
    const res = await axiosClient.post<any, ApiResponse<PurchaseDto>>(
      `/purchases/${id}/receipt-photo`,
      formData,
      { headers: { 'Content-Type': 'multipart/form-data' } }
    );
    return res.data!;
  },

  deletePurchase: async (id: string): Promise<boolean> => {
    const res = await axiosClient.delete<any, ApiResponse<boolean>>(`/purchases/${id}`);
    return res.data!;
  },
};

export const adminPurchaseApi = {
  getAllPurchases: async (params?: {
    page?: number;
    pageSize?: number;
    branchId?: string | null;

    startDate?: string;
    endDate?: string;
  }): Promise<PagedResult<PurchaseDto>> => {
    const res = await axiosClient.get<any, ApiResponse<PagedResult<PurchaseDto>>>('/admin/purchases', { params });
    return res.data!;
  },
};
