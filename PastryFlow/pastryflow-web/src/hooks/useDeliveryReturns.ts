import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { message } from 'antd';
import { deliveryReturnApi } from '../api/deliveryReturnApi';

export const useDeliveryReturns = (branchId?: string, startDate?: string, endDate?: string) => {
  return useQuery({
    queryKey: ['delivery-returns', branchId, startDate, endDate],
    queryFn: () => deliveryReturnApi.getByBranch(branchId, startDate, endDate),
    enabled: !!branchId,
  });
};

export const useDemandReturns = (demandId: string) => {
  return useQuery({
    queryKey: ['delivery-returns', 'demand', demandId],
    queryFn: () => deliveryReturnApi.getByDemand(demandId),
    enabled: !!demandId,
  });
};

export const useAcknowledgeReturn = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => deliveryReturnApi.acknowledge(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['delivery-returns'] });
      message.success('İade onaylandı');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'İade onaylanırken hata oluştu');
    },
  });
};
