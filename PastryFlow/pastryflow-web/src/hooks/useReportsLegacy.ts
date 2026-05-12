import { useQuery } from '@tanstack/react-query';
import { reportLegacyApi } from '../api/reportLegacyApi';
import type {
  DailySalesFilterParams,
  WasteSummaryFilterParams,
  DemandSummaryFilterParams,
  BranchComparisonFilterParams,
} from '../types/reportLegacy';

export const useDailySalesReport = (params: DailySalesFilterParams, enabled = true) => {
  return useQuery({
    queryKey: ['reports', 'daily-sales', params],
    queryFn: () => reportLegacyApi.getDailySales(params),
    enabled: enabled && !!params.date,
  });
};

export const useWasteSummaryReport = (params: WasteSummaryFilterParams, enabled = true) => {
  return useQuery({
    queryKey: ['reports', 'waste-summary', params],
    queryFn: () => reportLegacyApi.getWasteSummary(params),
    enabled: enabled && !!params.startDate && !!params.endDate,
  });
};

export const useDemandSummaryReport = (params: DemandSummaryFilterParams, enabled = true) => {
  return useQuery({
    queryKey: ['reports', 'demand-summary', params],
    queryFn: () => reportLegacyApi.getDemandSummary(params),
    enabled: enabled && !!params.startDate && !!params.endDate,
  });
};

export const useBranchComparisonReport = (params: BranchComparisonFilterParams, enabled = true) => {
  return useQuery({
    queryKey: ['reports', 'branch-comparison', params],
    queryFn: () => reportLegacyApi.getBranchComparison(params),
    enabled: enabled && !!params.startDate && !!params.endDate,
  });
};

export const usePurchaseReport = (
  branchId?: string,
  startDate?: string,
  endDate?: string
) =>
  useQuery({
    queryKey: ['purchaseReport', branchId, startDate, endDate],
    queryFn: () => reportLegacyApi.getPurchaseReport(branchId, startDate, endDate),
    enabled: true,
  });

export const useCashTransactionReport = (
  branchId?: string,
  startDate?: string,
  endDate?: string
) =>
  useQuery({
    queryKey: ['cashTransactionReport', branchId, startDate, endDate],
    queryFn: () => reportLegacyApi.getCashTransactionReport(branchId, startDate, endDate),
    enabled: true,
  });
