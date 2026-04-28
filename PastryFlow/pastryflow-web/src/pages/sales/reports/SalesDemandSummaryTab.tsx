import React, { useState, useMemo } from 'react';
import { 
  Table, Card, Row, Col, Statistic, DatePicker, Button, Space, 
  Empty, Spin, Tag, Typography 
} from 'antd';
import { 
  CheckCircleOutlined, CloseCircleOutlined, InfoCircleOutlined,
  BarChartOutlined, FileExcelOutlined, FilePdfOutlined, SearchOutlined
} from '@ant-design/icons';
import { 
  LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, 
  ResponsiveContainer 
} from 'recharts';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { useDemandSummaryReport } from '../../../hooks/useReports';
import useAuth from '../../../hooks/useAuth';
import { exportToExcel, exportToPdf, formatDateRangeForExport } from '../../../utils/exportUtils';
import type { DemandSummaryItemDto } from '../../../types/report';

const { RangePicker } = DatePicker;
const { Text } = Typography;

const SalesDemandSummaryTab: React.FC = () => {
  const { user } = useAuth();
  const [dateRange, setDateRange] = useState<[string, string]>([
    dayjs().subtract(7, 'days').format('YYYY-MM-DD'),
    dayjs().format('YYYY-MM-DD')
  ]);
  
  const branchId = user?.branchId || undefined;

  const { data: report, isLoading } = useDemandSummaryReport({ 
    startDate: dateRange[0],
    endDate: dateRange[1],
    branchId
  }, !!branchId);

  const chartData = useMemo(() => {
    if (!report?.items) return [];
    
    const dailyStats: Record<string, { date: string, total: number, approved: number }> = {};
    
    report.items.forEach(item => {
      const date = dayjs(item.date).format('DD.MM');
      if (!dailyStats[date]) {
        dailyStats[date] = { date, total: 0, approved: 0 };
      }
      dailyStats[date].total += 1;
      if (['Approved', 'PartiallyApproved', 'Delivered', 'Received'].includes(item.status)) {
        dailyStats[date].approved += 1;
      }
    });
    
    return Object.values(dailyStats).sort((a, b) => a.date.localeCompare(b.date));
  }, [report]);

  const columns: ColumnsType<DemandSummaryItemDto> = [
    {
      title: 'Tarih',
      dataIndex: 'date',
      key: 'date',
      render: (date) => dayjs(date).format('DD.MM.YYYY'),
      sorter: (a, b) => dayjs(a.date).unix() - dayjs(b.date).unix(),
    },
    {
      title: 'Karşılayan Şube',
      dataIndex: 'toBranchName',
      key: 'toBranchName',
    },
    {
      title: 'Toplam Kalem',
      dataIndex: 'totalItems',
      key: 'totalItems',
      align: 'right',
    },
    {
      title: 'Onay',
      dataIndex: 'approvedItems',
      key: 'approvedItems',
      align: 'right',
      render: (val) => <Text type="success">{val}</Text>
    },
    {
      title: 'Durum',
      dataIndex: 'status',
      key: 'status',
      render: (status) => {
        let color = 'default';
        let label = status;
        switch (status) {
          case 'Pending': color = 'orange'; label = 'Bekliyor'; break;
          case 'Approved': color = 'green'; label = 'Onaylandı'; break;
          case 'Rejected': color = 'red'; label = 'Reddedildi'; break;
          case 'PartiallyApproved': color = 'blue'; label = 'Kısmen Onay'; break;
          case 'Delivered': color = 'cyan'; label = 'Yolda'; break;
          case 'Received': color = 'purple'; label = 'Teslim Alındı'; break;
        }
        return <Tag color={color}>{label}</Tag>;
      }
    }
  ];

  const handleExportExcel = () => {
    if (!report || !branchId) return;
    exportToExcel({
      fileName: `PastryFlow_TalepRaporu_${branchId}_${dateRange[0]}_${dateRange[1]}`,
      sheetName: 'Talep Özeti',
      title: 'Talep Özet Raporu',
      subtitle: `Tarih Aralığı: ${formatDateRangeForExport(dateRange[0], dateRange[1])}`,
      columns: [
        { header: 'Tarih', key: 'date', width: 15 },
        { header: 'Karşılayan Şube', key: 'toBranchName', width: 25 },
        { header: 'Toplam Kalem', key: 'totalItems', width: 12 },
        { header: 'Onaylanan', key: 'approvedItems', width: 12 },
        { header: 'Durum', key: 'status', width: 15 },
      ],
      data: report.items.map(i => ({ ...i, date: dayjs(i.date).format('DD.MM.YYYY') })),
    });
  };

  const handleExportPdf = () => {
    if (!report || !branchId) return;
    exportToPdf({
      fileName: `PastryFlow_TalepRaporu_${branchId}_${dateRange[0]}_${dateRange[1]}`,
      title: 'Talep Özet Raporu',
      subtitle: `Tarih: ${formatDateRangeForExport(dateRange[0], dateRange[1])}`,
      columns: [
        { header: 'Tarih', dataKey: 'date' },
        { header: 'Alan', dataKey: 'toBranchName' },
        { header: 'Kalem', dataKey: 'totalItems', halign: 'right' },
        { header: 'Onay', dataKey: 'approvedItems', halign: 'right' },
        { header: 'Durum', dataKey: 'status' },
      ],
      data: report.items.map(i => ({ 
        ...i, 
        date: dayjs(i.date).format('DD.MM.YYYY') 
      })),
    });
  };

  return (
    <div style={{ padding: '24px 0' }}>
      <Card style={{ marginBottom: 24 }}>
        <Row gutter={[16, 16]} align="middle">
          <Col xs={24} md={12}>
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
          <Col xs={24} md={12}>
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
            <Col xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title="Toplam Talep"
                  value={report.totalDemands}
                  prefix={<InfoCircleOutlined />}
                />
              </Card>
            </Col>
            <Col xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title="Onaylanan"
                  value={report.totalApproved}
                  prefix={<CheckCircleOutlined />}
                  valueStyle={{ color: '#52c41a' }}
                />
              </Card>
            </Col>
            <Col xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title="Reddedilen"
                  value={report.totalRejected}
                  prefix={<CloseCircleOutlined />}
                  valueStyle={{ color: '#f5222d' }}
                />
              </Card>
            </Col>
            <Col xs={24} sm={12} md={6}>
              <Card>
                <Statistic
                  title="Onay Oranı"
                  value={report.approvalRate}
                  precision={1}
                  suffix="%"
                  prefix={<BarChartOutlined />}
                  valueStyle={{ color: report.approvalRate >= 80 ? '#52c41a' : report.approvalRate >= 50 ? '#faad14' : '#f5222d' }}
                />
              </Card>
            </Col>
          </Row>

          <Row gutter={24}>
            <Col xs={24} lg={16}>
              <Card title="Talep Listesi">
                <Table
                  columns={columns}
                  dataSource={report.items}
                  rowKey={(record) => `${record.date}-${record.fromBranchId}-${record.toBranchId}`}
                  pagination={{ pageSize: 10 }}
                />
              </Card>
            </Col>
            <Col xs={24} lg={8}>
              <Card title="Günlük Talep Trendi">
                <div style={{ height: 400 }}>
                  <ResponsiveContainer width="100%" height="100%">
                    <LineChart data={chartData}>
                      <CartesianGrid strokeDasharray="3 3" />
                      <XAxis dataKey="date" />
                      <YAxis />
                      <Tooltip />
                      <Legend />
                      <Line type="monotone" dataKey="total" name="Toplam Talep" stroke="#1890ff" strokeWidth={2} />
                      <Line type="monotone" dataKey="approved" name="Onaylanan" stroke="#52c41a" strokeWidth={2} />
                    </LineChart>
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

export default SalesDemandSummaryTab;
