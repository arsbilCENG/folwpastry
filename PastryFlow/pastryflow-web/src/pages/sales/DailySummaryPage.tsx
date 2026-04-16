import React, { useEffect, useState } from 'react';
import { Table, DatePicker, message, Card, Row, Col, Typography, Spin } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { dayClosingApi } from '../../api/dayClosingApi';
import useAuth from '../../hooks/useAuth';
import { DayClosingSummary, DailySummaryItem } from '../../types/dayClosing';

const { Title, Text } = Typography;

const DailySummaryPage: React.FC = () => {
  const [data, setData] = useState<DayClosingSummary | null>(null);
  const [loading, setLoading] = useState(false);
  const [selectedDate, setSelectedDate] = useState(dayjs());
  const { user } = useAuth();

  const fetchSummary = async (date: string) => {
    if (!user?.branchId) return;
    setLoading(true);
    try {
      const res = await dayClosingApi.getSummary(user.branchId, date);
      if (res.success && res.data && res.data.isClosed) {
        setData(res.data);
      } else {
        setData(null);
      }
    } catch (err) {
      setData(null);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchSummary(selectedDate.format('YYYY-MM-DD'));
  }, [selectedDate, user]);

  const columns: ColumnsType<DailySummaryItem> = [
    { title: 'Kategori', dataIndex: 'categoryName', key: 'categoryName' },
    { title: 'Ürün', dataIndex: 'productName', key: 'productName' },
    { title: 'Açılış', dataIndex: 'openingStock', key: 'openingStock' },
    { title: 'Gelen', dataIndex: 'receivedFromDemands', key: 'receivedFromDemands' },
    { title: 'Zayiat(Gün)', dataIndex: 'dayWaste', key: 'dayWaste' },
    { title: 'Kalan(Gece)', dataIndex: 'endOfDayCount', key: 'endOfDayCount' },
    { title: 'Zayiat(Gece)', dataIndex: 'endOfDayWaste', key: 'endOfDayWaste', render: v => <Text type={v > 0 ? 'danger' : 'secondary'}>{v}</Text> },
    { title: 'Devir', dataIndex: 'carryOver', key: 'carryOver', render: v => <Text strong>{v}</Text> },
    { title: 'Satış', dataIndex: 'calculatedSales', key: 'calculatedSales', render: v => <Text style={{ color: '#52c41a', fontWeight: 'bold' }}>{v}</Text> },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={2}>Günlük Rapor (Z Raporu)</Title>
        <DatePicker 
          value={selectedDate} 
          onChange={(date) => date && setSelectedDate(date)} 
          allowClear={false} 
        />
      </div>

      {loading ? (
        <Spin size="large" />
      ) : data ? (
        <>
          <Row gutter={16} style={{ marginBottom: 24 }}>
            <Col span={8}>
              <Card>
                <Statistic title="Toplam Satış Kalemi" value={data.totals.totalSales} valueStyle={{ color: '#3f8600' }} />
              </Card>
            </Col>
            <Col span={8}>
              <Card>
                <Statistic title="Toplam Zayiat Miktarı" value={data.totals.totalWaste} valueStyle={{ color: '#cf1322' }} />
              </Card>
            </Col>
            <Col span={8}>
              <Card>
                <Statistic title="Yarına Devreden" value={data.totals.totalCarryOver} />
              </Card>
            </Col>
          </Row>

          <Table 
            columns={columns} 
            dataSource={data.items} 
            rowKey="productId" 
            pagination={false}
            scroll={{ y: 600 }}
          />
        </>
      ) : (
        <Card>
          <Text type="secondary">Seçilen tarihe ait kapatılmış bir rapor bulunamadı. Gün henüz kapanmamış olabilir.</Text>
        </Card>
      )}
    </div>
  );
};

// Antd missing import fix:
import { Statistic } from 'antd';

export default DailySummaryPage;
