import React from 'react';
import { Card, Tabs, Typography } from 'antd';
import { 
  BarChartOutlined, 
  ShoppingOutlined, 
  DeleteOutlined, 
  SwapOutlined,
  ShoppingCartOutlined,
  TransactionOutlined
} from '@ant-design/icons';
import DailySalesTab from './reports/DailySalesTab';
import WasteSummaryTab from './reports/WasteSummaryTab';
import DemandSummaryTab from './reports/DemandSummaryTab';
import BranchComparisonTab from './reports/BranchComparisonTab';
import PurchasesTab from '../../components/reports/PurchasesTab';
import CashTransactionsTab from '../../components/reports/CashTransactionsTab';

const { Title } = Typography;

const Reports: React.FC = () => {
  const items = [
    {
      key: 'daily-sales',
      label: (
        <span>
          <ShoppingCartOutlined />
          Günlük Satış
        </span>
      ),
      children: <DailySalesTab />,
    },
    {
      key: 'purchases',
      label: (
        <span>
          <ShoppingOutlined />
          Satın Alımlar
        </span>
      ),
      children: <PurchasesTab showBranchFilter={true} />,
    },
    {
      key: 'cash-transactions',
      label: (
        <span>
          <TransactionOutlined />
          Kasa Hareketleri
        </span>
      ),
      children: <CashTransactionsTab showBranchFilter={true} />,
    },
    {
      key: 'waste-summary',
      label: (
        <span>
          <DeleteOutlined />
          Zayiat Raporu
        </span>
      ),
      children: <WasteSummaryTab />,
    },
    {
      key: 'demand-summary',
      label: (
        <span>
          <SwapOutlined />
          Talep Raporu
        </span>
      ),
      children: <DemandSummaryTab />,
    },
    {
      key: 'branch-comparison',
      label: (
        <span>
          <BarChartOutlined />
          Şube Karşılaştırma
        </span>
      ),
      children: <BranchComparisonTab />,
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <Title level={2}>📋 Raporlar</Title>
      <Card bodyStyle={{ padding: 0 }}>
        <Tabs
          defaultActiveKey="daily-sales"
          items={items}
          size="large"
          tabBarStyle={{ padding: '0 24px', backgroundColor: '#fafafa', marginBottom: 0 }}
          style={{ minHeight: '600px' }}
        />
      </Card>
    </div>
  );
};

export default Reports;
