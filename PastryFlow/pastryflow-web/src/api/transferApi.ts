import axiosClient from './axiosClient';
import type {
  TransferDto,
  CreateTransferRequest,
  CancelTransferRequest,
} from '../types/transfer';
import { TransferStatus } from '../types/transfer';

export const createTransfer = async (
  request: CreateTransferRequest
): Promise<TransferDto> => {
  const response = await axiosClient.post('/transfers', request);
  return response.data;
};

export const receiveTransfer = async (id: string): Promise<TransferDto> => {
  const response = await axiosClient.put(`/transfers/${id}/receive`);
  return response.data;
};

export const cancelTransfer = async (
  id: string,
  request: CancelTransferRequest
): Promise<void> => {
  await axiosClient.put(`/transfers/${id}/cancel`, request);
};

export const getOutgoingTransfers = async (
  status?: TransferStatus
): Promise<TransferDto[]> => {
  const params = status !== undefined ? `?status=${status}` : '';
  const response = await axiosClient.get(`/transfers/outgoing${params}`);
  return response.data;
};

export const getIncomingTransfers = async (
  status?: TransferStatus
): Promise<TransferDto[]> => {
  const params = status !== undefined ? `?status=${status}` : '';
  const response = await axiosClient.get(`/transfers/incoming${params}`);
  return response.data;
};

export const getAllTransfers = async (
  status?: TransferStatus
): Promise<TransferDto[]> => {
  const params = status !== undefined ? `?status=${status}` : '';
  const response = await axiosClient.get(`/transfers${params}`);
  return response.data;
};

export const getTransferById = async (id: string): Promise<TransferDto> => {
  const response = await axiosClient.get(`/transfers/${id}`);
  return response.data;
};
