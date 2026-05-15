import React, { useEffect, useState, useCallback, useMemo } from 'react';
import {
  Form,
  Button,
  InputNumber,
  Collapse,
  Typography,
  message,
  Spin,
  Row,
  Col,
  Space,
  Empty,
  Divider,
  Radio,
  Progress,
  Card,
  Statistic,
  Tag,
} from 'antd';
import { 
  HistoryOutlined, 
  SendOutlined,
  UnorderedListOutlined,
  AppstoreOutlined,
  LeftOutlined,
  RightOutlined
} from '@ant-design/icons';
import { productApi } from '../../api/productApi';
import { demandApi } from '../../api/demandApi';
import { stockApi } from '../../api/stockApi';
import { CategoryWithProducts } from '../../types/product';
import useAuth from '../../hooks/useAuth';
import { useNavigate } from 'react-router-dom';
import dayjs from 'dayjs';

const { Title, Text } = Typography;
const { Panel } = Collapse;

const CreateDemandPage: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [categories, setCategories] = useState<CategoryWithProducts[]>([]);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [repeatLoading, setRepeatLoading] = useState(false);
  const [quantities, setQuantities] = useState<Record<string, number>>({});
  const [stocks, setStocks] = useState<Record<string, number>>({});

  // View Mode States
  const [viewMode, setViewMode] = useState<'table' | 'card'>(() => 
    (localStorage.getItem('pastryflow_input_mode') as 'table' | 'card') || 'table'
  );
  const [currentProductIndex, setCurrentProductIndex] = useState(0);

  const fetchProducts = useCallback(async () => {
    try {
      const res = await productApi.getCategoriesWithProducts({ productType: 'FinishedProduct' });
      if (res.success && res.data) {
        setCategories(res.data);
      }
    } catch {
      message.error('Ürünler yüklenemedi.');
    } finally {
      setLoading(false);
    }
  }, []);

  const fetchStocks = useCallback(async () => {
    if (!user?.branchId) return;
    try {
      const today = dayjs().format('YYYY-MM-DD');
      const res = await stockApi.getCurrentStock(user.branchId, today);
      if (res.success && res.data) {
        const stockMap: Record<string, number> = {};
        res.data.forEach(s => {
          stockMap[s.productId] = s.currentStock;
        });
        setStocks(stockMap);
      }
    } catch {
      console.error('Stok bilgileri yüklenemedi.');
    }
  }, [user?.branchId]);

  useEffect(() => {
    fetchProducts();
    fetchStocks();
  }, [fetchProducts, fetchStocks]);

  // Save View Mode Preference
  useEffect(() => {
    localStorage.setItem('pastryflow_input_mode', viewMode);
  }, [viewMode]);

  // Memoized Flat Product List for Card Mode
  const flatProducts = useMemo(() => {
    return categories.flatMap(cat => 
      cat.products.map(p => ({
        ...p,
        categoryName: cat.name
      }))
    );
  }, [categories]);

  const handleQuantityChange = (productId: string, val: number | null) => {
    setQuantities(prev => {
      const next = { ...prev };
      if (val === null || val === 0) delete next[productId];
      else next[productId] = val;
      return next;
    });
  };

  const handleRepeatYesterday = async () => {
    if (!user?.branchId) return;

    const productionBranchIds = new Set<string>();
    categories.forEach(cat => {
      cat.products.forEach(p => {
        if (p.productionBranchId) productionBranchIds.add(p.productionBranchId);
      });
    });

    if (productionBranchIds.size === 0) {
      message.info('Henüz üretim merkezi tanımlı ürün bulunmuyor.');
      return;
    }

    setRepeatLoading(true);
    try {
      let foundAny = false;
      const newQuantities: Record<string, number> = {};

      for (const pbId of Array.from(productionBranchIds)) {
        const res = await demandApi.getLastDemand(user.branchId, pbId);
        if (res.success && res.data && res.data.items) {
          foundAny = true;
          res.data.items.forEach(item => {
            newQuantities[item.productId] = item.requestedQuantity;
          });
        }
      }

      if (foundAny) {
        setQuantities(newQuantities);
        message.success('Dünkü talepler yüklendi.');
      } else {
        message.info('Dün bu üretim merkezlerine yönelik bir talep bulunamadı.');
      }
    } catch {
      message.error('Dünkü talepler alınırken bir hata oluştu.');
    } finally {
      setRepeatLoading(false);
    }
  };

  const handleSubmit = async () => {
    const keys = Object.keys(quantities);
    if (keys.length === 0) {
      message.warning('Lütfen en az bir ürün talep edin.');
      return;
    }

    if (!user?.branchId) return;

    const itemsByBranch: Record<string, any[]> = {};
    
    categories.forEach(cat => {
      cat.products.forEach(p => {
        if (quantities[p.id]) {
          const pbId = p.productionBranchId || '00000000-0000-0000-0000-000000000000';
          if (!itemsByBranch[pbId]) itemsByBranch[pbId] = [];
          itemsByBranch[pbId].push({
            productId: p.id,
            requestedQuantity: quantities[p.id]
          });
        }
      });
    });

    setSubmitting(true);
    try {
      const branchIds = Object.keys(itemsByBranch);
      for (const bId of branchIds) {
        await demandApi.createDemand({
          salesBranchId: user.branchId,
          productionBranchId: bId,
          notes: 'Yeni talep girişi',
          items: itemsByBranch[bId]
        });
      }
      message.success('Talepleriniz başarıyla oluşturuldu.');
      navigate('/sales/demands');
    } catch {
      message.error('Talep oluşturulurken bir hata oluştu.');
    } finally {
      setSubmitting(false);
    }
  };

  const renderCardMode = () => {
    if (flatProducts.length === 0) return <Empty description="Ürün bulunamadı" />;
    const product = flatProducts[currentProductIndex];
    if (!product) return <Empty />;

    return (
      <Card 
        style={{ borderRadius: 16, boxShadow: '0 4px 20px rgba(0,0,0,0.08)', marginBottom: 24 }}
        bodyStyle={{ padding: '24px 16px' }}
      >
        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <Tag color="blue" style={{ marginBottom: 8, borderRadius: 4 }}>{product.categoryName}</Tag>
          <Title level={3} style={{ margin: 0, fontSize: 22 }}>{product.name}</Title>
          <Text type="secondary" style={{ fontSize: 14 }}>{product.unitName}</Text>
        </div>

        <div style={{ 
          background: '#f5f5f5', 
          padding: '16px', 
          borderRadius: 12, 
          marginBottom: 24,
          display: 'flex',
          justifyContent: 'center'
        }}>
          <Statistic 
            title="Mevcut Stok" 
            value={stocks[product.id] || 0} 
            valueStyle={{ fontSize: 20, fontWeight: 600 }}
          />
        </div>

        <div style={{ marginBottom: 32 }}>
          <div style={{ textAlign: 'center', marginBottom: 8 }}>
            <Text strong style={{ fontSize: 16 }}>Talep Miktarı</Text>
          </div>
          <InputNumber 
            min={0}
            value={quantities[product.id] || null}
            onChange={val => handleQuantityChange(product.id, val)}
            style={{ 
              width: '100%', 
              fontSize: 28, 
              height: 64, 
              display: 'flex', 
              alignItems: 'center',
              borderRadius: 12,
            }}
            onFocus={(e) => e.target.select()}
            inputMode="numeric"
            keyboard={false}
            placeholder="0"
          />
        </div>

        <Row gutter={12}>
          <Col span={12}>
            <Button 
              size="large" 
              block 
              icon={<LeftOutlined />}
              disabled={currentProductIndex === 0}
              onClick={() => setCurrentProductIndex(prev => prev - 1)}
              style={{ height: 50, borderRadius: 10 }}
            >
              Önceki
            </Button>
          </Col>
          <Col span={12}>
            <Button 
              type="primary" 
              size="large" 
              block 
              icon={<RightOutlined />}
              disabled={currentProductIndex === flatProducts.length - 1}
              onClick={() => setCurrentProductIndex(prev => prev + 1)}
              style={{ height: 50, borderRadius: 10 }}
            >
              Sonraki
            </Button>
          </Col>
        </Row>

        <div style={{ marginTop: 20, textAlign: 'center' }}>
          <Progress 
            percent={Math.round(((currentProductIndex + 1) / flatProducts.length) * 100)} 
            showInfo={false}
            size="small"
            strokeColor="#1890ff"
          />
          <div style={{ marginTop: 8 }}>
            <Text type="secondary" style={{ fontSize: 12 }}>
              {currentProductIndex + 1} / {flatProducts.length} Ürün
            </Text>
          </div>
        </div>
      </Card>
    );
  };

  const renderModeSelector = () => (
    <div style={{ 
      display: 'flex', 
      justifyContent: 'space-between', 
      alignItems: 'center', 
      marginBottom: 16,
      background: '#fff',
      padding: '8px 16px',
      borderRadius: 12,
      border: '1px solid #f0f0f0'
    }}>
      <Radio.Group 
        value={viewMode} 
        onChange={e => setViewMode(e.target.value)}
        optionType="button"
        buttonStyle="solid"
        size="middle"
      >
        <Radio.Button value="table"><UnorderedListOutlined /> Tablo</Radio.Button>
        <Radio.Button value="card"><AppstoreOutlined /> Kart</Radio.Button>
      </Radio.Group>
      <Text type="secondary" style={{ fontSize: 13 }}>
        {Object.keys(quantities).length} / {flatProducts.length} Ürün Seçili
      </Text>
    </div>
  );

  if (loading) return <Spin style={{ display: 'block', marginTop: 50 }} />;

  return (
    <div style={{ padding: '0 8px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16, flexWrap: 'wrap', gap: 12 }}>
        <Title level={2} style={{ margin: 0 }}>Yeni Talep Oluştur</Title>
        <Button 
          icon={<HistoryOutlined />} 
          onClick={handleRepeatYesterday} 
          loading={repeatLoading}
          shape="round"
        >
          Dünkü Talebi Tekrarla
        </Button>
      </div>
      
      <div style={{ marginBottom: 24 }}>
        <Text type="secondary">Gerekli miktarları ilgili ürünlerin yanına giriniz ve GÖNDER butonuna basınız.</Text>
      </div>

      <div style={{ marginBottom: 16 }}>
        {renderModeSelector()}
      </div>
      
      {categories.length === 0 ? (
        <Empty description="Ürün bulunamadı" />
      ) : (
        viewMode === 'card' ? (
          renderCardMode()
        ) : (
          <Collapse defaultActiveKey={categories.length > 0 ? [categories[0].id] : undefined} ghost expandIconPosition="end">
            {categories.map(cat => (
              <Panel 
                header={<Text strong style={{ fontSize: 16 }}>{cat.name} ({cat.products.length})</Text>} 
                key={cat.id}
                style={{ marginBottom: 12, background: '#fff', borderRadius: 8, border: '1px solid #f0f0f0' }}
              >
                {cat.products.map(p => (
                  <Row key={p.id} style={{ marginBottom: 12, padding: '8px 0', borderBottom: '1px solid #fafafa', alignItems: 'center' }}>
                    <Col span={14}>
                      <Text strong>{p.name}</Text>
                      <div style={{ fontSize: 12, color: '#aaa' }}>Birim: {p.unitName} | Stok: {stocks[p.id] || 0}</div>
                    </Col>
                    <Col span={10} style={{ textAlign: 'right' }}>
                      <InputNumber 
                        min={0} 
                        value={quantities[p.id] || null} 
                        onChange={val => handleQuantityChange(p.id, val)}
                        style={{ width: '100%' }}
                        placeholder="Miktar"
                        onFocus={(e) => e.target.select()}
                        inputMode="numeric"
                        keyboard={false}
                      />
                    </Col>
                  </Row>
                ))}
              </Panel>
            ))}
          </Collapse>
        )
      )}

      <Divider />

      <div style={{ marginTop: 24, textAlign: 'right', paddingBottom: 40 }}>
        <Space wrap>
          <Button size="large" onClick={() => navigate('/sales/demands')}>Vazgeç</Button>
          <Button 
            type="primary" 
            size="large" 
            icon={<SendOutlined />}
            onClick={handleSubmit} 
            loading={submitting}
            style={{ minWidth: 200, height: 50, borderRadius: 10, fontWeight: 'bold' }}
          >
            TALEBİ GÖNDER
          </Button>
        </Space>
      </div>
    </div>
  );
};

export default CreateDemandPage;
