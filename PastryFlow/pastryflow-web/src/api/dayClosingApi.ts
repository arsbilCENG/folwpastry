import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { DayClosingSummary, CountInputDto, CarryOverInputDto } from '../types/dayClosing';

export const dayClosingApi = {
  saveCount: async (data: CountInputDto): Promise<ApiResponse<string>> => {
    return axiosClient.post('/day-closing/count', data);
  },
  saveCarryOver: async (data: CarryOverInputDto): Promise<ApiResponse<string>> => {
    return axiosClient.post('/day-closing/carry-over', data);
  },
  closeDay: async (data: { branchId: string; date: string; closedByUserId: string }): Promise<ApiResponse<DayClosingSummary>> => {
    return axiosClient.post('/day-closing/close', data);
  },
  getSummary: async (branchId: string, date: string): Promise<ApiResponse<DayClosingSummary>> => {
    return axiosClient.get('/day-closing/summary', { params: { branchId, date } });
  }
};
