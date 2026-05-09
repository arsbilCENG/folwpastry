import React from 'react';
import { Card, Tabs, Typography } from 'antd';
import { 
  BarChartOutlined, 
  DeleteOutlined, 
  ShoppingCartOutlined,
  ShoppingOutlined,
  TransactionOutlined,
  SwapOutlined
} from '@ant-design/icons';
import SalesDailySalesTab from './reports/SalesDailySalesTab';
import SalesWasteSummaryTab from './reports/SalesWasteSummaryTab';
import SalesDemandSummaryTab from './reports/SalesDemandSummaryTab';
import PurchasesTab from '../../components/reports/PurchasesTab';
import CashTransactionsTab from '../../components/reports/CashTransactionsTab';
import useAuth from '../../hooks/useAuth';

const { Title } = Typography;

const SalesReports: React.FC = () => {
  const { user } = useAuth();
  const isAdmin = user?.role === 'Admin';

  const items = [
    {
      key: 'daily-sales',
      label: (
        <span>
          <ShoppingCartOutlined />
          Günlük Satış
        </span>
      ),
      children: <SalesDailySalesTab />,
    },
    {
      key: 'purchases',
      label: (
        <span>
          <ShoppingOutlined />
          Satın Alımlar
        </span>
      ),
      children: <PurchasesTab branchId={user?.branchId || undefined} showBranchFilter={false} />,
    },
    ...(isAdmin ? [{
      key: 'cash-transactions',
      label: (
        <span>
          <TransactionOutlined />
          Kasa Hareketleri
        </span>
      ),
      children: <CashTransactionsTab branchId={user?.branchId || undefined} showBranchFilter={false} />,
    }] : []),
    {
      key: 'waste-summary',
      label: (
        <span>
          <DeleteOutlined />
          Zayiat Raporu
        </span>
      ),
      children: <SalesWasteSummaryTab />,
    },
    {
      key: 'demand-summary',
      label: (
        <span>
          <BarChartOutlined />
          Talep Raporu
        </span>
      ),
      children: <SalesDemandSummaryTab />,
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ marginBottom: '24px' }}>
        <Title level={2}>📋 Raporlar — {user?.branchName || 'Şube'}</Title>
      </div>
      <Card>
        <Tabs defaultActiveKey="daily-sales" items={items} />
      </Card>
    </div>
  );
};

export default SalesReports;
