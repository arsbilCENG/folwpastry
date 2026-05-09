import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type {
  DailyReportDto,
  DailySalesFilterParams,
  WasteSummaryReportDto,
  WasteSummaryFilterParams,
  DemandSummaryReportDto,
  DemandSummaryFilterParams,
  BranchComparisonReportDto,
  BranchComparisonFilterParams,
  PurchaseReport,
  CashTransactionReport,
} from '../types/report';

export const reportApi = {
  getDailySales: async (params: DailySalesFilterParams): Promise<DailyReportDto> => {
    const res = await axiosClient.get<any, ApiResponse<DailyReportDto>>('/reports/daily-sales', { params });
    return res.data!;
  },

  getWasteSummary: async (params: WasteSummaryFilterParams): Promise<WasteSummaryReportDto> => {
    const res = await axiosClient.get<any, ApiResponse<WasteSummaryReportDto>>('/reports/waste-summary', { params });
    return res.data!;
  },

  getDemandSummary: async (params: DemandSummaryFilterParams): Promise<DemandSummaryReportDto> => {
    const res = await axiosClient.get<any, ApiResponse<DemandSummaryReportDto>>('/reports/demand-summary', { params });
    return res.data!;
  },

  getBranchComparison: async (params: BranchComparisonFilterParams): Promise<BranchComparisonReportDto> => {
    const res = await axiosClient.get<any, ApiResponse<BranchComparisonReportDto>>('/reports/branch-comparison', { params });
    return res.data!;
  },

  getPurchaseReport: async (
    branchId?: string,
    startDate?: string,
    endDate?: string
  ): Promise<PurchaseReport> => {
    const params = new URLSearchParams();
    if (branchId) params.append('branchId', branchId);
    if (startDate) params.append('startDate', startDate);
    if (endDate) params.append('endDate', endDate);
    const res = await axiosClient.get<any, ApiResponse<PurchaseReport>>(`/reports/purchases?${params}`);
    return res.data!;
  },

  getCashTransactionReport: async (
    branchId?: string,
    startDate?: string,
    endDate?: string
  ): Promise<CashTransactionReport> => {
    const params = new URLSearchParams();
    if (branchId) params.append('branchId', branchId);
    if (startDate) params.append('startDate', startDate);
    if (endDate) params.append('endDate', endDate);
    const res = await axiosClient.get<any, ApiResponse<CashTransactionReport>>(`/reports/cash-transactions?${params}`);
    return res.data!;
  },
};
