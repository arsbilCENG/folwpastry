import React, { useState } from 'react';
import {
  Tabs, Card, Table, Button, Modal, Form, Select, Radio,
  InputNumber, DatePicker, Input, Tag, Space, Typography,
  Statistic, Row, Col, Alert
} from 'antd';
import {
  BankOutlined, PlusOutlined, MinusOutlined,
  DeleteOutlined, WalletOutlined
} from '@ant-design/icons';
import dayjs from 'dayjs';
import {
  useAdminCashSummaries, useAdminCashTransactions,
  useCreateCashTransaction, useDeleteCashTransaction
} from '../../hooks/useCashTransactions';
import { formatCurrency, formatDate } from '../../utils/formatters';
import {
  TransactionType, TRANSACTION_TYPE_LABELS, TRANSACTION_TYPE_COLORS
} from '../../types/cashTransaction';
import { PaymentMethod, PAYMENT_METHOD_LABELS } from '../../types/purchase';

const { Title, Text } = Typography;

const AdminCashManagementPage: React.FC = () => {
  const [selectedDate, setSelectedDate] = useState(dayjs().format('YYYY-MM-DD'));
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [preselectedBranchId, setPreselectedBranchId] = useState<string | undefined>();
  const [form] = Form.useForm();
  const [transactionPage, setTransactionPage] = useState(1);

  const { data: summariesRes, isLoading: summariesLoading } =
    useAdminCashSummaries(selectedDate);
  const { data: transactionsRes, isLoading: transactionsLoading } =
    useAdminCashTransactions({ page: transactionPage, pageSize: 20 });
  const createTransaction = useCreateCashTransaction();
  const deleteTransaction = useDeleteCashTransaction();

  const handleOpenModal = (branchId?: string) => {
    setPreselectedBranchId(branchId);
    form.resetFields();
    if (branchId) form.setFieldValue('branchId', branchId);
    setIsModalOpen(true);
  };

  const handleSubmit = async () => {
    const values = await form.validateFields();
    await createTransaction.mutateAsync({
      branchId: values.branchId,
      transactionDate: values.transactionDate.toISOString(),
      transactionType: values.transactionType,
      amount: values.amount,
      method: values.method,
      description: values.description,
    });
    setIsModalOpen(false);
  };

  const transactionColumns = [
    {
      title: 'Tarih',
      dataIndex: 'transactionDate',
      key: 'date',
      render: (d: string) => formatDate(d),
    },
    {
      title: 'Şube',
      dataIndex: 'branchName',
      key: 'branch',
    },
    {
      title: 'İşlem',
      dataIndex: 'transactionType',
      key: 'type',
      render: (t: TransactionType) => (
        <Tag color={TRANSACTION_TYPE_COLORS[t]}>
          {TRANSACTION_TYPE_LABELS[t]}
        </Tag>
      ),
    },
    {
      title: 'Tutar',
      dataIndex: 'amount',
      key: 'amount',
      render: (amount: number, record: any) => (
        <Text strong style={{
          color: record.transactionType === TransactionType.AdminWithdrawal
            ? '#ff4d4f' : '#52c41a'
        }}>
          {record.transactionType === TransactionType.AdminWithdrawal ? '-' : '+'}
          {formatCurrency(amount)}
        </Text>
      ),
    },
    {
      title: 'Yöntem',
      dataIndex: 'method',
      key: 'method',
      render: (m: PaymentMethod) => PAYMENT_METHOD_LABELS[m],
    },
    {
      title: 'Açıklama',
      dataIndex: 'description',
      key: 'description',
    },
    {
      title: 'Yapan',
      dataIndex: 'createdByUserName',
      key: 'user',
    },
    {
      title: '',
      key: 'action',
      render: (_: any, record: any) => (
        <Button
          size="small"
          danger
          icon={<DeleteOutlined />}
          onClick={() => {
            Modal.confirm({
              title: 'Kayıt silinsin mi?',
              onOk: () => deleteTransaction.mutate(record.id),
            });
          }}
        />
      ),
    },
  ];

  const summaries = summariesRes?.data || [];
  const transactions = transactionsRes?.data?.items || [];
  const totalTransactions = transactionsRes?.data?.totalCount || 0;

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16, flexWrap: 'wrap', gap: 8 }}>
        <Title level={4} style={{ margin: 0 }}>
          <WalletOutlined /> Kasa Yönetimi
        </Title>
        <Button
          type="primary"
          icon={<BankOutlined />}
          onClick={() => handleOpenModal()}
        >
          Para Çek / Yatır
        </Button>
      </div>

      <Tabs defaultActiveKey="summaries">
        {/* Tab 1: Şube Kasa Özetleri */}
        <Tabs.TabPane tab="Şube Kasa Özetleri" key="summaries">
          <div style={{ marginBottom: 16 }}>
            <DatePicker
              value={dayjs(selectedDate)}
              onChange={(d) => d && setSelectedDate(d.format('YYYY-MM-DD'))}
              format="DD.MM.YYYY"
            />
          </div>

          <Row gutter={[16, 16]}>
            {summaries.map((summary: any) => {
              const diff = summary.lastCountedCash !== undefined && summary.lastCountedCash !== null
                ? summary.lastCountedCash - summary.expectedCashBalance
                : null;

              return (
                <Col xs={24} sm={12} lg={8} key={summary.branchId}>
                  <Card
                    loading={summariesLoading}
                    title={summary.branchName}
                    extra={
                      <Button
                        size="small"
                        icon={<BankOutlined />}
                        onClick={() => handleOpenModal(summary.branchId)}
                      >
                        İşlem Yap
                      </Button>
                    }
                  >
                    <Space direction="vertical" style={{ width: '100%' }} size={4}>
                      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Text type="secondary">Açılış Bakiyesi</Text>
                        <Text>{formatCurrency(summary.openingCashBalance)}</Text>
                      </div>
                      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Text type="secondary">Nakit Satış</Text>
                        <Text style={{ color: '#52c41a' }}>+{formatCurrency(summary.todayCashSales)}</Text>
                      </div>
                      {summary.todayDeposits > 0 && (
                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                          <Text type="secondary">Admin Yatırım</Text>
                          <Text style={{ color: '#52c41a' }}>+{formatCurrency(summary.todayDeposits)}</Text>
                        </div>
                      )}
                      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Text type="secondary">Nakit Alımlar</Text>
                        <Text style={{ color: '#ff4d4f' }}>-{formatCurrency(summary.todayCashPurchases)}</Text>
                      </div>
                      {summary.todayWithdrawals > 0 && (
                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                          <Text type="secondary">Admin Çekim</Text>
                          <Text style={{ color: '#ff4d4f' }}>-{formatCurrency(summary.todayWithdrawals)}</Text>
                        </div>
                      )}
                      <div style={{ borderTop: '1px solid #f0f0f0', paddingTop: 8, marginTop: 4 }}>
                        <Statistic
                          title="Beklenen Kasa"
                          value={summary.expectedCashBalance}
                          precision={2}
                          prefix="₺"
                          valueStyle={{ color: '#1890ff', fontSize: 20 }}
                        />
                      </div>
                      {diff !== null && (
                        <Alert
                          message={`Son Sayım Farkı: ${diff >= 0 ? '+' : ''}${formatCurrency(diff)}`}
                          type={Math.abs(diff) < 1 ? 'success' : 'warning'}
                          showIcon
                          style={{ padding: '4px 8px' }}
                        />
                      )}
                    </Space>
                  </Card>
                </Col>
              );
            })}
          </Row>
        </Tabs.TabPane>

        {/* Tab 2: Kasa Hareketleri */}
        <Tabs.TabPane tab="Kasa Hareketleri" key="transactions">
          <Table
            columns={transactionColumns}
            dataSource={transactions}
            rowKey="id"
            loading={transactionsLoading}
            scroll={{ x: 'max-content' }}
            pagination={{
              current: transactionPage,
              total: totalTransactions,
              pageSize: 20,
              onChange: setTransactionPage,
            }}
          />
        </Tabs.TabPane>
      </Tabs>

      {/* Para Çek/Yatır Modal */}
      <Modal
        title={<><BankOutlined /> Para Çek / Yatır</>}
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        onOk={handleSubmit}
        confirmLoading={createTransaction.isPending}
        okText="Kaydet"
        cancelText="İptal"
      >
        <Form form={form} layout="vertical">
          <Form.Item
            name="branchId"
            label="Şube"
            rules={[{ required: true, message: 'Şube seçiniz' }]}
          >
            <Select placeholder="Şube seçin...">
              {summaries.map((s: any) => (
                <Select.Option key={s.branchId} value={s.branchId}>
                  {s.branchName}
                </Select.Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item
            name="transactionType"
            label="İşlem Tipi"
            initialValue={TransactionType.AdminWithdrawal}
            rules={[{ required: true }]}
          >
            <Radio.Group>
              <Radio value={TransactionType.AdminWithdrawal}>
                <MinusOutlined style={{ color: '#ff4d4f' }} /> Para Çekme
              </Radio>
              <Radio value={TransactionType.AdminDeposit}>
                <PlusOutlined style={{ color: '#52c41a' }} /> Para Yatırma
              </Radio>
            </Radio.Group>
          </Form.Item>

          <Form.Item
            name="transactionDate"
            label="Tarih"
            initialValue={dayjs()}
            rules={[{ required: true }]}
          >
            <DatePicker style={{ width: '100%' }} format="DD.MM.YYYY" />
          </Form.Item>

          <Form.Item
            name="amount"
            label="Tutar (₺)"
            rules={[{ required: true, message: 'Tutar giriniz' }]}
          >
            <InputNumber
              min={0.01}
              style={{ width: '100%' }}
              placeholder="0.00"
              precision={2}
            />
          </Form.Item>

          <Form.Item
            name="method"
            label="Yöntem"
            initialValue={PaymentMethod.Cash}
            rules={[{ required: true }]}
          >
            <Radio.Group>
              <Radio value={PaymentMethod.Cash}>💵 Nakit</Radio>
              <Radio value={PaymentMethod.CreditCard}>🏦 Banka Havalesi</Radio>
            </Radio.Group>
          </Form.Item>

          <Form.Item
            name="description"
            label="Açıklama"
            rules={[{ required: true, message: 'Açıklama giriniz' }]}
          >
            <Input placeholder="Haftalık hasılat teslimi, bozukluk vs..." />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default AdminCashManagementPage;
