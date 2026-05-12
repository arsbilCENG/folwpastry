import React, { useState, useMemo } from 'react';
import { 
  Table, Card, Row, Col, Statistic, DatePicker, Button, Space, 
  Spin, Typography, Select, Tag
} from 'antd';
import { 
  SearchOutlined, DollarOutlined, CalendarOutlined, 
  InfoCircleOutlined, ShopOutlined
} from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { usePurchaseReport } from '../../hooks/useReportsLegacy';
import { useAdminBranches } from '../../hooks/useAdmin';
import type { PurchaseReportItem, PurchaseReportItemDetail } from '../../types/reportLegacy';

const { RangePicker } = DatePicker;
const { Text, Title } = Typography;

interface PurchasesTabProps {
  branchId?: string;
  showBranchFilter?: boolean;
}

const PurchasesTab: React.FC<PurchasesTabProps> = ({ branchId: initialBranchId, showBranchFilter = false }) => {
  const [dates, setDates] = useState<[dayjs.Dayjs, dayjs.Dayjs]>([
    dayjs().startOf('month'),
    dayjs()
  ]);
  const [selectedBranchId, setSelectedBranchId] = useState<string | undefined>(initialBranchId);

  const { data: branches } = useAdminBranches();
  const { data: report, isLoading } = usePurchaseReport(
    selectedBranchId,
    dates[0].format('YYYY-MM-DD'),
    dates[1].format('YYYY-MM-DD')
  );

  const columns: ColumnsType<PurchaseReportItem> = [
    {
      title: 'Tarih',
      dataIndex: 'purchaseDate',
      key: 'purchaseDate',
      render: (date) => dayjs(date).format('DD.MM.YYYY'),
      sorter: (a, b) => dayjs(a.purchaseDate).unix() - dayjs(b.purchaseDate).unix(),
    },
    {
      title: 'Fiş No',
      dataIndex: 'purchaseNumber',
      key: 'purchaseNumber',
      render: (text) => <Text strong>{text}</Text>,
    },
    {
      title: 'Şube',
      dataIndex: 'branchName',
      key: 'branchName',
      hidden: !showBranchFilter,
    },
    {
      title: 'Ödeme',
      dataIndex: 'paymentMethodLabel',
      key: 'paymentMethodLabel',
      render: (label) => (
        <Tag color={label === 'Nakit' ? 'green' : 'blue'}>
          {label}
        </Tag>
      ),
    },
    {
      title: 'Tutar',
      dataIndex: 'totalAmount',
      key: 'totalAmount',
      align: 'right',
      render: (val) => <Text strong>₺ {val.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}</Text>,
      sorter: (a, b) => a.totalAmount - b.totalAmount,
    },
    {
      title: 'Notlar',
      dataIndex: 'notes',
      key: 'notes',
      ellipsis: true,
    }
  ];

  const expandedRowRender = (record: PurchaseReportItem) => {
    const itemColumns: ColumnsType<PurchaseReportItemDetail> = [
      { title: 'Ürün Adı', dataIndex: 'itemName', key: 'itemName' },
      { title: 'Miktar', dataIndex: 'quantity', key: 'quantity', align: 'right' },
      { title: 'Birim', dataIndex: 'unit', key: 'unit' },
      { 
        title: 'Birim Fiyat', 
        dataIndex: 'unitPrice', 
        key: 'unitPrice', 
        align: 'right',
        render: (val) => `₺ ${val.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}`
      },
      { 
        title: 'Toplam', 
        dataIndex: 'totalPrice', 
        key: 'totalPrice', 
        align: 'right',
        render: (val) => <Text strong>₺ {val.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}</Text>
      },
    ];

    return (
      <Table 
        columns={itemColumns} 
        dataSource={record.items} 
        pagination={false} 
        size="small" 
        rowKey={(item) => `${record.purchaseId}-${item.itemName}`}
      />
    );
  };

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
                  title="Toplam Harcama"
                  value={report?.totalExpense || 0}
                  precision={2}
                  prefix={<DollarOutlined />}
                  suffix="₺"
                  valueStyle={{ color: '#cf1322' }}
                />
              </Card>
            </Col>
            <Col xs={24} sm={8}>
              <Card>
                <Statistic
                  title="Nakit Ödeme"
                  value={report?.cashExpense || 0}
                  precision={2}
                  prefix={<DollarOutlined />}
                  suffix="₺"
                />
              </Card>
            </Col>
            <Col xs={24} sm={8}>
              <Card>
                <Statistic
                  title="Kartlı Ödeme"
                  value={report?.cardExpense || 0}
                  precision={2}
                  prefix={<DollarOutlined />}
                  suffix="₺"
                />
              </Card>
            </Col>
          </Row>

          <Card title={<Space><ShopOutlined /> Satın Alım Listesi</Space>}>
            <Table
              columns={columns.filter(c => !c.hidden)}
              dataSource={report?.purchases || []}
              rowKey="purchaseId"
              expandable={{ expandedRowRender }}
              scroll={{ x: 'max-content' }}
              pagination={{ pageSize: 10 }}
            />
          </Card>
        </>
      )}
    </div>
  );
};

export default PurchasesTab;
