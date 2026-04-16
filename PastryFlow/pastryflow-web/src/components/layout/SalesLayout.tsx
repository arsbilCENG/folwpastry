import React, { useState } from 'react';
import { Layout, Menu, Button, Grid, Typography, theme } from 'antd';
import { 
  HomeOutlined, 
  DatabaseOutlined, 
  PlusCircleOutlined, 
  UnorderedListOutlined, 
  CheckCircleOutlined, 
  WarningOutlined, 
  CalculatorOutlined, 
  BarChartOutlined,
  LogoutOutlined,
  MenuOutlined
} from '@ant-design/icons';
import { Outlet, useNavigate, useLocation } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';

const { Header, Sider, Content } = Layout;
const { useBreakpoint } = Grid;
const { Title, Text } = Typography;

const SalesLayout: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  const [drawerVisible, setDrawerVisible] = useState(false);
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const screens = useBreakpoint();
  
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const isMobile = !screens.md;

  const menuItems = [
    { key: '/sales/dashboard', icon: <HomeOutlined />, label: 'Dashboard' },
    { key: '/sales/stock', icon: <DatabaseOutlined />, label: 'Mevcut Stok' },
    { key: '/sales/demands/create', icon: <PlusCircleOutlined />, label: 'Talep Oluştur' },
    { key: '/sales/demands', icon: <UnorderedListOutlined />, label: 'Taleplerim' },
    { key: '/sales/demands/receive', icon: <CheckCircleOutlined />, label: 'Teslimat Kabul' },
    { key: '/sales/wastes/add', icon: <WarningOutlined />, label: 'Zayiat Ekle' },
    { key: '/sales/day-closing', icon: <CalculatorOutlined />, label: 'Gün Sonu Sayım' },
    { key: '/sales/reports/daily', icon: <BarChartOutlined />, label: 'Günlük Rapor' },
  ];

  const handleMenuClick = ({ key }: { key: string }) => {
    navigate(key);
    if (isMobile) setDrawerVisible(false);
  };

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const sidebarContent = (
    <>
      <div style={{ height: 64, display: 'flex', alignItems: 'center', justifyContent: 'center', padding: '16px', color: 'white' }}>
        <Title level={4} style={{ color: 'white', margin: 0 }}>
          {isMobile || !collapsed ? 'PastryFlow' : 'PF'}
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
        <Sider collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
          {sidebarContent}
        </Sider>
      ) : null}

      <Layout>
        <Header style={{ padding: '0 16px', background: colorBgContainer, display: 'flex', alignItems: 'center' }}>
          {isMobile && (
            <Button
              type="text"
              icon={<MenuOutlined />}
              onClick={() => setDrawerVisible(true)}
              style={{ fontSize: '16px', width: 64, height: 64 }}
            />
          )}
          <Title level={4} style={{ margin: 0, marginLeft: isMobile ? 16 : 0 }}>
            {menuItems.find(i => i.key === location.pathname)?.label || 'PastryFlow'}
          </Title>
        </Header>
        <Content style={{ margin: '16px', padding: 24, minHeight: 280, background: colorBgContainer, borderRadius: borderRadiusLG }}>
          <Outlet />
        </Content>
      </Layout>
    </Layout>
  );
};

export default SalesLayout;
