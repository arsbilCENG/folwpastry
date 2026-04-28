import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { ConfigProvider, App as AntdApp } from 'antd';
import trTR from 'antd/locale/tr_TR';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { AuthProvider } from './context/AuthContext';
import { NotificationProvider } from './context/NotificationContext';
import ProtectedRoute from './components/layout/ProtectedRoute';
import SalesLayout from './components/layout/SalesLayout';
import ProductionLayout from './components/layout/ProductionLayout';
import AdminLayout from './components/layout/AdminLayout';

// Shared pages
import LoginPage from './pages/LoginPage';
import NotificationList from './pages/notifications/NotificationList';

// Sales pages
import SalesDashboard from './pages/sales/SalesDashboard';
import CurrentStockPage from './pages/sales/CurrentStockPage';
import CreateDemandPage from './pages/sales/CreateDemandPage';
import DemandListPage from './pages/sales/DemandListPage';
import ReceiveDeliveryPage from './pages/sales/ReceiveDeliveryPage';
import AddWastePage from './pages/sales/AddWastePage';
import DayClosingPage from './pages/sales/DayClosingPage';
import DailySummaryPage from './pages/sales/DailySummaryPage';
import SalesReports from './pages/sales/Reports';

// Production pages
import ProductionDashboard from './pages/production/ProductionDashboard';
import IncomingDemandsPage from './pages/production/IncomingDemandsPage';
import DemandReviewPage from './pages/production/DemandReviewPage';
import ShipDemandPage from './pages/production/ShipDemandPage';
import DeliveryReturnsPage from './pages/production/DeliveryReturnsPage';
import ProductionReports from './pages/production/Reports';

// Admin pages
import AdminDashboard from './pages/admin/Dashboard';
import AdminUsers from './pages/admin/Users';
import AdminCategories from './pages/admin/Categories';
import AdminProducts from './pages/admin/Products';
import AdminBranches from './pages/admin/Branches';
import AdminCakeOptions from './pages/admin/CakeOptions';
import AdminReports from './pages/admin/Reports';
import AdminDayCorrection from './pages/admin/DayCorrection';

// Cake Orders
import SalesCakeOrders from './pages/sales/CakeOrders';
import ProductionCakeOrders from './pages/production/CakeOrders';

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
          colorPrimary: '#1677ff',
          borderRadius: 8,
        },
      }}>
        <AntdApp>
          <AuthProvider>
            <NotificationProvider>
              <BrowserRouter>
                <Routes>
                  <Route path="/login" element={<LoginPage />} />

                  {/* Admin Routes */}
                  <Route element={<ProtectedRoute requireRole={['Admin']} />}>
                    <Route path="/admin" element={<AdminLayout />}>
                      <Route index element={<Navigate to="dashboard" replace />} />
                      <Route path="dashboard" element={<AdminDashboard />} />
                      <Route path="users" element={<AdminUsers />} />
                      <Route path="categories" element={<AdminCategories />} />
                      <Route path="products" element={<AdminProducts />} />
                      <Route path="branches" element={<AdminBranches />} />
                      <Route path="cake-options" element={<AdminCakeOptions />} />
                      <Route path="reports" element={<AdminReports />} />
                      <Route path="day-correction" element={<AdminDayCorrection />} />
                      <Route path="notifications" element={<NotificationList />} />
                    </Route>
                  </Route>

                  {/* Sales Routes */}
                  <Route element={<ProtectedRoute requireRole={['Sales', 'Admin']} />}>
                    <Route path="/sales" element={<SalesLayout />}>
                      <Route index element={<Navigate to="dashboard" replace />} />
                      <Route path="dashboard" element={<SalesDashboard />} />
                      <Route path="stock" element={<CurrentStockPage />} />
                      <Route path="demands/create" element={<CreateDemandPage />} />
                      <Route path="demands" element={<DemandListPage />} />
                      <Route path="receive-delivery" element={<ReceiveDeliveryPage />} />
                      <Route path="wastes/add" element={<AddWastePage />} />
                      <Route path="day-closing" element={<DayClosingPage />} />
                      <Route path="cake-orders" element={<SalesCakeOrders />} />
                      <Route path="reports/daily" element={<DailySummaryPage />} />
                      <Route path="reports" element={<SalesReports />} />
                      <Route path="notifications" element={<NotificationList />} />
                      {/* Backward compatibility route if needed */}
                      <Route path="demands/receive" element={<ReceiveDeliveryPage />} />
                    </Route>
                  </Route>

                  {/* Production Routes */}
                  <Route element={<ProtectedRoute requireRole={['Production', 'Admin']} />}>
                    <Route path="/production" element={<ProductionLayout />}>
                      <Route index element={<Navigate to="dashboard" replace />} />
                      <Route path="dashboard" element={<ProductionDashboard />} />
                      <Route path="demands" element={<IncomingDemandsPage />} />
                      <Route path="demands/:id" element={<DemandReviewPage />} />
                      <Route path="ship-demand/:id" element={<ShipDemandPage />} />
                      <Route path="delivery-returns" element={<DeliveryReturnsPage />} />
                      <Route path="cake-orders" element={<ProductionCakeOrders />} />
                      <Route path="reports" element={<ProductionReports />} />
                      <Route path="notifications" element={<NotificationList />} />
                    </Route>
                  </Route>

                  {/* Default & Catch-all */}
                  <Route path="/" element={<Navigate to="/login" replace />} />
                  <Route path="*" element={<Navigate to="/login" replace />} />
                </Routes>
              </BrowserRouter>
            </NotificationProvider>
          </AuthProvider>
        </AntdApp>
      </ConfigProvider>
    </QueryClientProvider>
  );
};

export default App;
