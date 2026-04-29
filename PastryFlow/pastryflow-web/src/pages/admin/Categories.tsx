import React, { useState } from 'react';
import { 
  Table, Card, Button, Space, Modal, Form, 
  Input, InputNumber, Popconfirm, Badge, Typography, Tooltip
} from 'antd';
import { 
  AppstoreAddOutlined, 
  EditOutlined, 
  DeleteOutlined, 
  OrderedListOutlined,
  TagOutlined
} from '@ant-design/icons';
import { 
  useAdminCategories, 
  useCreateCategory, 
  useUpdateCategory, 
  useDeleteCategory 
} from '../../hooks/useAdmin';
import type { CategoryListDto, CreateCategoryDto, UpdateCategoryDto } from '../../types/admin';

const { Title } = Typography;

const Categories: React.FC = () => {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState<CategoryListDto | null>(null);
  const [form] = Form.useForm();
  
  const { data: categories, isLoading } = useAdminCategories();
  const createMutation = useCreateCategory();
  const updateMutation = useUpdateCategory();
  const deleteMutation = useDeleteCategory();

  const handleAdd = () => {
    setSelectedCategory(null);
    form.resetFields();
    setIsModalVisible(true);
  };

  const handleEdit = (category: CategoryListDto) => {
    setSelectedCategory(category);
    form.setFieldsValue({
      name: category.name,
      sortOrder: category.sortOrder
    });
    setIsModalVisible(true);
  };

  const onFinish = async (values: any) => {
    if (selectedCategory) {
      await updateMutation.mutateAsync({
        id: selectedCategory.id,
        data: values as UpdateCategoryDto
      });
    } else {
      await createMutation.mutateAsync(values as CreateCategoryDto);
    }
    setIsModalVisible(false);
  };

  const columns = [
    {
      title: 'Sıralama',
      dataIndex: 'sortOrder',
      key: 'sortOrder',
      width: 100,
      render: (val: number) => (
        <Space>
          <OrderedListOutlined style={{ color: '#8c8c8c' }} />
          <Typography.Text strong>{val}</Typography.Text>
        </Space>
      ),
      sorter: (a: CategoryListDto, b: CategoryListDto) => a.sortOrder - b.sortOrder,
      defaultSortOrder: 'ascend' as const,
    },
    {
      title: 'Kategori Adı',
      dataIndex: 'name',
      key: 'name',
      render: (text: string) => (
        <Space>
          <TagOutlined style={{ color: '#1677ff' }} />
          <Typography.Text strong>{text}</Typography.Text>
        </Space>
      ),
    },
    {
      title: 'Ürün Sayısı',
      dataIndex: 'productCount',
      key: 'productCount',
      render: (count: number) => (
        <Badge count={count} showZero color={count > 0 ? '#1677ff' : '#d9d9d9'}>
          <div style={{ width: 40 }} />
        </Badge>
      ),
    },
    {
      title: 'Oluşturma',
      dataIndex: 'createdAt',
      key: 'createdAt',
      render: (date: string) => new Date(date).toLocaleDateString('tr-TR'),
    },
    {
      title: 'İşlemler',
      key: 'actions',
      render: (_: any, record: CategoryListDto) => (
        <Space>
          <Tooltip title="Düzenle">
            <Button size="small" icon={<EditOutlined />} onClick={() => handleEdit(record)} />
          </Tooltip>
          <Popconfirm
            title="Kategoriyi Sil"
            description="Bu kategoriyi silmek istediğinizden emin misiniz? Altındaki ürünler varsa silme işlemi başarısız olacaktır."
            onConfirm={() => deleteMutation.mutate(record.id)}
            okText="Evet"
            cancelText="Hayır"
            okButtonProps={{ danger: true, loading: deleteMutation.isPending }}
            disabled={record.productCount > 0}
          >
             <Tooltip title={record.productCount > 0 ? "Ürün içeren kategori silinemez" : "Sil"}>
                <Button size="small" danger icon={<DeleteOutlined />} disabled={record.productCount > 0} />
             </Tooltip>
          </Popconfirm>
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
          <Title level={4} style={{ margin: 0 }}>📂 Kategori Yönetimi</Title>
          <Button type="primary" icon={<AppstoreAddOutlined />} onClick={handleAdd}>
            Yeni Kategori
          </Button>
        </div>
      } bordered={false}>
        <Table 
          columns={columns} 
          dataSource={categories} 
          rowKey="id"
          loading={isLoading}
          pagination={false}
          locale={{ emptyText: 'Kategori bulunamadı' }}
          scroll={{ x: 'max-content' }}
        />
      </Card>

      <Modal
        title={selectedCategory ? "Kategori Düzenle" : "Yeni Kategori"}
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
        destroyOnClose
      >
        <Form
          form={form}
          layout="vertical"
          onFinish={onFinish}
          initialValues={{ sortOrder: 0 }}
        >
          <Form.Item name="name" label="Kategori Adı" rules={[{ required: true, message: 'Lütfen kategori adını giriniz!' }]}>
            <Input placeholder="Örn: KEK, MAYALILAR, KURABİYE" />
          </Form.Item>
          <Form.Item name="sortOrder" label="Görüntüleme Sırası" rules={[{ required: true, message: 'Lütfen sıralama giriniz!' }]}>
            <InputNumber min={0} style={{ width: '100%' }} placeholder="Menüdeki konumu (0, 1, 2...)" />
          </Form.Item>
          <Form.Item style={{ marginBottom: 0, textAlign: 'right' }}>
            <Space>
              <Button onClick={() => setIsModalVisible(false)}>İptal</Button>
              <Button type="primary" htmlType="submit" loading={createMutation.isPending || updateMutation.isPending}>
                {selectedCategory ? "Güncelle" : "Oluştur"}
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default Categories;
