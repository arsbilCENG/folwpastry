import { useQuery } from '@tanstack/react-query';
import axiosClient from '../api/axiosClient';

interface BranchDto {
  id: string;
  name: string;
}

const getBranches = async (): Promise<BranchDto[]> => {
  const response = await axiosClient.get('/branches');
  return response.data;
};

export const useBranches = () => {
  return useQuery({
    queryKey: ['branches'],
    queryFn: getBranches,
  });
};
