import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { message } from 'antd';
import * as transferApi from '../api/transferApi';
import type {
  CreateTransferRequest,
  CancelTransferRequest,
} from '../types/transfer';
import { TransferStatus } from '../types/transfer';

export const useOutgoingTransfers = (status?: TransferStatus) =>
  useQuery({
    queryKey: ['transfers', 'outgoing', status],
    queryFn: () => transferApi.getOutgoingTransfers(status),
  });

export const useIncomingTransfers = (status?: TransferStatus) =>
  useQuery({
    queryKey: ['transfers', 'incoming', status],
    queryFn: () => transferApi.getIncomingTransfers(status),
  });

export const useAllTransfers = (status?: TransferStatus) =>
  useQuery({
    queryKey: ['transfers', 'all', status],
    queryFn: () => transferApi.getAllTransfers(status),
  });

export const useCreateTransfer = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (request: CreateTransferRequest) =>
      transferApi.createTransfer(request),
    onSuccess: () => {
      message.success('Transfer gönderildi');
      queryClient.invalidateQueries({ queryKey: ['transfers'] });
      queryClient.invalidateQueries({ queryKey: ['stock'] });
    },
    onError: (error: any) => {
      const msg = error?.response?.data?.message ?? 'Transfer oluşturulamadı';
      message.error(msg);
    },
  });
};

export const useReceiveTransfer = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => transferApi.receiveTransfer(id),
    onSuccess: () => {
      message.success('Transfer teslim alındı');
      queryClient.invalidateQueries({ queryKey: ['transfers'] });
      queryClient.invalidateQueries({ queryKey: ['stock'] });
    },
    onError: (error: any) => {
      const msg = error?.response?.data?.message ?? 'Teslim alma işlemi başarısız';
      message.error(msg);
    },
  });
};

export const useCancelTransfer = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, request }: { id: string; request: CancelTransferRequest }) =>
      transferApi.cancelTransfer(id, request),
    onSuccess: () => {
      message.success('Transfer iptal edildi');
      queryClient.invalidateQueries({ queryKey: ['transfers'] });
      queryClient.invalidateQueries({ queryKey: ['stock'] });
    },
    onError: (error: any) => {
      const msg = error?.response?.data?.message ?? 'İptal işlemi başarısız';
      message.error(msg);
    },
  });
};
