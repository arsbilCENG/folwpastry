import React from 'react';
import { Card, Tabs, Typography } from 'antd';
import { BarChartOutlined, DeleteOutlined } from '@ant-design/icons';
import ProductionDemandSummaryTab from './reports/ProductionDemandSummaryTab';
import ProductionWasteSummaryTab from './reports/ProductionWasteSummaryTab';
import useAuth from '../../hooks/useAuth';

const { Title } = Typography;

const ProductionReports: React.FC = () => {
  const { user } = useAuth();

  const items = [
    {
      key: 'demand-summary',
      label: (
        <span>
          <BarChartOutlined />
          Gelen Talepler
        </span>
      ),
      children: <ProductionDemandSummaryTab />,
    },
    {
      key: 'waste-summary',
      label: (
        <span>
          <DeleteOutlined />
          Zayiat Raporu
        </span>
      ),
      children: <ProductionWasteSummaryTab />,
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ marginBottom: '24px' }}>
        <Title level={2}>📋 Raporlar — {user?.branchName || 'Üretim'}</Title>
      </div>
      <Card>
        <Tabs defaultActiveKey="demand-summary" items={items} />
      </Card>
    </div>
  );
};

export default ProductionReports;
