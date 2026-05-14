import React, { useState, useMemo } from 'react';
import { Table, DatePicker, Button, Space, Card, Typography, Spin, Alert, Row, Col, Statistic, Select, Dropdown } from 'antd';
import { 
  FileTextOutlined, 
  FileExcelOutlined, 
  InfoCircleOutlined, 
  DownOutlined,
  CloudDownloadOutlined 
} from '@ant-design/icons';
import dayjs from 'dayjs';
import { useProductionReport } from '../../hooks/useReports';
import { 
  exportProductionReportPdf, 
  exportProductionReportExcel,
  exportAllCategoriesPdf,
  exportAllCategoriesExcel
} from '../../utils/exportUtils';
import { formatDate } from '../../utils/formatters';
import type { ProductionReportRow } from '../../types/report';

const { Title, Text } = Typography;

const ProductionReport: React.FC = () => {
  const [selectedDate, setSelectedDate] = useState<string>(
    dayjs().format('YYYY-MM-DD')
  );
  const [selectedCategory, setSelectedCategory] = useState<string | undefined>(undefined);

  const { data: report, isLoading, isError } = useProductionReport(selectedDate);

  // Kategorileri report'tan dinamik türet
  const categories = useMemo(() => {
    if (!report) return [];
    return [...new Set(report.rows.map(r => r.categoryName))].sort();
  }, [report]);

  // Tabloda gösterilecek satırlar
  const filteredRows = useMemo(() => {
    if (!report) return [];
    return selectedCategory
      ? report.rows.filter(r => r.categoryName === selectedCategory)
      : report.rows;
  }, [report, selectedCategory]);

  // Filtrelenmiş toplam
  const filteredTotalQuantity = useMemo(() => {
    return filteredRows.reduce((sum, r) => sum + r.totalQuantity, 0);
  }, [filteredRows]);

  const handleExportPdf = () => {
    if (report) exportProductionReportPdf(report, selectedCategory);
  };

  const handleExportExcel = () => {
    if (report) exportProductionReportExcel(report, selectedCategory);
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

  const exportMenuItems = [
    {
      key: 'excel-all',
      icon: <FileExcelOutlined />,
      label: 'Tüm Kategoriler (Excel — tek dosya, ayrı sheet)',
      onClick: () => report && exportAllCategoriesExcel(report)
    },
    {
      key: 'pdf-all',
      icon: <FileTextOutlined />,
      label: 'Her Kategori Ayrı PDF',
      onClick: () => report && exportAllCategoriesPdf(report)
    }
  ];

  return (
    <div style={{ padding: '0 0 24px 0' }}>
      <Card bordered={false}>
        <Row justify="space-between" align="middle" style={{ marginBottom: 24 }}>
          <Col>
            <Space direction="vertical" size={0}>
              <Title level={3} style={{ margin: 0 }}>Üretim Raporu</Title>
              {report && (
                <Text type="secondary">{report.productionBranchName}</Text>
              )}
            </Space>
          </Col>
          <Col>
            <Space>
              <Button 
                icon={<FileExcelOutlined />} 
                onClick={handleExportExcel}
                disabled={!report || filteredRows.length === 0}
              >
                Excel
              </Button>
              <Button 
                icon={<FileTextOutlined />} 
                onClick={handleExportPdf}
                disabled={!report || filteredRows.length === 0}
              >
                PDF
              </Button>
              <Dropdown 
                menu={{ items: exportMenuItems }} 
                disabled={!report || report.rows.length === 0}
                placement="bottomRight"
              >
                <Button type="primary" icon={<CloudDownloadOutlined />}>
                  Tümünü İndir <DownOutlined />
                </Button>
              </Dropdown>
            </Space>
          </Col>
        </Row>

        <Space direction="vertical" size="middle" style={{ width: '100%' }}>
          <Row gutter={24} align="middle">
            <Col>
              <Space>
                <Text strong>Tarih:</Text>
                <DatePicker 
                  value={dayjs(selectedDate)} 
                  onChange={(date) => setSelectedDate(date?.format('YYYY-MM-DD') ?? dayjs().format('YYYY-MM-DD'))}
                  format="DD.MM.YYYY"
                  allowClear={false}
                />
              </Space>
            </Col>
            <Col>
              <Space>
                <Text strong>Kategori:</Text>
                <Select
                  style={{ width: 200 }}
                  placeholder="Tümü"
                  value={selectedCategory}
                  onChange={setSelectedCategory}
                  allowClear
                  onClear={() => setSelectedCategory(undefined)}
                >
                  {categories.map(cat => (
                    <Select.Option key={cat} value={cat}>{cat}</Select.Option>
                  ))}
                </Select>
              </Space>
            </Col>
          </Row>

          {report && (
            <Alert
              message={
                <span>
                  <strong>Dünkü onaylanan talepler gösteriliyor</strong> ({formatDate(report.demandDate)})
                </span>
              }
              type="info"
              showIcon
              icon={<InfoCircleOutlined />}
            />
          )}

          {isLoading ? (
            <div style={{ textAlign: 'center', padding: '50px 0' }}>
              <Spin size="large" tip="Rapor yükleniyor..." />
            </div>
          ) : isError ? (
            <Alert message="Rapor yüklenirken bir hata oluştu." type="error" />
          ) : report && report.rows.length > 0 ? (
            <>
              <Table
                dataSource={filteredRows}
                columns={columns}
                rowKey="productId"
                pagination={false}
                bordered
                scroll={{ x: 'max-content' }}
                summary={() => {
                  return (
                    <Table.Summary fixed>
                      <Table.Summary.Row style={{ backgroundColor: '#fafafa', fontWeight: 'bold' }}>
                        <Table.Summary.Cell index={0} colSpan={3}>
                          {selectedCategory ? `${selectedCategory.toUpperCase()} TOPLAM` : 'GENEL TOPLAM'}
                        </Table.Summary.Cell>
                        {report.salesBranches.map((branch, index) => {
                          const total = filteredRows.reduce((sum, row) => sum + (row.branchQuantities[branch.branchId] ?? 0), 0);
                          return (
                            <Table.Summary.Cell index={index + 3} key={branch.branchId} align="center">
                              {total}
                            </Table.Summary.Cell>
                          );
                        })}
                        <Table.Summary.Cell index={report.salesBranches.length + 3} align="center">
                          {filteredTotalQuantity}
                        </Table.Summary.Cell>
                      </Table.Summary.Row>
                    </Table.Summary>
                  );
                }}
              />
              
              <Row gutter={16} style={{ marginTop: 24 }}>
                <Col xs={24} sm={12}>
                  <Statistic 
                    title={selectedCategory ? `${selectedCategory} — Ürün Çeşidi` : "Toplam Ürün Çeşidi"}
                    value={filteredRows.length} 
                    prefix={<InfoCircleOutlined />} 
                  />
                </Col>
                <Col xs={24} sm={12}>
                  <Statistic 
                    title={selectedCategory ? `${selectedCategory} — Toplam Miktar` : "Toplam Üretilecek Adet/Birim"}
                    value={filteredTotalQuantity} 
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

export default ProductionReport;
