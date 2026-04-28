import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { DayClosingSummary, CountInputDto, CarryOverInputDto, CashCountDto, ExpectedCashDto } from '../types/dayClosing';

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
  },
  getExpectedCash: async (branchId: string, date: string): Promise<ApiResponse<ExpectedCashDto>> => {
    return axiosClient.get('/day-closing/expected-cash', { params: { branchId, date } });
  },
  submitCashCount: async (dayClosingId: string, data: CashCountDto): Promise<ApiResponse<DayClosingSummary>> => {
    return axiosClient.put(`/day-closing/${dayClosingId}/cash-count`, data);
  },
  uploadReceiptPhoto: async (dayClosingId: string, photo: File): Promise<ApiResponse<string>> => {
    const formData = new FormData();
    formData.append('photo', photo);
    return axiosClient.post(`/day-closing/${dayClosingId}/receipt-photo`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    });
  },
  uploadCounterPhoto: async (dayClosingId: string, photo: File): Promise<ApiResponse<string>> => {
    const formData = new FormData();
    formData.append('photo', photo);
    return axiosClient.post(`/day-closing/${dayClosingId}/counter-photo`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    });
  }
};
