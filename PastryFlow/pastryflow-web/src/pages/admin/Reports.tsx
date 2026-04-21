import React from 'react';
import { Card, Tabs, Typography } from 'antd';
import { 
  BarChartOutlined, 
  ShoppingOutlined, 
  DeleteOutlined, 
  SwapOutlined 
} from '@ant-design/icons';
import DailySalesTab from './reports/DailySalesTab';
import WasteSummaryTab from './reports/WasteSummaryTab';
import DemandSummaryTab from './reports/DemandSummaryTab';
import BranchComparisonTab from './reports/BranchComparisonTab';

const { Title } = Typography;

const Reports: React.FC = () => {
  const items = [
    {
      key: '1',
      label: (
        <span>
          <ShoppingOutlined />
          Günlük Satış
        </span>
      ),
      children: <DailySalesTab />,
    },
    {
      key: '2',
      label: (
        <span>
          <DeleteOutlined />
          Zayiat Raporu
        </span>
      ),
      children: <WasteSummaryTab />,
    },
    {
      key: '3',
      label: (
        <span>
          <SwapOutlined />
          Talep Raporu
        </span>
      ),
      children: <DemandSummaryTab />,
    },
    {
      key: '4',
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
          defaultActiveKey="1"
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
