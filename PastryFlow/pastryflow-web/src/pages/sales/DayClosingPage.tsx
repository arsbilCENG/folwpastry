import React, { useEffect, useState, useMemo } from 'react';
import { 
  Button, InputNumber, Collapse, Typography, message, Spin, Row, Col, Space, 
  Modal, Result, Tag, Steps, Card, Statistic, Alert, Input, Table, Form, Divider,
  Radio, Progress, Empty
} from 'antd';
import { 
  CheckCircleOutlined, ExclamationCircleOutlined, InfoCircleOutlined,
  DollarOutlined, CameraOutlined, ExceptionOutlined, CalculatorOutlined, ShopOutlined,
  UnorderedListOutlined, AppstoreOutlined, LeftOutlined, RightOutlined
} from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { DayClosingCounterItem } from '../../types/dayClosing';
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
  const navigate = useNavigate();
  const isEmployee = user?.role === 'Employee';
  
  // Data States
  const [categories, setCategories] = useState<CategoryWithProducts[]>([]);
  const [stocks, setStocks] = useState<Record<string, number>>({});
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [isClosed, setIsClosed] = useState(false);
  const [dayClosingId, setDayClosingId] = useState<string | null>(null);

  // View Mode States
  const [viewMode, setViewMode] = useState<'table' | 'card'>(() => 
    (localStorage.getItem('pastryflow_input_mode') as 'table' | 'card') || 'table'
  );
  const [currentProductIndex, setCurrentProductIndex] = useState(0);

  // Step 1: Sayım
  const [counts, setCounts] = useState<Record<string, number>>({});
  const [counterItems, setCounterItems] = useState<DayClosingCounterItem[]>([]);
  
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
  
  const getBusinessDate = () => {
    const now = new Date();
    if (now.getHours() < 3) {
      now.setDate(now.getDate() - 1);
    }
    return now.toLocaleDateString('en-CA');
  };
  const today = getBusinessDate();
  
  // Hooks
  const expectedCashQuery = useExpectedCash(user?.branchId || '', today, currentStep === 1 && !!user?.branchId);
  const submitCashCountMutation = useSubmitCashCount();
  const uploadReceiptMutation = useUploadReceiptPhoto();
  const uploadCounterMutation = useUploadCounterPhoto();

  // Memoized Flat Product List for Card Mode
  const flatProducts = useMemo(() => {
    return categories.flatMap(cat => 
      cat.products.map(p => ({
        ...p,
        categoryName: cat.name
      }))
    );
  }, [categories]);

  // Save View Mode Preference
  useEffect(() => {
    localStorage.setItem('pastryflow_input_mode', viewMode);
  }, [viewMode]);

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

          // Counter ürünleri başlat
          if (summaryRes.data.counterProducts && summaryRes.data.counterProducts.length > 0) {
            setCounterItems(
              summaryRes.data.counterProducts.map((p: any) => ({
                productId: p.productId,
                productName: p.productName,
                unitPrice: p.unitPrice ?? null,
                counterSoldQuantity: 0,
              }))
            );
          }

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
      
      if (isEmployee) {
        message.success({
          content: 'Sayımınız kaydedildi. Kasa kapama işlemi müdür tarafından yapılacaktır.',
          duration: 5
        });
        setTimeout(() => navigate('/sales/stock'), 1500);
        return;
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

    const expectedInfo = expectedCashQuery.data?.data;
    const dynamicExpectedCash = Math.max(0, 
      (expectedInfo?.openingCashBalance || 0) + 
      (expectedInfo?.totalSalesRevenue || 0) + 
      (expectedInfo?.cashDeposits || 0) - 
      (expectedInfo?.cashPurchases || 0) - 
      (expectedInfo?.cashWithdrawals || 0) - 
      (posAmount || 0)
    );
    const diff = cashAmount - dynamicExpectedCash;

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
          const carryItems = flatProducts.map(item => ({
            productId: item.id,
            carryOverQuantity: carryOvers[item.id] ?? counts[item.id] ?? 0
          }));
          await dayClosingApi.saveCarryOver({ branchId: user.branchId!, date: today, items: carryItems });

          const res = await dayClosingApi.closeDay({
            branchId: user.branchId!,
            date: today,
            closedByUserId: user.id!,
            counterItems: counterItems.filter(ci => ci.counterSoldQuantity > 0),
          });
          if (res.success) {
            message.success('Gün başarıyla kapatıldı! Z Raporu oluşturuldu.');
            setIsClosed(true);
          } else {
            message.error(res.message);
          }
          } catch(e: any) {
            message.error(e.message || 'Zaten kapatılmış olabilir veya bir hata oluştu.');
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

  const renderProductCard = (type: 'count' | 'carryover') => {
    if (flatProducts.length === 0) return <Empty />;
    const product = flatProducts[currentProductIndex];
    if (!product) return <Empty />;

    const isCount = type === 'count';
    const value = isCount ? counts[product.id] || 0 : (carryOvers[product.id] ?? counts[product.id] ?? 0);
    const maxValue = isCount ? undefined : (counts[product.id] || 0);

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
          justifyContent: 'center',
          gap: 24
        }}>
          {isCount ? (
            <Statistic 
              title="Mevcut Stok" 
              value={stocks[product.id] || 0} 
              valueStyle={{ fontSize: 20, fontWeight: 600 }}
            />
          ) : (
            <Statistic 
              title="Gün Sonu Sayım" 
              value={counts[product.id] || 0} 
              valueStyle={{ fontSize: 20, fontWeight: 600, color: '#1890ff' }}
            />
          )}
        </div>

        <div style={{ marginBottom: 32 }}>
          <div style={{ textAlign: 'center', marginBottom: 8 }}>
            <Text strong style={{ fontSize: 16 }}>{isCount ? 'Sayım Miktarı' : 'Yarına Devir'}</Text>
          </div>
          <InputNumber 
            min={0}
            max={maxValue}
            precision={2}
            value={value}
            onChange={val => {
              if (isCount) {
                setCounts(prev => ({ ...prev, [product.id]: val || 0 }));
              } else {
                setCarryOvers(prev => ({ ...prev, [product.id]: val || 0 }));
              }
            }}
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
            placeholder="0.00"
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
        {Object.keys(counts).length} / {flatProducts.length} Sayılan
      </Text>
    </div>
  );

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

  const expectedInfo = expectedCashQuery.data?.data;
  
  const dynamicExpectedCash = Math.max(0, 
    (expectedInfo?.openingCashBalance || 0) + 
    (expectedInfo?.totalSalesRevenue || 0) + 
    (expectedInfo?.cashDeposits || 0) - 
    (expectedInfo?.cashPurchases || 0) - 
    (expectedInfo?.cashWithdrawals || 0) - 
    (posAmount || 0)
  );

  const cashDifference = (cashAmount || 0) - dynamicExpectedCash;
  const hasCashDiff = Math.abs(cashDifference) > 0.01;
  const missingPriceProducts = expectedInfo?.items?.filter(i => i.unitPrice === null || i.unitPrice === 0) || [];

  const formatCurrency = (val: number) => `₺${val.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}`;

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      <Title level={2}>📅 Gün Sonu Kapanış</Title>
      
      <Card style={{ marginBottom: 24, borderRadius: 12, boxShadow: '0 2px 8px rgba(0,0,0,0.05)' }}>
        <Steps 
          current={currentStep}
          responsive
          items={isEmployee ? [
            { title: 'Sayım', icon: <ExceptionOutlined /> }
          ] : [
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

        {renderModeSelector()}
        
        {viewMode === 'card' ? (
          renderProductCard('count')
        ) : (
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
                            onFocus={(e) => e.target.select()}
                            inputMode="numeric"
                            keyboard={false}
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
        )}

        {/* SAYAÇ ÜRÜNLERİ */}
        {counterItems.length > 0 && (
          <Card
            style={{ marginTop: 24, borderRadius: 8, border: '1px solid #d9f7be', background: '#f6ffed' }}
            title={
              <Space>
                <ShopOutlined style={{ color: '#52c41a' }} />
                <span style={{ color: '#135200', fontWeight: 600 }}>🎫 Sayaç Ürünleri</span>
                <Tag color="green" style={{ marginLeft: 4 }}>Bugün kaç adet sattınız?</Tag>
              </Space>
            }
          >
            <div style={{ marginBottom: 12 }}>
              <Text type="secondary">Boş bırakılan ürünler 0 satış olarak kaydedilir.</Text>
            </div>
            {counterItems.map((ci) => (
              <Row key={ci.productId} align="middle" style={{ marginBottom: 12, padding: '8px 12px', background: '#fff', borderRadius: 6, border: '1px solid #b7eb8f' }}>
                <Col flex="auto">
                  <Text strong>{ci.productName}</Text>
                  {ci.unitPrice != null && (
                    <Text type="secondary" style={{ fontSize: 11, marginLeft: 8 }}>₺{ci.unitPrice.toLocaleString('tr-TR', { minimumFractionDigits: 2 })} / adet</Text>
                  )}
                </Col>
                <Col>
                  <InputNumber
                    min={0}
                    step={1}
                    precision={0}
                    value={ci.counterSoldQuantity}
                    onChange={val => setCounterItems(prev =>
                      prev.map(item => item.productId === ci.productId
                        ? { ...item, counterSoldQuantity: val ?? 0 }
                        : item
                      )
                    )}
                    style={{ width: 120 }}
                    addonAfter="adet"
                    onFocus={(e) => e.target.select()}
                    inputMode="numeric"
                    keyboard={false}
                  />
                </Col>
              </Row>
            ))}
          </Card>
        )}
        
        <div style={{ textAlign: 'right', marginTop: 24 }}>
          <Button type="primary" size="large" onClick={handleSaveCountsAndNext} loading={saving}>
            {isEmployee ? 'Sayımı Kaydet ve Çıkış' : 'İleri ►'}
          </Button>
        </div>
      </div>

      {!isEmployee && (
        <>
          {/* STEP 2: KASA SAYIMI */}
          <div style={{ display: currentStep === 1 ? 'block' : 'none' }}>
            {expectedCashQuery.isLoading ? (
              <div style={{ textAlign: 'center', padding: 40 }}><Spin tip="Beklenen kasa hesaplanıyor..." /></div>
            ) : (
              <Row gutter={[24, 24]}>
                <Col xs={24} md={10}>
                  <Card 
                    title={<Space><CalculatorOutlined /> Kasa Denklemi</Space>}
                    style={{ background: '#f9f9f9', borderRadius: 12, height: '100%' }}
                  >
                    <Space direction="vertical" style={{ width: '100%' }} size={12}>
                      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Text type="secondary">Açılış Bakiyesi:</Text>
                        <Text>{formatCurrency(expectedInfo?.openingCashBalance || 0)}</Text>
                      </div>
                      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Text type="secondary">Ürün Satışları:</Text>
                        <Text style={{ color: '#52c41a' }}>+{formatCurrency((expectedInfo?.totalSalesRevenue || 0) - (expectedInfo?.counterSalesTotal || 0))}</Text>
                      </div>
                      {(expectedInfo?.counterSalesTotal || 0) > 0 && (
                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                          <Text type="secondary">🎫 Sayaç Satışları:</Text>
                          <Text style={{ color: '#52c41a' }}>+{formatCurrency(expectedInfo?.counterSalesTotal || 0)}</Text>
                        </div>
                      )}
                      {expectedInfo?.cashDeposits! > 0 && (
                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                          <Text type="secondary">Admin Yatırım:</Text>
                          <Text style={{ color: '#52c41a' }}>+{formatCurrency(expectedInfo?.cashDeposits || 0)}</Text>
                        </div>
                      )}
                      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Text type="secondary">Nakit Satın Alımlar:</Text>
                        <Text style={{ color: '#ff4d4f' }}>-{formatCurrency(expectedInfo?.cashPurchases || 0)}</Text>
                      </div>
                      {expectedInfo?.cashWithdrawals! > 0 && (
                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                          <Text type="secondary">Admin Çekim:</Text>
                          <Text style={{ color: '#ff4d4f' }}>-{formatCurrency(expectedInfo?.cashWithdrawals || 0)}</Text>
                        </div>
                      )}
                      {(posAmount || 0) > 0 && (
                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                          <Text type="secondary">Günlük POS (Z Raporu):</Text>
                          <Text style={{ color: '#ff4d4f' }}>-{formatCurrency(posAmount || 0)}</Text>
                        </div>
                      )}
                      
                      <Divider style={{ margin: '8px 0' }} />
                      
                      <Statistic 
                        title={<Text strong style={{ fontSize: 16 }}>Beklenen Nakit Kasa</Text>}
                        value={dynamicExpectedCash}
                        precision={2}
                        prefix="₺"
                        valueStyle={{ color: '#1d39c4', fontSize: 32, fontWeight: 'bold' }}
                      />
                      
                      {(expectedInfo?.productsWithoutPrice || 0) > 0 && (
                        <Alert 
                          type="warning" 
                          showIcon 
                          style={{ marginTop: 16 }}
                          message={
                            <div style={{ fontSize: 12 }}>
                              <Text strong>{expectedInfo?.productsWithoutPrice} ürünün fiyatı tanımlı değil.</Text>
                              <div style={{ marginTop: 4 }}>
                                <Button size="small" type="link" onClick={() => setIsMissingPricesModalVisible(true)}>Ürünleri Gör</Button>
                              </div>
                            </div>
                          }
                        />
                      )}
                    </Space>
                  </Card>
                </Col>
                
                <Col xs={24} md={14}>
                  <Card title="Kasa Girişi" style={{ borderRadius: 12, height: '100%' }}>
                    <Form layout="vertical">
                      <Row gutter={16}>
                        <Col xs={24} sm={12}>
                          <Form.Item 
                            label={<Text strong>Sayılan Nakit (Elde Kalan)</Text>} 
                            required
                          >
                            <InputNumber 
                              style={{ width: '100%', borderColor: '#1890ff' }} 
                              size="large" 
                              addonBefore="₺" 
                              precision={2} 
                              min={0}
                              value={cashAmount}
                              onChange={val => setCashAmount(val)}
                              placeholder="Kasada ne kadar nakit var?"
                              onFocus={(e) => e.target.select()}
                              inputMode="numeric"
                              keyboard={false}
                            />
                          </Form.Item>
                        </Col>
                        <Col xs={24} sm={12}>
                          <Form.Item label="Günlük POS (Z Raporu)" required>
                            <InputNumber 
                              style={{ width: '100%' }} 
                              size="large" 
                              addonBefore="₺" 
                              precision={2} 
                              min={0}
                              value={posAmount}
                              onChange={val => setPosAmount(val)}
                              onFocus={(e) => e.target.select()}
                              inputMode="numeric"
                              keyboard={false}
                            />
                          </Form.Item>
                        </Col>
                      </Row>
                      
                      <div style={{ padding: '16px', background: '#f0f5ff', borderRadius: 8, marginTop: 8, marginBottom: 16 }}>
                        <Row justify="space-between" align="middle">
                          <Col>
                            <Text strong style={{ fontSize: 16 }}>Kasa Farkı:</Text>
                          </Col>
                          <Col>
                            {cashAmount !== null ? (
                              <Text strong style={{ 
                                color: diffColor(cashDifference), 
                                fontSize: 24 
                              }}>
                                {cashDifference > 0 ? '+' : ''}₺ {cashDifference.toLocaleString('tr-TR', { minimumFractionDigits: 2 })}
                                {' '}{diffIcon(cashDifference)}
                              </Text>
                            ) : (
                              <Text type="secondary">Tutar bekleniyor...</Text>
                            )}
                          </Col>
                        </Row>
                      </div>

                      {hasCashDiff && (
                        <Form.Item 
                          label={
                            <span>Fark Açıklaması <Text type="danger">* Zorunlu</Text></span>
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
                        <Panel header={<Text style={{ color: '#1890ff' }}>Ürün Bazlı Satış Detaylarını Gör</Text>} key="1">
                          <style>{`
                            .counter-row {
                               background-color: #f0f5ff;
                            }
                          `}</style>
                          <Table 
                            dataSource={expectedInfo?.items || []} 
                            size="small"
                            rowKey={(record) => `${record.productName}-${record.isCounter}`}
                            pagination={{ pageSize: 10 }}
                            scroll={{ x: 'max-content' }}
                            rowClassName={(record) => record.isCounter ? 'counter-row' : ''}
                            columns={[
                              { title: 'Kategori', dataIndex: 'categoryName' },
                              { title: 'Ürün', dataIndex: 'productName' },
                              { title: 'Satış Miktarı', dataIndex: 'calculatedSales', align: 'right' },
                              { title: 'Birim Fiyat', dataIndex: 'unitPrice', align: 'right', render: v => v ? `₺${v.toFixed(2)}` : '-' },
                              { title: 'Gelir', dataIndex: 'salesValue', align: 'right', render: v => v ? `₺${v.toFixed(2)}` : '-' },
                              { 
                                title: 'Tür', 
                                key: 'type', 
                                render: (_, record) => 
                                  record.isCounter 
                                    ? <Tag color="blue">Sayaç</Tag> 
                                    : <Tag color="green">Stok</Tag> 
                              }
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

            {renderModeSelector()}
            
            {viewMode === 'card' ? (
              renderProductCard('carryover')
            ) : (
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
                                value={carryOvers[p.id] ?? counts[p.id] ?? 0} 
                                onChange={val => setCarryOvers(prev => ({ ...prev, [p.id]: val || 0 }))}
                                style={{ width: '80%' }}
                                onFocus={(e) => e.target.select()}
                                inputMode="numeric"
                                keyboard={false}
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
            )}
            
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
        </>
      )}
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
