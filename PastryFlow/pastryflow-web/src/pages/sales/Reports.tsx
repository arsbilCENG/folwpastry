import React from 'react';
import { Card, Tabs, Typography } from 'antd';
import { BarChartOutlined, DeleteOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import SalesDailySalesTab from './reports/SalesDailySalesTab';
import SalesWasteSummaryTab from './reports/SalesWasteSummaryTab';
import SalesDemandSummaryTab from './reports/SalesDemandSummaryTab';
import useAuth from '../../hooks/useAuth';

const { Title } = Typography;

const SalesReports: React.FC = () => {
  const { user } = useAuth();

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
