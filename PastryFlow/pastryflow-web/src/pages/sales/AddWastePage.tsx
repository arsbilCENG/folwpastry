import React, { useEffect, useState, useCallback } from 'react';
import {
  Form,
  Button,
  InputNumber,
  Input,
  Upload,
  message,
  Typography,
  Select,
  Row,
  Col,
  Table,
  Tag,
  Divider,
  Empty,
  Image,
} from 'antd';
import { UploadOutlined, ReloadOutlined } from '@ant-design/icons';
import { productApi } from '../../api/productApi';
import { wasteApi } from '../../api/wasteApi';
import { stockApi } from '../../api/stockApi';
import { Product } from '../../types/product';
import { Waste } from '../../types/waste';
import useAuth from '../../hooks/useAuth';
import dayjs from 'dayjs';

const { Title, Text } = Typography;
const { Option } = Select;
const { TextArea } = Input;

const AddWastePage: React.FC = () => {
  const { user } = useAuth();
  const [products, setProducts] = useState<Product[]>([]);
  const [stockMap, setStockMap] = useState<Record<string, number>>({});
  const [loading, setLoading] = useState(false);
  const [wastes, setWastes] = useState<Waste[]>([]);
  const [wastesLoading, setWastesLoading] = useState(false);
  const [form] = Form.useForm();
  const today = dayjs().format('YYYY-MM-DD');

  const fetchStock = useCallback(async () => {
    if (!user?.branchId) return;
    try {
      const res = await stockApi.getCurrentStock(user.branchId, today);
      if (res.success && res.data) {
        const map: Record<string, number> = {};
        res.data.forEach((s: any) => {
          map[s.productId] = s.currentStock ?? 0;
        });
        setStockMap(map);
      }
    } catch {
      // silent
    }
  }, [user, today]);

  const fetchProducts = useCallback(async () => {
    try {
      const res = await productApi.getProducts();
      if (res.success && res.data) {
        setProducts(res.data);
      }
    } catch {
      message.error('Ürünler yüklenemedi.');
    }
  }, []);

  const fetchWastes = useCallback(async () => {
    if (!user?.branchId) return;
    setWastesLoading(true);
    try {
      const res = await wasteApi.getWastes(user.branchId, today);
      if (res.success && res.data) {
        setWastes(res.data);
      }
    } catch {
      // silent
    } finally {
      setWastesLoading(false);
    }
  }, [user, today]);

  useEffect(() => {
    fetchProducts();
    fetchStock();
    fetchWastes();
  }, [fetchProducts, fetchStock, fetchWastes]);

  const onFinish = async (values: any) => {
    if (!user?.branchId) return;

    const selectedProductStock = stockMap[values.productId] ?? 0;
    if (values.quantity > selectedProductStock) {
      message.error(`Girilen miktar mevcut stoktan (${selectedProductStock}) fazla olamaz.`);
      return;
    }

    const formData = new FormData();
    formData.append('branchId', user.branchId);
    formData.append('productId', values.productId);
    formData.append('quantity', values.quantity.toString());
    formData.append('date', today);
    if (values.notes) formData.append('notes', values.notes);

    if (values.photo && values.photo.fileList && values.photo.fileList.length > 0) {
      formData.append('photo', values.photo.fileList[0].originFileObj);
    }

    setLoading(true);
    try {
      const res = await wasteApi.createWaste(formData);
      if (res.success) {
        message.success('Zayiat kaydı başarıyla eklendi.');
        form.resetFields();
        // Refresh stock and waste list
        fetchStock();
        fetchWastes();
      } else {
        message.error(res.message || 'Zayiat eklenemedi.');
      }
    } catch (err: any) {
      message.error(err.message || 'Zayiat eklenirken hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  // Products with stock > 0
  const availableProducts = products.filter(p => (stockMap[p.id] ?? 0) > 0);

  const wasteColumns = [
    {
      title: 'Ürün',
      dataIndex: 'productName',
      key: 'productName',
      render: (val: string) => <Text strong>{val}</Text>,
    },
    {
      title: 'Miktar',
      key: 'qty',
      render: (_: any, record: Waste) => `${record.quantity} ${record.unitName || ''}`,
    },
    {
      title: 'Sebep',
      dataIndex: 'notes',
      key: 'notes',
      render: (val: string) => val || <Text type="secondary">—</Text>,
    },
    {
      title: 'Fotoğraf',
      dataIndex: 'photoPath',
      key: 'photoPath',
      render: (val: string) =>
        val ? (
          <Image
            src={`${import.meta.env.VITE_API_URL || 'http://localhost:5000'}${val}`}
            width={48}
            height={48}
            style={{ objectFit: 'cover', borderRadius: 4 }}
          />
        ) : (
          <Text type="secondary">—</Text>
        ),
    },
    {
      title: 'Saat',
      dataIndex: 'createdAt',
      key: 'createdAt',
      render: (val: string) => dayjs(val).format('HH:mm'),
    },
  ];

  return (
    <div style={{ maxWidth: 700 }}>
      <Title level={2}>Zayiat / Fire Kaydı</Title>
      <div style={{ marginBottom: 24 }}>
        <Text>
          Yere düşen, bozulan, iade gelen veya kullanılamaz durumdaki ürünleri buradan fotoğraflı
          olarak sisteme giriniz. Bu işlem stoktan anında düşer.
        </Text>
      </div>

      <Form form={form} layout="vertical" onFinish={onFinish}>
        <Form.Item
          name="productId"
          label="Ürün"
          rules={[{ required: true, message: 'Lütfen ürün seçiniz' }]}
        >
          <Select
            showSearch
            placeholder="Ürün Ara (sadece stoklu ürünler)"
            optionFilterProp="label"
            options={availableProducts.map(p => ({
              value: p.id,
              label: `${p.name} (${p.unitName}) — Stok: ${stockMap[p.id] ?? 0}`,
            }))}
          />
        </Form.Item>

        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              name="quantity"
              label="Miktar"
              rules={[
                { required: true, message: 'Lütfen miktar giriniz' },
                { type: 'number', min: 0.01, message: 'Miktar 0\'dan büyük olmalı' },
              ]}
            >
              <InputNumber min={0.01} style={{ width: '100%' }} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item name="photo" label="Fotoğraf (İsteğe Bağlı)">
              <Upload maxCount={1} beforeUpload={() => false} accept="image/*" listType="picture">
                <Button icon={<UploadOutlined />}>Fotoğraf Yükle</Button>
              </Upload>
            </Form.Item>
          </Col>
        </Row>

        <Form.Item name="notes" label="Zayiat Sebebi (Açıklama)">
          <TextArea rows={3} placeholder="Örn: Tezgahtan düştü, müşteri iade etti..." />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" size="large" loading={loading} danger block>
            ZAYİAT KAYDET
          </Button>
        </Form.Item>
      </Form>

      <Divider />

      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 12 }}>
        <Title level={4} style={{ margin: 0 }}>
          Bugünün Zayiatları ({dayjs().format('DD/MM/YYYY')})
        </Title>
        <Button icon={<ReloadOutlined />} onClick={fetchWastes} loading={wastesLoading} size="small">
          Yenile
        </Button>
      </div>

      {wastes.length === 0 ? (
        <Empty description="Bugün henüz zayiat kaydı yok" />
      ) : (
        <Table
          dataSource={wastes}
          columns={wasteColumns}
          rowKey="id"
          size="small"
          pagination={false}
          loading={wastesLoading}
        />
      )}
    </div>
  );
};

export default AddWastePage;
