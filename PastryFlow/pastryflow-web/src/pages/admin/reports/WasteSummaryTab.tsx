import React, { useState, useMemo } from 'react';
import { 
  Table, Card, Row, Col, Statistic, DatePicker, Select, Button, Space, 
  Empty, Spin, Typography, Tooltip as AntTooltip 
} from 'antd';
import { 
  FileExcelOutlined, FilePdfOutlined, SearchOutlined,
  DeleteOutlined, WarningOutlined, DollarOutlined
} from '@ant-design/icons';
import { 
  PieChart, Pie, Cell, Tooltip, Legend, ResponsiveContainer 
} from 'recharts';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { useWasteSummaryReport } from '../../../hooks/useReports';
import { useAdminBranches, useAdminCategories } from '../../../hooks/useAdmin';
import { exportToExcel, exportToPdf, formatDateRangeForExport } from '../../../utils/exportUtils';
import { CHART_COLORS } from '../../../utils/constants';
import type { WasteSummaryItemDto } from '../../../types/report';

const { RangePicker } = DatePicker;

const WasteSummaryTab: React.FC = () => {
  const [dateRange, setDateRange] = useState<[string, string]>([
    dayjs().startOf('month').format('YYYY-MM-DD'),
    dayjs().format('YYYY-MM-DD')
  ]);
  const [branchId, setBranchId] = useState<string | undefined>(undefined);
  const [categoryId, setCategoryId] = useState<string | undefined>(undefined);
  
  const { data: branches } = useAdminBranches();
  const { data: categories } = useAdminCategories();

  const { data: report, isLoading } = useWasteSummaryReport({ 
    startDate: dateRange[0],
    endDate: dateRange[1],
    branchId,
    categoryId
  });

  const chartData = useMemo(() => {
    if (!report?.items) return [];
    
    const categoryTotals: Record<string, number> = {};
    report.items.forEach(item => {
      categoryTotals[item.categoryName] = (categoryTotals[item.categoryName] || 0) + item.totalQuantity;
    });
    
    return Object.entries(categoryTotals).map(([name, value]) => ({ name, value }));
  }, [report]);

  const columns: ColumnsType<WasteSummaryItemDto> = [
    {
      title: 'Ürün',
      dataIndex: 'productName',
      key: 'productName',
      sorter: (a, b) => a.productName.localeCompare(b.productName),
    },
    {
      title: 'Kategori',
      dataIndex: 'categoryName',
      key: 'categoryName',
    },
    {
      title: 'Birim',
      dataIndex: 'unitType',
      key: 'unitType',
    },
    {
      title: 'Toplam Miktar',
      dataIndex: 'totalQuantity',
      key: 'totalQuantity',
      align: 'right',
      sorter: (a, b) => a.totalQuantity - b.totalQuantity,
    },
    {
      title: 'Kayıt Sayısı',
      dataIndex: 'wasteCount',
      key: 'wasteCount',
      align: 'right',
    },
    {
      title: 'Tahmini Kayıp (₺)',
      dataIndex: 'estimatedLoss',
      key: 'estimatedLoss',
      align: 'right',
      render: (val) => val ? val.toLocaleString('tr-TR', { minimumFractionDigits: 2 }) : '-',
      sorter: (a, b) => (a.estimatedLoss || 0) - (b.estimatedLoss || 0),
    }
  ];

  const handleExportExcel = () => {
    if (!report) return;
    exportToExcel({
      fileName: `PastryFlow_ZayiatRaporu_${dateRange[0]}_${dateRange[1]}`,
      sheetName: 'Zayiat Özeti',
      title: 'Zayiat Özet Raporu',
      subtitle: `Tarih Aralığı: ${formatDateRangeForExport(dateRange[0], dateRange[1])}${report.branchName ? ` | Şube: ${report.branchName}` : ''}`,
      columns: [
        { header: 'Ürün', key: 'productName', width: 25 },
        { header: 'Kategori', key: 'categoryName', width: 15 },
        { header: 'Birim', key: 'unitType', width: 10 },
        { header: 'Toplam Miktar', key: 'totalQuantity', width: 15 },
        { header: 'Kayıt Sayısı', key: 'wasteCount', width: 15 },
        { header: 'Tahmini Kayıp (₺)', key: 'estimatedLoss', width: 18 },
      ],
      data: report.items,
    });
  };

  const handleExportPdf = () => {
    if (!report) return;
    exportToPdf({
      fileName: `PastryFlow_ZayiatRaporu_${dateRange[0]}_${dateRange[1]}`,
      title: 'Zayiat Özet Raporu',
      subtitle: `Tarih: ${formatDateRangeForExport(dateRange[0], dateRange[1])}${report.branchName ? ` | Şube: ${report.branchName}` : ''}`,
      columns: [
        { header: 'Ürün', dataKey: 'productName' },
        { header: 'Kategori', dataKey: 'categoryName' },
        { header: 'Birim', dataKey: 'unitType' },
        { header: 'Miktar', dataKey: 'totalQuantity', halign: 'right' },
        { header: 'Sayı', dataKey: 'wasteCount', halign: 'right' },
        { header: 'Kayıp (₺)', dataKey: 'estimatedLoss', halign: 'right' },
      ],
      data: report.items.map(item => ({
        ...item,
        estimatedLoss: item.estimatedLoss ? item.estimatedLoss.toFixed(2) : '0.00'
      })),
    });
  };

  return (
    <div style={{ padding: '24px 0' }}>
      <Card style={{ marginBottom: 24 }}>
        <Row gutter={[16, 16]} align="middle">
          <Col xs={24} md={8}>
            <RangePicker 
              style={{ width: '100%' }}
              value={[dayjs(dateRange[0]), dayjs(dateRange[1])]}
              onChange={(dates) => {
                if (dates) {
                  setDateRange([dates[0]!.format('YYYY-MM-DD'), dates[1]!.format('YYYY-MM-DD')]);
                }
              }}
            />
          </Col>
          <Col xs={24} md={4}>
            <Select
              style={{ width: '100%' }}
              placeholder="Tüm Şubeler"
              allowClear
              value={branchId}
              onChange={setBranchId}
              options={branches?.map(b => ({ label: b.name, value: b.id }))}
            />
          </Col>
          <Col xs={24} md={4}>
            <Select
              style={{ width: '100%' }}
              placeholder="Tüm Kategoriler"
              allowClear
              value={categoryId}
              onChange={setCategoryId}
              options={categories?.map(c => ({ label: c.name, value: c.id }))}
            />
          </Col>
          <Col xs={24} md={8}>
            <Space>
              <Button type="primary" icon={<SearchOutlined />} loading={isLoading}>Göster</Button>
              <Button icon={<FileExcelOutlined />} onClick={handleExportExcel} disabled={!report}>Excel</Button>
              <Button icon={<FilePdfOutlined />} onClick={handleExportPdf} disabled={!report}>PDF</Button>
            </Space>
          </Col>
        </Row>
      </Card>

      {isLoading ? (
        <div style={{ textAlign: 'center', padding: '100px' }}><Spin size="large" /></div>
      ) : !report || report.items.length === 0 ? (
        <Empty description="Veri bulunamadı" />
      ) : (
        <>
          <Row gutter={16} style={{ marginBottom: 24 }}>
            <Col xs={24} md={8}>
              <Card>
                <Statistic
                  title="Toplam Zayiat Miktarı"
                  value={report.totalWasteQuantity}
                  prefix={<DeleteOutlined />}
                  valueStyle={{ color: '#cf1322' }}
                />
              </Card>
            </Col>
            <Col xs={24} md={8}>
              <Card>
                <Statistic
                  title="Zayiat Kayıt Sayısı"
                  value={report.items.reduce((acc, i) => acc + i.wasteCount, 0)}
                  prefix={<WarningOutlined />}
                />
              </Card>
            </Col>
            <Col xs={24} md={8}>
              <Card>
                <Statistic
                  title="Tahmini Toplam Kayıp"
                  value={report.totalEstimatedLoss || 0}
                  precision={2}
                  prefix={<DollarOutlined />}
                  suffix="₺"
                />
              </Card>
            </Col>
          </Row>

          <Row gutter={24}>
            <Col xs={24} lg={14}>
              <Card title="Ürün Bazlı Zayiat">
                <Table
                  columns={columns}
                  dataSource={report.items}
                  rowKey="productId"
                  pagination={{ pageSize: 10 }}
                />
              </Card>
            </Col>
            <Col xs={24} lg={10}>
              <Card title="Kategori Dağılımı (Miktar)">
                <div style={{ height: 400 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <PieChart>
                      <Pie
                        data={chartData}
                        cx="50%"
                        cy="50%"
                        labelLine={true}
                        label={({ name, percent }) => `${name} (${((percent || 0) * 100).toFixed(0)}%)`}
                        outerRadius={120}
                        fill="#8884d8"
                        dataKey="value"
                      >
                        {chartData.map((_, index) => (
                          <Cell key={`cell-${index}`} fill={CHART_COLORS[index % CHART_COLORS.length]} />
                        ))}
                      </Pie>
                      <Tooltip />
                      <Legend />
                    </PieChart>
                  </ResponsiveContainer>
                </div>
              </Card>
            </Col>
          </Row>
        </>
      )}
    </div>
  );
};

export default WasteSummaryTab;
