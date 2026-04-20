import React, { useState, useEffect } from 'react';
import { Form, Input, Button, Card, Typography, message, Layout } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import useAuth from '../hooks/useAuth';
import { authApi } from '../api/authApi';

const { Title } = Typography;
const { Content } = Layout;

const getRoleRoute = (role: string): string => {
  switch (role) {
    case 'Production':
      return '/production/dashboard';
    case 'Admin':
    case 'Sales':
    case 'Driver':
    default:
      return '/sales/dashboard';
  }
};

const LoginPage: React.FC = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const { login, isAuthenticated, user } = useAuth();

  useEffect(() => {
    if (isAuthenticated && user) {
      navigate(getRoleRoute(user.role), { replace: true });
    }
  }, [isAuthenticated, user, navigate]);

  const onFinish = async (values: any) => {
    setLoading(true);
    try {
      const res = await authApi.login({ email: values.email, password: values.password });
      if (res.success && res.data) {
        message.success('Giriş başarılı');
        login(res.data);
        const route = getRoleRoute(res.data.user.role);
        setTimeout(() => navigate(route, { replace: true }), 100);
      } else {
        message.error(res.message || 'Giriş başarısız');
      }
    } catch (err: any) {
      message.error(err.message || 'Bir hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Content style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)' }}>
        <Card style={{ width: 400, boxShadow: '0 4px 12px rgba(0,0,0,0.1)', borderRadius: 12 }}>
          <div style={{ textAlign: 'center', marginBottom: 24 }}>
            <Title level={2}>🧁 PastryFlow</Title>
            <div style={{ color: '#888' }}>Şube Yönetim Sistemi</div>
          </div>
          <Form name="login" onFinish={onFinish} layout="vertical" size="large">
            <Form.Item name="email" rules={[{ required: true, message: 'Lütfen e-posta adresinizi giriniz!' }, { type: 'email', message: 'Geçerli bir e-posta adresi giriniz!' }]}>
              <Input prefix={<UserOutlined />} placeholder="E-posta" />
            </Form.Item>
            <Form.Item name="password" rules={[{ required: true, message: 'Lütfen şifrenizi giriniz!' }]}>
              <Input.Password prefix={<LockOutlined />} placeholder="Şifre" />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit" loading={loading} block style={{ height: 40, borderRadius: 6 }}>
                Giriş Yap
              </Button>
            </Form.Item>
          </Form>
        </Card>
      </Content>
    </Layout>
  );
};

export default LoginPage;
