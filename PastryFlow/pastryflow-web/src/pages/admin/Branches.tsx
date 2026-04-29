import React, { useState } from 'react';
import { 
  Table, Card, Button, Tag, Space, 
  Modal, Form, Input, Badge, Typography, Tooltip, Switch
} from 'antd';
import { 
  ShopOutlined, 
  EditOutlined, 
  EnvironmentOutlined,
  TeamOutlined,
  LinkOutlined
} from '@ant-design/icons';
import { useAdminBranches, useUpdateBranch } from '../../hooks/useAdmin';
import type { BranchListDto, UpdateBranchDto } from '../../types/admin';

const { Title } = Typography;

const Branches: React.FC = () => {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedBranch, setSelectedBranch] = useState<BranchListDto | null>(null);
  const [form] = Form.useForm();
  
  const { data: branches, isLoading } = useAdminBranches();
  const updateMutation = useUpdateBranch();

  const handleEdit = (branch: BranchListDto) => {
    setSelectedBranch(branch);
    form.setFieldsValue({
      name: branch.name,
      city: branch.city,
      isActive: branch.isActive
    });
    setIsModalVisible(true);
  };

  const onFinish = async (values: any) => {
    if (!selectedBranch) return;
    await updateMutation.mutateAsync({
      id: selectedBranch.id,
      data: values as UpdateBranchDto
    });
    setIsModalVisible(false);
  };

  const columns = [
    {
      title: 'Şube Adı',
      dataIndex: 'name',
      key: 'name',
      render: (text: string) => (
        <Space>
          <ShopOutlined style={{ color: '#1677ff' }} />
          <Typography.Text strong>{text}</Typography.Text>
        </Space>
      ),
    },
    {
      title: 'Şehir',
      dataIndex: 'city',
      key: 'city',
      render: (text: string) => (
        <Space>
          <EnvironmentOutlined style={{ color: '#8c8c8c' }} />
          <span>{text}</span>
        </Space>
      ),
    },
    {
      title: 'Tip',
      dataIndex: 'branchType',
      key: 'branchType',
      render: (type: string) => (
        <Tag color={type === 'Production' ? 'blue' : 'green'}>
          {type === 'Production' ? 'Üretim' : 'Satış'}
        </Tag>
      ),
    },
    {
      title: 'Eşleşen Şube',
      dataIndex: 'pairedBranchName',
      key: 'pairedBranchName',
      render: (name: string) => (
        name ? (
          <Space>
            <LinkOutlined style={{ color: '#8c8c8c', fontSize: 12 }} />
            <span>{name}</span>
          </Space>
        ) : <Typography.Text type="secondary">-</Typography.Text>
      ),
    },
    {
      title: 'Kullanıcı',
      dataIndex: 'userCount',
      key: 'userCount',
      width: 100,
      render: (count: number) => (
        <Space>
          <TeamOutlined style={{ color: '#8c8c8c' }} />
          <span>{count}</span>
        </Space>
      ),
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
      title: 'İşlemler',
      key: 'actions',
      width: 80,
      render: (_: any, record: BranchListDto) => (
        <Tooltip title="Düzenle">
          <Button size="small" icon={<EditOutlined />} onClick={() => handleEdit(record)} />
        </Tooltip>
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
          <Title level={4} style={{ margin: 0 }}>🏪 Şube Yönetimi</Title>
        </div>
      } bordered={false}>
        <Table 
          columns={columns} 
          dataSource={branches} 
          rowKey="id"
          loading={isLoading}
          pagination={false}
          locale={{ emptyText: 'Şube bulunamadı' }}
          scroll={{ x: 'max-content' }}
        />
        <div style={{ marginTop: 16 }}>
           <Typography.Text type="secondary" style={{ fontSize: 12 }}>* Şube tipi ve eşleşme ayarları şu an için sadece veritabanı üzerinden değiştirilebilir.</Typography.Text>
        </div>
      </Card>

      <Modal
        title="Şube Düzenle"
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
        destroyOnClose
      >
        <Form
          form={form}
          layout="vertical"
          onFinish={onFinish}
        >
          <Form.Item name="name" label="Şube Adı" rules={[{ required: true, message: 'Lütfen şube adını giriniz!' }]}>
            <Input prefix={<ShopOutlined />} />
          </Form.Item>
          <Form.Item name="city" label="Şehir" rules={[{ required: true, message: 'Lütfen şehir giriniz!' }]}>
            <Input prefix={<EnvironmentOutlined />} />
          </Form.Item>
          <Form.Item label="Şube Tipi">
            <Tag color={selectedBranch?.branchType === 'Production' ? 'blue' : 'green'}>
              {selectedBranch?.branchType === 'Production' ? 'Üretim' : 'Satış'}
            </Tag>
            <Typography.Text type="secondary" style={{ marginLeft: 8, fontSize: 12 }}>(Değiştirilemez)</Typography.Text>
          </Form.Item>
          <Form.Item label="Eşleşme">
            <LinkOutlined style={{ marginRight: 8 }} />
            <Typography.Text>{selectedBranch?.pairedBranchName || 'Eşleşme yok'}</Typography.Text>
          </Form.Item>
          <Form.Item name="isActive" label="Durum" valuePropName="checked">
            <Switch checkedChildren="Aktif" unCheckedChildren="Pasif" />
          </Form.Item>
          
          <Form.Item style={{ marginBottom: 0, textAlign: 'right', marginTop: 16 }}>
            <Space>
              <Button onClick={() => setIsModalVisible(false)}>İptal</Button>
              <Button type="primary" htmlType="submit" loading={updateMutation.isPending}>
                Güncelle
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default Branches;
