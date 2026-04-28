import { useMutation, useQueryClient } from '@tanstack/react-query';
import { message } from 'antd';
import { demandApi } from '../api/demandApi';
import type { ShipDemandDto, AcceptDeliveryDto } from '../types/demand';

// Mutfak gönderim
export const useShipDemand = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ demandId, data }: { demandId: string; data: ShipDemandDto }) =>
      demandApi.shipDemand(demandId, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['demands'] });
      message.success('Sevkiyat başarıyla gönderildi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Sevkiyat gönderilirken hata oluştu');
    },
  });
};

// Tezgah sevkiyat kabul
export const useAcceptDelivery = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ demandId, data }: { demandId: string; data: AcceptDeliveryDto }) =>
      demandApi.acceptDelivery(demandId, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['demands'] });
      queryClient.invalidateQueries({ queryKey: ['stock'] });
      message.success('Sevkiyat başarıyla teslim alındı');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Sevkiyat kabul edilirken hata oluştu');
    },
  });
};

// Red fotoğrafı upload
export const useUploadRejectionPhoto = () => {
  return useMutation({
    mutationFn: ({ demandId, itemId, photo }: { demandId: string; itemId: string; photo: File }) =>
      demandApi.uploadRejectionPhoto(demandId, itemId, photo),
    onSuccess: () => {
      message.success('Fotoğraf yüklendi');
    },
    onError: (error: any) => {
      message.error(error.response?.data?.message || 'Fotoğraf yüklenirken hata oluştu');
    },
  });
};
