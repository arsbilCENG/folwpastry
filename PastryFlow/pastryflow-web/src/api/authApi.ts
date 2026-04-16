import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { CurrentUser, LoginResponse } from '../types/auth';

export const authApi = {
  login: async (credentials: any): Promise<ApiResponse<LoginResponse>> => {
    return axiosClient.post('/auth/login', credentials);
  },
  getCurrentUser: async (): Promise<ApiResponse<CurrentUser>> => {
    return axiosClient.get('/auth/me');
  }
};
