import React, { useState } from 'react';
import {
  Table, DatePicker, Tag, Typography, Space, Button, Modal, Image, Divider
} from 'antd';
import {
  ShoppingCartOutlined, EyeOutlined, SearchOutlined
} from '@ant-design/icons';
import { useAdminPurchases } from '../../hooks/usePurchases';
import { formatCurrency, formatDate } from '../../utils/formatters';
import BranchSelector from '../../components/admin/BranchSelector';
import {
  PaymentMethod,
  PAYMENT_METHOD_LABELS,
  PAYMENT_METHOD_COLORS,
  type PurchaseDto
} from '../../types/purchase';
import dayjs from 'dayjs';

const { Title, Text } = Typography;
const { RangePicker } = DatePicker;

const AdminPurchasesPage: React.FC = () => {
  const [page, setPage] = useState(1);
  const [branchId, setBranchId] = useState<string | null>(null);

  const [dateRange, setDateRange] = useState<[dayjs.Dayjs, dayjs.Dayjs] | null>(null);
  const [selectedPurchase, setSelectedPurchase] = useState<PurchaseDto | null>(null);
  const [isDetailModalOpen, setIsDetailModalOpen] = useState(false);

  const { data: purchasesData, isLoading } = useAdminPurchases({
    page,
    pageSize: 20,
    branchId,
    startDate: dateRange?.[0]?.toISOString(),
    endDate: dateRange?.[1]?.toISOString(),
  });

  const columns = [
    {
      title: 'Şube',
      dataIndex: 'branchName',
      key: 'branchName',
      render: (text: string) => <Text strong>{text}</Text>,
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
          <Button
            size="small"
            icon={<EyeOutlined />}
            onClick={() => { setSelectedPurchase(record); setIsDetailModalOpen(true); }}
          >
            Detay
          </Button>
        ),
      },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <Title level={3}>
        <ShoppingCartOutlined /> Tüm Satın Alımlar (Admin)
      </Title>

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
                <Text strong>{selectedPurchase.branchName}</Text>
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
