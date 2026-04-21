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
  0: 'Admin',
  1: 'Production',
  2: 'Sales',
  3: 'Driver',
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
    setIsLoading(false);
  }, []);

  useEffect(() => {
    const initAuth = async () => {
      // If we already have a user and token (e.g. from fresh login), skip redundant verification
      if (token && !user) {
        setIsLoading(true);
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
  }, [token, user, logout]);

  const login = (data: LoginResponse) => {
    localStorage.setItem('token', data.token);
    setToken(data.token);
    const normalizedUser = normalizeUser(data.user);
    console.log('Login successful. Role:', normalizedUser.role);
    setUser(normalizedUser);
    setIsLoading(false); // Optimization: Finish loading as soon as login data is applied
  };

  return (
    <AuthContext.Provider value={{ user, token, isAuthenticated: !!token && !!user, isLoading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
