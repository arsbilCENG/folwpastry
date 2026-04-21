import React, { useState, useMemo } from 'react';
import { 
  Table, Card, Row, Col, DatePicker, Select, Button, Space, 
  Empty, Spin, Typography, Modal, Form, InputNumber, Input, 
  message, Tag, Alert, Descriptions, Divider 
} from 'antd';
import { 
  SearchOutlined, 
  EditOutlined, 
  CheckCircleOutlined,
  CalendarOutlined,
  InfoCircleOutlined
} from '@ant-design/icons';
import dayjs from 'dayjs';
import { useAdminBranches } from '../../hooks/useAdmin';
import { useAdminDayClosings, useDayClosingCorrection } from '../../hooks/useAdmin';

const { Title, Text, Paragraph } = Typography;
const { TextArea } = Input;

const DayCorrection: React.FC = () => {
  const [date, setDate] = useState<string>(dayjs().format('YYYY-MM-DD'));
  const [branchId, setBranchId] = useState<string | undefined>(undefined);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentRecord, setCurrentRecord] = useState<any>(null);
  
  const [form] = Form.useForm();
  
  const { data: branches } = useAdminBranches();
  const salesBranches = useMemo(() => 
    branches?.filter(b => b.branchType === 'Sales') || [], 
  [branches]);

  const { data: dayClosings, isLoading, refetch } = useAdminDayClosings(branchId, date);
  const correctionMutation = useDayClosingCorrection();

  const handleEdit = (record: any) => {
    setCurrentRecord(record);
    form.setFieldsValue({
      correctedEndOfDayCount: record.endOfDayCount,
      correctedCarryOverQuantity: record.carryOverQuantity,
      correctionReason: '',
    });
    setIsModalOpen(true);
  };

  const handleModalSubmit = async () => {
    try {
      const values = await form.validateFields();
      
      if (values.correctedCarryOverQuantity > values.correctedEndOfDayCount) {
        message.error('Devir miktarı gün sonu sayımından büyük olamaz!');
        return;
      }

      await correctionMutation.mutateAsync({
        dayClosingId: currentRecord.id,
        data: {
          dayClosingDetailId: currentRecord.id, // API expects this
          correctedEndOfDayCount: values.correctedEndOfDayCount,
          correctedCarryOverQuantity: values.correctedCarryOverQuantity,
          correctionReason: values.correctionReason,
        }
      });
      
      setIsModalOpen(false);
      refetch();
    } catch (error) {
      console.error('Validation failed:', error);
    }
  };

  const columns = [
    {
      title: 'Ürün Adı',
      dataIndex: 'productName',
      key: 'productName',
      sorter: (a: any, b: any) => a.productName.localeCompare(b.productName),
    },
    {
      title: 'Birim',
      dataIndex: 'unit',
      key: 'unit',
      width: 80,
    },
    {
      title: 'Açılış',
      dataIndex: 'openingStock',
      key: 'openingStock',
      align: 'right' as const,
    },
    {
      title: 'Zayiat',
      dataIndex: 'dayWaste',
      key: 'dayWaste',
      align: 'right' as const,
    },
    {
      title: 'Sayım',
      dataIndex: 'endOfDayCount',
      key: 'endOfDayCount',
      align: 'right' as const,
      render: (val: number, record: any) => (
        <Space direction="vertical" size={0}>
          <Text>{val}</Text>
          {record.isCorrected && (
            <Text type="secondary" delete style={{ fontSize: '11px' }}>
              {record.originalEndOfDayCount}
            </Text>
          )}
        </Space>
      )
    },
    {
      title: 'Devir',
      dataIndex: 'carryOver',
      key: 'carryOver',
      align: 'right' as const,
      render: (val: number, record: any) => (
        <Space direction="vertical" size={0}>
          <Text>{val}</Text>
          {record.isCorrected && (
            <Text type="secondary" delete style={{ fontSize: '11px' }}>
              {record.originalCarryOverQuantity}
            </Text>
          )}
        </Space>
      )
    },
    {
      title: 'Hes. Satış',
      dataIndex: 'calculatedSales',
      key: 'calculatedSales',
      align: 'right' as const,
    },
    {
      title: 'Durum',
      key: 'status',
      align: 'center' as const,
      render: (_: any, record: any) => record.isCorrected ? (
        <Tag icon={<CheckCircleOutlined />} color="success">Düzeltildi</Tag>
      ) : (
        <Text type="secondary">—</Text>
      )
    },
    {
      title: 'İşlem',
      key: 'action',
      align: 'center' as const,
      render: (_: any, record: any) => (
        <Button 
          type="link" 
          icon={<EditOutlined />} 
          onClick={() => handleEdit(record)}
        >
          Düzelt
        </Button>
      )
    }
  ];

  return (
    <div style={{ padding: '24px' }}>
      <Title level={2}>📅 Gün Sonu Düzeltme</Title>

      <Card style={{ marginBottom: 24 }}>
        <Row gutter={[16, 16]} align="middle">
          <Col xs={24} md={8}>
            <Select
              style={{ width: '100%' }}
              placeholder="Şube Seçin"
              value={branchId}
              onChange={setBranchId}
              options={salesBranches.map(b => ({ label: b.name, value: b.id }))}
            />
          </Col>
          <Col xs={24} md={6}>
            <DatePicker 
              style={{ width: '100%' }}
              value={dayjs(date)}
              onChange={(d) => setDate(d ? d.format('YYYY-MM-DD') : dayjs().format('YYYY-MM-DD'))}
            />
          </Col>
          <Col xs={24} md={4}>
            <Button 
              type="primary" 
              icon={<SearchOutlined />} 
              loading={isLoading}
              disabled={!branchId}
              onClick={() => refetch()}
            >
              Getir
            </Button>
          </Col>
        </Row>
      </Card>

      <Alert
        message="Bilgilendirme"
        description="Bu sayfa kapatılmış günlerin sayım değerlerini düzeltmek içindir. Yapılan tüm değişiklikler ve düzeltme sebepleri sistemde kayıt altına alınır."
        type="info"
        showIcon
        icon={<InfoCircleOutlined />}
        style={{ marginBottom: 24 }}
      />

      <Card>
        {!branchId ? (
          <Empty description="Lütfen bir şube ve tarih seçerek verileri getirin" />
        ) : isLoading ? (
          <div style={{ textAlign: 'center', padding: '50px' }}><Spin tip="Veriler yükleniyor..." /></div>
        ) : !dayClosings || ((dayClosings as any).items?.length || 0) === 0 ? (
          <Empty description="Bu şube ve tarih için gün kapanış verisi bulunamadı" />
        ) : (
          <Table
            columns={columns}
            dataSource={(dayClosings as any).items || []}
            rowKey="id"
            pagination={false}
            bordered
          />
        )}
      </Card>

      <Modal
        title={`Düzeltme: ${currentRecord?.productName}`}
        open={isModalOpen}
        onOk={handleModalSubmit}
        onCancel={() => setIsModalOpen(false)}
        confirmLoading={correctionMutation.isPending}
        okText="Kaydet"
        cancelText="İptal"
        width={600}
      >
        <Form form={form} layout="vertical">
          <Divider orientation={"left" as any}>Mevcut Değerler</Divider>
          <Descriptions column={2} bordered size="small" style={{ marginBottom: 24 }}>
            <Descriptions.Item label="Gün Sonu Sayım">{currentRecord?.endOfDayCount}</Descriptions.Item>
            <Descriptions.Item label="Devir Miktarı">{currentRecord?.carryOver}</Descriptions.Item>
            {currentRecord?.isCorrected && (
              <Descriptions.Item label="Son Düzeltme Sebebi" span={2}>
                <Text type="secondary">{currentRecord.lastCorrectionReason}</Text>
              </Descriptions.Item>
            )}
          </Descriptions>

          <Divider orientation={"left" as any}>Yeni Değerler</Divider>
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item
                name="correctedEndOfDayCount"
                label="Yeni Gün Sonu Sayım"
                rules={[{ required: true, message: 'Lütfen yeni sayım miktarını girin' }]}
              >
                <InputNumber 
                  style={{ width: '100%' }} 
                  min={0} 
                  precision={currentRecord?.unit === 'Kg' ? 2 : 0} 
                />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                name="correctedCarryOverQuantity"
                label="Yeni Devir Miktarı"
                rules={[{ required: true, message: 'Lütfen yeni devir miktarını girin' }]}
              >
                <InputNumber 
                  style={{ width: '100%' }} 
                  min={0} 
                  precision={currentRecord?.unit === 'Kg' ? 2 : 0} 
                />
              </Form.Item>
            </Col>
          </Row>

          <Form.Item
            name="correctionReason"
            label="Düzeltme Sebebi"
            extra="Minimum 10 karakter girmelisiniz."
            rules={[
              { required: true, message: 'Lütfen düzeltme sebebini belirtin' },
              { min: 10, message: 'Sebep en az 10 karakter olmalıdır' }
            ]}
          >
            <TextArea rows={4} placeholder="Düzeltme nedenini detaylıca yazınız..." showCount maxLength={500} />
          </Form.Item>
          
          <Paragraph type="warning">
            <InfoCircleOutlined /> Dikkat: Devir miktarı, girilen gün sonu sayımından büyük olamaz.
          </Paragraph>
        </Form>
      </Modal>
    </div>
  );
};

export default DayCorrection;
