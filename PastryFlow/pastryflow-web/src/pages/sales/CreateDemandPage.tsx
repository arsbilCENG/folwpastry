import React, { useEffect, useState, useCallback } from 'react';
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
} from 'antd';
import { HistoryOutlined, SendOutlined } from '@ant-design/icons';
import { productApi } from '../../api/productApi';
import { demandApi } from '../../api/demandApi';
import { CategoryWithProducts } from '../../types/product';
import useAuth from '../../hooks/useAuth';
import { useNavigate } from 'react-router-dom';

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

  useEffect(() => {
    fetchProducts();
  }, [fetchProducts]);

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

    // We need to fetch last demands for each production branch we work with.
    // For simplicity, we find all production branch IDs used in our categories.
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

    // Group items by ProductionBranchId
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
          notes: 'Dünkü talebi tekrarla veya yeni talep girişi',
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

  if (loading) return <Spin style={{ display: 'block', marginTop: 50 }} />;

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={2} style={{ margin: 0 }}>Yeni Talep Oluştur</Title>
        <Button 
          icon={<HistoryOutlined />} 
          onClick={handleRepeatYesterday} 
          loading={repeatLoading}
        >
          Dünkü Talebi Tekrarla
        </Button>
      </div>
      
      <div style={{ marginBottom: 24 }}>
        <Text type="secondary">Gerekli miktarları ilgili ürünlerin yanına giriniz ve GÖNDER butonuna basınız.</Text>
      </div>
      
      {categories.length === 0 ? (
        <Empty description="Ürün bulunamadı" />
      ) : (
        <Collapse defaultActiveKey={[categories[0].id]}>
          {categories.map(cat => (
            <Panel header={`${cat.name} (${cat.products.length} ürün)`} key={cat.id}>
              {cat.products.map(p => (
                <Row key={p.id} style={{ marginBottom: 8, padding: '8px 0', borderBottom: '1px solid #f0f0f0' }}>
                  <Col span={16}>
                    <Text strong>{p.name}</Text>
                    <div style={{ fontSize: 12, color: '#aaa' }}>Birim: {p.unitName}</div>
                  </Col>
                  <Col span={8} style={{ textAlign: 'right' }}>
                    <InputNumber 
                      min={0} 
                      value={quantities[p.id] || null} 
                      onChange={val => handleQuantityChange(p.id, val)}
                      style={{ width: '100%' }}
                      placeholder="Miktar"
                    />
                  </Col>
                </Row>
              ))}
            </Panel>
          ))}
        </Collapse>
      )}

      <Divider />

      <div style={{ marginTop: 24, textAlign: 'right' }}>
        <Space>
          <Button size="large" onClick={() => navigate('/sales/demands')}>Vazgeç</Button>
          <Button 
            type="primary" 
            size="large" 
            icon={<SendOutlined />}
            onClick={handleSubmit} 
            loading={submitting}
            style={{ minWidth: 200 }}
          >
            TALEBİ GÖNDER
          </Button>
        </Space>
      </div>
    </div>
  );
};

export default CreateDemandPage;
