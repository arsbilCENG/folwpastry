import React, { useEffect, useState } from 'react';
import { Table, DatePicker, message, Tag } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import dayjs from 'dayjs';
import { stockApi } from '../../api/stockApi';
import useAuth from '../../hooks/useAuth';
import { CurrentStock } from '../../types/stock';

const CurrentStockPage: React.FC = () => {
  const [data, setData] = useState<CurrentStock[]>([]);
  const [loading, setLoading] = useState(false);
  const [selectedDate, setSelectedDate] = useState(dayjs());
  const { user } = useAuth();

  const fetchStock = async (date: string) => {
    if (!user?.branchId) return;
    setLoading(true);
    try {
      const res = await stockApi.getCurrentStock(user.branchId, date);
      if (res.success && res.data) {
        setData(res.data);
      }
    } catch (err) {
      message.error('Stok bilgisi alınamadı.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchStock(selectedDate.format('YYYY-MM-DD'));
    
    // Auto refresh every 30 seconds if looking at today
    let interval: any;
    if (selectedDate.format('YYYY-MM-DD') === dayjs().format('YYYY-MM-DD')) {
      interval = setInterval(() => {
         fetchStock(dayjs().format('YYYY-MM-DD'));
      }, 30000);
    }
    
    return () => {
      if (interval) clearInterval(interval);
    };
  }, [selectedDate, user]);

  const columns: ColumnsType<CurrentStock> = [
    {
      title: 'Kategori',
      dataIndex: 'categoryName',
      key: 'categoryName',
      onCell: (_, index) => {
        // Group by category visual trick can be done here, simple version is just rendering it.
        return {};
      }
    },
    {
      title: 'Ürün Adı',
      dataIndex: 'productName',
      key: 'productName',
    },
    {
      title: 'Birim',
      dataIndex: 'unitName',
      key: 'unitName',
    },
    {
      title: 'Açılış',
      dataIndex: 'openingStock',
      key: 'openingStock',
    },
    {
      title: 'Gelen',
      dataIndex: 'receivedFromDemands',
      key: 'receivedFromDemands',
    },
    {
      title: 'Zayiat',
      dataIndex: 'dayWaste',
      key: 'dayWaste',
    },
    {
      title: 'Mevcut Stok',
      dataIndex: 'currentStock',
      key: 'currentStock',
      render: (val: number) => {
        let color = '';
        if (val === 0) color = 'red';
        else if (val < 5) color = 'orange';
        
        return <Tag color={color} style={{ fontSize: 14, padding: '4px 8px' }}>{val}</Tag>;
      }
    }
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <h2>Mevcut Stok</h2>
        <DatePicker 
          value={selectedDate} 
          onChange={(date) => date && setSelectedDate(date)} 
          allowClear={false} 
        />
      </div>
      <Table 
        columns={columns} 
        dataSource={data} 
        rowKey="productId" 
        loading={loading}
        pagination={false}
        scroll={{ y: 600 }}
      />
    </div>
  );
};

export default CurrentStockPage;
