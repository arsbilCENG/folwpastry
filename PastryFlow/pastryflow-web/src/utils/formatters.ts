export const formatDate = (date: string | null | undefined): string => {
  if (!date) return '-';
  return new Date(date).toLocaleDateString('tr-TR');
};

export const formatDateTime = (date: string | null | undefined): string => {
  if (!date) return '-';
  return new Date(date).toLocaleString('tr-TR');
};

export const formatCurrency = (value: number | null | undefined): string => {
  if (value == null) return '-';
  return `₺ ${value.toLocaleString('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
};

export const formatNumber = (value: number | null | undefined, unit: string = ''): string => {
  if (value == null) return '-';
  const formatted = value.toLocaleString('tr-TR', { maximumFractionDigits: 2 });
  return unit ? `${formatted} ${unit}` : formatted;
};

export const getStatusColor = (status: string): string => {
  switch (status.toLowerCase()) {
    case 'pending': return 'orange';
    case 'approved': return 'green';
    case 'partiallyapproved': return 'cyan';
    case 'rejected': return 'red';
    case 'delivered': return 'blue';
    case 'received': return 'purple';
    default: return 'default';
  }
};

export const getStatusText = (status: string): string => {
  switch (status.toLowerCase()) {
    case 'pending': return 'Bekliyor';
    case 'approved': return 'Onaylandı';
    case 'partiallyapproved': return 'Kısmen Onaylandı';
    case 'rejected': return 'Reddedildi';
    case 'delivered': return 'Yola Çıktı';
    case 'received': return 'Teslim Alındı';
    default: return status;
  }
};
