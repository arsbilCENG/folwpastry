import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { CurrentStock } from '../types/stock';

export const stockApi = {
  getCurrentStock: async (branchId: string, date: string): Promise<ApiResponse<CurrentStock[]>> => {
    return axiosClient.get('/stock/current', { params: { branchId, date } });
  }
};
