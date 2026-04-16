import React, { useEffect, useState } from 'react';
import { Form, Button, InputNumber, Collapse, Typography, message, Spin, Row, Col } from 'antd';
import { productApi } from '../../api/productApi';
import { branchApi } from '../../api/branchApi';
import { demandApi } from '../../api/demandApi';
import { CategoryWithProducts } from '../../types/product';
import { Branch } from '../../types/branch';
import useAuth from '../../hooks/useAuth';
import { useNavigate } from 'react-router-dom';

const { Title, Text } = Typography;
const { Panel } = Collapse;

// Helper to determine production branch for a category (business rule: based on product type mostly, but we can load product's ProductionBranchId)
// For MVP, user can pick which production branch to send to, or we automatically send to the one defined in product.
// In PastryFlow, we can group products by ProductionBranch, then create 1 demand per Production Branch.

const CreateDemandPage: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [categories, setCategories] = useState<CategoryWithProducts[]>([]);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [quantities, setQuantities] = useState<Record<string, number>>({});

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        // Fetch all Finished Products categories
        const res = await productApi.getCategoriesWithProducts({ productType: 'FinishedProduct' });
        if (res.success && res.data) {
          setCategories(res.data);
        }
      } catch (err) {
        message.error('Ürünler yüklenemedi.');
      } finally {
        setLoading(false);
      }
    };
    fetchProducts();
  }, []);

  const handleQuantityChange = (productId: string, val: number | null) => {
    setQuantities(prev => {
      const next = { ...prev };
      if (val === null || val === 0) delete next[productId];
      else next[productId] = val;
      return next;
    });
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
      // Create separate demand for each production branch
      const branchIds = Object.keys(itemsByBranch);
      for (const bId of branchIds) {
        await demandApi.createDemand({
          salesBranchId: user.branchId,
          productionBranchId: bId, // Note: In real app, might need to ensure backend doesn't crash on invalid Guid
          notes: 'Mobil App üzerinden gönderildi',
          items: itemsByBranch[bId]
        });
      }
      message.success('Talepleriniz başarıyla oluşturuldu.');
      navigate('/sales/demands');
    } catch (err) {
      message.error('Talep oluşturulurken bir hata oluştu.');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) return <Spin />;

  return (
    <div>
      <Title level={2}>Yeni Talep Oluştur</Title>
      <div style={{ marginBottom: 16 }}>
        <Text>Gerekli miktarları ilgili ürünlerin yanına giriniz ve GÖNDER butonuna basınız.</Text>
      </div>
      
      <Collapse defaultActiveKey={categories.length > 0 ? [categories[0].id] : undefined}>
        {categories.map(cat => (
          <Panel header={`${cat.name} (${cat.products.length} ürün)`} key={cat.id}>
            {cat.products.map(p => (
              <Row key={p.id} style={{ marginBottom: 8, padding: '8px 0', borderBottom: '1px solid #f0f0f0' }}>
                <Col span={16}>
                  <Text>{p.name}</Text>
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

      <div style={{ marginTop: 24, textAlign: 'right' }}>
        <Button 
          type="primary" 
          size="large" 
          onClick={handleSubmit} 
          loading={submitting}
          style={{ width: 200 }}
        >
          TALEBİ GÖNDER
        </Button>
      </div>
    </div>
  );
};

export default CreateDemandPage;
