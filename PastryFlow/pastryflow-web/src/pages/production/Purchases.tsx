import React, { useState } from 'react';
import {
  Table, Button, Modal, Form, Select, Input, InputNumber,
  DatePicker, Radio, Tag, Space, Divider, Typography,
  message, Image
} from 'antd';
import {
  PlusOutlined, DeleteOutlined, EyeOutlined,
  ShoppingCartOutlined
} from '@ant-design/icons';
import dayjs from 'dayjs';
import { usePurchases, useCreatePurchase, useUploadReceiptPhoto, useDeletePurchase } from '../../hooks/usePurchases';
import useAuth from '../../hooks/useAuth';

import PhotoUpload from '../../components/common/PhotoUpload';
import { formatCurrency, formatDate } from '../../utils/formatters';
import {
  PaymentMethod,
  PAYMENT_METHOD_LABELS,
  PAYMENT_METHOD_COLORS,
  type CreatePurchaseItemDto,
  type PurchaseDto
} from '../../types/purchase';

const { Title, Text } = Typography;

const UNIT_OPTIONS = ['Adet', 'Kg', 'Lt', 'Paket', 'Kutu', 'Torba', 'Koli', 'Diğer'];

const ProductionPurchasesPage: React.FC = () => {
  const { user } = useAuth();
  const [page, setPage] = useState(1);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isDetailModalOpen, setIsDetailModalOpen] = useState(false);
  const [selectedPurchase, setSelectedPurchase] = useState<PurchaseDto | null>(null);
  const [form] = Form.useForm();
  const [items, setItems] = useState<CreatePurchaseItemDto[]>([]);
  const [itemForm] = Form.useForm();
  const [receiptPhoto, setReceiptPhoto] = useState<File | null>(null);
  const [submitting, setSubmitting] = useState(false);

  const { data: purchasesData, isLoading } = usePurchases({ page, pageSize: 20 });
  const createPurchase = useCreatePurchase();
  const uploadPhoto = useUploadReceiptPhoto();
  const deletePurchase = useDeletePurchase();

  const handleAddItem = () => {
    itemForm.validateFields().then(values => {
      const newItem: CreatePurchaseItemDto = {
        productId: undefined, // Production şubelerinde stok etkisi yok
        itemName: values.itemName,
        quantity: values.quantity,
        unit: values.unit,
        unitPrice: values.unitPrice,
      };
      setItems(prev => [...prev, newItem]);
      itemForm.resetFields();
    });
  };

  const handleRemoveItem = (index: number) => {
    setItems(prev => prev.filter((_, i) => i !== index));
  };

  const calculateTotal = () =>
    items.reduce((sum, item) => sum + item.quantity * item.unitPrice, 0);

  const handleSubmit = async () => {
    if (items.length === 0) {
      message.error('En az bir kalem ekleyin.');
      return;
    }
    if (!receiptPhoto) {
      message.error('Fiş fotoğrafı zorunludur.');
      return;
    }

    try {
      setSubmitting(true);
      const values = await form.validateFields();
      
      const createDto = {
        purchaseDate: values.purchaseDate.toISOString(),
        paymentMethod: values.paymentMethod,
        notes: values.notes,
        items,
      };

      const purchase = await createPurchase.mutateAsync(createDto);
      await uploadPhoto.mutateAsync({ id: purchase.id, photo: receiptPhoto });

      message.success(`${purchase.purchaseNumber} numaralı satın alım kaydedildi.`);
      setIsModalOpen(false);
      setItems([]);
      setReceiptPhoto(null);
      form.resetFields();
    } catch (error: any) {
      // Error handling managed in useMutation
    } finally {
      setSubmitting(false);
    }
  };

  const columns = [
    {
      title: 'Satın Alım No',
      dataIndex: 'purchaseNumber',
      key: 'purchaseNumber',
      render: (text: string) => <Text strong style={{ color: '#1890ff' }}>{text}</Text>,
    },
    {
      title: 'Tarih',
      dataIndex: 'purchaseDate',
      key: 'purchaseDate',
      render: (date: string) => formatDate(date),
    },
    {
      title: 'Kalem',
      dataIndex: 'items',
      key: 'items',
      render: (items: any[]) => `${items?.length ?? 0} kalem`,
    },
    {
      title: 'Toplam',
      dataIndex: 'totalAmount',
      key: 'totalAmount',
      render: (amount: number) => formatCurrency(amount),
    },
    {
      title: 'Ödeme',
      dataIndex: 'paymentMethod',
      key: 'paymentMethod',
      render: (method: PaymentMethod) => (
        <Tag color={PAYMENT_METHOD_COLORS[method]}>
          {PAYMENT_METHOD_LABELS[method]}
        </Tag>
      ),
    },
    {
      title: 'Fiş',
      dataIndex: 'receiptPhotoUrl',
      key: 'receiptPhotoUrl',
      render: (url: string) =>
        url ? (
          <Button type="link" size="small" onClick={(e) => {
            e.stopPropagation();
            window.open(url, '_blank');
          }}>Görüntüle</Button>
        ) : (
          <Tag color="red">Yüklenmedi</Tag>
        ),
    },
    {
      title: 'İşlemler',
      key: 'actions',
      render: (_: any, record: PurchaseDto) => (
        <Space>
          <Button
            size="small"
            icon={<EyeOutlined />}
            onClick={() => { setSelectedPurchase(record); setIsDetailModalOpen(true); }}
          >
            Detay
          </Button>
          <Button
            size="small"
            danger
            icon={<DeleteOutlined />}
            onClick={() => {
              Modal.confirm({
                title: 'Satın alım silinsin mi?',
                content: 'Bu işlem geri alınamaz.',
                onOk: () => deletePurchase.mutate(record.id),
              });
            }}
          >
            Sil
          </Button>
        </Space>
      ),
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}>
        <Title level={3} style={{ margin: 0 }}>
          <ShoppingCartOutlined /> Satın Alımlar (Gider Kaydı)
        </Title>
        <Button
          type="primary"
          icon={<PlusOutlined />}
          onClick={() => setIsModalOpen(true)}
          size="large"
        >
          Yeni Gider Ekle
        </Button>
      </div>

      <Table
        columns={columns}
        dataSource={purchasesData?.items ?? []}
        rowKey="id"
        loading={isLoading}
        scroll={{ x: 'max-content' }}
        pagination={{
          current: page,
          total: purchasesData?.totalCount ?? 0,
          pageSize: 20,
          onChange: setPage,
        }}
        onRow={(record) => ({
          onClick: () => {
            setSelectedPurchase(record);
            setIsDetailModalOpen(true);
          },
          style: { cursor: 'pointer' }
        })}
      />

      <Modal
        title={<span><ShoppingCartOutlined /> Yeni Satın Alım / Gider</span>}
        open={isModalOpen}
        onCancel={() => { 
          if (!submitting) {
            setIsModalOpen(false); 
            setItems([]); 
            setReceiptPhoto(null); 
            form.resetFields(); 
          }
        }}
        onOk={handleSubmit}
        confirmLoading={submitting}
        okText="Kaydet"
        cancelText="İptal"
        width={800}
        okButtonProps={{ disabled: items.length === 0 || !receiptPhoto }}
      >
        <Form form={form} layout="vertical">
          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '16px' }}>
            <Form.Item name="purchaseDate" label="Tarih" initialValue={dayjs()} rules={[{ required: true }]}>
              <DatePicker style={{ width: '100%' }} format="DD.MM.YYYY" />
            </Form.Item>
            <Form.Item name="paymentMethod" label="Ödeme Yöntemi" initialValue={PaymentMethod.Cash} rules={[{ required: true }]}>
              <Radio.Group buttonStyle="solid">
                <Radio.Button value={PaymentMethod.Cash}>Nakit</Radio.Button>
                <Radio.Button value={PaymentMethod.CreditCard}>Kart</Radio.Button>
              </Radio.Group>
            </Form.Item>
          </div>
          <Form.Item name="notes" label="Not">
            <Input.TextArea rows={2} placeholder="Opsiyonel not..." />
          </Form.Item>
        </Form>

        <Divider orientation={"left" as any}>Gider Kalemleri</Divider>

        <div style={{ background: '#fafafa', padding: 16, borderRadius: 8, marginBottom: 16, border: '1px solid #f0f0f0' }}>
          <Form form={itemForm} layout="vertical">
            <div style={{ display: 'grid', gridTemplateColumns: '2fr 1fr 1fr 1fr', gap: '8px', alignItems: 'start' }}>
              <Form.Item name="itemName" label="Ürün / Gider Adı" rules={[{ required: true, message: 'Giriniz' }]}>
                <Input placeholder="Un, şeker, odun, kırtasiye..." />
              </Form.Item>

              <Form.Item name="quantity" label="Miktar" rules={[{ required: true }]} initialValue={1}>
                <InputNumber min={0.01} style={{ width: '100%' }} />
              </Form.Item>

              <Form.Item name="unit" label="Birim" initialValue="Adet" rules={[{ required: true }]}>
                <Select>
                  {UNIT_OPTIONS.map(u => (
                    <Select.Option key={u} value={u}>{u}</Select.Option>
                  ))}
                </Select>
              </Form.Item>

              <Form.Item name="unitPrice" label="Birim Fiyat" rules={[{ required: true }]} initialValue={0}>
                <InputNumber min={0} style={{ width: '100%' }} precision={2} suffix="₺" />
              </Form.Item>
            </div>

            <Button type="dashed" icon={<PlusOutlined />} onClick={handleAddItem} block style={{ height: '40px' }}>
              Kalem Ekle
            </Button>
          </Form>
        </div>

        {items.length > 0 && (
          <div style={{ marginBottom: 16 }}>
            <Table
              dataSource={items.map((item, idx) => ({ ...item, key: idx }))}
              size="small"
              pagination={false}
              columns={[
                { title: 'Ürün/Gider', dataIndex: 'itemName', key: 'itemName' },
                { 
                  title: 'Miktar', 
                  key: 'quantity',
                  render: (_, r) => `${r.quantity} ${r.unit}`
                },
                {
                  title: 'Toplam',
                  key: 'total',
                  align: 'right',
                  render: (_, r) => formatCurrency(r.quantity * r.unitPrice),
                },
                {
                  title: '',
                  key: 'action',
                  width: 50,
                  render: (_, __, idx) => (
                    <Button
                      size="small"
                      type="text"
                      danger
                      icon={<DeleteOutlined />}
                      onClick={() => handleRemoveItem(idx)}
                    />
                  ),
                },
              ]}
              summary={() => (
                <Table.Summary.Row>
                  <Table.Summary.Cell index={0} colSpan={2}><Text strong>Genel Toplam</Text></Table.Summary.Cell>
                  <Table.Summary.Cell index={2} align="right">
                    <Text strong style={{ fontSize: '16px', color: '#f5222d' }}>
                      {formatCurrency(calculateTotal())}
                    </Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={3} />
                </Table.Summary.Row>
              )}
            />
          </div>
        )}

        <Divider orientation={"left" as any}>Belge / Fiş Fotoğrafı</Divider>
        <PhotoUpload
          value={receiptPhoto}
          onChange={(file: File | null) => setReceiptPhoto(file)}
          required
        />
      </Modal>

      <Modal
        title={`Satın Alım Detayı — ${selectedPurchase?.purchaseNumber}`}
        open={isDetailModalOpen}
        onCancel={() => setIsDetailModalOpen(false)}
        footer={null}
        width={700}
      >
        {selectedPurchase && (
          <Space direction="vertical" style={{ width: '100%' }} size="large">
            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '16px', background: '#f5f5f5', padding: '16px', borderRadius: '8px' }}>
              <div>
                <Text type="secondary">Tarih</Text><br />
                <Text strong>{formatDate(selectedPurchase.purchaseDate)}</Text>
              </div>
              <div>
                <Text type="secondary">Ödeme Yöntemi</Text><br />
                <Tag color={PAYMENT_METHOD_COLORS[selectedPurchase.paymentMethod]}>
                  {PAYMENT_METHOD_LABELS[selectedPurchase.paymentMethod]}
                </Tag>
              </div>
              <div>
                <Text type="secondary">Toplam Tutar</Text><br />
                <Text strong style={{ fontSize: '18px', color: '#f5222d' }}>{formatCurrency(selectedPurchase.totalAmount)}</Text>
              </div>
            </div>

            {selectedPurchase.notes && (
              <div>
                <Text type="secondary">Notlar:</Text>
                <div style={{ padding: '8px', background: '#fffbe6', border: '1px solid #ffe58f', borderRadius: '4px', marginTop: '4px' }}>
                  {selectedPurchase.notes}
                </div>
              </div>
            )}

            <Table
              dataSource={selectedPurchase.items}
              rowKey="id"
              size="small"
              pagination={false}
              columns={[
                { title: 'Ürün/Gider', dataIndex: 'itemName', key: 'itemName' },
                { 
                  title: 'Miktar', 
                  key: 'qty',
                  render: (_, r) => `${r.quantity} ${r.unit}` 
                },
                { 
                    title: 'Birim Fiyat', 
                    dataIndex: 'unitPrice', 
                    key: 'unitPrice', 
                    align: 'right',
                    render: (v) => formatCurrency(v) 
                },
                {
                  title: 'Toplam',
                  dataIndex: 'totalPrice',
                  key: 'totalPrice',
                  align: 'right',
                  render: (v) => formatCurrency(v),
                },
              ]}
            />

            {selectedPurchase.receiptPhotoUrl && (
              <div>
                <Divider orientation={"left" as any}>Fiş Fotoğrafı</Divider>
                <div style={{ textAlign: 'center' }}>
                  <Image
                    src={selectedPurchase.receiptPhotoUrl}
                    alt="Receipt"
                    style={{ maxWidth: '100%', borderRadius: 8, boxShadow: '0 2px 8px rgba(0,0,0,0.1)' }}
                  />
                </div>
              </div>
            )}
            
            <div style={{ textAlign: 'right', color: '#999', fontSize: '12px' }}>
              Kaydeden: {selectedPurchase.createdByUserName} | {formatDate(selectedPurchase.createdAt)}
            </div>
          </Space>
        )}
      </Modal>
    </div>
  );
};

export default ProductionPurchasesPage;
