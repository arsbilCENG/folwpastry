import React, { useState } from 'react';
import { Table, DatePicker, Button, Space, Card, Typography, Spin, Alert, Row, Col, Select, Statistic } from 'antd';
import { FileTextOutlined, FileExcelOutlined, InfoCircleOutlined, ShopOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import { useQuery } from '@tanstack/react-query';
import axiosClient from '../../api/axiosClient';
import { useProductionReport } from '../../hooks/useReports';
import { exportProductionReportPdf, exportProductionReportExcel } from '../../utils/exportUtils';
import { formatDate } from '../../utils/formatters';
import type { ProductionReportRow } from '../../types/report';

const { Title, Text } = Typography;
const { Option } = Select;

const AdminProductionReport: React.FC = () => {
  const [selectedDate, setSelectedDate] = useState<string>(
    dayjs().format('YYYY-MM-DD')
  );
  const [selectedBranchId, setSelectedBranchId] = useState<string | undefined>(undefined);

  // Fetch all branches to filter production ones
  const { data: branchesResponse } = useQuery({
    queryKey: ['admin', 'branches'],
    queryFn: () => axiosClient.get('/admin/branches'),
  });

  const productionBranches = (branchesResponse?.data ?? []).filter(
    (b: any) => b.branchType === 'Production' || b.branchType === 1
  );

  const { data: report, isLoading, isError } = useProductionReport(
    selectedDate, 
    selectedBranchId
  );

  const handleExportPdf = () => {
    if (report) exportProductionReportPdf(report);
  };

  const handleExportExcel = () => {
    if (report) exportProductionReportExcel(report);
  };

  const columns = [
    { 
      title: 'Kategori', 
      dataIndex: 'categoryName', 
      key: 'categoryName',
      width: 120,
      fixed: 'left' as const
    },
    { 
      title: 'Ürün', 
      dataIndex: 'productName', 
      key: 'productName',
      width: 200,
      fixed: 'left' as const
    },
    { 
      title: 'Birim', 
      dataIndex: 'unit', 
      key: 'unit',
      width: 80, 
      align: 'center' as const 
    },
    ...(report?.salesBranches ?? []).map(branch => ({
      title: branch.branchName,
      key: branch.branchId,
      width: 120,
      align: 'center' as const,
      render: (_: any, row: ProductionReportRow) => {
        const qty = row.branchQuantities[branch.branchId] ?? 0;
        return qty > 0 ? qty : <Text type="secondary">—</Text>;
      }
    })),
    {
      title: 'TOPLAM',
      dataIndex: 'totalQuantity',
      key: 'totalQuantity',
      width: 100,
      align: 'center' as const,
      fixed: 'right' as const,
      render: (val: number) => <Text strong>{val}</Text>
    }
  ];

  return (
    <div style={{ padding: '0 0 24px 0' }}>
      <Card bordered={false}>
        <Row justify="space-between" align="middle" style={{ marginBottom: 24 }}>
          <Col>
            <Space direction="vertical" size={0}>
              <Title level={3} style={{ margin: 0 }}>Üretim Raporu (Genel)</Title>
              <Text type="secondary">Tüm üretim şubeleri için planlama raporu</Text>
            </Space>
          </Col>
          <Col>
            <Space>
              <Button 
                icon={<FileExcelOutlined />} 
                onClick={handleExportExcel}
                disabled={!report || report.rows.length === 0}
              >
                Excel İndir
              </Button>
              <Button 
                type="primary"
                icon={<FileTextOutlined />} 
                onClick={handleExportPdf}
                disabled={!report || report.rows.length === 0}
              >
                PDF İndir
              </Button>
            </Space>
          </Col>
        </Row>

        <Space direction="vertical" size="large" style={{ width: '100%' }}>
          <Card size="small" type="inner" style={{ backgroundColor: '#fafafa' }}>
            <Row gutter={[24, 16]} align="middle">
              <Col xs={24} sm={12} md={8}>
                <Space direction="vertical" style={{ width: '100%' }}>
                  <Text strong>Üretim Şubesi Seçin:</Text>
                  <Select
                    placeholder="Şube seçin"
                    style={{ width: '100%' }}
                    onChange={setSelectedBranchId}
                    value={selectedBranchId}
                    suffixIcon={<ShopOutlined />}
                  >
                    {productionBranches.map((b: any) => (
                      <Option key={b.id} value={b.id}>{b.name}</Option>
                    ))}
                  </Select>
                </Space>
              </Col>
              <Col xs={24} sm={12} md={8}>
                <Space direction="vertical" style={{ width: '100%' }}>
                  <Text strong>Tarih Seçimi:</Text>
                  <DatePicker 
                    style={{ width: '100%' }}
                    value={dayjs(selectedDate)} 
                    onChange={(date) => setSelectedDate(date?.format('YYYY-MM-DD') ?? dayjs().format('YYYY-MM-DD'))}
                    format="DD.MM.YYYY"
                    allowClear={false}
                  />
                </Space>
              </Col>
            </Row>
          </Card>

          {!selectedBranchId ? (
            <div style={{ textAlign: 'center', padding: '40px 0' }}>
              <Alert 
                message="Şube Seçimi Gerekli" 
                description="Lütfen raporu görüntülemek istediğiniz üretim şubesini yukarıdan seçin." 
                type="info" 
                showIcon 
              />
            </div>
          ) : isLoading ? (
            <div style={{ textAlign: 'center', padding: '50px 0' }}>
              <Spin size="large" tip="Rapor yükleniyor..." />
            </div>
          ) : isError ? (
            <Alert message="Rapor yüklenirken bir hata oluştu." type="error" />
          ) : report && report.rows.length > 0 ? (
            <>
              <Alert
                message={
                  <span>
                    <strong>Dünkü onaylanan talepler gösteriliyor</strong> ({formatDate(report.demandDate)})
                  </span>
                }
                type="info"
                showIcon
                icon={<InfoCircleOutlined />}
                style={{ marginBottom: 16 }}
              />

              <Table
                dataSource={report.rows}
                columns={columns}
                rowKey="productId"
                pagination={false}
                bordered
                scroll={{ x: 'max-content' }}
                summary={() => {
                  return (
                    <Table.Summary fixed>
                      <Table.Summary.Row style={{ backgroundColor: '#fafafa', fontWeight: 'bold' }}>
                        <Table.Summary.Cell index={0} colSpan={3}>TOPLAM</Table.Summary.Cell>
                        {report.salesBranches.map((branch, index) => {
                          const total = report.rows.reduce((sum, row) => sum + (row.branchQuantities[branch.branchId] ?? 0), 0);
                          return (
                            <Table.Summary.Cell index={index + 3} key={branch.branchId} align="center">
                              {total}
                            </Table.Summary.Cell>
                          );
                        })}
                        <Table.Summary.Cell index={report.salesBranches.length + 3} align="center">
                          {report.totalQuantity}
                        </Table.Summary.Cell>
                      </Table.Summary.Row>
                    </Table.Summary>
                  );
                }}
              />
              
              <Row gutter={16} style={{ marginTop: 24 }}>
                <Col span={8}>
                  <Statistic 
                    title="Toplam Ürün Çeşidi" 
                    value={report.totalProductCount} 
                    prefix={<InfoCircleOutlined />} 
                  />
                </Col>
                <Col span={8}>
                  <Statistic 
                    title="Toplam Üretilecek Adet/Birim" 
                    value={report.totalQuantity} 
                    valueStyle={{ color: '#3f8600' }}
                  />
                </Col>
              </Row>
            </>
          ) : (
            <Alert 
              message={`${formatDate(report?.demandDate ?? '')} tarihinde onaylanan talep bulunamadı.`} 
              type="warning" 
            />
          )}
        </Space>
      </Card>
    </div>
  );
};

export default AdminProductionReport;
