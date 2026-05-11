import React, { useState } from 'react';
import {
  Tabs,
  Table,
  Button,
  Modal,
  Drawer,
  Form,
  Input,
  Select,
  Tag,
  Space,
  Popconfirm,
  Card,
  InputNumber,
  Row,
  Col,
  Typography,
} from 'antd';
import {
  PlusOutlined,
  EyeOutlined,
  CloseCircleOutlined,
  CheckCircleOutlined,
  DeleteOutlined,
} from '@ant-design/icons';
import dayjs from 'dayjs';
import type { ColumnsType } from 'antd/es/table';

import {
  useOutgoingTransfers,
  useIncomingTransfers,
  useCreateTransfer,
  useReceiveTransfer,
  useCancelTransfer,
} from '../../hooks/useTransfers';
import { TransferStatus, TransferStatusColor, TransferStatusLabel } from '../../types/transfer';
import type { TransferDto, CreateTransferItemRequest } from '../../types/transfer';
import { useBranches } from '../../hooks/useBranches';
import { useStock } from '../../hooks/useStock';
import useAuth from '../../hooks/useAuth';

const { Text } = Typography;

const Transfers: React.FC = () => {
  const { user } = useAuth();
  const [activeTab, setActiveTab] = useState('outgoing');
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [isCancelModalOpen, setIsCancelModalOpen] = useState(false);
  const [selectedTransfer, setSelectedTransfer] = useState<TransferDto | null>(null);
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);
  
  const [createForm] = Form.useForm();
  const [cancelForm] = Form.useForm();

  const { data: outgoingTransfers = [], isLoading: isOutgoingLoading } = useOutgoingTransfers();
  const { data: incomingTransfers = [], isLoading: isIncomingLoading } = useIncomingTransfers();
  const { data: branches = [] } = useBranches();
  const { data: stockData = [] } = useStock(user?.branchId || '', dayjs().format('YYYY-MM-DD'));

  const createMutation = useCreateTransfer();
  const receiveMutation = useReceiveTransfer();
  const cancelMutation = useCancelTransfer();

  // Stok listesi (Counter olmayanlar)
  const availableProducts = stockData.filter((s: any) => s.currentStock > 0);

  const handleCreateSubmit = async (values: any) => {
    await createMutation.mutateAsync({
      receiverBranchId: values.receiverBranchId,
      notes: values.notes,
      items: values.items.map((i: any) => ({
        productId: i.productId,
        quantity: Number(i.quantity),
      })),
    });
    setIsCreateModalOpen(false);
    createForm.resetFields();
  };

  const handleCancelSubmit = async (values: any) => {
    if (!selectedTransfer) return;
    await cancelMutation.mutateAsync({
      id: selectedTransfer.id,
      request: { cancellationReason: values.cancellationReason },
    });
    setIsCancelModalOpen(false);
    cancelForm.resetFields();
    setSelectedTransfer(null);
  };

  const handleReceive = async (transfer: TransferDto) => {
    Modal.confirm({
      title: 'Transferi Teslim Al',
      content: (
        <div>
          <p>{transfer.transferNumber} numaralı transferi teslim almak üzeresiniz.</p>
          <p>Tüm ürünler stoğunuza eklenecek.</p>
          <ul>
            {transfer.items.map((item, idx) => (
              <li key={idx}>
                {item.productName} × {item.quantity} {item.unit}
              </li>
            ))}
          </ul>
        </div>
      ),
      okText: 'Teslim Al',
      cancelText: 'İptal',
      onOk: async () => {
        await receiveMutation.mutateAsync(transfer.id);
      },
    });
  };

  const columns: ColumnsType<TransferDto> = [
    {
      title: 'Transfer No',
      dataIndex: 'transferNumber',
      key: 'transferNumber',
    },
    {
      title: activeTab === 'outgoing' ? 'Alıcı Şube' : 'Gönderen Şube',
      key: 'branch',
      render: (_, record) =>
        activeTab === 'outgoing' ? record.receiverBranchName : record.senderBranchName,
    },
    {
      title: 'Ürün Sayısı',
      key: 'itemCount',
      render: (_, record) => `${record.items.length} kalem`,
    },
    {
      title: 'Durum',
      dataIndex: 'status',
      key: 'status',
      render: (status: TransferStatus) => (
        <Tag color={TransferStatusColor[status]}>{TransferStatusLabel[status]}</Tag>
      ),
    },
    {
      title: 'Tarih',
      dataIndex: 'shippedAt',
      key: 'shippedAt',
      render: (date: string) => dayjs(date).format('DD.MM.YYYY HH:mm'),
    },
    {
      title: 'İşlem',
      key: 'action',
      render: (_, record) => (
        <Space size="middle">
          <Button
            type="link"
            icon={<EyeOutlined />}
            onClick={() => {
              setSelectedTransfer(record);
              setIsDrawerOpen(true);
            }}
          >
            Detay
          </Button>

          {activeTab === 'outgoing' && record.status === TransferStatus.Shipped && (
            <Button
              type="link"
              danger
              icon={<CloseCircleOutlined />}
              onClick={() => {
                setSelectedTransfer(record);
                setIsCancelModalOpen(true);
              }}
            >
              İptal
            </Button>
          )}

          {activeTab === 'incoming' && record.status === TransferStatus.Shipped && (
            <Button
              type="primary"
              icon={<CheckCircleOutlined />}
              onClick={() => handleReceive(record)}
            >
              Teslim Al
            </Button>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Card
        title={
          <Space>
            {activeTab === 'outgoing' ? '📤 Gönderilen Transferler' : '📥 Gelen Transferler'}
          </Space>
        }
        extra={
          activeTab === 'outgoing' && (
            <Button
              type="primary"
              icon={<PlusOutlined />}
              onClick={() => {
                createForm.resetFields();
                setIsCreateModalOpen(true);
              }}
            >
              Yeni Transfer
            </Button>
          )
        }
      >
        <Tabs
          activeKey={activeTab}
          onChange={setActiveTab}
          items={[
            {
              key: 'outgoing',
              label: '📤 Gönderilen',
              children: (
                <Table
                  columns={columns}
                  dataSource={outgoingTransfers}
                  rowKey="id"
                  loading={isOutgoingLoading}
                  scroll={{ x: 'max-content' }}
                />
              ),
            },
            {
              key: 'incoming',
              label: '📥 Gelen',
              children: (
                <Table
                  columns={columns}
                  dataSource={incomingTransfers}
                  rowKey="id"
                  loading={isIncomingLoading}
                  scroll={{ x: 'max-content' }}
                />
              ),
            },
          ]}
        />
      </Card>

      {/* Yeni Transfer Modal */}
      <Modal
        title="Yeni Transfer Gönder"
        open={isCreateModalOpen}
        onCancel={() => setIsCreateModalOpen(false)}
        onOk={() => createForm.submit()}
        confirmLoading={createMutation.isPending}
        width={700}
      >
        <Form form={createForm} layout="vertical" onFinish={handleCreateSubmit}>
          <Form.Item
            name="receiverBranchId"
            label="Alıcı Şube"
            rules={[{ required: true, message: 'Lütfen alıcı şube seçin' }]}
          >
            <Select
              showSearch
              placeholder="Şube Seçin"
              filterOption={(input, option) =>
                (option?.label ?? '').toString().toLowerCase().includes(input.toLowerCase())
              }
              options={branches
                .filter((b: any) => b.id !== user?.branchId)
                .map((b: any) => ({ value: b.id, label: b.name }))}
            />
          </Form.Item>

          <Form.List
            name="items"
            rules={[
              {
                validator: async (_, items) => {
                  if (!items || items.length < 1) {
                    return Promise.reject(new Error('En az 1 ürün eklemelisiniz.'));
                  }
                },
              },
            ]}
          >
            {(fields, { add, remove }, { errors }) => (
              <>
                <div style={{ marginBottom: 16 }}>
                  <Button type="dashed" onClick={() => add()} icon={<PlusOutlined />} block>
                    Ürün Ekle
                  </Button>
                  <Form.ErrorList errors={errors} />
                </div>
                {fields.map(({ key, name, ...restField }) => (
                  <Card size="small" key={key} style={{ marginBottom: 8 }}>
                    <Row gutter={16} align="middle">
                      <Col span={14}>
                        <Form.Item
                          {...restField}
                          name={[name, 'productId']}
                          rules={[{ required: true, message: 'Ürün seçin' }]}
                          style={{ marginBottom: 0 }}
                        >
                          <Select
                            showSearch
                            placeholder="Ürün Seçin"
                            filterOption={(input, option) =>
                              (option?.label ?? '').toString().toLowerCase().includes(input.toLowerCase())
                            }
                            options={stockData.map((s: any) => ({
                              value: s.productId,
                              label: `${s.productName} (Stok: ${s.currentStock})`,
                            }))}
                          />
                        </Form.Item>
                      </Col>
                      <Col span={8}>
                        <Form.Item
                          {...restField}
                          name={[name, 'quantity']}
                          rules={[
                            { required: true, message: 'Miktar girin' },
                            { type: 'number', min: 0.01, message: 'Miktar 0 dan büyük olmalı' },
                          ]}
                          style={{ marginBottom: 0 }}
                        >
                          <InputNumber placeholder="Miktar" style={{ width: '100%' }} />
                        </Form.Item>
                      </Col>
                      <Col span={2}>
                        <Button type="text" danger icon={<DeleteOutlined />} onClick={() => remove(name)} />
                      </Col>
                    </Row>
                  </Card>
                ))}
              </>
            )}
          </Form.List>

          <Form.Item name="notes" label="Notlar">
            <Input.TextArea rows={2} placeholder="Opsiyonel" />
          </Form.Item>
        </Form>
      </Modal>

      {/* İptal Modal */}
      <Modal
        title="Transferi İptal Et"
        open={isCancelModalOpen}
        onCancel={() => setIsCancelModalOpen(false)}
        onOk={() => cancelForm.submit()}
        confirmLoading={cancelMutation.isPending}
      >
        <Form form={cancelForm} layout="vertical" onFinish={handleCancelSubmit}>
          <Form.Item
            name="cancellationReason"
            label="İptal Sebebi"
            rules={[{ required: true, message: 'Lütfen iptal sebebi girin' }]}
          >
            <Input.TextArea rows={3} placeholder="Neden iptal ediliyor?" />
          </Form.Item>
        </Form>
      </Modal>

      {/* Detay Drawer */}
      <Drawer
        title="Transfer Detayı"
        placement="right"
        width={500}
        onClose={() => setIsDrawerOpen(false)}
        open={isDrawerOpen}
      >
        {selectedTransfer && (
          <Space direction="vertical" style={{ width: '100%' }} size="large">
            <Card size="small" title="Genel Bilgiler">
              <Row gutter={[16, 16]}>
                <Col span={12}>
                  <Text type="secondary">Transfer No:</Text>
                  <div><Text strong>{selectedTransfer.transferNumber}</Text></div>
                </Col>
                <Col span={12}>
                  <Text type="secondary">Durum:</Text>
                  <div>
                    <Tag color={TransferStatusColor[selectedTransfer.status]}>
                      {TransferStatusLabel[selectedTransfer.status]}
                    </Tag>
                  </div>
                </Col>
                <Col span={12}>
                  <Text type="secondary">Gönderen:</Text>
                  <div><Text>{selectedTransfer.senderBranchName}</Text></div>
                </Col>
                <Col span={12}>
                  <Text type="secondary">Alıcı:</Text>
                  <div><Text>{selectedTransfer.receiverBranchName}</Text></div>
                </Col>
                <Col span={12}>
                  <Text type="secondary">Gönderim Zamanı:</Text>
                  <div><Text>{dayjs(selectedTransfer.shippedAt).format('DD.MM.YYYY HH:mm')}</Text></div>
                </Col>
                {selectedTransfer.receivedAt && (
                  <Col span={12}>
                    <Text type="secondary">Teslim Zamanı:</Text>
                    <div><Text>{dayjs(selectedTransfer.receivedAt).format('DD.MM.YYYY HH:mm')}</Text></div>
                  </Col>
                )}
                {selectedTransfer.cancelledAt && (
                  <Col span={12}>
                    <Text type="secondary">İptal Zamanı:</Text>
                    <div><Text>{dayjs(selectedTransfer.cancelledAt).format('DD.MM.YYYY HH:mm')}</Text></div>
                  </Col>
                )}
                <Col span={24}>
                  <Text type="secondary">Notlar:</Text>
                  <div><Text>{selectedTransfer.notes || '—'}</Text></div>
                </Col>
                {selectedTransfer.cancellationReason && (
                  <Col span={24}>
                    <Text type="secondary">İptal Sebebi:</Text>
                    <div><Text type="danger">{selectedTransfer.cancellationReason}</Text></div>
                  </Col>
                )}
              </Row>
            </Card>

            <Card size="small" title={`Ürünler (${selectedTransfer.items.length} kalem)`}>
              <Table
                dataSource={selectedTransfer.items}
                rowKey="productId"
                pagination={false}
                size="small"
                scroll={{ x: 'max-content' }}
                columns={[
                  {
                    title: 'Ürün',
                    dataIndex: 'productName',
                    key: 'productName',
                  },
                  {
                    title: 'Miktar',
                    dataIndex: 'quantity',
                    key: 'quantity',
                  },
                  {
                    title: 'Birim',
                    dataIndex: 'unit',
                    key: 'unit',
                  },
                ]}
              />
            </Card>
          </Space>
        )}
      </Drawer>
    </div>
  );
};

export default Transfers;
