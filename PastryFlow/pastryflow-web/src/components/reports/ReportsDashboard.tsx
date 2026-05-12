import React, { useEffect, useMemo, useState } from 'react';
import {
  Alert,
  Button,
  Card,
  Col,
  DatePicker,
  Row,
  Select,
  Space,
  Table,
  Tabs,
  Tag,
  Typography,
  message,
} from 'antd';
import type { ColumnsType } from 'antd/es/table';
import {
  CalendarOutlined,
  DollarOutlined,
  FileExcelOutlined,
  FilePdfOutlined,
  ShoppingCartOutlined,
  ShopOutlined,
  TransactionOutlined,
  WalletOutlined,
  WarningOutlined,
  DeleteOutlined,
} from '@ant-design/icons';
import dayjs, { type Dayjs } from 'dayjs';
import { useAdminBranches } from '../../hooks/useAdmin';
import { useDailySummary, useManagementReport, usePeriodSummary } from '../../hooks/useReports';
import { useWalletSummary } from '../../hooks/useWallet';
import type {
  DailyProductSaleDto,
  DailySummaryReport,
  PeriodDailyRow,
  PeriodPreset,
  PeriodProductSummary,
  WalletMovementDto,
} from '../../types/report';
import { formatCurrency, formatDate } from '../../utils/formatters';
import { getDateRange } from '../../utils/reportPeriod';
import {
  exportDailySummaryExcel,
  exportDailySummaryPdf,
  exportManagementExcel,
  exportManagementPdf,
  exportPeriodSummaryExcel,
  exportPeriodSummaryPdf,
  type WalletBalanceExportRow,
} from '../../utils/exportUtils';

const { Title, Text } = Typography;
const { RangePicker } = DatePicker;

export interface ReportsDashboardProps {
  pageTitle: string;
  /** Satış kullanıcısı şube id */
  branchId?: string;
  /** Admin modunda Sales şubeleri seçimi */
  adminMode?: boolean;
}

const counterRowStyle = { backgroundColor: 'rgba(24, 144, 255, 0.08)' };

const cashDiffStyle = (v: number): React.CSSProperties => {
  if (v > 0) return { color: '#389e0d' };
  if (v < 0) return { color: '#cf1322' };
  return { color: '#8c8c8c' };
};

const movementTagColor = (label: string): string => {
  if (label.includes('Çekim')) return 'orange';
  if (label.includes('Gönderim')) return 'green';
  return 'gold';
};

const productColumns: ColumnsType<DailyProductSaleDto> = [
  { title: 'Kategori', dataIndex: 'categoryName', key: 'categoryName' },
  { title: 'Ürün', dataIndex: 'productName', key: 'productName' },
  {
    title: 'Tür',
    key: 'type',
    render: (_, r) => (
      <Tag color={r.isCounter ? 'blue' : 'green'}>{r.isCounter ? 'Sayaç' : 'Stok'}</Tag>
    ),
  },
  {
    title: 'Miktar',
    dataIndex: 'soldQuantity',
    key: 'soldQuantity',
    align: 'right',
    render: (q: number) => q.toLocaleString('tr-TR', { maximumFractionDigits: 2 }),
  },
  {
    title: 'Birim Fiyat',
    dataIndex: 'unitPrice',
    key: 'unitPrice',
    align: 'right',
    render: (p?: number) => (p != null ? formatCurrency(p) : '—'),
  },
  {
    title: 'Gelir',
    dataIndex: 'revenue',
    key: 'revenue',
    align: 'right',
    render: (v: number) => formatCurrency(v),
  },
];

