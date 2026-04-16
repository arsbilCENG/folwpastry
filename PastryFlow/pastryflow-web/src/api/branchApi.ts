import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { Branch } from '../types/branch';

export const branchApi = {
  getBranches: async (type?: number): Promise<ApiResponse<Branch[]>> => {
    const params = type ? { type } : {};
    return axiosClient.get('/branches', { params });
  },
  getBranch: async (id: string): Promise<ApiResponse<Branch>> => {
    return axiosClient.get(`/branches/${id}`);
  }
};
