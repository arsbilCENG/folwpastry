import React, { useState, useMemo } from 'react';
import { 
  Table, Card, Row, Col, Statistic, DatePicker, Button, Space, 
  Empty, Spin, Typography 
} from 'antd';
import { 
  FileExcelOutlined, FilePdfOutlined, SearchOutlined, 
  ShoppingCartOutlined, DeleteOutlined, DollarOutlined, ProductOutlined
} from '@ant-design/icons';
import { 
  BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, 
  ResponsiveContainer, Cell 
} from 'recharts';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { useDailySalesReport } from '../../../hooks/useReports';
import useAuth from '../../../hooks/useAuth';
import { exportToExcel, exportToPdf, formatDateForExport } from '../../../utils/exportUtils';
import { CHART_COLORS } from '../../../utils/constants';
import type { DailySalesItemDto } from '../../../types/report';

const { Text } = Typography;

const SalesDailySalesTab: React.FC = () => {
  const { user } = useAuth();
  const [date, setDate] = useState<string>(dayjs().format('YYYY-MM-DD'));
  
  const branchId = user?.branchId || undefined;

  const { data: report, isLoading } = useDailySalesReport({ 
    date, 
    branchId 
  }, !!branchId);

  const chartData = useMemo(() => {
    if (!report?.items) return [];
    
    const categoryTotals: Record<string, number> = {};
    report.items.forEach(item => {
      categoryTotals[item.categoryName] = (categoryTotals[item.categoryName] || 0) + item.calculatedSales;
    });
    
    return Object.entries(categoryTotals).map(([name, value]) => ({ name, value }));
  }, [report]);

  const columns: ColumnsType<DailySalesItemDto> = [
    {
      title: 'Ürün',
      dataIndex: 'productName',
      key: 'productName',
      fixed: 'left',
      width: 150,
      sorter: (a, b) => a.productName.localeCompare(b.productName),
    },
    {
      title: 'Kategori',
      dataIndex: 'categoryName',
      key: 'categoryName',
      width: 120,
    },
    {
      title: 'Açılış',
      dataIndex: 'openingStock',
      key: 'openingStock',
      align: 'right',
      width: 100,
    },
    {
      title: 'Gelen',
      children: [
        {
          title: 'Talep',
          dataIndex: 'receivedFromDemand',
          key: 'receivedFromDemand',
          align: 'right',
          width: 80,
        },
        {
          title: 'Trans.',
          dataIndex: 'receivedFromTransfer',
          key: 'receivedFromTransfer',
          align: 'right',
          width: 80,
        },
      ]
    },
    {
      title: 'Giden',
      dataIndex: 'sentByTransfer',
      key: 'sentByTransfer',
      align: 'right',
      width: 80,
    },
    {
      title: 'Zayiat',
      dataIndex: 'wasteQuantity',
      key: 'wasteQuantity',
      align: 'right',
      width: 80,
      render: (val) => val > 0 ? <Text type="danger">{val}</Text> : val,
    },
    {
      title: 'Sayım',
      dataIndex: 'endOfDayCount',
      key: 'endOfDayCount',
      align: 'right',
      width: 100,
    },
    {
      title: 'Satış',
      dataIndex: 'calculatedSales',
      key: 'calculatedSales',
      align: 'right',
      width: 100,
      render: (val) => <Text strong>{val}</Text>
    },
    {
      title: 'Tutar (₺)',
      dataIndex: 'salesValue',
      key: 'salesValue',
      align: 'right',
      width: 120,
      render: (val) => val ? val.toLocaleString('tr-TR', { minimumFractionDigits: 2 }) : '-',
    }
  ];

  const handleExportExcel = () => {
    if (!report || !branchId) return;
    
    exportToExcel({
      fileName: `PastryFlow_GunlukSatis_${branchId}_${date.replace(/-/g, '')}`,
      sheetName: 'Günlük Satış Raporu',
      title: `${report.branchName} - Günlük Satış Raporu`,
      subtitle: `Tarih: ${formatDateForExport(date)}`,
      columns: [
        { header: 'Ürün', key: 'productName', width: 25 },
        { header: 'Kategori', key: 'categoryName', width: 15 },
        { header: 'Açılış Stoku', key: 'openingStock', width: 12 },
        { header: 'Gelen Talep', key: 'receivedFromDemand', width: 12 },
        { header: 'Zayiat', key: 'wasteQuantity', width: 10 },
        { header: 'Gün Sonu Sayım', key: 'endOfDayCount', width: 14 },
        { header: 'Hesaplanan Satış', key: 'calculatedSales', width: 15 },
        { header: 'Satış Tutarı (₺)', key: 'salesValue', width: 14 },
      ],
      data: report.items,
    });
  };

  const handleExportPdf = () => {
    if (!report || !branchId) return;
    
    exportToPdf({
      fileName: `PastryFlow_GunlukSatis_${branchId}_${date.replace(/-/g, '')}`,
      title: 'Günlük Satış Raporu',
      subtitle: `${report.branchName} | Tarih: ${formatDateForExport(date)}`,
      columns: [
        { header: 'Ürün', dataKey: 'productName' },
        { header: 'Kat.', dataKey: 'categoryName' },
        { header: 'Açılış', dataKey: 'openingStock', halign: 'right' },
        { header: 'Zayiat', dataKey: 'wasteQuantity', halign: 'right' },
        { header: 'Sayım', dataKey: 'endOfDayCount', halign: 'right' },
        { header: 'Satış', dataKey: 'calculatedSales', halign: 'right' },
        { header: 'Tutar', dataKey: 'salesValue', halign: 'right' },
      ],
      data: report.items.map(item => ({
        ...item,
        salesValue: item.salesValue ? item.salesValue.toFixed(2) : '0.00'
      })),
    });
  };

  return (
    <div style={{ padding: '24px 0' }}>
      <Card bodyStyle={{ padding: '16px 24px' }} style={{ marginBottom: 24 }}>
        <Row gutter={[16, 16]} align="middle">
          <Col xs={24} md={6}>
            <DatePicker 
              style={{ width: '100%' }} 
              value={dayjs(date)}
              onChange={(d) => setDate(d ? d.format('YYYY-MM-DD') : dayjs().format('YYYY-MM-DD'))}
              placeholder="Tarih Seçin"
            />
          </Col>
          <Col xs={24} md={18}>
            <Space>
              <Button 
                type="primary" 
                icon={<SearchOutlined />} 
                loading={isLoading}
                onClick={() => {}} 
              >
                Göster
              </Button>
              <Button 
                icon={<FileExcelOutlined />} 
                onClick={handleExportExcel}
                disabled={!report}
              >
                Excel
              </Button>
              <Button 
                icon={<FilePdfOutlined />} 
                onClick={handleExportPdf}
                disabled={!report}
              >
                PDF
              </Button>
            </Space>
          </Col>
        </Row>
      </Card>

      {isLoading ? (
        <div style={{ textAlign: 'center', padding: '100px' }}>
          <Spin size="large" tip="Rapor yükleniyor..." />
        </div>
      ) : !report || report.items.length === 0 ? (
        <Empty description="Bu tarih için veri bulunamadı" />
      ) : (
        <>
          <Row gutter={16} style={{ marginBottom: 24 }}>
            <Col xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title="Toplam Satış"
                  value={report.totalCalculatedSales}
                  prefix={<ShoppingCartOutlined />}
                  valueStyle={{ color: '#3f8600' }}
                />
              </Card>
            </Col>
            <Col xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title="Toplam Zayiat"
                  value={report.totalWaste}
                  prefix={<DeleteOutlined />}
                  valueStyle={{ color: '#cf1322' }}
                />
              </Card>
            </Col>
            <Col xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title="Satış Tutarı"
                  value={report.totalSalesValue || 0}
                  precision={2}
                  prefix={<DollarOutlined />}
                  suffix="₺"
                />
              </Card>
            </Col>
            <Col xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title="Ürün Sayısı"
                  value={report.items.length}
                  prefix={<ProductOutlined />}
                />
              </Card>
            </Col>
          </Row>

          <Row gutter={24} style={{ marginBottom: 24 }}>
            <Col xs={24} lg={16}>
              <Card title="Satış Detayları">
                <Table
                  columns={columns}
                  dataSource={report.items}
                  rowKey="productId"
                  scroll={{ x: 1000 }}
                  pagination={{ pageSize: 10 }}
                  bordered
                />
              </Card>
            </Col>
            <Col xs={24} lg={8}>
              <Card title="Kategori Bazlı Satış">
                <div style={{ height: 400 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <BarChart data={chartData} layout="vertical" margin={{ left: 20 }}>
                      <CartesianGrid strokeDasharray="3 3" />
                      <XAxis type="number" />
                      <YAxis dataKey="name" type="category" width={80} />
                      <Tooltip />
                      <Bar dataKey="value" name="Satış Miktarı">
                        {chartData.map((_, index) => (
                          <Cell key={`cell-${index}`} fill={CHART_COLORS[index % CHART_COLORS.length]} />
                        ))}
                      </Bar>
                    </BarChart>
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

export default SalesDailySalesTab;
