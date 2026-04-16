import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { Waste } from '../types/waste';

export const wasteApi = {
  createWaste: async (formData: FormData): Promise<ApiResponse<Waste>> => {
    return axiosClient.post('/wastes', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  },
  getWastes: async (branchId: string, date: string): Promise<ApiResponse<Waste[]>> => {
    return axiosClient.get('/wastes', { params: { branchId, date } });
  }
};
