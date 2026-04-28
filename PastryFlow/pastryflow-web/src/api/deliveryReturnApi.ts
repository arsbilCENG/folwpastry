import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type { DeliveryReturnDto } from '../types/deliveryReturn';

export const deliveryReturnApi = {
  getByBranch: async (branchId?: string, startDate?: string, endDate?: string): Promise<DeliveryReturnDto[]> => {
    const params: Record<string, string> = {};
    if (branchId) params.branchId = branchId;
    if (startDate) params.startDate = startDate;
    if (endDate) params.endDate = endDate;
    const res = await axiosClient.get<any, ApiResponse<DeliveryReturnDto[]>>('/delivery-returns', { params });
    return res.data!;
  },

  getByDemand: async (demandId: string): Promise<DeliveryReturnDto[]> => {
    const res = await axiosClient.get<any, ApiResponse<DeliveryReturnDto[]>>(`/delivery-returns/demand/${demandId}`);
    return res.data!;
  },

  acknowledge: async (id: string): Promise<ApiResponse<boolean>> => {
    return axiosClient.put(`/delivery-returns/${id}/acknowledge`);
  },
};
