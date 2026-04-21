import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import type {
  DailySalesReportDto,
  DailySalesFilterParams,
  WasteSummaryReportDto,
  WasteSummaryFilterParams,
  DemandSummaryReportDto,
  DemandSummaryFilterParams,
  BranchComparisonReportDto,
  BranchComparisonFilterParams,
} from '../types/report';

export const reportApi = {
  getDailySales: async (params: DailySalesFilterParams): Promise<DailySalesReportDto> => {
    const res = await axiosClient.get<any, ApiResponse<DailySalesReportDto>>('/reports/daily-sales', { params });
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
};
