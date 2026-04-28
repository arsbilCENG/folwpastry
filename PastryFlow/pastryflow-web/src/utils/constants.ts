export const DEMAND_STATUS_CONFIG: Record<string, { color: string; text: string }> = {
  Pending: { color: 'orange', text: 'Bekliyor' },
  Approved: { color: 'green', text: 'Onaylandı' },
  Rejected: { color: 'red', text: 'Reddedildi' },
  PartiallyApproved: { color: 'blue', text: 'Kısmen Onaylandı' },
  Shipped: { color: 'cyan', text: 'Gönderildi' },
  Delivered: { color: 'geekblue', text: 'Yolda' },
  Received: { color: 'purple', text: 'Teslim Alındı' },
  Cancelled: { color: 'default', text: 'İptal Edildi' },
};

export const UNIT_TYPES = {
  0: 'Adet',
  1: 'Kilogram',
  2: 'Gram',
  3: 'Litre',
  4: 'Paket'
};

export const ROLE_LABELS: Record<string | number, string> = {
  0: 'Admin',
  1: 'Mutfak',
  2: 'Satış',
  3: 'Şoför',
  'Admin': 'Admin',
  'Production': 'Mutfak',
  'Sales': 'Satış',
  'Driver': 'Şoför'
};

export const CHART_COLORS = [
  '#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#8884d8', 
  '#82ca9d', '#ffc658', '#8dd1e1', '#a4de6c', '#d0ed57'
];

export const CAKE_ORDER_STATUS_CONFIG: Record<string, { color: string; text: string }> = {
  Created: { color: 'orange', text: 'Oluşturuldu' },
  SentToProduction: { color: 'blue', text: 'Üretime Gönderildi' },
  InProduction: { color: 'processing', text: 'Üretimde' },
  Ready: { color: 'green', text: 'Hazır' },
  Delivered: { color: 'purple', text: 'Teslim Edildi' },
  Cancelled: { color: 'default', text: 'İptal' },
};
