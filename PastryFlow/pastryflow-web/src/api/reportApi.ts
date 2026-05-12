import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type {
  DailySummaryReport,
  PeriodSummaryReport,
  ManagementReport,
} from '../types/report';

export const getDailySummary = async (
  branchId?: string,
  date?: string
): Promise<DailySummaryReport> => {
  const params = new URLSearchParams();
  if (branchId) params.append('branchId', branchId);
  if (date) params.append('date', date);
  const qs = params.toString();
  const url = qs ? `/reports/daily-summary?${qs}` : '/reports/daily-summary';
  const response = await axiosClient.get<any, ApiResponse<DailySummaryReport>>(url);
  return response.data!;
};

export const getPeriodSummary = async (
  branchId?: string,
  startDate?: string,
  endDate?: string
): Promise<PeriodSummaryReport> => {
  const params = new URLSearchParams();
  if (branchId) params.append('branchId', branchId);
  if (startDate) params.append('startDate', startDate);
  if (endDate) params.append('endDate', endDate);
  const qs = params.toString();
  const url = qs ? `/reports/period-summary?${qs}` : '/reports/period-summary';
  const response = await axiosClient.get<any, ApiResponse<PeriodSummaryReport>>(url);
  return response.data!;
};

export const getManagementReport = async (
  startDate?: string,
  endDate?: string
): Promise<ManagementReport> => {
  const params = new URLSearchParams();
  if (startDate) params.append('startDate', startDate);
  if (endDate) params.append('endDate', endDate);
  const qs = params.toString();
  const url = qs ? `/reports/management?${qs}` : '/reports/management';
  const response = await axiosClient.get<any, ApiResponse<ManagementReport>>(url);
  return response.data!;
};
