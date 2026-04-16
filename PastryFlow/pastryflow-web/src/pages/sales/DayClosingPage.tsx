import React, { useEffect, useState } from 'react';
import { Form, Button, InputNumber, Collapse, Typography, message, Spin, Row, Col, Space, Modal, Result } from 'antd';
import { dayClosingApi } from '../../api/dayClosingApi';
import { productApi } from '../../api/productApi';
import { CategoryWithProducts } from '../../types/product';
import useAuth from '../../hooks/useAuth';

const { Title, Text } = Typography;
const { Panel } = Collapse;

// This page is extremely important. It features "Save Draft" for end-of-day counts and carry-overs.
const DayClosingPage: React.FC = () => {
  const { user } = useAuth();
  const [categories, setCategories] = useState<CategoryWithProducts[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  
  // State for End of day counts (Gün Sonu Sayımı)
  const [counts, setCounts] = useState<Record<string, number>>({});
  
  // State for Carry overs (Yarına Devreden)
  const [carryOvers, setCarryOvers] = useState<Record<string, number>>({});

  const [isClosed, setIsClosed] = useState(false);

  // Load existing data if available
  useEffect(() => {
    const fetchData = async () => {
      if (!user?.branchId) return;
      try {
        const today = new Date().toISOString().split('T')[0];
        const [prodRes, summaryRes] = await Promise.all([
          productApi.getCategoriesWithProducts(),
          dayClosingApi.getSummary(user.branchId, today)
        ]);

        if (prodRes.success && prodRes.data) {
          setCategories(prodRes.data);
        }

        if (summaryRes.success && summaryRes.data) {
          setIsClosed(summaryRes.data.isClosed);
          
          // prefill existing counts
          const newCounts: Record<string, number> = {};
          const newCarryOvers: Record<string, number> = {};
          summaryRes.data.items.forEach(item => {
            if (item.endOfDayCount > 0) newCounts[item.productId] = item.endOfDayCount;
            if (item.carryOver > 0) newCarryOvers[item.productId] = item.carryOver;
          });
          setCounts(newCounts);
          setCarryOvers(newCarryOvers);
        }
      } catch (err) {
        message.error('Veriler yüklenemedi.');
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [user]);

  const handleSaveCounts = async () => {
    if (!user?.branchId) return;
    setSaving(true);
    try {
      const today = new Date().toISOString().split('T')[0];
      const items = Object.keys(counts).map(k => ({ productId: k, endOfDayCount: counts[k] }));
      await dayClosingApi.saveCount({ branchId: user.branchId, date: today, items });
      message.success('Sayım rakamları kaydedildi. Tamamlamak için Devir kayıtlarını da kontrol ediniz.');
    } catch {
      message.error('Sayım kaydedilemedi.');
    } finally {
      setSaving(false);
    }
  };

  const handleSaveCarryOvers = async () => {
    if (!user?.branchId) return;
    setSaving(true);
    try {
      const today = new Date().toISOString().split('T')[0];
      const items = Object.keys(carryOvers).map(k => ({ productId: k, carryOverQuantity: carryOvers[k] }));
      await dayClosingApi.saveCarryOver({ branchId: user.branchId, date: today, items });
      message.success('Yarına devreden rakamlar kaydedildi.');
    } catch {
      message.error('Devir kaydedilemedi.');
    } finally {
      setSaving(false);
    }
  };

  const handleCloseDay = async () => {
    if (!user?.id || !user?.branchId) return;
    
    Modal.confirm({
      title: 'Günü Kapatmak İstediğinize Emin Misiniz?',
      content: 'Gün kapatıldıktan sonra geriye dönük işlem yapılamaz. Satışlar ve otomatik zayiatlar hesaplanacak ve sonraki güne açılış stoğu oluşturulacaktır.',
      okText: 'Evet, Kapat',
      cancelText: 'Vazgeç',
      onOk: async () => {
        setSaving(true);
        try {
          const today = new Date().toISOString().split('T')[0];
          
          // First autosave everything to be sure
          const countItems = Object.keys(counts).map(k => ({ productId: k, endOfDayCount: counts[k] }));
          await dayClosingApi.saveCount({ branchId: user.branchId!, date: today, items: countItems });
          
          const carryItems = Object.keys(carryOvers).map(k => ({ productId: k, carryOverQuantity: carryOvers[k] }));
          await dayClosingApi.saveCarryOver({ branchId: user.branchId!, date: today, items: carryItems });

          const res = await dayClosingApi.closeDay({ branchId: user.branchId!, date: today, closedByUserId: user.id! });
          if (res.success) {
            message.success('Gün başarıyla kapatıldı! Z Raporu oluşturuldu.');
            setIsClosed(true);
          } else {
            message.error(res.message);
          }
        } catch(e) {
          message.error('Zaten kapatılmış olabilir veya bir hata oluştu.');
        } finally {
          setSaving(false);
        }
      }
    });
  };

  if (loading) return <Spin />;

  if (isClosed) {
    return (
      <Result
        status="success"
        title="Bugünün Gün Sonu Sayımı Zaten Tamamlanmış"
        subTitle="Mevcut Raporunuzu Dashboard üzerinden veya Raporlar sekmesinden görüntüleyebilirsiniz."
      />
    );
  }

  return (
    <div>
      <Title level={2}>Gün Sonu Sayımı</Title>
      <div style={{ marginBottom: 16 }}>
        <Text>Adım 1: Gün sonunda elinizde Kalan Ürün sayısını girin.</Text><br/>
        <Text>Adım 2: Devredecek Ürün sayısını girin (Aradaki fark otomatik çöpe atılan zayiat olarak hesaplanıp stoktan düşer ve satılan ürün miktarı maliyetle hesaplanır).</Text>
      </div>

      <Collapse defaultActiveKey={categories.length > 0 ? [categories[0].id] : undefined}>
        {categories.map(cat => (
          <Panel header={cat.name} key={cat.id}>
            {/* Header Row */}
            <Row style={{ fontWeight: 'bold', marginBottom: 8, paddingBottom: 8, borderBottom: '2px solid #ccc' }}>
              <Col span={10}>Ürün Adı</Col>
              <Col span={7} style={{ textAlign: 'center' }}>Gün Sonu Kalan</Col>
              <Col span={7} style={{ textAlign: 'center' }}>Yarına Devreden</Col>
            </Row>

            {cat.products.map(p => (
              <Row key={p.id} style={{ marginBottom: 8, padding: '8px 0', borderBottom: '1px solid #f0f0f0' }}>
                <Col span={10} style={{ display: 'flex', alignItems: 'center' }}>
                  {p.name}
                </Col>
                <Col span={7} style={{ textAlign: 'center' }}>
                  <InputNumber 
                    min={0} 
                    value={counts[p.id] || 0} 
                    onChange={val => setCounts(prev => ({ ...prev, [p.id]: val || 0 }))}
                    style={{ width: '80%' }}
                  />
                </Col>
                <Col span={7} style={{ textAlign: 'center' }}>
                  <InputNumber 
                    min={0} 
                    max={counts[p.id] || 0} // Cannot carry over more than end-of-day count
                    value={carryOvers[p.id] || 0} 
                    onChange={val => setCarryOvers(prev => ({ ...prev, [p.id]: val || 0 }))}
                    style={{ width: '80%' }}
                  />
                </Col>
              </Row>
            ))}
          </Panel>
        ))}
      </Collapse>

      <div style={{ marginTop: 24, padding: 16, background: '#fafafa', border: '1px solid #d9d9d9', borderRadius: 8 }}>
        <Row justify="space-between" align="middle">
          <Col>
            <Space>
              <Button onClick={handleSaveCounts} loading={saving}>Sadece Kalanları Kaydet</Button>
              <Button onClick={handleSaveCarryOvers} loading={saving}>Sadece Devirleri Kaydet</Button>
            </Space>
          </Col>
          <Col>
            <Button type="primary" size="large" onClick={handleCloseDay} loading={saving} style={{ background: '#52c41a' }}>
              GÜNÜ KAPAT VE Z RAPORU OLUŞTUR
            </Button>
          </Col>
        </Row>
      </div>

    </div>
  );
};

export default DayClosingPage;
