import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { 
  Demand, 
  CreateDemandDto, 
  ReviewDemandDto, 
  DeliverDemandDto,
  ShipDemandDto,
  AcceptDeliveryDto
} from '../types/demand';

export const demandApi = {
  createDemand: async (data: CreateDemandDto): Promise<ApiResponse<Demand>> => {
    return axiosClient.post('/demands', data);
  },
  getDemands: async (params?: {
    branchId?: string;
    productionBranchId?: string;
    status?: string;
    date?: string;
  }): Promise<ApiResponse<Demand[]>> => {
    return axiosClient.get('/demands', { params });
  },
  getDemand: async (id: string): Promise<ApiResponse<Demand>> => {
    return axiosClient.get(`/demands/${id}`);
  },
  getLastDemand: async (salesBranchId: string, productionBranchId: string): Promise<ApiResponse<Demand | null>> => {
    return axiosClient.get('/demands/last', { params: { salesBranchId, productionBranchId } });
  },
  reviewDemand: async (id: string, dto: ReviewDemandDto): Promise<ApiResponse<Demand>> => {
    return axiosClient.patch(`/demands/${id}/review`, dto);
  },
  deliverDemand: async (id: string, dto: DeliverDemandDto): Promise<ApiResponse<Demand>> => {
    return axiosClient.patch(`/demands/${id}/deliver`, dto);
  },
  receiveDemand: async (id: string, receivedByUserId: string): Promise<ApiResponse<Demand>> => {
    return axiosClient.patch(`/demands/${id}/receive`, { receivedByUserId });
  },

  // Mutfak gönderim
  shipDemand: async (demandId: string, data: ShipDemandDto): Promise<ApiResponse<Demand>> => {
    return axiosClient.post(`/demands/${demandId}/ship`, data);
  },

  // Tezgah sevkiyat kabul
  acceptDelivery: async (demandId: string, data: AcceptDeliveryDto): Promise<ApiResponse<Demand>> => {
    return axiosClient.put(`/demands/${demandId}/accept-delivery`, data);
  },

  // Red fotoğrafı upload
  uploadRejectionPhoto: async (demandId: string, itemId: string, photo: File): Promise<ApiResponse<string>> => {
    const formData = new FormData();
    formData.append('photo', photo);
    return axiosClient.post(
      `/demands/${demandId}/items/${itemId}/rejection-photo`,
      formData,
      { headers: { 'Content-Type': 'multipart/form-data' } }
    );
  },
};
