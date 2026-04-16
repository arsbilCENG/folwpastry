import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { Demand, CreateDemandDto } from '../types/demand';

export const demandApi = {
  createDemand: async (data: CreateDemandDto): Promise<ApiResponse<Demand>> => {
    return axiosClient.post('/demands', data);
  },
  getDemands: async (params?: any): Promise<ApiResponse<Demand[]>> => {
    return axiosClient.get('/demands', { params });
  },
  getDemand: async (id: string): Promise<ApiResponse<Demand>> => {
    return axiosClient.get(`/demands/${id}`);
  },
  receiveDemand: async (id: string, receivedByUserId: string): Promise<ApiResponse<Demand>> => {
    return axiosClient.patch(`/demands/${id}/receive`, { receivedByUserId });
  }
};
