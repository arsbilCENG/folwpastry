import { useQuery } from '@tanstack/react-query';
import { stockApi } from '../api/stockApi';
import type { CurrentStock } from '../types/stock';

export const useStock = (branchId: string, date: string) => {
  return useQuery<CurrentStock[]>({
    queryKey: ['stock', branchId, date],
    queryFn: async () => {
      if (!branchId) return [];
      const res = await stockApi.getCurrentStock(branchId, date);
      return res.data || [];
    },
    enabled: !!branchId && !!date,
  });
};
