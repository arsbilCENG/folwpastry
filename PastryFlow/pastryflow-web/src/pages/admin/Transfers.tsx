import React, { useState } from 'react';
import {
  Tabs,
  Table,
  Button,
  Modal,
  Drawer,
  Form,
  Input,
  Tag,
  Space,
  Card,
  Row,
  Col,
  Typography,
} from 'antd';
import {
  EyeOutlined,
  CloseCircleOutlined,
} from '@ant-design/icons';
import dayjs from 'dayjs';
import type { ColumnsType } from 'antd/es/table';
import { useOutletContext } from 'react-router-dom';

import {
  useAllTransfers,
  useCancelTransfer,
} from '../../hooks/useTransfers';
import { TransferStatus, TransferStatusColor, TransferStatusLabel } from '../../types/transfer';
import type { TransferDto } from '../../types/transfer';

const { Text } = Typography;

const AdminTransfers: React.FC = () => {
  const { selectedBranchId } = useOutletContext<{ selectedBranchId: string | null }>();
  const [activeTab, setActiveTab] = useState('all');
  const [isCancelModalOpen, setIsCancelModalOpen] = useState(false);
  const [selectedTransfer, setSelectedTransfer] = useState<TransferDto | null>(null);
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);
  
  const [cancelForm] = Form.useForm();

  const { data: allTransfers = [], isLoading } = useAllTransfers();
  const cancelMutation = useCancelTransfer();

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

  const filteredTransfers = allTransfers.filter((t) => {
    if (activeTab === 'outgoing') {
      if (selectedBranchId && t.senderBranchId !== selectedBranchId) return false;
      return true;
    }
    if (activeTab === 'incoming') {
      if (selectedBranchId && t.receiverBranchId !== selectedBranchId) return false;
      return true;
    }
    // all tab
    if (selectedBranchId && t.senderBranchId !== selectedBranchId && t.receiverBranchId !== selectedBranchId) {
      return false;
    }
    return true;
  });

  const columns: ColumnsType<TransferDto> = [
    {
      title: 'Transfer No',
      dataIndex: 'transferNumber',
      key: 'transferNumber',
    },
    {
      title: 'Gönderen Şube',
      dataIndex: 'senderBranchName',
      key: 'senderBranchName',
    },
    {
      title: 'Alıcı Şube',
      dataIndex: 'receiverBranchName',
      key: 'receiverBranchName',
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

          {record.status === TransferStatus.Shipped && (
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
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Card title="Tüm Şube Transferleri">
        <Tabs
          activeKey={activeTab}
          onChange={setActiveTab}
          items={[
            {
              key: 'outgoing',
              label: '📤 Gönderilen',
            },
            {
              key: 'incoming',
              label: '📥 Gelen',
            },
            {
              key: 'all',
              label: '📋 Tüm Transferler',
            },
          ]}
        />
        <Table
          columns={columns}
          dataSource={filteredTransfers.filter(t => {
            if (activeTab === 'outgoing') return t.senderBranchId === selectedBranchId || !selectedBranchId;
            if (activeTab === 'incoming') return t.receiverBranchId === selectedBranchId || !selectedBranchId;
            return true;
          })}
          rowKey="id"
          loading={isLoading}
          scroll={{ x: 'max-content' }}
        />
      </Card>

      {/* İptal Modal */}
      <Modal
        title="Transferi İptal Et (Admin)"
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

export default AdminTransfers;
