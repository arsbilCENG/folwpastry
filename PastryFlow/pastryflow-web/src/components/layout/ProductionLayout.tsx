import React, { useState } from 'react';
import { Layout, Menu, Button, Grid, Typography, theme, Avatar, Space, Dropdown, Drawer, type MenuProps } from 'antd';
import {
  HomeOutlined,
  InboxOutlined,
  LogoutOutlined,
  MenuOutlined,
  UserOutlined
} from '@ant-design/icons';
import { Outlet, useNavigate, useLocation } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';

const { Sider, Header, Content } = Layout;
const { useBreakpoint } = Grid;
const { Title, Text } = Typography;

const ProductionLayout: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  const [drawerVisible, setDrawerVisible] = useState(false);
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const screens = useBreakpoint();
  
  const {
    token: { colorBgContainer, borderRadiusLG, colorPrimary },
  } = theme.useToken();

  const isMobile = !screens.md;

  const menuItems = [
    { key: '/production/dashboard', icon: <HomeOutlined />, label: 'Dashboard' },
    { key: '/production/demands', icon: <InboxOutlined />, label: 'Gelen Talepler' },
  ];

  const handleMenuClick = ({ key }: { key: string }) => {
    navigate(key);
    if (isMobile) setDrawerVisible(false);
  };

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const profileMenuItems: MenuProps['items'] = [
    {
      key: 'user-info',
      label: (
        <div style={{ padding: '4px 8px' }}>
          <Text strong style={{ display: 'block' }}>{user?.fullName}</Text>
          <Text type="secondary" style={{ fontSize: 12 }}>{user?.role} - {user?.branchName}</Text>
        </div>
      ),
      disabled: true,
    },
    { type: 'divider' },
    {
      key: 'logout',
      icon: <LogoutOutlined />,
      label: 'Çıkış Yap',
      danger: true,
      onClick: handleLogout
    }
  ];

  const sidebarContent = (
    <div style={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
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
        style={{ flex: 1 }}
      />
      <div style={{ padding: '16px', borderTop: '1px solid rgba(255,255,255,0.1)' }}>
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
    </div>
  );

  return (
    <Layout style={{ minHeight: '100vh' }}>
      {!isMobile ? (
        <Sider collapsible collapsed={collapsed} onCollapse={setCollapsed}>
          {sidebarContent}
        </Sider>
      ) : (
        <Drawer
          placement="left"
          onClose={() => setDrawerVisible(false)}
          open={drawerVisible}
          styles={{ body: { padding: 0, backgroundColor: '#001529' } }}
          width={250}
          closable={false}
        >
          {sidebarContent}
        </Drawer>
      )}

      <Layout>
        <Header style={{ 
          padding: '0 24px', 
          background: colorBgContainer, 
          display: 'flex', 
          alignItems: 'center', 
          justifyContent: 'space-between',
          boxShadow: '0 1px 4px rgba(0,21,41,.08)',
          zIndex: 1
        }}>
          <Space>
            {isMobile && (
              <Button
                type="text"
                icon={<MenuOutlined />}
                onClick={() => setDrawerVisible(true)}
                style={{ fontSize: '16px', width: 40, height: 40 }}
              />
            )}
            <Title level={4} style={{ margin: 0 }}>
              {menuItems.find(i => i.key === location.pathname)?.label || 'Üretim Paneli'}
            </Title>
          </Space>

          <Space size="middle">
            {!isMobile && (
              <div style={{ textAlign: 'right' }}>
                <Text strong style={{ display: 'block', lineHeight: '1.2' }}>{user?.fullName}</Text>
                <Text type="secondary" style={{ fontSize: 12 }}>{user?.branchName}</Text>
              </div>
            )}
            <Dropdown menu={{ items: profileMenuItems }} placement="bottomRight" arrow>
              <Avatar 
                icon={<UserOutlined />} 
                style={{ backgroundColor: colorPrimary, cursor: 'pointer' }} 
              />
            </Dropdown>
          </Space>
        </Header>
        <Content
          style={{
            margin: isMobile ? '8px' : '24px',
            padding: 24,
            minHeight: 280,
            background: colorBgContainer,
            borderRadius: borderRadiusLG,
          }}
        >
          <div style={{ minHeight: '100%' }}>
            <Outlet />
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default ProductionLayout;
