import React from 'react';
import { Tooltip } from 'antd';
import { WifiOutlined, DisconnectOutlined } from '@ant-design/icons';
import { useNotifications } from '../../context/NotificationContext';

const ConnectionStatus: React.FC = () => {
  const { isConnected } = useNotifications();

  return (
    <Tooltip title={isConnected ? 'Sunucuya bağlı' : 'Sunucu bağlantısı kesildi'}>
      <div style={{ 
        padding: '12px 24px', 
        fontSize: 12, 
        color: isConnected ? '#52c41a' : '#f5222d', 
        display: 'flex', 
        alignItems: 'center', 
        gap: 8,
        borderTop: '1px solid #f0f0f0',
        marginTop: 'auto'
      }}>
        {isConnected ? <WifiOutlined /> : <DisconnectOutlined />}
        <span>{isConnected ? 'Bağlı' : 'Bağlantı Kesildi'}</span>
      </div>
    </Tooltip>
  );
};

export default ConnectionStatus;
