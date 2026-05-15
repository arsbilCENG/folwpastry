import React, { useState, useEffect } from 'react';
import {
  Form,
  Input,
  InputNumber,
  Button,
  Select,
  Card,
  Typography,
  message,
  Space,
  Divider,
} from 'antd';
import { DeleteOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';
import { productApi } from '../../api/productApi';
import { wasteApi } from '../../api/wasteApi';
import { Product } from '../../types/product';
import PhotoUpload from '../../components/common/PhotoUpload';

import { stockApi } from '../../api/stockApi';
import { CurrentStock } from '../../types/stock';
import dayjs from 'dayjs';

const { Title, Text } = Typography;
const { Option } = Select;

const AddWastePage: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [form] = Form.useForm();
  const [stocks, setStocks] = useState<CurrentStock[]>([]);
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [photoFile, setPhotoFile] = useState<File | null>(null);

  const getBusinessDate = () => {
    const now = new Date();
    if (now.getHours() < 3) {
      now.setDate(now.getDate() - 1);
    }
    return now.toLocaleDateString('en-CA');
  };
  const today = getBusinessDate();

  useEffect(() => {
    const fetchStock = async () => {
      if (!user?.branchId) return;
      setLoading(true);
      try {
        const res = await stockApi.getCurrentStock(user.branchId, today);
        if (res.success && res.data) {
          // Only show items with stock > 0
          setStocks(res.data.filter(s => s.currentStock > 0));
        }
      } catch (err) {
        message.error('Stok bilgileri yüklenemedi.');
      } finally {
        setLoading(false);
      }
    };

    fetchStock();
  }, [user?.branchId, today]);

  const onFinish = async (values: any) => {
    if (!user?.branchId || !user?.id) return;

    setSaving(true);
    try {
      const formData = new FormData();
      formData.append('branchId', user.branchId);
      formData.append('recordedByUserId', user.id);
      formData.append('productId', values.productId);
      formData.append('quantity', values.quantity.toString());
      formData.append('date', today);
      formData.append('notes', values.reason);
      
      if (photoFile) {
        formData.append('photo', photoFile);
      }

      const res = await wasteApi.createWaste(formData);
      if (res.success) {
        message.success('Zayiat kaydı başarıyla oluşturuldu.');
        navigate('/sales/reports');
      } else {
        message.error(res.message || 'Hata oluştu.');
      }
    } catch (err) {
      message.error('Bağlantı hatası.');
    } finally {
      setSaving(false);
    }
  };

  return (
    <div style={{ padding: '24px', maxWidth: 800, margin: '0 auto' }}>
      <Card bordered={false} style={{ borderRadius: 12, boxShadow: '0 4px 12px rgba(0,0,0,0.05)' }}>
        <Title level={2}>Yeni Zayiat Kaydı</Title>
        <Text type="secondary">Hatalı, bozuk veya son kullanma tarihi geçmiş ürünleri buradan kaydedebilirsiniz.</Text>
        
        <Divider />

        <Form
          form={form}
          layout="vertical"
          onFinish={onFinish}
          initialValues={{ quantity: 1 }}
        >
          <Form.Item
            label="Ürün"
            name="productId"
            rules={[{ required: true, message: 'Lütfen ürün seçiniz' }]}
          >
            <Select
              showSearch
              placeholder="Ürün seçin veya arayın"
              optionFilterProp="children"
              loading={loading}
              onChange={() => form.validateFields(['quantity'])}
            >
              {stocks.map(s => (
                <Option key={s.productId} value={s.productId}>
                  {s.productName} (Mevcut: {s.currentStock} {s.unitName})
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item
            label="Miktar"
            name="quantity"
            dependencies={['productId']}
            rules={[
              { required: true, message: 'Lütfen miktar giriniz' },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  const productId = getFieldValue('productId');
                  const selectedStock = stocks.find(s => s.productId === productId);
                  if (selectedStock && value > selectedStock.currentStock) {
                    return Promise.reject(new Error(`Zayiat miktarı mevcut stoktan (${selectedStock.currentStock}) fazla olamaz.`));
                  }
                  return Promise.resolve();
                },
              }),
            ]}
          >
            <InputNumber 
              min={0.1} 
              step={0.1} 
              style={{ width: '100%' }} 
              placeholder="Zayi olan miktar" 
              onFocus={(e) => e.target.select()}
              inputMode="numeric"
              keyboard={false}
            />
          </Form.Item>

          <Form.Item
            label="Zayiat Nedeni"
            name="reason"
            rules={[{ required: true, message: 'Lütfen bir neden belirtiniz' }]}
          >
            <Input.TextArea rows={3} placeholder="Ürün neden zayi oldu? (Örn: Tarihi geçti, düşürüldü, bozuk çıktı)" />
          </Form.Item>

          <Form.Item label="Fotoğraf" name="photo">
            <PhotoUpload
              value={photoFile}
              onChange={setPhotoFile}
              placeholder="Zayiat fotoğrafı yükleyin (isteğe bağlı)"
              required={false}
            />
          </Form.Item>

          <Divider />

          <Form.Item style={{ marginBottom: 0 }}>
            <Space style={{ width: '100%', justifyContent: 'flex-end' }}>
              <Button onClick={() => navigate(-1)}>Vazgeç</Button>
              <Button type="primary" htmlType="submit" icon={<SaveOutlined />} loading={saving}>
                Kaydet
              </Button>
            </Space>
          </Form.Item>
        </Form>
      </Card>
    </div>
  );
};

export default AddWastePage;
