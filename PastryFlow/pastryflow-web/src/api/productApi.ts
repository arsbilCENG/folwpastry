import axiosClient from './axiosClient';
import { ApiResponse } from '../types/api';
import { Product, CategoryWithProducts } from '../types/product';

export const productApi = {
  getProducts: async (params?: any): Promise<ApiResponse<Product[]>> => {
    return axiosClient.get('/products', { params });
  },
  getCategoriesWithProducts: async (params?: any): Promise<ApiResponse<CategoryWithProducts[]>> => {
    return axiosClient.get('/products/by-category', { params });
  }
};
