import React from 'react';
import { Result } from 'antd';
import { BarChartOutlined } from '@ant-design/icons';

const ReportsPlaceholder: React.FC = () => (
  <Result
    icon={<BarChartOutlined style={{ color: '#1677ff' }} />}
    title="Raporlar"
    subTitle="Gelişmiş raporlama ve analiz modülü bir sonraki aşamada aktif olacaktır."
  />
);

export default ReportsPlaceholder;
