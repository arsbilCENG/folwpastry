import React, { useState } from 'react';
import { Layout, Menu, Button, Grid, Typography, theme } from 'antd';
import {
  HomeOutlined,
  InboxOutlined,
  LogoutOutlined,
  MenuOutlined,
} from '@ant-design/icons';
import { Outlet, useNavigate, useLocation } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';

const { Sider, Header, Content } = Layout;
const { useBreakpoint } = Grid;
const { Title, Text } = Typography;

const ProductionLayout: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const screens = useBreakpoint();
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const isMobile = !screens.md;

  const menuItems = [
    { key: '/production/dashboard', icon: <HomeOutlined />, label: 'Dashboard' },
    { key: '/production/demands', icon: <InboxOutlined />, label: 'Gelen Talepler' },
  ];

  const handleMenuClick = ({ key }: { key: string }) => {
    navigate(key);
  };

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const sidebarContent = (
    <>
      <div style={{ height: 64, display: 'flex', alignItems: 'center', justifyContent: 'center', padding: '16px' }}>
        <Title level={4} style={{ color: 'white', margin: 0 }}>
          {isMobile || !collapsed ? '🏭 Üretim' : '🏭'}
        </Title>
      </div>
      <div style={{ padding: '0 16px', marginBottom: 16, textAlign: 'center' }}>
        {(!collapsed || isMobile) && <Text style={{ color: '#aaa', fontSize: 12 }}>{user?.branchName}</Text>}
      </div>
      <Menu
        theme="dark"
        mode="inline"
        selectedKeys={[location.pathname]}
        items={menuItems}
        onClick={handleMenuClick}
      />
      <div style={{ position: 'absolute', bottom: 16, width: '100%', padding: '0 16px' }}>
        <Button
          type="primary"
          danger
          icon={<LogoutOutlined />}
          onClick={handleLogout}
          block={!collapsed || isMobile}
        >
          {(!collapsed || isMobile) && 'Çıkış Yap'}
        </Button>
      </div>
    </>
  );

  return (
    <Layout style={{ minHeight: '100vh' }}>
      {!isMobile ? (
        <Sider collapsible collapsed={collapsed} onCollapse={setCollapsed}>
          {sidebarContent}
        </Sider>
      ) : null}

      <Layout>
        <Header style={{ padding: '0 16px', background: colorBgContainer, display: 'flex', alignItems: 'center' }}>
          {isMobile && (
            <Button
              type="text"
              icon={<MenuOutlined />}
              style={{ fontSize: '16px', width: 64, height: 64 }}
            />
          )}
          <Title level={4} style={{ margin: 0, marginLeft: isMobile ? 16 : 0 }}>
            {menuItems.find(i => i.key === location.pathname)?.label || 'Üretim Paneli'}
          </Title>
        </Header>
        <Content
          style={{
            margin: '16px',
            padding: 24,
            minHeight: 280,
            background: colorBgContainer,
            borderRadius: borderRadiusLG,
          }}
        >
          <Outlet />
        </Content>
      </Layout>
    </Layout>
  );
};

export default ProductionLayout;
