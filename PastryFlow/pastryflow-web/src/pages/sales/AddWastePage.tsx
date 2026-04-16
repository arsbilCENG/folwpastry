import React, { useEffect, useState } from 'react';
import { Form, Button, InputNumber, Input, Upload, message, Typography, Select, Row, Col } from 'antd';
import { UploadOutlined } from '@ant-design/icons';
import { productApi } from '../../api/productApi';
import { wasteApi } from '../../api/wasteApi';
import { Product } from '../../types/product';
import useAuth from '../../hooks/useAuth';
import { useNavigate } from 'react-router-dom';

const { Title, Text } = Typography;
const { Option } = Select;
const { TextArea } = Input;

const AddWastePage: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  
  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const res = await productApi.getProducts();
        if (res.success && res.data) {
          setProducts(res.data);
        }
      } catch (err) {
        message.error('Ürünler yüklenemedi.');
      }
    };
    fetchProducts();
  }, []);

  const onFinish = async (values: any) => {
    if (!user?.branchId) return;

    const formData = new FormData();
    formData.append('branchId', user.branchId);
    formData.append('productId', values.productId);
    formData.append('quantity', values.quantity.toString());
    formData.append('date', new Date().toISOString().split('T')[0]);
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
        navigate('/sales/dashboard');
      } else {
        message.error(res.message);
      }
    } catch (err: any) {
      message.error(err.message || 'Zayiat eklenirken hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ maxWidth: 600 }}>
      <Title level={2}>Zayiat / Fire Kaydı</Title>
      <div style={{ marginBottom: 24 }}>
        <Text>Yere düşen, bozulan, iade gelen veya kullanılamaz durumdaki ürünleri buradan fotoğraflı olarak sisteme giriniz. Bu işlem stoktan anında düşer.</Text>
      </div>

      <Form form={form} layout="vertical" onFinish={onFinish}>
        <Form.Item name="productId" label="Ürün" rules={[{ required: true, message: 'Lütfen ürün seçiniz' }]}>
          <Select 
            showSearch 
            placeholder="Ürün Ara" 
            optionFilterProp="children"
            filterOption={(input, option) => (option?.children as unknown as string).toLowerCase().includes(input.toLowerCase())}
          >
            {products.map(p => (
              <Option key={p.id} value={p.id}>{p.name} ({p.unitName})</Option>
            ))}
          </Select>
        </Form.Item>

        <Row gutter={16}>
          <Col span={12}>
            <Form.Item name="quantity" label="Miktar" rules={[{ required: true, message: 'Lütfen miktar giriniz' }]}>
              <InputNumber min={0.01} style={{ width: '100%' }} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item name="photo" label="Fotoğraf (Zorunlu Değil)">
              <Upload maxCount={1} beforeUpload={() => false}>
                <Button icon={<UploadOutlined />}>Fotoğraf Çek / Yükle</Button>
              </Upload>
            </Form.Item>
          </Col>
        </Row>

        <Form.Item name="notes" label="Zayiat Sebebi (Açıklama)">
          <TextArea rows={4} placeholder="Örn: Tezgahtan düştü, müşteri iade etti..." />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" size="large" loading={loading} danger block>
            ZAYİAT KAYDET
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default AddWastePage;
