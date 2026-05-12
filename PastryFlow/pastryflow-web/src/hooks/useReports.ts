import { useQuery } from '@tanstack/react-query';
import * as reportApi from '../api/reportApi';

export const useDailySummary = (branchId?: string, date?: string) =>
  useQuery({
    queryKey: ['report', 'daily', branchId, date],
    queryFn: () => reportApi.getDailySummary(branchId, date),
    enabled: !!branchId,
  });

export const usePeriodSummary = (
  branchId?: string,
  startDate?: string,
  endDate?: string
) =>
  useQuery({
    queryKey: ['report', 'period', branchId, startDate, endDate],
    queryFn: () => reportApi.getPeriodSummary(branchId, startDate, endDate),
    enabled: !!(branchId && startDate && endDate),
  });

export const useManagementReport = (startDate?: string, endDate?: string) =>
  useQuery({
    queryKey: ['report', 'management', startDate, endDate],
    queryFn: () => reportApi.getManagementReport(startDate, endDate),
    enabled: !!(startDate && endDate),
  });
