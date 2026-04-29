import React, { useState } from 'react';
import { 
  Table, Card, Button, Input, Select, Tag, Space, 
  Modal, Form, Popconfirm, Badge, Tooltip, 
  Typography, InputNumber, Radio, Switch, Row, Col
} from 'antd';
import { 
  SearchOutlined, 
  PlusOutlined, 
  EditOutlined, 
  DeleteOutlined, 
  ShoppingOutlined,
  AppstoreOutlined,
  ShopOutlined,
  OrderedListOutlined
} from '@ant-design/icons';
import { 
  useAdminProducts, 
  useAdminCategories, 
  useAdminBranches,
  useCreateProduct,
  useUpdateProduct,
  useDeleteProduct
} from '../../hooks/useAdmin';
import type { 
  ProductListDto, 
  CreateProductDto, 
  UpdateProductDto, 
  ProductFilterParams 
} from '../../types/admin';
import { formatCurrency } from '../../utils/formatters';

const { Title } = Typography;

const Products: React.FC = () => {
  const [filters, setFilters] = useState<ProductFilterParams>({ pageNumber: 1, pageSize: 12 });
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<ProductListDto | null>(null);
  const [form] = Form.useForm();
  
  const { data, isLoading } = useAdminProducts(filters);
  const { data: categories } = useAdminCategories();
  const { data: branches } = useAdminBranches();
  
  const createMutation = useCreateProduct();
  const updateMutation = useUpdateProduct();
  const deleteMutation = useDeleteProduct();

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

  const handleAdd = () => {
    setSelectedProduct(null);
    form.resetFields();
    form.setFieldsValue({ unitType: 'Adet', isRawMaterial: false, sortOrder: 0 });
    setIsModalVisible(true);
  };

  const handleEdit = (product: ProductListDto) => {
    setSelectedProduct(product);
    form.setFieldsValue({
      name: product.name,
      categoryId: product.categoryId,
      productionBranchId: product.productionBranchId,
      unitType: product.unitType,
      unitPrice: product.unitPrice,
      isRawMaterial: product.isRawMaterial,
      sortOrder: product.sortOrder
    });
    setIsModalVisible(true);
  };

  const onFinish = async (values: any) => {
    if (selectedProduct) {
      await updateMutation.mutateAsync({
        id: selectedProduct.id,
        data: values as UpdateProductDto
      });
    } else {
      await createMutation.mutateAsync(values as CreateProductDto);
    }
    setIsModalVisible(false);
  };

  const columns = [
    {
      title: 'Ürün Adı',
      dataIndex: 'name',
      key: 'name',
      render: (text: string, record: ProductListDto) => (
        <Space>
          <ShoppingOutlined style={{ color: record.isRawMaterial ? '#722ed1' : '#1677ff' }} />
          <Typography.Text strong>{text}</Typography.Text>
          {record.isRawMaterial && <Tag color="purple" style={{ fontSize: 10 }}>Hammadde</Tag>}
        </Space>
      ),
      sorter: (a: ProductListDto, b: ProductListDto) => a.name.localeCompare(b.name),
    },
    {
      title: 'Kategori',
      dataIndex: 'categoryName',
      key: 'categoryName',
      render: (name: string) => <Tag icon={<AppstoreOutlined />}>{name}</Tag>,
    },
    {
      title: 'Üretim Şubesi',
      dataIndex: 'productionBranchName',
      key: 'productionBranchName',
      render: (name: string, record: ProductListDto) => (
        name ? <Space><ShopOutlined style={{ color: '#8c8c8c' }} />{name}</Space> : (record.isRawMaterial ? <Typography.Text type="secondary">N/A</Typography.Text> : <Badge status="processing" text="Paylaşımlı" />)
      ),
    },
    {
      title: 'Birim',
      dataIndex: 'unitType',
      key: 'unitType',
      width: 100,
    },
    {
      title: 'Birim Fiyat',
      dataIndex: 'unitPrice',
      key: 'unitPrice',
      render: (price: number | null) => price ? formatCurrency(price) : <Typography.Text type="secondary">-</Typography.Text>,
    },
    {
      title: 'Sıra',
      dataIndex: 'sortOrder',
      key: 'sortOrder',
      width: 80,
      align: 'center' as const,
    },
    {
      title: 'İşlemler',
      key: 'actions',
      width: 120,
      render: (_: any, record: ProductListDto) => (
        <Space>
          <Tooltip title="Düzenle">
            <Button size="small" icon={<EditOutlined />} onClick={() => handleEdit(record)} />
          </Tooltip>
          <Popconfirm
            title="Ürünü Sil"
            description="Bu ürünü silmek istediğinizden emin misiniz? (Not: Eğer bu üründe aktif stok varsa sistem uyarı verecektir.)"
            onConfirm={() => deleteMutation.mutate(record.id)}
            okText="Evet"
            cancelText="Hayır"
            okButtonProps={{ danger: true, loading: deleteMutation.isPending }}
          >
            <Button size="small" danger icon={<DeleteOutlined />} />
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
          <Title level={4} style={{ margin: 0 }}>🧁 Ürün Yönetimi</Title>
          <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
            Yeni Ürün
          </Button>
        </div>
      } bordered={false}>
        <Space style={{ marginBottom: 16 }} wrap>
          <Input 
            placeholder="Ürün adı ara..." 
            prefix={<SearchOutlined />} 
            style={{ width: 250 }} 
            onChange={(e) => handleSearch(e.target.value)}
            allowClear
          />
          <Select
            placeholder="Kategori Filtrele"
            style={{ width: 150 }}
            allowClear
            onChange={(val) => setFilters(prev => ({ ...prev, categoryId: val, pageNumber: 1 }))}
            options={categories?.map(c => ({ label: c.name, value: c.id }))}
          />
          <Select
            placeholder="Ürt. Şubesi Filtrele"
            style={{ width: 200 }}
            allowClear
            onChange={(val) => setFilters(prev => ({ ...prev, productionBranchId: val, pageNumber: 1 }))}
            options={branches?.filter(b => b.branchType === 'Production').map(b => ({ label: b.name, value: b.id }))}
          />
          <Select
            placeholder="Birim"
            style={{ width: 120 }}
            allowClear
            onChange={(val) => setFilters(prev => ({ ...prev, unitType: val, pageNumber: 1 }))}
            options={[
              { label: 'Adet', value: 'Adet' },
              { label: 'Kg', value: 'Kg' },
            ]}
          />
          <Badge count={data?.totalCount} overflowCount={999} color="#1677ff">
             <Typography.Text type="secondary" style={{ marginLeft: 8 }}>Toplam Ürün</Typography.Text>
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
            showTotal: (total) => `Toplam ${total} ürün`,
          }}
          onChange={handleTableChange}
          scroll={{ x: 'max-content' }}
        />
      </Card>

      <Modal
        title={selectedProduct ? "Ürün Düzenle" : "Yeni Ürün"}
        open={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
        destroyOnClose
        width={600}
      >
        <Form
          form={form}
          layout="vertical"
          onFinish={onFinish}
        >
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item name="name" label="Ürün Adı" rules={[{ required: true, message: 'Lütfen ürün adını giriniz!' }]}>
                <Input prefix={<ShoppingOutlined />} />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item name="categoryId" label="Kategori" rules={[{ required: true, message: 'Lütfen kategori seçiniz!' }]}>
                <Select options={categories?.map(c => ({ label: c.name, value: c.id }))} />
              </Form.Item>
            </Col>
          </Row>

          <Row gutter={16}>
            <Col span={12}>
              <Form.Item name="unitType" label="Birim Tipi" rules={[{ required: true }]}>
                <Radio.Group>
                  <Radio.Button value="Adet">Adet</Radio.Button>
                  <Radio.Button value="Kg">Kg</Radio.Button>
                </Radio.Group>
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item name="unitPrice" label="Birim Fiyat (Opsiyonel)">
                <InputNumber 
                  style={{ width: '100%' }} 
                  min={0} 
                  precision={2} 
                  addonBefore="₺"
                  placeholder="0,00"
                />
              </Form.Item>
            </Col>
          </Row>

          <Form.Item 
            noStyle 
            shouldUpdate={(prev, curr) => prev.isRawMaterial !== curr.isRawMaterial}
          >
            {({ getFieldValue }) => {
              const isRaw = getFieldValue('isRawMaterial');
              return (
                <Form.Item 
                  name="productionBranchId" 
                  label="Üretim Şubesi" 
                  extra={isRaw ? "Hammadde ürünleri üretim şubesi gerektirmez." : "Boş bırakılırsa tüm üretim şubelerinden talep edilebilir."}
                >
                  <Select 
                    disabled={isRaw}
                    allowClear 
                    placeholder={isRaw ? "-" : "Şube Seçin (Opsiyonel)"}
                    options={branches?.filter(b => b.branchType === 'Production').map(b => ({ label: b.name, value: b.id }))} 
                  />
                </Form.Item>
              );
            }}
          </Form.Item>

          <Row gutter={16}>
            <Col span={12}>
              <Form.Item name="isRawMaterial" label="Hammadde Ürünü mü?" valuePropName="checked">
                <Switch />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item name="sortOrder" label="Görüntüleme Sırası" rules={[{ required: true }]}>
                <InputNumber min={0} style={{ width: '100%' }} prefix={<OrderedListOutlined />} />
              </Form.Item>
            </Col>
          </Row>

          <Form.Item style={{ marginBottom: 0, textAlign: 'right', marginTop: 16 }}>
            <Space>
              <Button onClick={() => setIsModalVisible(false)}>İptal</Button>
              <Button type="primary" htmlType="submit" loading={createMutation.isPending || updateMutation.isPending}>
                {selectedProduct ? "Güncelle" : "Oluştur"}
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default Products;
