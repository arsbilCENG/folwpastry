import React, { useEffect, useState } from 'react';
import { 
  Button, InputNumber, Collapse, Typography, message, Spin, Row, Col, Space, 
  Modal, Result, Tag, Steps, Card, Statistic, Alert, Input, Table, Form 
} from 'antd';
import { 
  CheckCircleOutlined, ExclamationCircleOutlined, InfoCircleOutlined,
  DollarOutlined, CameraOutlined, ExceptionOutlined
} from '@ant-design/icons';
import { dayClosingApi } from '../../api/dayClosingApi';
import { productApi } from '../../api/productApi';
import { stockApi } from '../../api/stockApi';
import { CategoryWithProducts } from '../../types/product';
import { CurrentStock } from '../../types/stock';
import useAuth from '../../hooks/useAuth';
import PhotoUpload from '../../components/common/PhotoUpload';
import { 
  useExpectedCash, 
  useSubmitCashCount, 
  useUploadReceiptPhoto, 
  useUploadCounterPhoto 
} from '../../hooks/useDayClosing';

const { Title, Text } = Typography;
const { Panel } = Collapse;
const { TextArea } = Input;

const DayClosingPage: React.FC = () => {
  const { user } = useAuth();
  
  // Data States
  const [categories, setCategories] = useState<CategoryWithProducts[]>([]);
  const [stocks, setStocks] = useState<Record<string, number>>({});
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [isClosed, setIsClosed] = useState(false);
  const [dayClosingId, setDayClosingId] = useState<string | null>(null);

  // Step 1: Sayım
  const [counts, setCounts] = useState<Record<string, number>>({});
  
  // Step 2: Kasa
  const [cashAmount, setCashAmount] = useState<number | null>(null);
  const [posAmount, setPosAmount] = useState<number | null>(null);
  const [differenceNote, setDifferenceNote] = useState<string>('');
  const [isMissingPricesModalVisible, setIsMissingPricesModalVisible] = useState(false);

  // Step 3: Fotoğraflar
  const [receiptPhoto, setReceiptPhoto] = useState<File | string | null>(null);
  const [counterPhoto, setCounterPhoto] = useState<File | string | null>(null);

  // Step 4: Devir
  const [carryOvers, setCarryOvers] = useState<Record<string, number>>({});

  // UI State
  const [currentStep, setCurrentStep] = useState(0);
  
  const today = new Date().toISOString().split('T')[0];
  
  // Hooks
  const expectedCashQuery = useExpectedCash(user?.branchId || '', today, currentStep === 1 && !!user?.branchId);
  const submitCashCountMutation = useSubmitCashCount();
  const uploadReceiptMutation = useUploadReceiptPhoto();
  const uploadCounterMutation = useUploadCounterPhoto();

  // Load existing data
  useEffect(() => {
    const fetchData = async () => {
      if (!user?.branchId) return;
      try {
        const [prodRes, summaryRes, stockRes] = await Promise.all([
          productApi.getCategoriesWithProducts(),
          dayClosingApi.getSummary(user.branchId, today).catch(() => ({ success: false, data: null })), // Handle 404 if not started
          stockApi.getCurrentStock(user.branchId, today)
        ]);

        if (prodRes.success && prodRes.data) {
          setCategories(prodRes.data);
        }

        if (stockRes.success && stockRes.data) {
          const stockMap: Record<string, number> = {};
          stockRes.data.forEach((s: CurrentStock) => {
            stockMap[s.productId] = s.currentStock;
          });
          setStocks(stockMap);
        }

        if (summaryRes.success && summaryRes.data) {
          setIsClosed(summaryRes.data.isClosed);
          
          // @ts-ignore
          setDayClosingId(summaryRes.data.id || summaryRes.data.dayClosingId || null);
          
          const newCounts: Record<string, number> = {};
          const newCarryOvers: Record<string, number> = {};
          summaryRes.data.items.forEach((item: any) => {
            if (item.endOfDayCount > 0) newCounts[item.productId] = item.endOfDayCount;
            if (item.carryOver > 0) newCarryOvers[item.productId] = item.carryOver;
          });
          setCounts(newCounts);
          setCarryOvers(newCarryOvers);

          if (summaryRes.data.cashAmount !== null) setCashAmount(summaryRes.data.cashAmount);
          if (summaryRes.data.posAmount !== null) setPosAmount(summaryRes.data.posAmount);
          if (summaryRes.data.differenceNote) setDifferenceNote(summaryRes.data.differenceNote);
          
          if (summaryRes.data.receiptPhotoUrl) setReceiptPhoto(summaryRes.data.receiptPhotoUrl);
          if (summaryRes.data.counterPhotoUrl) setCounterPhoto(summaryRes.data.counterPhotoUrl);
        }
      } catch (err) {
        console.error('DayClosing Load Error:', err);
        message.error('Veriler yüklenemedi.');
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [user]);

  // Handle Step 1 -> 2: Save Counts
  const handleSaveCountsAndNext = async () => {
    if (!user?.branchId) return;
    
    // Check if at least one item is counted
    const hasCounts = Object.values(counts).some(v => v > 0);
    if (!hasCounts) {
      message.error('Lütfen en az bir ürün sayımı giriniz.');
      return;
    }

    setSaving(true);
    try {
      const items = Object.keys(counts).map(k => ({ productId: k, endOfDayCount: counts[k] }));
      await dayClosingApi.saveCount({ branchId: user.branchId, date: today, items });
      
      const summaryRes = await dayClosingApi.getSummary(user.branchId, today);
      if (summaryRes.success && summaryRes.data) {
        // @ts-ignore
        setDayClosingId(summaryRes.data.id || summaryRes.data.dayClosingId || null);
      }
      
      setCurrentStep(1);
    } catch {
      message.error('Sayım kaydedilemedi.');
    } finally {
      setSaving(false);
    }
  };

  // Handle Step 2 -> 3: Save Cash Count
  const handleSaveCashAndNext = async () => {
    if (!dayClosingId) {
      message.error('Gün sonu kaydı bulunamadı.');
      return;
    }

    if (cashAmount === null || posAmount === null) {
      message.error('Lütfen Nakit ve POS tutarlarını giriniz. (Yoksa 0 giriniz)');
      return;
    }

    const expectedAmount = expectedCashQuery.data?.data?.expectedAmount || 0;
    const totalCounted = cashAmount + posAmount;
    const diff = totalCounted - expectedAmount;

    if (Math.abs(diff) > 0.01 && (!differenceNote || differenceNote.trim() === '')) {
      message.error('Kasa farkı bulunmaktadır. Lütfen açıklama giriniz.');
      return;
    }

    setSaving(true);
    try {
      await submitCashCountMutation.mutateAsync({
        dayClosingId,
        data: {
          cashAmount,
          posAmount,
          differenceNote: differenceNote.trim() || undefined
        }
      });
      setCurrentStep(2);
    } catch {
      // Error handled by mutation
    } finally {
      setSaving(false);
    }
  };

  // Handle Step 3 -> 4: Upload Photos
  const handleUploadPhotosAndNext = async () => {
    if (!dayClosingId) return;

    if (!receiptPhoto || !counterPhoto) {
      message.error('Lütfen her iki fotoğrafı da yükleyiniz.');
      return;
    }

    setSaving(true);
    try {
      const uploadPromises = [];
      
      if (receiptPhoto instanceof File) {
        uploadPromises.push(uploadReceiptMutation.mutateAsync({ dayClosingId, photo: receiptPhoto }));
      }
      if (counterPhoto instanceof File) {
        uploadPromises.push(uploadCounterMutation.mutateAsync({ dayClosingId, photo: counterPhoto }));
      }

      if (uploadPromises.length > 0) {
        await Promise.all(uploadPromises);
      }
      
      setCurrentStep(3);
    } catch {
      message.error('Fotoğraflar yüklenirken hata oluştu.');
    } finally {
      setSaving(false);
    }
  };

  // Handle Step 4: Close Day
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
          const carryItems = Object.keys(carryOvers).map(k => ({ productId: k, carryOverQuantity: carryOvers[k] }));
          await dayClosingApi.saveCarryOver({ branchId: user.branchId!, date: today, items: carryItems });

          const res = await dayClosingApi.closeDay({ branchId: user.branchId!, date: today, closedByUserId: user.id! });
          if (res.success) {
            message.success('Gün başarıyla kapatıldı! Z Raporu oluşturuldu.');
            setIsClosed(true);
          } else {
            message.error(res.message);
          }
        } catch(e: any) {
          message.error(e.response?.data?.message || 'Zaten kapatılmış olabilir veya bir hata oluştu.');
        } finally {
          setSaving(false);
        }
      }
    });
  };

  const renderDiff = (productId: string) => {
    const currentStock = stocks[productId] || 0;
    const countedQuantity = counts[productId] || 0;
    const diff = countedQuantity - currentStock;

    if (diff === 0) return <Tag color="success" icon={<CheckCircleOutlined />}>0</Tag>;
    if (diff < 0) return <Tag color="error" icon={<ExclamationCircleOutlined />}>{diff.toFixed(2)}</Tag>;
    return <Tag color="warning" icon={<InfoCircleOutlined />}>+{diff.toFixed(2)}</Tag>;
  };

  if (loading) return (
    <div style={{ textAlign: 'center', padding: '50px' }}>
      <Spin size="large" tip="Veriler yükleniyor..." />
    </div>
  );

  if (isClosed) {
    return (
      <Result
        status="success"
        title="Bugünün Gün Sonu İşlemleri Tamamlanmış"
        subTitle="Mevcut Raporunuzu Dashboard üzerinden veya Raporlar sekmesinden görüntüleyebilirsiniz."
      />
    );
  }

  // Calculate Cash Differences
  const expectedAmount = expectedCashQuery.data?.data?.expectedAmount || 0;
  const totalCounted = (cashAmount || 0) + (posAmount || 0);
  const cashDifference = totalCounted - expectedAmount;
  
  const hasCashDiff = Math.abs(cashDifference) > 0.01;

  const missingPriceProducts = expectedCashQuery.data?.data?.items?.filter(i => i.unitPrice === null || i.unitPrice === 0) || [];

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      <Title level={2}>📅 Gün Sonu Kapanış</Title>
      
      <Card style={{ marginBottom: 24, borderRadius: 12, boxShadow: '0 2px 8px rgba(0,0,0,0.05)' }}>
        <Steps 
          current={currentStep}
          responsive
          items={[
            { title: 'Sayım', icon: <ExceptionOutlined /> },
            { title: 'Kasa', icon: <DollarOutlined /> },
            { title: 'Fotoğraflar', icon: <CameraOutlined /> },
            { title: 'Devir + Kapat', icon: <CheckCircleOutlined /> },
          ]}
        />
      </Card>

      {/* STEP 1: ÜRÜN SAYIMI */}
      <div style={{ display: currentStep === 0 ? 'block' : 'none' }}>
        <div style={{ marginBottom: 16 }}>
          <Text type="secondary">Adım 1: Gün sonunda elinizde Kalan Ürün sayısını girin. Mevcut stok ile karşılaştırma yapılır.</Text>
        </div>
        
        <Collapse defaultActiveKey={categories.length > 0 ? [categories[0].id] : undefined} ghost expandIconPosition="end">
          {categories.map(cat => (
            <Panel 
              header={<Text strong style={{ fontSize: 16 }}>{cat.name}</Text>} 
              key={cat.id}
              style={{ marginBottom: 16, background: '#fff', borderRadius: 8, border: '1px solid #f0f0f0' }}
            >
              <div style={{ overflowX: 'auto' }}>
                <div style={{ minWidth: 600 }}>
                  <Row style={{ fontWeight: 'bold', marginBottom: 12, paddingBottom: 12, borderBottom: '1px solid #f0f0f0', textAlign: 'center' }}>
                    <Col span={8} style={{ textAlign: 'left' }}>Ürün Adı</Col>
                    <Col span={4}>Stok</Col>
                    <Col span={8}>Sayım Miktarı</Col>
                    <Col span={4}>Fark</Col>
                  </Row>

                  {cat.products.map(p => (
                    <Row key={p.id} style={{ marginBottom: 12, padding: '8px 0', borderBottom: '1px solid #fafafa', alignItems: 'center', textAlign: 'center' }}>
                      <Col span={8} style={{ textAlign: 'left' }}>
                        <Text>{p.name}</Text><br />
                        <Text type="secondary" style={{ fontSize: 11 }}>{p.unitName}</Text>
                      </Col>
                      <Col span={4}>
                        <Text strong>{stocks[p.id] || 0}</Text>
                      </Col>
                      <Col span={8}>
                        <InputNumber 
                          min={0} 
                          precision={2}
                          value={counts[p.id] || 0} 
                          onChange={val => setCounts(prev => ({ ...prev, [p.id]: val || 0 }))}
                          style={{ width: '80%' }}
                        />
                      </Col>
                      <Col span={4}>
                        {renderDiff(p.id)}
                      </Col>
                    </Row>
                  ))}
                </div>
              </div>
            </Panel>
          ))}
        </Collapse>
        
        <div style={{ textAlign: 'right', marginTop: 24 }}>
          <Button type="primary" size="large" onClick={handleSaveCountsAndNext} loading={saving}>
            İleri ►
          </Button>
        </div>
      </div>

      {/* STEP 2: KASA SAYIMI */}
      <div style={{ display: currentStep === 1 ? 'block' : 'none' }}>
        {expectedCashQuery.isLoading ? (
          <div style={{ textAlign: 'center', padding: 40 }}><Spin tip="Beklenen kasa hesaplanıyor..." /></div>
        ) : (
          <Row gutter={[24, 24]}>
            <Col xs={24} md={10}>
              <Card style={{ background: '#f0f5ff', borderColor: '#adc6ff', borderRadius: 12, height: '100%' }}>
                <Statistic 
                  title={<Text strong style={{ fontSize: 16 }}>Beklenen Kasa Tutarı</Text>}
                  value={expectedAmount}
                  precision={2}
                  prefix="₺"
                  valueStyle={{ color: '#1d39c4', fontSize: 32, fontWeight: 'bold' }}
                />
                <div style={{ marginTop: 12 }}>
                  <Text type="secondary">
                    ({expectedCashQuery.data?.data?.items?.length || 0} üründen hesaplandı, 
                    {expectedCashQuery.data?.data?.productsWithoutPrice || 0} ürünün fiyatı yok)
                  </Text>
                </div>
                
                {(expectedCashQuery.data?.data?.productsWithoutPrice || 0) > 0 && (
                  <Alert 
                    type="warning" 
                    showIcon 
                    style={{ marginTop: 16 }}
                    message={
                      <div>
                        <Text strong>{expectedCashQuery.data?.data?.productsWithoutPrice} ürünün birim fiyatı tanımlı değil.</Text><br/>
                        <Text style={{ fontSize: 12 }}>Beklenen tutar eksik hesaplanmış olabilir.</Text>
                        <div style={{ marginTop: 8 }}>
                          <Button size="small" onClick={() => setIsMissingPricesModalVisible(true)}>Fiyatsız Ürünleri Gör</Button>
                        </div>
                      </div>
                    }
                  />
                )}
              </Card>
            </Col>
            
            <Col xs={24} md={14}>
              <Card title="Kasa Girişi" style={{ borderRadius: 12, height: '100%' }}>
                <Form layout="vertical">
                  <Row gutter={16}>
                    <Col xs={24} sm={12}>
                      <Form.Item label="Kasadaki Nakit" required>
                        <InputNumber 
                          style={{ width: '100%' }} 
                          size="large" 
                          addonBefore="₺" 
                          precision={2} 
                          min={0}
                          value={cashAmount}
                          onChange={val => setCashAmount(val)}
                        />
                      </Form.Item>
                    </Col>
                    <Col xs={24} sm={12}>
                      <Form.Item label="POS / Kredi Kartı Toplamı" required>
                        <InputNumber 
                          style={{ width: '100%' }} 
                          size="large" 
                          addonBefore="₺" 
                          precision={2} 
                          min={0}
                          value={posAmount}
                          onChange={val => setPosAmount(val)}
                        />
                      </Form.Item>
                    </Col>
                  </Row>
                  
                  <div style={{ padding: '16px', background: '#fafafa', borderRadius: 8, marginTop: 8, marginBottom: 16 }}>
                    <Row justify="space-between" style={{ marginBottom: 8 }}>
                      <Col><Text>Toplam Sayılan:</Text></Col>
                      <Col><Text strong>₺ {totalCounted.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}</Text></Col>
                    </Row>
                    <Row justify="space-between" style={{ marginBottom: 8 }}>
                      <Col><Text>Beklenen:</Text></Col>
                      <Col><Text strong>₺ {expectedAmount.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}</Text></Col>
                    </Row>
                    <div style={{ height: 1, background: '#d9d9d9', margin: '8px 0' }}></div>
                    <Row justify="space-between">
                      <Col><Text strong>FARK:</Text></Col>
                      <Col>
                        {cashAmount !== null && posAmount !== null ? (
                          <Text strong style={{ 
                            color: diffColor(cashDifference), 
                            fontSize: 18 
                          }}>
                            {cashDifference > 0 ? '+' : ''}₺ {cashDifference.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}
                            {' '}{diffIcon(cashDifference)}
                          </Text>
                        ) : (
                          <Text type="secondary">Tutar giriniz</Text>
                        )}
                      </Col>
                    </Row>
                  </div>

                  {hasCashDiff && (
                    <Form.Item 
                      label={
                        <span>Açıklama <Text type="danger">* (Kasa farkı olduğu için zorunlu)</Text></span>
                      }
                    >
                      <TextArea 
                        rows={3} 
                        showCount 
                        maxLength={500} 
                        value={differenceNote}
                        onChange={e => setDifferenceNote(e.target.value)}
                        placeholder="Kasa farkının nedenini açıklayınız..."
                      />
                    </Form.Item>
                  )}
                  {!hasCashDiff && (
                    <Form.Item label="Açıklama (Opsiyonel)">
                      <TextArea 
                        rows={2} 
                        showCount 
                        maxLength={500} 
                        value={differenceNote}
                        onChange={e => setDifferenceNote(e.target.value)}
                      />
                    </Form.Item>
                  )}
                  
                  <Collapse ghost style={{ marginTop: 16 }}>
                    <Panel header={<Text style={{ color: '#1890ff' }}>Satış Detaylarını Gör</Text>} key="1">
                      <Table 
                        dataSource={expectedCashQuery.data?.data?.items || []} 
                        size="small"
                        rowKey="productName"
                        pagination={{ pageSize: 10 }}
                        scroll={{ x: 'max-content' }}
                        columns={[
                          { title: 'Ürün', dataIndex: 'productName' },
                          { title: 'Satış', dataIndex: 'calculatedSales', align: 'right' },
                          { title: 'Birim Fiyat', dataIndex: 'unitPrice', align: 'right', render: v => v ? `₺${v.toFixed(2)}` : '-' },
                          { title: 'Toplam', dataIndex: 'salesValue', align: 'right', render: v => v ? `₺${v.toFixed(2)}` : '-' }
                        ]}
                      />
                    </Panel>
                  </Collapse>
                </Form>
              </Card>
            </Col>
          </Row>
        )}
        
        <div style={{ display: 'flex', justifyContent: 'space-between', marginTop: 24, flexWrap: 'wrap', gap: 12 }}>
          <Button onClick={() => setCurrentStep(0)}>◄ Geri</Button>
          <Button type="primary" size="large" onClick={handleSaveCashAndNext} loading={saving}>
            İleri ►
          </Button>
        </div>
        
        {/* Missing Prices Modal */}
        <Modal 
          title="Fiyatı Tanımlı Olmayan Ürünler" 
          open={isMissingPricesModalVisible} 
          onCancel={() => setIsMissingPricesModalVisible(false)}
          footer={[<Button key="ok" type="primary" onClick={() => setIsMissingPricesModalVisible(false)}>Tamam</Button>]}
        >
          <Alert message="Aşağıdaki ürünlerin satış fiyatı sistemde tanımlı olmadığı için beklenen kasa tutarına dahil edilmemiştir." type="info" showIcon style={{ marginBottom: 16 }} />
          <ul>
            {missingPriceProducts.map(p => (
              <li key={p.productName}>{p.productName} (Satış: {p.calculatedSales})</li>
            ))}
          </ul>
        </Modal>
      </div>

      {/* STEP 3: FOTOĞRAFLAR */}
      <div style={{ display: currentStep === 2 ? 'block' : 'none' }}>
        <Alert message="Her iki fotoğrafı da sisteme yüklemek zorunludur." type="info" showIcon style={{ marginBottom: 24 }} />
        
        <Row gutter={[24, 24]}>
          <Col xs={24} md={12}>
            <Card title="Gün Sonu Fişi" style={{ borderRadius: 12 }}>
              <PhotoUpload 
                value={receiptPhoto}
                onChange={setReceiptPhoto}
                required={true}
                placeholder="Gün sonu POS/Z raporu fiş fotoğrafı"
              />
            </Card>
          </Col>
          <Col xs={24} md={12}>
            <Card title="Tezgah Fotoğrafı" style={{ borderRadius: 12 }}>
              <PhotoUpload 
                value={counterPhoto}
                onChange={setCounterPhoto}
                required={true}
                placeholder="Günün sonunda tezgahın genel durumu"
              />
            </Card>
          </Col>
        </Row>
        
        <div style={{ display: 'flex', justifyContent: 'space-between', marginTop: 24, flexWrap: 'wrap', gap: 12 }}>
          <Button onClick={() => setCurrentStep(1)}>◄ Geri</Button>
          <Button type="primary" size="large" onClick={handleUploadPhotosAndNext} loading={saving}>
            İleri ►
          </Button>
        </div>
      </div>

      {/* STEP 4: DEVİR VE KAPAT */}
      <div style={{ display: currentStep === 3 ? 'block' : 'none' }}>
        <div style={{ marginBottom: 16 }}>
          <Text type="secondary">Son Adım: Yarına devredecek ürün sayısını girin. (Aradaki fark otomatik çöpe atılan zayiat olarak hesaplanacaktır).</Text>
        </div>
        
        <Collapse defaultActiveKey={categories.length > 0 ? [categories[0].id] : undefined} ghost expandIconPosition="end">
          {categories.map(cat => {
            return (
            <Panel 
              header={<Text strong style={{ fontSize: 16 }}>{cat.name}</Text>} 
              key={cat.id}
              style={{ marginBottom: 16, background: '#fff', borderRadius: 8, border: '1px solid #f0f0f0' }}
            >
              <div style={{ overflowX: 'auto' }}>
                <div style={{ minWidth: 600 }}>
                  <Row style={{ fontWeight: 'bold', marginBottom: 12, paddingBottom: 12, borderBottom: '1px solid #f0f0f0', textAlign: 'center' }}>
                    <Col span={10} style={{ textAlign: 'left' }}>Ürün Adı</Col>
                    <Col span={6}>Sayım (Kalan)</Col>
                    <Col span={8}>Yarına Devir</Col>
                  </Row>

                  {cat.products.map(p => (
                    <Row key={p.id} style={{ marginBottom: 12, padding: '8px 0', borderBottom: '1px solid #fafafa', alignItems: 'center', textAlign: 'center' }}>
                      <Col span={10} style={{ textAlign: 'left' }}>
                        <Text>{p.name}</Text><br />
                        <Text type="secondary" style={{ fontSize: 11 }}>{p.unitName}</Text>
                      </Col>
                      <Col span={6}>
                        <Text strong>{counts[p.id] || 0}</Text>
                      </Col>
                      <Col span={8}>
                        <InputNumber 
                          min={0} 
                          max={counts[p.id] || 0}
                          precision={2}
                          value={carryOvers[p.id] || 0} 
                          onChange={val => setCarryOvers(prev => ({ ...prev, [p.id]: val || 0 }))}
                          style={{ width: '80%' }}
                        />
                      </Col>
                    </Row>
                  ))}
                </div>
              </div>
            </Panel>
            );
          })}
        </Collapse>
        
        <div style={{ marginTop: 24, padding: 24, background: '#fff', border: '1px solid #f0f0f0', borderRadius: 12, boxShadow: '0 4px 12px rgba(0,0,0,0.05)' }}>
          <Row justify="space-between" align="middle" gutter={[16, 16]}>
            <Col xs={24} md={8}>
              <Button onClick={() => setCurrentStep(2)}>◄ Geri</Button>
            </Col>
            <Col xs={24} md={16} style={{ textAlign: 'right' }}>
              <Button 
                type="primary" 
                size="large" 
                onClick={handleCloseDay} 
                loading={saving} 
                style={{ background: '#52c41a', borderColor: '#52c41a', height: 50, padding: '0 40px', fontWeight: 'bold', whiteSpace: 'normal', lineHeight: '1.2' }}
              >
                GÜNÜ KAPAT VE Z RAPORU OLUŞTUR
              </Button>
            </Col>
          </Row>
        </div>
      </div>
    </div>
  );
};

// Helper functions for Step 2
function diffColor(diff: number) {
  if (Math.abs(diff) < 0.01) return '#52c41a'; // green
  if (diff < 0) return '#f5222d'; // red
  return '#1890ff'; // blue
}

function diffIcon(diff: number) {
  if (Math.abs(diff) < 0.01) return <span title="Kasa tutarlı">✅</span>;
  if (diff < 0) return <span title="Kasa açığı">🔴</span>;
  return <span title="Kasa fazlası">ℹ️</span>;
}

export default DayClosingPage;
