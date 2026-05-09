import React, { useState } from 'react';
import {
  Tabs,
  Card,
  Table,
  Button,
  Modal,
  Form,
  Select,
  Radio,
  InputNumber,
  DatePicker,
  Input,
  Tag,
  Typography,
  Space,
} from 'antd';
import { WalletOutlined, RetweetOutlined, DollarOutlined, HistoryOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import {
  useWalletSummary,
  useSetInitialBalance,
  useTransferBranchToAdmin,
  useTransferAdminToBranch,
  useWalletTransactions,
  useBranchWallet,
} from '../../hooks/useWallet';
import { WalletType, WalletTransactionType } from '../../types/wallet';
import { formatCurrency, formatDate } from '../../utils/formatters';

const { Title, Text } = Typography;
const { RangePicker } = DatePicker;

const WalletManagementPage: React.FC = () => {
  const [activeTab, setActiveTab] = useState('summary');

  // Summary Tab State
  const { data: walletSummary, isLoading: loadingSummary } = useWalletSummary();
  const [isBalanceModalOpen, setIsBalanceModalOpen] = useState(false);
  const [balanceForm] = Form.useForm();
  const setInitialBalance = useSetInitialBalance();

  // Transfer Tab State
  const [transferForm] = Form.useForm();
  const transferBranchToAdmin = useTransferBranchToAdmin();
  const transferAdminToBranch = useTransferAdminToBranch();
  const [transferBranchId, setTransferBranchId] = useState<string | undefined>();
  const { data: selectedBranchWallet } = useBranchWallet(transferBranchId);

  // Transactions Tab State
  const [filterBranchId, setFilterBranchId] = useState<string | undefined>(undefined);
  const [dateRange, setDateRange] = useState<[dayjs.Dayjs, dayjs.Dayjs]>([
    dayjs().startOf('month'),
    dayjs(),
  ]);
  const { data: transactions, isLoading: loadingTransactions } = useWalletTransactions(
    filterBranchId,
    dateRange[0]?.format('YYYY-MM-DD'),
    dateRange[1]?.format('YYYY-MM-DD')
  );

  // === HANDLERS ===

  // Balance
  const handleOpenBalanceModal = () => {
    balanceForm.resetFields();
    setIsBalanceModalOpen(true);
  };

  const handleBalanceBranchChange = (value: string | undefined) => {
    if (!value) {
      // Admin
      balanceForm.setFieldsValue({
        cashBalance: walletSummary?.admin.cashBalance || 0,
        bankBalance: walletSummary?.admin.bankBalance || 0,
      });
    } else {
      // Branch
      const branch = walletSummary?.branches.find((b) => b.branchId === value);
      balanceForm.setFieldsValue({
        cashBalance: branch?.cashBalance || 0,
        bankBalance: branch?.bankBalance || 0,
      });
    }
  };

  const handleSaveBalance = async () => {
    const values = await balanceForm.validateFields();
    await setInitialBalance.mutateAsync({
      branchId: values.branchId === 'admin' ? undefined : values.branchId,
      cashBalance: values.cashBalance,
      bankBalance: values.bankBalance,
    });
    setIsBalanceModalOpen(false);
  };

  // Transfer
  const handleTransfer = async () => {
    const values = await transferForm.validateFields();
    if (values.direction === 'branchToAdmin') {
      await transferBranchToAdmin.mutateAsync({
        branchId: values.branchId,
        walletType: values.walletType,
        amount: values.amount,
        description: values.description,
      });
    } else {
      await transferAdminToBranch.mutateAsync({
        branchId: values.branchId,
        walletType: values.walletType,
        amount: values.amount,
        description: values.description,
      });
    }
    transferForm.resetFields(['amount', 'description']);
  };

  // === COLUMNS ===
  const summaryColumns = [
    {
      title: 'Şube / Hesap',
      dataIndex: 'branchName',
      key: 'branchName',
      render: (text: string, record: any) => {
        if (record.isTotal) return <Text strong>📊 {text}</Text>;
        if (record.isAdmin) return <Text strong>👤 {text}</Text>;
        return text;
      },
    },
    {
      title: '💵 Nakit',
      dataIndex: 'cashBalance',
      key: 'cashBalance',
      render: (amount: number) => (
        <Text type={amount < 0 ? 'danger' : undefined} strong={true}>
          {formatCurrency(amount)}
        </Text>
      ),
    },
    {
      title: '🏦 Banka',
      dataIndex: 'bankBalance',
      key: 'bankBalance',
      render: (amount: number) => (
        <Text type={amount < 0 ? 'danger' : undefined} strong={true}>
          {formatCurrency(amount)}
        </Text>
      ),
    },
  ];

  const getTransactionTagColor = (type: string) => {
    if (type.includes('Gün Sonu')) return 'blue';
    if (type.includes('Şubeden Çekim')) return 'orange';
    if (type.includes('Şubeye Gönderim')) return 'green';
    if (type.includes('Satın Alım')) return 'red';
    if (type.includes('İade')) return 'purple';
    if (type.includes('Başlangıç')) return 'default';
    if (type.includes('Düzeltme')) return 'gold';
    return 'default';
  };

  const transactionColumns = [
    {
      title: 'Tarih',
      dataIndex: 'transactionDate',
      key: 'date',
      render: (d: string) => formatDate(d),
    },
    {
      title: 'İşlem Tipi',
      dataIndex: 'transactionTypeLabel',
      key: 'type',
      render: (t: string) => <Tag color={getTransactionTagColor(t)}>{t}</Tag>,
    },
    {
      title: 'Kaynak',
      dataIndex: 'sourceLabel',
      key: 'source',
      render: (text: string) => text || '-',
    },
    {
      title: 'Hedef',
      dataIndex: 'targetLabel',
      key: 'target',
      render: (text: string) => text || '-',
    },
    {
      title: 'Cüzdan',
      dataIndex: 'walletTypeLabel',
      key: 'walletType',
    },
    {
      title: 'Tutar',
      dataIndex: 'amount',
      key: 'amount',
      render: (amount: number) => (
        <Text strong>{formatCurrency(amount)}</Text>
      ),
    },
    {
      title: 'Açıklama',
      dataIndex: 'description',
      key: 'description',
    },
    {
      title: 'Kullanıcı',
      dataIndex: 'createdByName',
      key: 'user',
    },
  ];

  // === DATA TRANSFORMS ===
  let summaryTableData: any[] = [];
  if (walletSummary) {
    summaryTableData = [
      ...walletSummary.branches.map((b) => ({
        key: b.branchId,
        branchName: b.branchName,
        cashBalance: b.cashBalance,
        bankBalance: b.bankBalance,
        isTotal: false,
        isAdmin: false,
      })),
      {
        key: 'admin',
        branchName: 'Admin',
        cashBalance: walletSummary.admin.cashBalance,
        bankBalance: walletSummary.admin.bankBalance,
        isTotal: false,
        isAdmin: true,
      },
      {
        key: 'total',
        branchName: 'GENEL TOPLAM',
        cashBalance: walletSummary.grandTotalCash,
        bankBalance: walletSummary.grandTotalBank,
        isTotal: true,
        isAdmin: false,
      },
    ];
  }

  return (
    <div>
      <div style={{ marginBottom: 16 }}>
        <Title level={3} style={{ margin: 0 }}>
          <WalletOutlined /> Cüzdan Yönetimi
        </Title>
      </div>

      <Card bodyStyle={{ padding: 0 }}>
        <Tabs
          activeKey={activeTab}
          onChange={setActiveTab}
          style={{ padding: '0 24px' }}
          items={[
            {
              key: 'summary',
              label: (
                <span>
                  <DollarOutlined />
                  Kasa Özeti
                </span>
              ),
              children: (
                <div style={{ padding: '24px 0' }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 16 }}>
                    <Title level={4} style={{ margin: 0 }}>💰 Kasa Genel Durum</Title>
                    <Button type="primary" onClick={handleOpenBalanceModal}>
                      ⚙️ Bakiye Ayarla
                    </Button>
                  </div>
                  <Table
                    columns={summaryColumns}
                    dataSource={summaryTableData}
                    pagination={false}
                    loading={loadingSummary}
                    scroll={{ x: 'max-content' }}
                    rowClassName={(record) => (record.isAdmin ? 'admin-row' : '')}
                  />
                  {walletSummary && (
                    <div style={{ marginTop: 16, textAlign: 'right' }}>
                      <Text type="secondary">Tüm Cüzdanlar Toplamı: </Text>
                      <Text strong style={{ fontSize: 18 }}>
                        {formatCurrency(walletSummary.grandTotal)}
                      </Text>
                    </div>
                  )}
                </div>
              ),
            },
            {
              key: 'transfer',
              label: (
                <span>
                  <RetweetOutlined />
                  Para Transferi
                </span>
              ),
              children: (
                <div style={{ padding: '24px 0', maxWidth: 600 }}>
                  <Form
                    form={transferForm}
                    layout="vertical"
                    onFinish={handleTransfer}
                    initialValues={{ direction: 'branchToAdmin', walletType: WalletType.Cash }}
                  >
                    <Form.Item name="direction" label="İşlem Tipi" rules={[{ required: true }]}>
                      <Radio.Group>
                        <Radio value="branchToAdmin">Şubeden Çek (Şube → Admin)</Radio>
                        <Radio value="adminToBranch">Şubeye Gönder (Admin → Şube)</Radio>
                      </Radio.Group>
                    </Form.Item>

                    <Form.Item name="branchId" label="Şube" rules={[{ required: true, message: 'Şube seçin' }]}>
                      <Select
                        placeholder="Şube Seçin ▼"
                        onChange={setTransferBranchId}
                        options={walletSummary?.branches.map((b) => ({
                          label: b.branchName,
                          value: b.branchId,
                        }))}
                      />
                    </Form.Item>

                    <Form.Item name="walletType" label="Cüzdan Tipi" rules={[{ required: true }]}>
                      <Radio.Group>
                        <Radio value={WalletType.Cash}>Nakit</Radio>
                        <Radio value={WalletType.Bank}>Banka</Radio>
                      </Radio.Group>
                    </Form.Item>

                    <Form.Item name="amount" label="Tutar" rules={[{ required: true, message: 'Tutar girin' }]}>
                      <InputNumber min={0.01} precision={2} style={{ width: '100%' }} placeholder="0.00" addonAfter="₺" />
                    </Form.Item>

                    <Form.Item name="description" label="Açıklama (Opsiyonel)">
                      <Input placeholder="Transfer açıklaması..." />
                    </Form.Item>

                    {transferBranchId && selectedBranchWallet && (
                      <div style={{ marginBottom: 24, padding: 16, background: '#fafafa', borderRadius: 8 }}>
                        <Space direction="vertical" style={{ width: '100%' }}>
                          <div>
                            <Text type="secondary">Seçili Şube Bakiyesi:</Text>
                            <br />
                            <Text>Nakit: {formatCurrency(selectedBranchWallet.cashBalance)} | Banka: {formatCurrency(selectedBranchWallet.bankBalance)}</Text>
                          </div>
                          <div>
                            <Text type="secondary">Admin Bakiyesi:</Text>
                            <br />
                            <Text>Nakit: {formatCurrency(walletSummary?.admin.cashBalance || 0)} | Banka: {formatCurrency(walletSummary?.admin.bankBalance || 0)}</Text>
                          </div>
                        </Space>
                      </div>
                    )}

                    <Button type="primary" htmlType="submit" loading={transferBranchToAdmin.isPending || transferAdminToBranch.isPending} block>
                      Transfer Yap
                    </Button>
                  </Form>
                </div>
              ),
            },
            {
              key: 'transactions',
              label: (
                <span>
                  <HistoryOutlined />
                  Hareket Geçmişi
                </span>
              ),
              children: (
                <div style={{ padding: '24px 0' }}>
                  <div style={{ marginBottom: 16, display: 'flex', gap: 16, flexWrap: 'wrap' }}>
                    <Select
                      placeholder="Şube Filtresi"
                      style={{ width: 200 }}
                      allowClear
                      onChange={setFilterBranchId}
                      options={walletSummary?.branches.map((b) => ({
                        label: b.branchName,
                        value: b.branchId,
                      }))}
                    />
                    <RangePicker
                      value={dateRange}
                      onChange={(dates) => {
                        if (dates && dates[0] && dates[1]) {
                          setDateRange([dates[0], dates[1]]);
                        }
                      }}
                      format="DD.MM.YYYY"
                      allowClear={false}
                    />
                  </div>
                  <Table
                    columns={transactionColumns}
                    dataSource={transactions}
                    rowKey="id"
                    loading={loadingTransactions}
                    scroll={{ x: 'max-content' }}
                  />
                </div>
              ),
            },
          ]}
        />
      </Card>

      <Modal
        title="Bakiye Ayarla"
        open={isBalanceModalOpen}
        onCancel={() => setIsBalanceModalOpen(false)}
        onOk={handleSaveBalance}
        confirmLoading={setInitialBalance.isPending}
      >
        <Form form={balanceForm} layout="vertical">
          <Form.Item name="branchId" label="Hesap Seçin" rules={[{ required: true }]}>
            <Select placeholder="Hesap Seçin" onChange={handleBalanceBranchChange}>
              <Select.Option value="admin">Admin Kasası</Select.Option>
              {walletSummary?.branches.map((b) => (
                <Select.Option key={b.branchId} value={b.branchId}>
                  {b.branchName}
                </Select.Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item name="cashBalance" label="Nakit Bakiye" rules={[{ required: true }]}>
            <InputNumber style={{ width: '100%' }} precision={2} addonAfter="₺" />
          </Form.Item>
          <Form.Item name="bankBalance" label="Banka Bakiye" rules={[{ required: true }]}>
            <InputNumber style={{ width: '100%' }} precision={2} addonAfter="₺" />
          </Form.Item>
        </Form>
      </Modal>

      <style>{`
        .admin-row {
          background-color: #f0f5ff;
        }
      `}</style>
    </div>
  );
};

export default WalletManagementPage;
