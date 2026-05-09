import { useQuery } from '@tanstack/react-query';
import { reportApi } from '../api/reportApi';
import type {
  DailySalesFilterParams,
  WasteSummaryFilterParams,
  DemandSummaryFilterParams,
  BranchComparisonFilterParams,
} from '../types/report';

export const useDailySalesReport = (params: DailySalesFilterParams, enabled = true) => {
  return useQuery({
    queryKey: ['reports', 'daily-sales', params],
    queryFn: () => reportApi.getDailySales(params),
    enabled: enabled && !!params.date,
  });
};

export const useWasteSummaryReport = (params: WasteSummaryFilterParams, enabled = true) => {
  return useQuery({
    queryKey: ['reports', 'waste-summary', params],
    queryFn: () => reportApi.getWasteSummary(params),
    enabled: enabled && !!params.startDate && !!params.endDate,
  });
};

export const useDemandSummaryReport = (params: DemandSummaryFilterParams, enabled = true) => {
  return useQuery({
    queryKey: ['reports', 'demand-summary', params],
    queryFn: () => reportApi.getDemandSummary(params),
    enabled: enabled && !!params.startDate && !!params.endDate,
  });
};

export const useBranchComparisonReport = (params: BranchComparisonFilterParams, enabled = true) => {
  return useQuery({
    queryKey: ['reports', 'branch-comparison', params],
    queryFn: () => reportApi.getBranchComparison(params),
    enabled: enabled && !!params.startDate && !!params.endDate,
  });
};

export const usePurchaseReport = (
  branchId?: string,
  startDate?: string,
  endDate?: string
) => useQuery({
  queryKey: ['purchaseReport', branchId, startDate, endDate],
  queryFn: () => reportApi.getPurchaseReport(branchId, startDate, endDate),
  enabled: true,
});

export const useCashTransactionReport = (
  branchId?: string,
  startDate?: string,
  endDate?: string
) => useQuery({
  queryKey: ['cashTransactionReport', branchId, startDate, endDate],
  queryFn: () => reportApi.getCashTransactionReport(branchId, startDate, endDate),
  enabled: true,
});