const ReportsDashboard: React.FC<ReportsDashboardProps> = ({
  pageTitle,
  branchId: propBranchId,
  adminMode = false,
}) => {
  const { data: branchesData } = useAdminBranches();
  const salesBranches = useMemo(
    () => (branchesData ?? []).filter((b) => b.branchType === 'Sales' && b.isActive),
    [branchesData]
  );

  const [adminBranchId, setAdminBranchId] = useState<string | undefined>();

  useEffect(() => {
    if (!adminMode) return;
    if (adminBranchId) return;
    if (salesBranches.length > 0) setAdminBranchId(salesBranches[0].id);
  }, [adminMode, adminBranchId, salesBranches]);

  const effectiveBranchId = adminMode ? adminBranchId : propBranchId;

  const [dailyDate, setDailyDate] = useState<Dayjs>(() => dayjs());

  const dailyDateStr = dailyDate.format('YYYY-MM-DD');
  const { data: dailyReport, isLoading: dailyLoading } = useDailySummary(
    effectiveBranchId,
    dailyDateStr
  );

  const [periodPreset, setPeriodPreset] = useState<PeriodPreset>('this_month');
  const [customRange, setCustomRange] = useState<[Dayjs, Dayjs]>(() => getDateRange('this_month'));

  const [periodRange, setPeriodRange] = useState<[Dayjs, Dayjs]>(() => getDateRange('this_month'));

  useEffect(() => {
    if (periodPreset === 'custom') {
      setPeriodRange(customRange);
    } else {
      setPeriodRange(getDateRange(periodPreset));
    }
  }, [periodPreset, customRange]);

  const periodStart = periodRange[0].format('YYYY-MM-DD');
  const periodEnd = periodRange[1].format('YYYY-MM-DD');

  const { data: periodReport, isLoading: periodLoading } = usePeriodSummary(
    effectiveBranchId,
    periodStart,
    periodEnd
  );

  const [mgmtPreset, setMgmtPreset] = useState<PeriodPreset>('this_month');
  const [mgmtCustomRange, setMgmtCustomRange] = useState<[Dayjs, Dayjs]>(() =>
    getDateRange('this_month')
  );
  const [mgmtRange, setMgmtRange] = useState<[Dayjs, Dayjs]>(() => getDateRange('this_month'));

  useEffect(() => {
    if (mgmtPreset === 'custom') {
      setMgmtRange(mgmtCustomRange);
    } else {
      setMgmtRange(getDateRange(mgmtPreset));
    }
  }, [mgmtPreset, mgmtCustomRange]);

  const mgmtStart = mgmtRange[0].format('YYYY-MM-DD');
  const mgmtEnd = mgmtRange[1].format('YYYY-MM-DD');

  const { data: managementReport, isLoading: mgmtLoading } = useManagementReport(mgmtStart, mgmtEnd);
  const { data: walletSummary, isLoading: walletLoading } = useWalletSummary();

  const walletExportRows: WalletBalanceExportRow[] = useMemo(() => {
    if (!walletSummary) return [];
    const rows: WalletBalanceExportRow[] = walletSummary.branches.map((b) => ({
      branchName: b.branchName,
      cashBalance: b.cashBalance,
      bankBalance: b.bankBalance,
      totalBalance: b.totalBalance,
    }));
    rows.push({
      branchName: 'Admin',
      cashBalance: walletSummary.admin.cashBalance,
      bankBalance: walletSummary.admin.bankBalance,
      totalBalance: walletSummary.admin.totalBalance,
    });
    rows.push({
      branchName: 'TOPLAM',
      cashBalance: walletSummary.grandTotalCash,
      bankBalance: walletSummary.grandTotalBank,
      totalBalance: walletSummary.grandTotal,
    });
    return rows;
  }, [walletSummary]);

  const [excelLoading, setExcelLoading] = useState(false);

  const runExcel = async (fn: () => Promise<void>) => {
    setExcelLoading(true);
    try {
      await fn();
    } catch (e) {
      console.error(e);
      message.error('Excel oluşturulamadı.');
    } finally {
      setExcelLoading(false);
    }
  };

  const dailyExportPdf = async (r: DailySummaryReport) => {
    try {
      await exportDailySummaryPdf(r);
    } catch (e) {
      console.error(e);
      message.error('PDF oluşturulamadı.');
    }
  };

  const periodExportPdf = async () => {
    if (!periodReport) return;
    try {
      await exportPeriodSummaryPdf(periodReport);
    } catch (e) {
      console.error(e);
      message.error('PDF oluşturulamadı.');
    }
  };

  const mgmtExportPdf = async () => {
    if (!managementReport) return;
    try {
      await exportManagementPdf(managementReport, walletExportRows);
    } catch (e) {
      console.error(e);
      message.error('PDF oluşturulamadı.');
    }
  };

  const periodDailyColumns: ColumnsType<PeriodDailyRow> = [
    { title: 'Tarih', dataIndex: 'date', key: 'date', render: (d: string) => formatDate(d) },
    {
      title: 'Ürün Satış',
      dataIndex: 'productSalesRevenue',
      key: 'productSalesRevenue',
      align: 'right',
      render: (v: number) => formatCurrency(v),
    },
    {
      title: 'Sayaç Satış',
      dataIndex: 'counterSalesRevenue',
      key: 'counterSalesRevenue',
      align: 'right',
      render: (v: number) => formatCurrency(v),
    },
    {
      title: 'Toplam',
      dataIndex: 'totalSalesRevenue',
      key: 'totalSalesRevenue',
      align: 'right',
      render: (v: number) => formatCurrency(v),
    },
    {
      title: 'Satın Alım',
      dataIndex: 'purchaseExpense',
      key: 'purchaseExpense',
      align: 'right',
      render: (v: number) => formatCurrency(v),
    },
    {
      title: 'Kasa Farkı',
      dataIndex: 'cashDifference',
      key: 'cashDifference',
      align: 'right',
      render: (v: number) => <span style={cashDiffStyle(v)}>{formatCurrency(v)}</span>,
    },
  ];

  const periodProductColumns: ColumnsType<PeriodProductSummary> = [
    { title: 'Kategori', dataIndex: 'categoryName', key: 'categoryName' },
    { title: 'Ürün', dataIndex: 'productName', key: 'productName' },
    {
      title: 'Tür',
      key: 'type',
      render: (_, r) => (
        <Tag color={r.isCounter ? 'blue' : 'green'}>{r.isCounter ? 'Sayaç' : 'Stok'}</Tag>
      ),
    },
    {
      title: 'Toplam Miktar',
      dataIndex: 'totalSoldQuantity',
      key: 'totalSoldQuantity',
      align: 'right',
      render: (q: number) => q.toLocaleString('tr-TR', { maximumFractionDigits: 2 }),
    },
    {
      title: 'Toplam Gelir',
      dataIndex: 'totalRevenue',
      key: 'totalRevenue',
      align: 'right',
      render: (v: number) => formatCurrency(v),
    },
  ];

  const movementColumns: ColumnsType<WalletMovementDto> = [
    {
      title: 'Tarih',
      dataIndex: 'transactionDate',
      key: 'transactionDate',
      render: (d: string) => formatDate(d),
    },
    { title: 'Şube', dataIndex: 'branchName', key: 'branchName' },
    {
      title: 'İşlem',
      dataIndex: 'transactionTypeLabel',
      key: 'transactionTypeLabel',
      render: (t: string) => <Tag color={movementTagColor(t)}>{t}</Tag>,
    },
    { title: 'Yöntem', dataIndex: 'walletTypeLabel', key: 'walletTypeLabel' },
    {
      title: 'Tutar',
      dataIndex: 'amount',
      key: 'amount',
      align: 'right',
      render: (v: number) => formatCurrency(v),
    },
    { title: 'Açıklama', dataIndex: 'description', key: 'description', ellipsis: true },
    { title: 'Yapan', dataIndex: 'createdByName', key: 'createdByName' },
  ];

  const renderPresetGroup = (
    preset: PeriodPreset,
    setPreset: (p: PeriodPreset) => void,
    rangeValue: [Dayjs, Dayjs],
    onRangeChange: (r: [Dayjs, Dayjs]) => void
  ) => (
    <Space direction="vertical" size="middle" style={{ width: '100%' }}>
      <Button.Group>
        {(
          [
            ['this_week', 'Bu Hafta'],
            ['this_month', 'Bu Ay'],
            ['last_month', 'Geçen Ay'],
            ['custom', 'Özel Aralık'],
          ] as const
        ).map(([key, label]) => (
          <Button
            key={key}
            type={preset === key ? 'primary' : 'default'}
            onClick={() => setPreset(key)}
          >
            {label}
          </Button>
        ))}
      </Button.Group>
      {preset === 'custom' && (
        <RangePicker
          value={rangeValue}
          onChange={(v) => {
            if (v?.[0] && v?.[1]) {
              onRangeChange([v[0], v[1]]);
              setPreset('custom');
            }
          }}
          format="DD.MM.YYYY"
        />
      )}
    </Space>
  );

  const renderDailyTab = () => (
    <Space direction="vertical" size="large" style={{ width: '100%' }}>
      <DatePicker
        value={dailyDate}
        onChange={(d) => d && setDailyDate(d)}
        format="DD.MM.YYYY"
        disabledDate={(d) => d.isAfter(dayjs(), 'day')}
        allowClear={false}
      />

      {!effectiveBranchId && (
        <Alert type="warning" showIcon message="Şube seçin veya şube bilgisi yüklenemedi." />
      )}

      {effectiveBranchId && dailyLoading && <Text type="secondary">Yükleniyor…</Text>}

      {effectiveBranchId && dailyReport && !dailyReport.isClosed && (
        <Alert
          type="warning"
          showIcon
          message="Bu gün henüz kapatılmamış. Raporlar sadece kapatılmış günleri gösterir."
        />
      )}

      {effectiveBranchId && dailyReport?.isClosed && (
        <>
          <Row gutter={[16, 16]}>
            <Col xs={24} sm={12} lg={8}>
              <Card size="small">
                <Space>
                  <DollarOutlined style={{ fontSize: 24, color: '#52c41a' }} />
                  <div>
                    <Text type="secondary">Toplam Satış</Text>
                    <Title level={4} style={{ margin: 0 }}>
                      {formatCurrency(dailyReport.totalSalesRevenue)}
                    </Title>
                  </div>
                </Space>
              </Card>
            </Col>
            {dailyReport.counterSalesRevenue > 0 && (
              <Col xs={24} sm={12} lg={8}>
                <Card size="small">
                  <Space>
                    <ShopOutlined style={{ fontSize: 24, color: '#1890ff' }} />
                    <div>
                      <Text type="secondary">Sayaç Satış</Text>
                      <Title level={4} style={{ margin: 0 }}>
                        {formatCurrency(dailyReport.counterSalesRevenue)}
                      </Title>
                    </div>
                  </Space>
                </Card>
              </Col>
            )}
            <Col xs={24} sm={12} lg={8}>
              <Card size="small">
                <Space>
                  <ShoppingCartOutlined style={{ fontSize: 24, color: '#fa8c16' }} />
                  <div>
                    <Text type="secondary">Satın Alım</Text>
                    <Title level={4} style={{ margin: 0 }}>
                      {formatCurrency(dailyReport.totalPurchaseExpense)}
                    </Title>
                    <Text type="secondary" style={{ fontSize: 12 }}>
                      Nakit: {formatCurrency(dailyReport.cashPurchaseExpense)} · Kart:{' '}
                      {formatCurrency(dailyReport.cardPurchaseExpense)}
                    </Text>
                  </div>
                </Space>
              </Card>
            </Col>
            <Col xs={24} sm={12} lg={8}>
              <Card size="small">
                <Space>
                  <WalletOutlined style={{ fontSize: 24 }} />
                  <div>
                    <Text type="secondary">Kasa Farkı</Text>
                    <Title level={4} style={{ margin: 0, ...cashDiffStyle(dailyReport.cashDifference) }}>
                      {formatCurrency(dailyReport.cashDifference)}
                      {dailyReport.cashDifference !== 0 && <WarningOutlined style={{ marginLeft: 8 }} />}
                    </Title>
                  </div>
                </Space>
              </Card>
            </Col>
            <Col xs={24} sm={12} lg={8}>
              <Card size="small">
                <Space>
                  <DeleteOutlined style={{ fontSize: 24, color: '#8c8c8c' }} />
                  <div>
                    <Text type="secondary">Zayiat</Text>
                    <Title level={4} style={{ margin: 0 }}>
                      {dailyReport.wasteItemCount} kalem
                    </Title>
                    <Text type="secondary" style={{ fontSize: 12 }}>
                      Toplam: {dailyReport.totalWasteQuantity.toLocaleString('tr-TR')} birim
                    </Text>
                  </div>
                </Space>
              </Card>
            </Col>
          </Row>

          <Table<DailyProductSaleDto>
            rowKey={(r, i) => `${r.productName}-${i}`}
            columns={productColumns}
            dataSource={dailyReport.productSales}
            pagination={false}
            scroll={{ x: 'max-content' }}
            onRow={(record) =>
              record.isCounter ? { style: counterRowStyle } : {}
            }
            summary={() => (
              <Table.Summary fixed>
                <Table.Summary.Row>
                  <Table.Summary.Cell index={0} colSpan={5} align="right">
                    <Text strong>Ürün Satışları</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={1} align="right">
                    <Text strong>{formatCurrency(dailyReport.productSalesRevenue)}</Text>
                  </Table.Summary.Cell>
                </Table.Summary.Row>
                {dailyReport.counterSalesRevenue > 0 && (
                  <Table.Summary.Row>
                    <Table.Summary.Cell index={0} colSpan={5} align="right">
                      <Text strong>Sayaç Satışları</Text>
                    </Table.Summary.Cell>
                    <Table.Summary.Cell index={1} align="right">
                      <Text strong>{formatCurrency(dailyReport.counterSalesRevenue)}</Text>
                    </Table.Summary.Cell>
                  </Table.Summary.Row>
                )}
                <Table.Summary.Row>
                  <Table.Summary.Cell index={0} colSpan={5} align="right">
                    <Text strong>Toplam</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={1} align="right">
                    <Text strong>{formatCurrency(dailyReport.totalSalesRevenue)}</Text>
                  </Table.Summary.Cell>
                </Table.Summary.Row>
              </Table.Summary>
            )}
          />

          {dailyReport.wastes.length > 0 && (
            <Table
              rowKey={(w, i) => `${w.productName}-${i}`}
              columns={[
                { title: 'Ürün', dataIndex: 'productName', key: 'productName' },
                { title: 'Kategori', dataIndex: 'categoryName', key: 'categoryName' },
                {
                  title: 'Miktar',
                  dataIndex: 'quantity',
                  key: 'quantity',
                  align: 'right',
                  render: (q: number) => q.toLocaleString('tr-TR', { maximumFractionDigits: 2 }),
                },
                { title: 'Birim', dataIndex: 'unit', key: 'unit' },
                { title: 'Tip', dataIndex: 'wasteTypeLabel', key: 'wasteTypeLabel' },
                { title: 'Sebep', dataIndex: 'reason', key: 'reason', ellipsis: true },
              ]}
              dataSource={dailyReport.wastes}
              pagination={false}
              scroll={{ x: 'max-content' }}
            />
          )}

          <Space>
            <Button
              icon={<FileExcelOutlined />}
              loading={excelLoading}
              onClick={() => dailyReport && runExcel(() => exportDailySummaryExcel(dailyReport))}
            >
              Excel İndir
            </Button>
            <Button icon={<FilePdfOutlined />} onClick={() => dailyReport && void dailyExportPdf(dailyReport)}>
              PDF İndir
            </Button>
          </Space>
        </>
      )}
    </Space>
  );

  const renderPeriodTab = () => (
    <Space direction="vertical" size="large" style={{ width: '100%' }}>
      {renderPresetGroup(periodPreset, setPeriodPreset, customRange, setCustomRange)}

      {!effectiveBranchId && (
        <Alert type="warning" showIcon message="Şube seçin veya şube bilgisi yüklenemedi." />
      )}

      {effectiveBranchId && periodLoading && <Text type="secondary">Yükleniyor…</Text>}

      {effectiveBranchId && periodReport && (
        <>
          <Row gutter={[16, 16]}>
            <Col xs={24} sm={8}>
              <Card size="small">
                <CalendarOutlined /> <Text type="secondary"> Kapalı Gün</Text>
                <Title level={4} style={{ margin: '8px 0 0' }}>
                  {periodReport.closedDayCount} gün
                </Title>
              </Card>
            </Col>
            <Col xs={24} sm={8}>
              <Card size="small">
                <DollarOutlined style={{ color: '#52c41a' }} /> <Text type="secondary"> Toplam Ciro</Text>
                <Title level={4} style={{ margin: '8px 0 0' }}>
                  {formatCurrency(periodReport.totalSalesRevenue)}
                </Title>
              </Card>
            </Col>
            <Col xs={24} sm={8}>
              <Card size="small">
                <ShoppingCartOutlined style={{ color: '#fa8c16' }} /> <Text type="secondary"> Toplam Gider</Text>
                <Title level={4} style={{ margin: '8px 0 0' }}>
                  {formatCurrency(periodReport.totalPurchaseExpense)}
                </Title>
              </Card>
            </Col>
          </Row>

          <Table<PeriodDailyRow>
            rowKey={(r) => r.date}
            columns={periodDailyColumns}
            dataSource={periodReport.dailyRows}
            pagination={false}
            scroll={{ x: 'max-content' }}
            summary={() => (
              <Table.Summary fixed>
                <Table.Summary.Row>
                  <Table.Summary.Cell index={0}>
                    <Text strong>TOPLAM</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={1} align="right">
                    <Text strong>
                      {formatCurrency(
                        periodReport.dailyRows.reduce((s, r) => s + r.productSalesRevenue, 0)
                      )}
                    </Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={2} align="right">
                    <Text strong>
                      {formatCurrency(
                        periodReport.dailyRows.reduce((s, r) => s + r.counterSalesRevenue, 0)
                      )}
                    </Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={3} align="right">
                    <Text strong>{formatCurrency(periodReport.totalSalesRevenue)}</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={4} align="right">
                    <Text strong>{formatCurrency(periodReport.totalPurchaseExpense)}</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={5} align="right">
                    <Text strong style={cashDiffStyle(periodReport.totalCashDifference)}>
                      {formatCurrency(periodReport.totalCashDifference)}
                    </Text>
                  </Table.Summary.Cell>
                </Table.Summary.Row>
              </Table.Summary>
            )}
          />

          <Table<PeriodProductSummary>
            rowKey={(r, i) => `${r.productName}-${i}`}
            columns={periodProductColumns}
            dataSource={periodReport.productSummaries}
            pagination={false}
            scroll={{ x: 'max-content' }}
            onRow={(record) => (record.isCounter ? { style: counterRowStyle } : {})}
            summary={() => (
              <Table.Summary fixed>
                <Table.Summary.Row>
                  <Table.Summary.Cell index={0} colSpan={4} align="right">
                    <Text strong>TOPLAM</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={1} align="right">
                    <Text strong>
                      {formatCurrency(
                        periodReport.productSummaries.reduce((s, p) => s + p.totalRevenue, 0)
                      )}
                    </Text>
                  </Table.Summary.Cell>
                </Table.Summary.Row>
              </Table.Summary>
            )}
          />

          <Space>
            <Button
              icon={<FileExcelOutlined />}
              loading={excelLoading}
              onClick={() => periodReport && runExcel(() => exportPeriodSummaryExcel(periodReport))}
            >
              Excel İndir
            </Button>
            <Button icon={<FilePdfOutlined />} onClick={() => void periodExportPdf()}>
              PDF İndir
            </Button>
          </Space>
        </>
      )}
    </Space>
  );

  const renderManagementTab = () => (
    <Space direction="vertical" size="large" style={{ width: '100%' }}>
      {renderPresetGroup(mgmtPreset, setMgmtPreset, mgmtCustomRange, setMgmtCustomRange)}

      {(mgmtLoading || walletLoading) && <Text type="secondary">Yükleniyor…</Text>}

      {managementReport && (
        <>
          <Table
            rowKey={(b) => b.branchName}
            columns={[
              { title: 'Şube', dataIndex: 'branchName', key: 'branchName' },
              {
                title: 'Toplam Ciro',
                dataIndex: 'totalSalesRevenue',
                key: 'totalSalesRevenue',
                align: 'right',
                render: (v: number) => formatCurrency(v),
              },
              {
                title: 'Satın Alım',
                dataIndex: 'totalPurchaseExpense',
                key: 'totalPurchaseExpense',
                align: 'right',
                render: (v: number) => formatCurrency(v),
              },
              {
                title: 'Kasa Farkı',
                dataIndex: 'totalCashDifference',
                key: 'totalCashDifference',
                align: 'right',
                render: (v: number) => formatCurrency(v),
              },
              {
                title: 'Net',
                dataIndex: 'netRevenue',
                key: 'netRevenue',
                align: 'right',
                render: (v: number) => formatCurrency(v),
              },
              { title: 'Kapalı Gün', dataIndex: 'closedDayCount', key: 'closedDayCount', align: 'right' },
            ]}
            dataSource={managementReport.branchComparisons}
            pagination={false}
            scroll={{ x: 'max-content' }}
            summary={() => (
              <Table.Summary fixed>
                <Table.Summary.Row style={{ background: '#e6f7ff' }}>
                  <Table.Summary.Cell index={0}>
                    <Text strong>TOPLAM</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={1} align="right">
                    <Text strong>{formatCurrency(managementReport.grandTotalRevenue)}</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={2} align="right">
                    <Text strong>{formatCurrency(managementReport.grandTotalExpense)}</Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={3} align="right">
                    <Text strong>
                      {formatCurrency(
                        managementReport.branchComparisons.reduce(
                          (s, b) => s + b.totalCashDifference,
                          0
                        )
                      )}
                    </Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={4} align="right">
                    <Text strong>
                      {formatCurrency(
                        managementReport.grandTotalRevenue - managementReport.grandTotalExpense
                      )}
                    </Text>
                  </Table.Summary.Cell>
                  <Table.Summary.Cell index={5} align="right">
                    <Text strong>
                      {managementReport.branchComparisons.reduce((s, b) => s + b.closedDayCount, 0)}
                    </Text>
                  </Table.Summary.Cell>
                </Table.Summary.Row>
              </Table.Summary>
            )}
          />

          <Card title="Kasa Bakiyeleri (anlık)" size="small">
            {walletSummary ? (
              <Table
                rowKey={(r) => r.branchName}
                columns={[
                  { title: 'Şube', dataIndex: 'branchName', key: 'branchName' },
                  {
                    title: 'Nakit',
                    dataIndex: 'cashBalance',
                    key: 'cashBalance',
                    align: 'right',
                    render: (v: number) => formatCurrency(v),
                  },
                  {
                    title: 'Banka',
                    dataIndex: 'bankBalance',
                    key: 'bankBalance',
                    align: 'right',
                    render: (v: number) => formatCurrency(v),
                  },
                  {
                    title: 'Toplam',
                    dataIndex: 'totalBalance',
                    key: 'totalBalance',
                    align: 'right',
                    render: (v: number) => formatCurrency(v),
                  },
                ]}
                dataSource={[
                  ...walletSummary.branches.map((b) => ({
                    branchName: b.branchName,
                    cashBalance: b.cashBalance,
                    bankBalance: b.bankBalance,
                    totalBalance: b.totalBalance,
                  })),
                  {
                    branchName: 'Admin',
                    cashBalance: walletSummary.admin.cashBalance,
                    bankBalance: walletSummary.admin.bankBalance,
                    totalBalance: walletSummary.admin.totalBalance,
                  },
                ]}
                pagination={false}
                scroll={{ x: 'max-content' }}
                summary={() => (
                  <Table.Summary fixed>
                    <Table.Summary.Row style={{ background: '#f0f0f0' }}>
                      <Table.Summary.Cell index={0}>
                        <Text strong>TOPLAM</Text>
                      </Table.Summary.Cell>
                      <Table.Summary.Cell index={1} align="right">
                        <Text strong>{formatCurrency(walletSummary.grandTotalCash)}</Text>
                      </Table.Summary.Cell>
                      <Table.Summary.Cell index={2} align="right">
                        <Text strong>{formatCurrency(walletSummary.grandTotalBank)}</Text>
                      </Table.Summary.Cell>
                      <Table.Summary.Cell index={3} align="right">
                        <Text strong>{formatCurrency(walletSummary.grandTotal)}</Text>
                      </Table.Summary.Cell>
                    </Table.Summary.Row>
                  </Table.Summary>
                )}
              />
            ) : (
              <Text type="secondary">Cüzdan özeti yüklenemedi.</Text>
            )}
          </Card>

          <Title level={5}>Kasa Hareketleri</Title>
          <Table<WalletMovementDto>
            rowKey={(m, i) => `${m.transactionDate}-${i}`}
            columns={movementColumns}
            dataSource={managementReport.walletMovements}
            pagination={{ pageSize: 20 }}
            scroll={{ x: 'max-content' }}
          />

          <Space>
            <Button
              icon={<FileExcelOutlined />}
              loading={excelLoading}
              onClick={() =>
                runExcel(() => exportManagementExcel(managementReport, walletExportRows))
              }
            >
              Excel İndir
            </Button>
            <Button icon={<FilePdfOutlined />} onClick={() => void mgmtExportPdf()}>
              PDF İndir
            </Button>
          </Space>
        </>
      )}
    </Space>
  );

  const tabItems = [
    {
      key: 'daily',
      label: (
        <span>
          <CalendarOutlined /> Günlük Özet
        </span>
      ),
      children: renderDailyTab(),
    },
    {
      key: 'period',
      label: (
        <span>
          <TransactionOutlined /> Dönem Raporu
        </span>
      ),
      children: renderPeriodTab(),
    },
    ...(adminMode
      ? [
          {
            key: 'management',
            label: (
              <span>
                <DollarOutlined /> Yönetim Paneli
              </span>
            ),
            children: renderManagementTab(),
          },
        ]
      : []),
  ];

  return (
    <div style={{ padding: 24 }}>
      <div style={{ marginBottom: 24 }}>
        <Title level={2}>{pageTitle}</Title>
      </div>

      {adminMode && (
        <Card size="small" style={{ marginBottom: 16 }}>
          <Space wrap>
            <Text strong>Şube:</Text>
            <Select
              style={{ minWidth: 220 }}
              placeholder="Şube seçin"
              value={adminBranchId}
              onChange={(v) => setAdminBranchId(v)}
              options={salesBranches.map((b) => ({ label: b.name, value: b.id }))}
            />
          </Space>
        </Card>
      )}

      <Card>
        <Tabs defaultActiveKey="daily" items={tabItems} />
      </Card>
    </div>
  );
};

export default ReportsDashboard;
