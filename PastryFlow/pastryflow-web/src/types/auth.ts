export enum UserRole {
  Admin = 0,
  Production = 1,
  Sales = 2,
  Driver = 3,
  Employee = 4
}

export interface CurrentUser {
  id: string;
  fullName: string;
  email: string;
  role: string;
  branchId: string | null;
  branchName: string | null;
}

export interface LoginResponse {
  token: string;
  refreshToken: string;
  user: CurrentUser;
}
