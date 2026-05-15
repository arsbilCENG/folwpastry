import React, { useState } from 'react';
import {
  Table, DatePicker, Tag, Typography, Space, Button, Modal, Image, Divider,
  Form, Radio, Input, InputNumber, Select, message, Popconfirm
} from 'antd';
import {
  ShoppingCartOutlined, EyeOutlined, SearchOutlined, PlusOutlined, DeleteOutlined
} from '@ant-design/icons';
import { useAdminPurchases, useCreateAdminPurchase, useUploadAdminReceiptPhoto, useDeleteAdminPurchase } from '../../hooks/usePurchases';
import { formatCurrency, formatDate } from '../../utils/formatters';
import BranchSelector from '../../components/admin/BranchSelector';
import PhotoUpload from '../../components/common/PhotoUpload';
import {
  PaymentMethod,
  PAYMENT_METHOD_LABELS,
  PAYMENT_METHOD_COLORS,
  type PurchaseDto,
  type CreatePurchaseItemDto
} from '../../types/purchase';
import dayjs from 'dayjs';

const { Title, Text } = Typography;
const { RangePicker } = DatePicker;

const UNIT_OPTIONS = ['Adet', 'Kg', 'Lt', 'Paket', 'Kutu', 'Torba', 'Koli', 'Diğer'];

