import React, { useState } from 'react';
import { 
  Table, Card, Button, Input, Select, Tag, Space, 
  Modal, Form, message, Popconfirm, Badge, Tooltip, Typography
} from 'antd';
import { 
  SearchOutlined, 
  EditOutlined, 
  DeleteOutlined, 
  KeyOutlined,
  UserOutlined,
  MailOutlined
} from '@ant-design/icons';
import dayjs from 'dayjs';
import { 
  useAdminUsers, 
  useUpdateUser, 
  useDeleteUser, 
  useResetPassword,
  useAdminBranches 
} from '../../hooks/useAdmin';
import useAuth from '../../hooks/useAuth';
import type { UserListDto, UpdateUserDto, ResetPasswordDto, UserFilterParams } from '../../types/admin';

const { Title } = Typography;

const Users: React.FC = () => {
  const [filters, setFilters] = useState<UserFilterParams>({ pageNumber: 1, pageSize: 10 });
  const [isEditModalVisible, setIsEditModalVisible] = useState(false);
  const [isPasswordModalVisible, setIsPasswordModalVisible] = useState(false);
  const [selectedUser, setSelectedUser] = useState<UserListDto | null>(null);
  const [form] = Form.useForm();
  const [passwordForm] = Form.useForm();
  
  const { data, isLoading } = useAdminUsers(filters);
  const { data: branches } = useAdminBranches();
  const { user: currentUser } = useAuth();
  
  const updateUserMutation = useUpdateUser();
  const deleteUserMutation = useDeleteUser();
  const resetPasswordMutation = useResetPassword();

  const handleTableChange = (pagination: any) => {
    setFilters(prev => ({
      ...prev,
      pageNumber: pagination.current,
      pageSize: pagination.pageSize
    }));
  };

  const handleSearch = (value: string) => {
    setFilters(prev => ({ ...prev, search: value, pageNumber: 1 }));
  };

  const handleEdit = (user: UserListDto) => {
    setSelectedUser(user);
    form.setFieldsValue({
      fullName: user.fullName,
      email: user.email,
      role: user.role,
      branchId: user.branchId,
      isActive: user.isActive
    });
    setIsEditModalVisible(true);
  };

  const handleResetPasswordClick = (user: UserListDto) => {
    setSelectedUser(user);
    passwordForm.resetFields();
    setIsPasswordModalVisible(true);
  };

  const onEditFinish = async (values: any) => {
    if (!selectedUser) return;
    await updateUserMutation.mutateAsync({
      id: selectedUser.id,
      data: values as UpdateUserDto
    });
    setIsEditModalVisible(false);
  };

  const onResetPasswordFinish = async (values: any) => {
    if (!selectedUser) return;
    await resetPasswordMutation.mutateAsync({
      id: selectedUser.id,
      data: { newPassword: values.newPassword } as ResetPasswordDto
    });
    setIsPasswordModalVisible(false);
  };

  const columns = [
    {
      title: 'Ad Soyad',
      dataIndex: 'fullName',
      key: 'fullName',
      render: (text: string) => (
        <Space>
          <UserOutlined />
          <span style={{ fontWeight: 500 }}>{text}</span>
        </Space>
      ),
    },
    {
      title: 'E-posta',
      dataIndex: 'email',
      key: 'email',
      render: (text: string) => (
        <Space>
          <MailOutlined style={{ color: '#8c8c8c' }} />
          <span>{text}</span>
        </Space>
      ),
    },
    {
      title: 'Rol',
      dataIndex: 'role',
      key: 'role',
      render: (role: string) => {
        let color = 'default';
        if (role === 'Admin') color = 'red';
        else if (role === 'Production') color = 'blue';
        else if (role === 'Sales') color = 'green';
        else if (role === 'Driver') color = 'orange';
        return <Tag color={color}>{role}</Tag>;
      },
    },
    {
      title: 'Şube',
      dataIndex: 'branchName',
      key: 'branchName',
      render: (text: string) => text || <Typography.Text type="secondary">-</Typography.Text>,
    },
    {
      title: 'Durum',
      dataIndex: 'isActive',
      key: 'isActive',
      render: (active: boolean) => (
        <Badge status={active ? 'success' : 'error'} text={active ? 'Aktif' : 'Pasif'} />
      ),
    },
    {
      title: 'Kayıt',
      dataIndex: 'createdAt',
      key: 'createdAt',
      render: (date: string) => dayjs(date).format('DD.MM.YYYY'),
    },
    {
      title: 'İşlemler',
      key: 'actions',
      render: (_: any, record: UserListDto) => (
        <Space>
          <Tooltip title="Düzenle">
            <Button size="small" icon={<EditOutlined />} onClick={() => handleEdit(record)} />
          </Tooltip>
          <Tooltip title="Şifre Sıfırla">
            <Button size="small" icon={<KeyOutlined />} onClick={() => handleResetPasswordClick(record)} />
          </Tooltip>
          {currentUser?.id !== record.id && (
            <Popconfirm
              title="Kullanıcıyı Sil"
              description="Bu kullanıcıyı silmek istediğinizden emin misiniz?"
              onConfirm={() => deleteUserMutation.mutate(record.id)}
              okText="Evet"
              cancelText="Hayır"
              okButtonProps={{ danger: true, loading: deleteUserMutation.isPending }}
            >
              <Button size="small" danger icon={<DeleteOutlined />} />
            </Popconfirm>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <Card title={
        <div style={{ 
          display: 'flex', 
          justifyContent: 'space-between', 
          alignItems: 'center',
          flexWrap: 'wrap',
          gap: 12
        }}>
          <Title level={4} style={{ margin: 0 }}>👥 Kullanıcı Yönetimi</Title>
        </div>
      } bordered={false}>
        <Space style={{ marginBottom: 16 }} wrap>
          <Input 
            placeholder="İsim veya e-posta ara..." 
            prefix={<SearchOutlined />} 
            style={{ width: 250 }} 
            onChange={(e) => handleSearch(e.target.value)}
            allowClear
          />
          <Select
            placeholder="Rol Filtrele"
            style={{ width: 150 }}
            allowClear
            onChange={(val) => setFilters(prev => ({ ...prev, role: val, pageNumber: 1 }))}
            options={[
              { label: 'Admin', value: 'Admin' },
              { label: 'Üretim', value: 'Production' },
              { label: 'Satış', value: 'Sales' },
              { label: 'Şoför', value: 'Driver' },
            ]}
          />
          <Select
            placeholder="Şube Filtrele"
            style={{ width: 200 }}
            allowClear
            onChange={(val) => setFilters(prev => ({ ...prev, branchId: val, pageNumber: 1 }))}
            options={branches?.map(b => ({ label: b.name, value: b.id }))}
          />
          <Badge count={data?.totalCount} overflowCount={999} color="#1677ff">
             <Typography.Text type="secondary" style={{ marginLeft: 8 }}>Toplam Kayıt</Typography.Text>
          </Badge>
        </Space>

        <Table 
          columns={columns} 
          dataSource={data?.items} 
          rowKey="id"
          loading={isLoading}
          pagination={{
            current: data?.pageNumber,
            pageSize: data?.pageSize,
            total: data?.totalCount,
            showSizeChanger: true,
            showTotal: (total) => `Toplam ${total} kullanıcı`,
          }}
          onChange={handleTableChange}
          scroll={{ x: 'max-content' }}
        />
      </Card>

      {/* Edit Modal */}
      <Modal
        title="Kullanıcı Düzenle"
        open={isEditModalVisible}
        onCancel={() => setIsEditModalVisible(false)}
        footer={null}
        destroyOnClose
      >
        <Form
          form={form}
          layout="vertical"
          onFinish={onEditFinish}
          initialValues={{ isActive: true }}
        >
          <Form.Item name="fullName" label="Ad Soyad" rules={[{ required: true, message: 'Lütfen ad soyad giriniz!' }]}>
            <Input prefix={<UserOutlined />} />
          </Form.Item>
          <Form.Item name="email" label="E-posta" rules={[{ required: true, type: 'email', message: 'Geçerli bir e-posta giriniz!' }]}>
            <Input prefix={<MailOutlined />} />
          </Form.Item>
          <Form.Item name="role" label="Rol" rules={[{ required: true, message: 'Lütfen rol seçiniz!' }]}>
            <Select options={[
              { label: 'Admin', value: 'Admin' },
              { label: 'Üretim', value: 'Production' },
              { label: 'Satış', value: 'Sales' },
              { label: 'Şoför', value: 'Driver' },
            ]} />
          </Form.Item>
          <Form.Item 
            noStyle 
            shouldUpdate={(prevValues, currentValues) => prevValues.role !== currentValues.role}
          >
            {({ getFieldValue }) => {
              const role = getFieldValue('role');
              const isStoreSpecific = role === 'Production' || role === 'Sales';
              return (
                <Form.Item 
                  name="branchId" 
                  label="Şube" 
                  rules={[{ required: isStoreSpecific, message: 'Lütfen şube seçiniz!' }]}
                >
                  <Select 
                    disabled={!isStoreSpecific} 
                    allowClear={!isStoreSpecific}
                    options={branches?.map(b => ({ label: b.name, value: b.id }))} 
                  />
                </Form.Item>
              );
            }}
          </Form.Item>
          <Form.Item name="isActive" label="Hesap Durumu" valuePropName="checked">
            <Select options={[{ label: 'Aktif', value: true }, { label: 'Pasif', value: false }]} />
          </Form.Item>
          <Form.Item style={{ marginBottom: 0, textAlign: 'right' }}>
            <Space>
              <Button onClick={() => setIsEditModalVisible(false)}>İptal</Button>
              <Button type="primary" htmlType="submit" loading={updateUserMutation.isPending}>Kaydet</Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>

      {/* Password Reset Modal */}
      <Modal
        title="Şifre Sıfırla"
        open={isPasswordModalVisible}
        onCancel={() => setIsPasswordModalVisible(false)}
        footer={null}
        destroyOnClose
      >
        <Form
          form={passwordForm}
          layout="vertical"
          onFinish={onResetPasswordFinish}
        >
          <Typography.Text type="secondary" style={{ display: 'block', marginBottom: 16 }}>
             Kullanıcı için yeni bir giriş şifresi belirleyin.
          </Typography.Text>
          <Form.Item 
            name="newPassword" 
            label="Yeni Şifre" 
            rules={[
              { required: true, message: 'Lütfen şifre giriniz!' },
              { min: 6, message: 'Şifre en az 6 karakter olmalıdır!' }
            ]}
          >
            <Input.Password prefix={<KeyOutlined />} />
          </Form.Item>
          <Form.Item 
            name="confirmPassword" 
            label="Şifre Tekrar" 
            dependencies={['newPassword']}
            rules={[
              { required: true, message: 'Lütfen şifreyi tekrar giriniz!' },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue('newPassword') === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(new Error('Şifreler eşleşmiyor!'));
                },
              }),
            ]}
          >
            <Input.Password prefix={<KeyOutlined />} />
          </Form.Item>
          <Form.Item style={{ marginBottom: 0, textAlign: 'right' }}>
            <Space>
              <Button onClick={() => setIsPasswordModalVisible(false)}>İptal</Button>
              <Button type="primary" htmlType="submit" loading={resetPasswordMutation.isPending}>Şifreyi Güncelle</Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default Users;
