import React, { useState } from 'react';
import { 
  Typography, Card, Tabs, Table, Button, Space, Tag, Modal, 
  Form, Input, InputNumber, Switch, Popconfirm 
} from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { 
  useAdminCakeOptions, 
  useCreateCakeOption, 
  useUpdateCakeOption, 
  useDeleteCakeOption 
} from '../../hooks/useCakeOrders';
import { CakeOptionType, CakeOptionDto } from '../../types/cakeOrder';

const { Title } = Typography;

const CakeOptionsAdmin: React.FC = () => {
  const [activeTab, setActiveTab] = useState<CakeOptionType>('CakeType');
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingOption, setEditingOption] = useState<CakeOptionDto | null>(null);
  
  const [form] = Form.useForm();

  const { data: optionsRes, isLoading } = useAdminCakeOptions(activeTab);
  const createMutation = useCreateCakeOption();
  const updateMutation = useUpdateCakeOption();
  const deleteMutation = useDeleteCakeOption();

  const options = optionsRes?.data || [];

  const handleOpenModal = (record?: CakeOptionDto) => {
    if (record) {
      setEditingOption(record);
      form.setFieldsValue(record);
    } else {
      setEditingOption(null);
      form.resetFields();
      form.setFieldsValue({
        sortOrder: options.length > 0 ? Math.max(...options.map(o => o.sortOrder)) + 1 : 1,
        isActive: true
      });
    }
    setIsModalVisible(true);
  };

  const handleCloseModal = () => {
    setIsModalVisible(false);
    form.resetFields();
    setEditingOption(null);
  };

  const handleSubmit = async (values: any) => {
    try {
      if (editingOption) {
        await updateMutation.mutateAsync({
          id: editingOption.id,
          data: {
            name: values.name,
            sortOrder: values.sortOrder,
            isActive: values.isActive
          }
        });
      } else {
        await createMutation.mutateAsync({
          name: values.name,
          optionType: activeTab,
          sortOrder: values.sortOrder
        });
      }
      handleCloseModal();
    } catch (e) {
      // handled in hook
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await deleteMutation.mutateAsync(id);
    } catch (e) {
      // handled in hook
    }
  };

  const columns = [
    {
      title: 'Sıra',
      dataIndex: 'sortOrder',
      key: 'sortOrder',
      width: 80,
    },
    {
      title: 'Seçenek Adı',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Durum',
      dataIndex: 'isActive',
      key: 'isActive',
      render: (isActive: boolean) => (
        <Tag color={isActive ? 'success' : 'default'}>
          {isActive ? 'Aktif' : 'Pasif'}
        </Tag>
      )
    },
    {
      title: 'İşlemler',
      key: 'actions',
      width: 150,
      render: (_: any, record: CakeOptionDto) => (
        <Space>
          <Button 
            type="text" 
            icon={<EditOutlined />} 
            onClick={() => handleOpenModal(record)} 
          />
          <Popconfirm
            title="Silmek istediğinize emin misiniz?"
            onConfirm={() => handleDelete(record.id)}
            okText="Evet"
            cancelText="Hayır"
          >
            <Button type="text" danger icon={<DeleteOutlined />} loading={deleteMutation.isPending && deleteMutation.variables === record.id} />
          </Popconfirm>
        </Space>
      )
    }
  ];

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}>
        <Title level={2} style={{ margin: 0 }}>🎂 Pasta Seçenekleri</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={() => handleOpenModal()} size="large">
          Yeni Seçenek
        </Button>
      </div>

      <Card>
        <Tabs 
          activeKey={activeTab} 
          onChange={(key) => setActiveTab(key as CakeOptionType)}
          items={[
            { key: 'CakeType', label: 'Kek Türleri' },
            { key: 'InnerCream', label: 'İç Kremalar' },
            { key: 'OuterCream', label: 'Dış Kremalar' },
          ]}
        />
        
        <Table 
          columns={columns} 
          dataSource={options} 
          rowKey="id" 
          loading={isLoading}
          pagination={false}
          size="middle"
        />
      </Card>

      <Modal
        title={editingOption ? 'Seçenek Düzenle' : 'Yeni Seçenek Ekle'}
        open={isModalVisible}
        onCancel={handleCloseModal}
        footer={null}
        destroyOnClose
      >
        <Form form={form} layout="vertical" onFinish={handleSubmit}>
          <Form.Item 
            name="name" 
            label="Seçenek Adı" 
            rules={[{ required: true, message: 'Lütfen seçenek adı giriniz' }]}
          >
            <Input placeholder="Örn: Kakaolu" />
          </Form.Item>
          
          <Form.Item 
            name="sortOrder" 
            label="Sıralama" 
            rules={[{ required: true, message: 'Lütfen sıralama giriniz' }]}
          >
            <InputNumber min={1} style={{ width: '100%' }} />
          </Form.Item>
          
          {editingOption && (
            <Form.Item 
              name="isActive" 
              label="Durum" 
              valuePropName="checked"
            >
              <Switch checkedChildren="Aktif" unCheckedChildren="Pasif" />
            </Form.Item>
          )}
          
          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 24 }}>
            <Button onClick={handleCloseModal}>İptal</Button>
            <Button type="primary" htmlType="submit" loading={createMutation.isPending || updateMutation.isPending}>
              Kaydet
            </Button>
          </div>
        </Form>
      </Modal>
    </div>
  );
};

export default CakeOptionsAdmin;
