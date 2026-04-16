import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { ConfigProvider } from 'antd';
import trTR from 'antd/locale/tr_TR';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/layout/ProtectedRoute';
import SalesLayout from './components/layout/SalesLayout';
import AppLayout from './components/layout/AppLayout';

import LoginPage from './pages/LoginPage';
import SalesDashboard from './pages/sales/SalesDashboard';
import CurrentStockPage from './pages/sales/CurrentStockPage';
import CreateDemandPage from './pages/sales/CreateDemandPage';
import DemandListPage from './pages/sales/DemandListPage';
import ReceiveDeliveryPage from './pages/sales/ReceiveDeliveryPage';
import AddWastePage from './pages/sales/AddWastePage';
import DayClosingPage from './pages/sales/DayClosingPage';
import DailySummaryPage from './pages/sales/DailySummaryPage';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
    },
  },
});

const App: React.FC = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <ConfigProvider locale={trTR} theme={{
        token: {
          colorPrimary: '#1677ff', // Blue theme 
        },
      }}>
        <AuthProvider>
          <BrowserRouter>
            <Routes>
              <Route path="/login" element={<LoginPage />} />
              
              <Route element={<ProtectedRoute requireRole={['Sales', 'Admin']} />}>
                <Route path="/sales" element={<SalesLayout />}>
                  <Route index element={<Navigate to="dashboard" replace />} />
                  <Route path="dashboard" element={<SalesDashboard />} />
                  <Route path="stock" element={<CurrentStockPage />} />
                  <Route path="demands/create" element={<CreateDemandPage />} />
                  <Route path="demands" element={<DemandListPage />} />
                  <Route path="demands/receive" element={<ReceiveDeliveryPage />} />
                  <Route path="wastes/add" element={<AddWastePage />} />
                  <Route path="day-closing" element={<DayClosingPage />} />
                  <Route path="reports/daily" element={<DailySummaryPage />} />
                </Route>
              </Route>

              {/* Admin Routes Mapping for Sprint 1 */}
              <Route path="/admin" element={<Navigate to="/sales/dashboard" replace />} />
              <Route path="/admin/*" element={<Navigate to="/sales/dashboard" replace />} />

              {/* Default & Catch-all Routes */}
              <Route path="/" element={<Navigate to="/login" replace />} />
              <Route path="*" element={<Navigate to="/login" replace />} />
            </Routes>
          </BrowserRouter>
        </AuthProvider>
      </ConfigProvider>
    </QueryClientProvider>
  );
};

export default App;
