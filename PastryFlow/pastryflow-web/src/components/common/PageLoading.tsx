import React from 'react';
import { Spin } from 'antd';

const PageLoading: React.FC = () => {
  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', width: '100vw' }}>
      <Spin size="large" tip="Yükleniyor..." />
    </div>
  );
};

export default PageLoading;
