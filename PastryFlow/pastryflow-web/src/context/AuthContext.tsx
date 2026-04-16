import React, { createContext, useState, useEffect, ReactNode, useCallback } from 'react';
import { CurrentUser, LoginResponse } from '../types/auth';
import { authApi } from '../api/authApi';

interface AuthContextType {
  user: CurrentUser | null;
  token: string | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (data: LoginResponse) => void;
  logout: () => void;
}

const ROLE_MAP: Record<string | number, string> = {
  1: 'Admin',
  2: 'Production',
  3: 'Sales',
  4: 'Driver',
  'Admin': 'Admin',
  'Production': 'Production',
  'Sales': 'Sales',
  'Driver': 'Driver'
};

const normalizeUser = (user: any): CurrentUser => {
  return {
    ...user,
    role: ROLE_MAP[user.role] || user.role
  };
};

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<CurrentUser | null>(null);
  const [token, setToken] = useState<string | null>(localStorage.getItem('token'));
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const logout = useCallback(() => {
    localStorage.removeItem('token');
    setToken(null);
    setUser(null);
  }, []);

  useEffect(() => {
    const initAuth = async () => {
      if (token) {
        try {
          const res = await authApi.getCurrentUser();
          if (res.success && res.data) {
            setUser(normalizeUser(res.data));
          } else {
            logout();
          }
        } catch (error) {
          console.error('Auth initialization error', error);
          logout();
        }
      }
      setIsLoading(false);
    };

    initAuth();
  }, []);

  const login = (data: LoginResponse) => {
    localStorage.setItem('token', data.token);
    setToken(data.token);
    const normalizedUser = normalizeUser(data.user);
    console.log('Login successful. Role:', normalizedUser.role, 'Branch:', normalizedUser.branchName);
    setUser(normalizedUser);
  };

  return (
    <AuthContext.Provider value={{ user, token, isAuthenticated: !!token && !!user, isLoading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
