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