const AdminPurchasesPage: React.FC = () => {
  const [page, setPage] = useState(1);
  const [branchId, setBranchId] = useState<string | null>(null);

  const [dateRange, setDateRange] = useState<[dayjs.Dayjs, dayjs.Dayjs] | null>(null);
  const [selectedPurchase, setSelectedPurchase] = useState<PurchaseDto | null>(null);
  const [isDetailModalOpen, setIsDetailModalOpen] = useState(false);
  
  // Yeni Satın Alım Modal State
  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [form] = Form.useForm();
  const [itemForm] = Form.useForm();
  const [items, setItems] = useState<CreatePurchaseItemDto[]>([]);
  const [receiptPhoto, setReceiptPhoto] = useState<File | null>(null);
  const [submitting, setSubmitting] = useState(false);

  const { data: purchasesData, isLoading } = useAdminPurchases({
    page,
    pageSize: 20,
    branchId,
    startDate: dateRange?.[0]?.toISOString(),
    endDate: dateRange?.[1]?.toISOString(),
  });

  const createAdminPurchase = useCreateAdminPurchase();
  const uploadPhoto = useUploadAdminReceiptPhoto();
  const deleteAdminPurchase = useDeleteAdminPurchase();

  const handleAddItem = () => {
    itemForm.validateFields().then(values => {
      const newItem: CreatePurchaseItemDto = {
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

      const purchase = await createAdminPurchase.mutateAsync(createDto);
      await uploadPhoto.mutateAsync({ id: purchase.id, photo: receiptPhoto });

      message.success('Admin satın alımı kaydedildi.');
      setIsAddModalOpen(false);
      setItems([]);
      setReceiptPhoto(null);
      form.resetFields();
    } catch (error: any) {
      // Error is handled in useMutation
    } finally {
      setSubmitting(false);
    }
  };

  const columns = [
    {
      title: 'Şube',
      dataIndex: 'branchName',
      key: 'branchName',
      render: (text: string) => text ? <Text strong>{text}</Text> : <Tag color="purple">Admin</Tag>,
    },
    {
      title: 'Satın Alım No',
      dataIndex: 'purchaseNumber',
      key: 'purchaseNumber',
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
            {!record.branchId && (
              <Popconfirm
                title="Bu satın alımı silmek istediğinize emin misiniz?"
                onConfirm={() => deleteAdminPurchase.mutate(record.id)}
                okText="Evet"
                cancelText="Hayır"
              >
                <Button
                  size="small"
                  danger
                  icon={<DeleteOutlined />}
                />
              </Popconfirm>
            )}
          </Space>
        ),
      },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}>
        <Title level={3} style={{ margin: 0 }}>
          <ShoppingCartOutlined /> Tüm Satın Alımlar (Admin)
        </Title>
        <Button
          type="primary"
          icon={<PlusOutlined />}
          onClick={() => setIsAddModalOpen(true)}
          size="large"
        >
          Yeni Satın Alım
        </Button>
      </div>

      <div style={{ background: '#fff', padding: '16px', borderRadius: '8px', marginBottom: '24px', boxShadow: '0 1px 2px rgba(0,0,0,0.03)' }}>
        <Space size="large" wrap>
          <div>
            <Text type="secondary" style={{ display: 'block', marginBottom: '4px' }}>Şube Seçimi</Text>
            <BranchSelector
              value={branchId}
              onChange={setBranchId}
              style={{ width: 250 }}
            />
          </div>
          <div>
            <Text type="secondary" style={{ display: 'block', marginBottom: '4px' }}>Tarih Aralığı</Text>
            <RangePicker
              onChange={(dates) => setDateRange(dates as any)}
              format="DD.MM.YYYY"
              placeholder={['Başlangıç', 'Bitiş']}
            />
          </div>
          <div style={{ alignSelf: 'flex-end' }}>
             <Button icon={<SearchOutlined />} type="primary" ghost>Filtrele</Button>
          </div>
        </Space>
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
        title={<span><ShoppingCartOutlined /> Yeni Admin Satın Alımı (Şubesiz)</span>}
        open={isAddModalOpen}
        onCancel={() => { 
          if (!submitting) {
            setIsAddModalOpen(false); 
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

        <Divider orientation={"left" as any}>Kalemler (Serbest Giriş)</Divider>

        <div style={{ background: '#fafafa', padding: 16, borderRadius: 8, marginBottom: 16, border: '1px solid #f0f0f0' }}>
          <Form form={itemForm} layout="vertical">
            <div style={{ display: 'grid', gridTemplateColumns: '2fr 1fr 1fr 1fr', gap: '8px', alignItems: 'start' }}>
              <Form.Item name="itemName" label="Gider Açıklaması" rules={[{ required: true, message: 'Giriniz' }]}>
                <Input placeholder="Örn: Kırtasiye malzemeleri" />
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
                { title: 'Açıklama', dataIndex: 'itemName', key: 'itemName' },
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
            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: '16px', background: '#f5f5f5', padding: '16px', borderRadius: '8px' }}>
              <div>
                <Text type="secondary">Şube</Text><br />
                <Text strong>{selectedPurchase.branchName || 'Admin'}</Text>
              </div>
              <div>
                <Text type="secondary">Tarih</Text><br />
                <Text strong>{formatDate(selectedPurchase.purchaseDate)}</Text>
              </div>
              <div>
                <Text type="secondary">Ödeme</Text><br />
                <Tag color={PAYMENT_METHOD_COLORS[selectedPurchase.paymentMethod]}>
                  {PAYMENT_METHOD_LABELS[selectedPurchase.paymentMethod]}
                </Tag>
              </div>
              <div>
                <Text type="secondary">Toplam</Text><br />
                <Text strong style={{ fontSize: '18px', color: '#f5222d' }}>{formatCurrency(selectedPurchase.totalAmount)}</Text>
              </div>
            </div>

            <Table
              dataSource={selectedPurchase.items}
              rowKey="id"
              size="small"
              pagination={false}
              columns={[
                { title: 'Ürün/Gider', dataIndex: 'itemName', key: 'itemName' },
                { title: 'Miktar', key: 'qty', render: (_, r) => `${r.quantity} ${r.unit}` },
                { title: 'Birim Fiyat', dataIndex: 'unitPrice', key: 'unitPrice', align: 'right', render: (v) => formatCurrency(v) },
                { title: 'Toplam', dataIndex: 'totalPrice', key: 'totalPrice', align: 'right', render: (v) => formatCurrency(v) },
                { title: 'Stok', dataIndex: 'affectsStock', key: 'stock', align: 'center', render: (v) => v ? <Tag color="success">Stokta</Tag> : <Tag color="default">Gider</Tag> },
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

export default AdminPurchasesPage;
