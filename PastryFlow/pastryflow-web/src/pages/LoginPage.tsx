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
    case 'Admin':
      return '/admin/dashboard';
    case 'Production':
      return '/production/dashboard';
    case 'Sales':
      return '/sales/dashboard';
    case 'Driver':
      return '/sales/dashboard'; // Placeholder until driver pages are ready
    default:
      return '/';
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
        // Small delay to ensure state update
        setTimeout(() => navigate(route, { replace: true }), 100);
      } else {
        message.error(res.message || 'Giriş başarısız');
      }
    } catch (err: any) {
      message.error(err.response?.data?.message || 'Giriş yapılırken bir hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Content style={{ 
        display: 'flex', 
        justifyContent: 'center', 
        alignItems: 'center', 
        background: 'linear-gradient(135deg, #1677ff 0%, #722ed1 100%)' 
      }}>
        <Card style={{ 
          width: 400, 
          boxShadow: '0 8px 24px rgba(0,0,0,0.15)', 
          borderRadius: 16,
          padding: '12px'
        }}>
          <div style={{ textAlign: 'center', marginBottom: 32 }}>
            <div style={{ fontSize: 48, marginBottom: 8 }}>🧁</div>
            <Title level={2} style={{ margin: 0 }}>PastryFlow</Title>
            <Typography.Text type="secondary">Zayıat ve Stok Yönetim Sistemi</Typography.Text>
          </div>

          <Form 
            name="login" 
            onFinish={onFinish} 
            layout="vertical" 
            size="large"
            requiredMark={false}
          >
            <Form.Item 
              name="email" 
              label="E-posta"
              rules={[
                { required: true, message: 'Lütfen e-posta adresinizi giriniz!' },
                { type: 'email', message: 'Geçerli bir e-posta adresi giriniz!' }
              ]}
            >
              <Input prefix={<UserOutlined style={{ color: '#bfbfbf' }} />} placeholder="ornek@pastryflow.com" />
            </Form.Item>

            <Form.Item 
              name="password" 
              label="Şifre"
              rules={[{ required: true, message: 'Lütfen şifrenizi giriniz!' }]}
            >
              <Input.Password prefix={<LockOutlined style={{ color: '#bfbfbf' }} />} placeholder="••••••••" />
            </Form.Item>

            <Form.Item style={{ marginTop: 8 }}>
              <Button 
                type="primary" 
                htmlType="submit" 
                loading={loading} 
                block 
                style={{ 
                  height: 48, 
                  borderRadius: 12, 
                  fontSize: 16,
                  fontWeight: 'bold',
                  boxShadow: '0 4px 12px rgba(22, 119, 255, 0.3)'
                }}
              >
                Giriş Yap
              </Button>
            </Form.Item>
          </Form>

          <div style={{ textAlign: 'center', marginTop: 16 }}>
            <Typography.Text type="secondary" style={{ fontSize: 12 }}>
              © 2024 PastryFlow - Tüm Hakları Saklıdır
            </Typography.Text>
          </div>
        </Card>
      </Content>
    </Layout>
  );
};

export default LoginPage;
