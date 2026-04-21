import React, { useState, useMemo } from 'react';
import { 
  Table, Card, Row, Col, DatePicker, Radio, Button, Space, 
  Empty, Spin, Typography 
} from 'antd';
import { 
  FileExcelOutlined, FilePdfOutlined, SearchOutlined,
  BarChartOutlined, LineChartOutlined
} from '@ant-design/icons';
import { 
  BarChart, Bar, LineChart, Line, XAxis, YAxis, CartesianGrid, 
  Tooltip, Legend, ResponsiveContainer, Cell 
} from 'recharts';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { useBranchComparisonReport } from '../../../hooks/useReports';
import { exportToExcel, exportToPdf, formatDateRangeForExport } from '../../../utils/exportUtils';
import { CHART_COLORS } from '../../../utils/constants';
import type { BranchComparisonItemDto } from '../../../types/report';

const { RangePicker } = DatePicker;

const BranchComparisonTab: React.FC = () => {
  const [dateRange, setDateRange] = useState<[string, string]>([
    dayjs().subtract(7, 'days').format('YYYY-MM-DD'),
    dayjs().format('YYYY-MM-DD')
  ]);
  const [metric, setMetric] = useState<'sales' | 'waste' | 'demand'>('sales');
  
  const { data: report, isLoading } = useBranchComparisonReport({ 
    startDate: dateRange[0],
    endDate: dateRange[1],
    metric
  });

  const barChartData = useMemo(() => {
    if (!report?.items) return [];
    return report.items.map(item => ({
      name: item.branchName,
      value: item.total
    })).sort((a, b) => b.value - a.value);
  }, [report]);

  const lineChartData = useMemo(() => {
    if (!report?.items || report.items.length === 0) return [];
    
    const dates = new Set<string>();
    report.items.forEach(item => {
      item.dailyData.forEach(d => dates.add(d.date));
    });
    
    return Array.from(dates).sort().map(date => {
      const dataPoint: any = { date: dayjs(date).format('DD.MM') };
      report.items.forEach(item => {
        const daily = item.dailyData.find(d => d.date === date);
        dataPoint[item.branchName] = daily ? daily.value : 0;
      });
      return dataPoint;
    });
  }, [report]);

  const columns: ColumnsType<BranchComparisonItemDto> = [
    {
      title: 'Şube',
      dataIndex: 'branchName',
      key: 'branchName',
      sorter: (a, b) => a.branchName.localeCompare(b.branchName),
    },
    {
      title: 'Toplam',
      dataIndex: 'total',
      key: 'total',
      align: 'right',
      sorter: (a, b) => a.total - b.total,
      render: (val) => val.toLocaleString('tr-TR'),
    },
    {
      title: 'Günlük Ortalama',
      key: 'average',
      align: 'right',
      render: (_, record) => {
        const days = record.dailyData.length || 1;
        return (record.total / days).toFixed(1);
      }
    },
    {
      title: 'En Yüksek Gün',
      key: 'peak',
      align: 'right',
      render: (_, record) => {
        if (!record.dailyData.length) return '-';
        const peak = [...record.dailyData].sort((a, b) => b.value - a.value)[0];
        return `${dayjs(peak.date).format('DD.MM')} (${peak.value})`;
      }
    }
  ];

  const handleExportExcel = () => {
    if (!report) return;
    const metricLabel = metric === 'sales' ? 'Satış' : metric === 'waste' ? 'Zayiat' : 'Talep';
    exportToExcel({
      fileName: `PastryFlow_SubeKarsilastirma_${metric}_${dateRange[0]}`,
      sheetName: 'Şube Karşılaştırma',
      title: `Şube Karşılaştırma Raporu (${metricLabel})`,
      subtitle: `Tarih Aralığı: ${formatDateRangeForExport(dateRange[0], dateRange[1])}`,
      columns: [
        { header: 'Şube', key: 'branchName', width: 25 },
        { header: 'Toplam', key: 'total', width: 15 },
        { header: 'Günlük Veri Sayısı', key: 'dataCount', width: 18 },
      ],
      data: report.items.map(i => ({ 
        ...i, 
        dataCount: i.dailyData.length 
      })),
    });
  };

  const handleExportPdf = () => {
    if (!report) return;
    const metricLabel = metric === 'sales' ? 'Satış' : metric === 'waste' ? 'Zayiat' : 'Talep';
    exportToPdf({
      fileName: `PastryFlow_SubeKarsilastirma_${metric}_${dateRange[0]}`,
      title: 'Şube Karşılaştırma Raporu',
      subtitle: `${metricLabel} | Tarih: ${formatDateRangeForExport(dateRange[0], dateRange[1])}`,
      columns: [
        { header: 'Şube', dataKey: 'branchName' },
        { header: 'Toplam', dataKey: 'total', halign: 'right' },
        { header: 'Günlük Ort.', dataKey: 'avg', halign: 'right' },
      ],
      data: report.items.map(i => ({ 
        ...i, 
        avg: (i.total / (i.dailyData.length || 1)).toFixed(1) 
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
          <Col xs={24} md={8}>
            <Radio.Group 
              value={metric} 
              onChange={e => setMetric(e.target.value)}
              buttonStyle="solid"
            >
              <Radio.Button value="sales">Satış</Radio.Button>
              <Radio.Button value="waste">Zayiat</Radio.Button>
              <Radio.Button value="demand">Talep</Radio.Button>
            </Radio.Group>
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
          <Row gutter={24} style={{ marginBottom: 24 }}>
            <Col xs={24} lg={12}>
              <Card title="Şube Bazlı Toplam" extra={<BarChartOutlined />}>
                <div style={{ height: 350 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <BarChart data={barChartData} margin={{ bottom: 40 }}>
                      <CartesianGrid strokeDasharray="3 3" />
                      <XAxis dataKey="name" angle={-45} textAnchor="end" height={60} />
                      <YAxis />
                      <Tooltip />
                      <Bar dataKey="value" name="Toplam Miktar">
                        {barChartData.map((_, index) => (
                          <Cell key={`cell-${index}`} fill={CHART_COLORS[index % CHART_COLORS.length]} />
                        ))}
                      </Bar>
                    </BarChart>
                  </ResponsiveContainer>
                </div>
              </Card>
            </Col>
            <Col xs={24} lg={12}>
              <Card title="Günlük Trend (Tüm Şubeler)" extra={<LineChartOutlined />}>
                <div style={{ height: 350 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <LineChart data={lineChartData}>
                      <CartesianGrid strokeDasharray="3 3" />
                      <XAxis dataKey="date" />
                      <YAxis />
                      <Tooltip />
                      <Legend />
                      {report.items.map((item, index) => (
                        <Line 
                          key={item.branchId}
                          type="monotone" 
                          dataKey={item.branchName} 
                          stroke={CHART_COLORS[index % CHART_COLORS.length]} 
                          strokeWidth={2}
                          dot={{ r: 4 }}
                        />
                      ))}
                    </LineChart>
                  </ResponsiveContainer>
                </div>
              </Card>
            </Col>
          </Row>

          <Card title="Karşılaştırma Tablosu">
            <Table
              columns={columns}
              dataSource={report.items}
              rowKey="branchId"
              pagination={false}
              bordered
            />
          </Card>
        </>
      )}
    </div>
  );
};

export default BranchComparisonTab;
