import React, { useState } from 'react';
import { 
  Table, Card, Row, Col, Statistic, DatePicker, Button, Space, 
  Spin, Typography, Select, Tag
} from 'antd';
import { 
  SearchOutlined, ArrowUpOutlined, ArrowDownOutlined, 
  SwapOutlined, TransactionOutlined, UserOutlined
} from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { useCashTransactionReport } from '../../hooks/useReports';
import { useAdminBranches } from '../../hooks/useAdmin';
import type { CashTransactionReportItem } from '../../types/report';

const { RangePicker } = DatePicker;
const { Text } = Typography;

interface CashTransactionsTabProps {
  branchId?: string;
  showBranchFilter?: boolean;
}

const CashTransactionsTab: React.FC<CashTransactionsTabProps> = ({ branchId: initialBranchId, showBranchFilter = false }) => {
  const [dates, setDates] = useState<[dayjs.Dayjs, dayjs.Dayjs]>([
    dayjs().startOf('month'),
    dayjs()
  ]);
  const [selectedBranchId, setSelectedBranchId] = useState<string | undefined>(initialBranchId);

  const { data: branches } = useAdminBranches();
  const { data: report, isLoading } = useCashTransactionReport(
    selectedBranchId,
    dates[0].format('YYYY-MM-DD'),
    dates[1].format('YYYY-MM-DD')
  );

  const columns: ColumnsType<CashTransactionReportItem> = [
    {
      title: 'Tarih',
      dataIndex: 'transactionDate',
      key: 'transactionDate',
      render: (date) => dayjs(date).format('DD.MM.YYYY HH:mm'),
      sorter: (a, b) => dayjs(a.transactionDate).unix() - dayjs(b.transactionDate).unix(),
    },
    {
      title: 'Şube',
      dataIndex: 'branchName',
      key: 'branchName',
      hidden: !showBranchFilter,
    },
    {
      title: 'İşlem Tipi',
      dataIndex: 'transactionTypeLabel',
      key: 'transactionTypeLabel',
      render: (label) => (
        <Tag color={label === 'Para Yatırma' ? 'success' : 'warning'} icon={label === 'Para Yatırma' ? <ArrowUpOutlined /> : <ArrowDownOutlined />}>
          {label}
        </Tag>
      ),
    },
    {
      title: 'Yöntem',
      dataIndex: 'methodLabel',
      key: 'methodLabel',
      render: (label) => <Tag color="default">{label}</Tag>,
    },
    {
      title: 'Tutar',
      dataIndex: 'amount',
      key: 'amount',
      align: 'right',
      render: (val, record) => (
        <Text strong style={{ color: record.transactionTypeLabel === 'Para Yatırma' ? '#3f8600' : '#cf1322' }}>
          ₺ {val.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}
        </Text>
      ),
      sorter: (a, b) => a.amount - b.amount,
    },
    {
      title: 'Açıklama',
      dataIndex: 'description',
      key: 'description',
      ellipsis: true,
    },
    {
      title: 'Yapan',
      dataIndex: 'createdByName',
      key: 'createdByName',
      render: (name) => <Space><UserOutlined style={{ fontSize: '12px' }} />{name}</Space>,
    }
  ];

  return (
    <div style={{ padding: '24px 0' }}>
      <Card style={{ marginBottom: 24 }}>
        <Row gutter={[16, 16]} align="middle">
          <Col xs={24} md={showBranchFilter ? 8 : 12}>
            <RangePicker 
              style={{ width: '100%' }} 
              value={dates}
              onChange={(vals) => vals && setDates([vals[0]!, vals[1]!])}
              format="DD.MM.YYYY"
              allowClear={false}
            />
          </Col>
          {showBranchFilter && (
            <Col xs={24} md={8}>
              <Select
                style={{ width: '100%' }}
                placeholder="Şube Seçin (Tümü)"
                allowClear
                value={selectedBranchId}
                onChange={setSelectedBranchId}
                options={branches?.map(b => ({ label: b.name, value: b.id }))}
              />
            </Col>
          )}
          <Col xs={24} md={showBranchFilter ? 8 : 12}>
            <Button 
              type="primary" 
              icon={<SearchOutlined />} 
              loading={isLoading}
              block
            >
              Filtrele
            </Button>
          </Col>
        </Row>
      </Card>

      {isLoading ? (
        <div style={{ textAlign: 'center', padding: '100px' }}>
          <Spin size="large" tip="Rapor yükleniyor..." />
        </div>
      ) : (
        <>
          <Row gutter={16} style={{ marginBottom: 24 }}>
            <Col xs={24} sm={8}>
              <Card>
                <Statistic
                  title="Net Akış"
                  value={report?.netFlow || 0}
                  precision={2}
                  prefix={<SwapOutlined />}
                  suffix="₺"
                  valueStyle={{ color: (report?.netFlow || 0) >= 0 ? '#3f8600' : '#cf1322' }}
                />
              </Card>
            </Col>
            <Col xs={24} sm={8}>
              <Card>
                <Statistic
                  title="Toplam Giren"
                  value={report?.totalDeposits || 0}
                  precision={2}
                  prefix={<ArrowUpOutlined />}
                  suffix="₺"
                  valueStyle={{ color: '#3f8600' }}
                />
              </Card>
            </Col>
            <Col xs={24} sm={8}>
              <Card>
                <Statistic
                  title="Toplam Çıkan"
                  value={report?.totalWithdrawals || 0}
                  precision={2}
                  prefix={<ArrowDownOutlined />}
                  suffix="₺"
                  valueStyle={{ color: '#cf1322' }}
                />
              </Card>
            </Col>
          </Row>

          <Card title={<Space><TransactionOutlined /> Kasa Hareketleri</Space>}>
            <Table
              columns={columns.filter(c => !c.hidden)}
              dataSource={report?.transactions || []}
              rowKey="transactionId"
              scroll={{ x: 'max-content' }}
              pagination={{ pageSize: 10 }}
            />
          </Card>
        </>
      )}
    </div>
  );
};

export default CashTransactionsTab;
