import ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import type {
  DailySummaryReport,
  PeriodSummaryReport,
  ManagementReport,
} from '../types/report';
import { formatCurrency, formatDate } from './formatters';

const safeFilePart = (s: string) => s.replace(/[/\\?%*:|"<>]/g, '-').trim() || 'rapor';

// ─── Türkçe PDF font (Roboto) — mevcut pattern ─────────────────

const loadTurkishFont = async (doc: jsPDF): Promise<boolean> => {
  try {
    const response = await fetch('/fonts/Roboto-Regular.ttf');
    if (!response.ok) return false;

    const buffer = await response.arrayBuffer();
    const binary = new Uint8Array(buffer);
    let str = '';
    for (let i = 0; i < binary.length; i++) {
      str += String.fromCharCode(binary[i]);
    }
    const base64 = btoa(str);

    doc.addFileToVFS('Roboto-Regular.ttf', base64);
    doc.addFont('Roboto-Regular.ttf', 'Roboto', 'normal');
    doc.setFont('Roboto');
    return true;
  } catch {
    console.warn('Türkçe font yüklenemedi, varsayılan font kullanılacak.');
    return false;
  }
};

const pdfFontName = async (doc: jsPDF): Promise<string> => {
  const ok = await loadTurkishFont(doc);
  return ok ? 'Roboto' : 'helvetica';
};

export type WalletBalanceExportRow = {
  branchName: string;
  cashBalance: number;
  bankBalance: number;
  totalBalance: number;
};

// ─── Günlük Özet Export ────────────────────────────────────

export const exportDailySummaryExcel = async (report: DailySummaryReport): Promise<void> => {
  const wb = new ExcelJS.Workbook();
  const ws = wb.addWorksheet('Günlük Özet');

  ws.mergeCells('A1:F1');
  ws.getCell('A1').value = `${report.branchName} — Günlük Özet — ${formatDate(report.date)}`;
  ws.getCell('A1').font = { bold: true, size: 14 };
  ws.getCell('A1').alignment = { horizontal: 'center' };

  ws.addRow([]);
  ws.addRow(['Toplam Satış', formatCurrency(report.totalSalesRevenue)]);
  ws.addRow(['  Ürün Satışları', formatCurrency(report.productSalesRevenue)]);
  ws.addRow(['  Sayaç Satışları', formatCurrency(report.counterSalesRevenue)]);
  ws.addRow(['Satın Alım Gideri', formatCurrency(report.totalPurchaseExpense)]);
  ws.addRow(['  Nakit', formatCurrency(report.cashPurchaseExpense)]);
  ws.addRow(['  Kart', formatCurrency(report.cardPurchaseExpense)]);
  ws.addRow(['Beklenen Kasa', formatCurrency(report.expectedCashAmount)]);
  ws.addRow(['Gerçek Kasa', formatCurrency(report.actualCashAmount)]);
  ws.addRow(['Kasa Farkı', formatCurrency(report.cashDifference)]);

  ws.addRow([]);
  ws.addRow(['KATEGORİ', 'ÜRÜN', 'TÜR', 'ADET/KG', 'BİRİM FİYAT', 'GELİR']);
  const headerRow = ws.lastRow!;
  headerRow.font = { bold: true };
  headerRow.fill = {
    type: 'pattern',
    pattern: 'solid',
    fgColor: { argb: 'FFE8F4FD' },
  };

  report.productSales.forEach((item) => {
    ws.addRow([
      item.categoryName,
      item.productName,
      item.isCounter ? 'Sayaç' : 'Stok',
      item.soldQuantity,
      item.unitPrice ?? '-',
      item.revenue,
    ]);
  });

  if (report.wastes.length > 0) {
    ws.addRow([]);
    ws.addRow(['ZAYİAT', 'KATEGORİ', 'MİKTAR', 'BİRİM', 'TİP', 'SEBEP']);
    const wasteHeader = ws.lastRow!;
    wasteHeader.font = { bold: true };
    wasteHeader.fill = {
      type: 'pattern',
      pattern: 'solid',
      fgColor: { argb: 'FFFFF3CD' },
    };

    report.wastes.forEach((w) => {
      ws.addRow([
        w.productName,
        w.categoryName,
        w.quantity,
        w.unit,
        w.wasteTypeLabel,
        w.reason ?? '-',
      ]);
    });
  }

  ws.columns.forEach((col) => {
    col.width = 20;
  });

  const buf = await wb.xlsx.writeBuffer();
  saveAs(
    new Blob([buf]),
    `gunluk-ozet-${safeFilePart(report.branchName)}-${report.date}.xlsx`
  );
};

export const exportDailySummaryPdf = async (report: DailySummaryReport): Promise<void> => {
  const doc = new jsPDF();
  const font = await pdfFontName(doc);

  doc.setFont(font, 'normal');
  doc.setFontSize(16);
  doc.text(`${report.branchName} — Günlük Özet`, 14, 20);
  doc.setFontSize(11);
  doc.text(`Tarih: ${formatDate(report.date)}`, 14, 28);

  autoTable(doc, {
    startY: 35,
    head: [['', 'Tutar']],
    body: [
      ['Toplam Satış', formatCurrency(report.totalSalesRevenue)],
      ['  Ürün Satışları', formatCurrency(report.productSalesRevenue)],
      ['  Sayaç Satışları', formatCurrency(report.counterSalesRevenue)],
      ['Satın Alım Gideri', formatCurrency(report.totalPurchaseExpense)],
      ['Kasa Farkı', formatCurrency(report.cashDifference)],
    ],
    theme: 'striped',
    styles: { font },
    headStyles: { font },
  });

  autoTable(doc, {
    startY: (doc as any).lastAutoTable.finalY + 10,
    head: [['Kategori', 'Ürün', 'Tür', 'Miktar', 'Birim Fiyat', 'Gelir']],
    body: report.productSales.map((item) => [
      item.categoryName,
      item.productName,
      item.isCounter ? 'Sayaç' : 'Stok',
      String(item.soldQuantity),
      item.unitPrice != null ? String(item.unitPrice) : '-',
      formatCurrency(item.revenue),
    ]),
    theme: 'striped',
    styles: { font, fontSize: 8 },
    headStyles: { font },
  });

  doc.save(`gunluk-ozet-${safeFilePart(report.branchName)}-${report.date}.pdf`);
};

// ─── Dönem Raporu Export ───────────────────────────────────

export const exportPeriodSummaryExcel = async (report: PeriodSummaryReport): Promise<void> => {
  const wb = new ExcelJS.Workbook();

  const ws1 = wb.addWorksheet('Günlük Ciro');
  ws1.addRow(['TARİH', 'ÜRÜN SATIŞ', 'SAYAÇ SATIŞ', 'TOPLAM', 'SATIN ALIM', 'KASA FARKI']);
  ws1.getRow(1).font = { bold: true };

  report.dailyRows.forEach((row) => {
    ws1.addRow([
      formatDate(row.date),
      row.productSalesRevenue,
      row.counterSalesRevenue,
      row.totalSalesRevenue,
      row.purchaseExpense,
      row.cashDifference,
    ]);
  });

  ws1.addRow([
    'TOPLAM',
    report.dailyRows.reduce((s, r) => s + r.productSalesRevenue, 0),
    report.dailyRows.reduce((s, r) => s + r.counterSalesRevenue, 0),
    report.totalSalesRevenue,
    report.totalPurchaseExpense,
    report.totalCashDifference,
  ]);
  const totalRow = ws1.lastRow!;
  totalRow.font = { bold: true };

  ws1.columns.forEach((col) => {
    col.width = 18;
  });

  const ws2 = wb.addWorksheet('Ürün Özeti');
  ws2.addRow(['KATEGORİ', 'ÜRÜN', 'TÜR', 'TOPLAM MİKTAR', 'TOPLAM GELİR']);
  ws2.getRow(1).font = { bold: true };

  report.productSummaries.forEach((p) => {
    ws2.addRow([
      p.categoryName,
      p.productName,
      p.isCounter ? 'Sayaç' : 'Stok',
      p.totalSoldQuantity,
      p.totalRevenue,
    ]);
  });

  ws2.columns.forEach((col) => {
    col.width = 20;
  });

  const buf = await wb.xlsx.writeBuffer();
  saveAs(
    new Blob([buf]),
    `donem-raporu-${safeFilePart(report.branchName)}-${report.startDate}-${report.endDate}.xlsx`
  );
};

export const exportPeriodSummaryPdf = async (report: PeriodSummaryReport): Promise<void> => {
  const doc = new jsPDF({ orientation: 'landscape' });
  const font = await pdfFontName(doc);

  doc.setFont(font, 'normal');
  doc.setFontSize(16);
  doc.text(`${report.branchName} — Dönem Raporu`, 14, 20);
  doc.setFontSize(11);
  doc.text(
    `${formatDate(report.startDate)} — ${formatDate(report.endDate)} | ${report.closedDayCount} gün`,
    14,
    28
  );

  autoTable(doc, {
    startY: 35,
    head: [['Tarih', 'Ürün Satış', 'Sayaç Satış', 'Toplam', 'Satın Alım', 'Kasa Farkı']],
    body: report.dailyRows.map((row) => [
      formatDate(row.date),
      formatCurrency(row.productSalesRevenue),
      formatCurrency(row.counterSalesRevenue),
      formatCurrency(row.totalSalesRevenue),
      formatCurrency(row.purchaseExpense),
      formatCurrency(row.cashDifference),
    ]),
    theme: 'striped',
    styles: { font, fontSize: 8 },
    headStyles: { font },
  });

  autoTable(doc, {
    startY: (doc as any).lastAutoTable.finalY + 10,
    head: [['Kategori', 'Ürün', 'Tür', 'Toplam Miktar', 'Toplam Gelir']],
    body: report.productSummaries.map((p) => [
      p.categoryName,
      p.productName,
      p.isCounter ? 'Sayaç' : 'Stok',
      String(p.totalSoldQuantity),
      formatCurrency(p.totalRevenue),
    ]),
    theme: 'striped',
    styles: { font, fontSize: 8 },
    headStyles: { font },
  });

  doc.save(
    `donem-raporu-${safeFilePart(report.branchName)}-${report.startDate}-${report.endDate}.pdf`
  );
};

// ─── Yönetim Paneli Export ─────────────────────────────────

export const exportManagementExcel = async (
  report: ManagementReport,
  walletRows: WalletBalanceExportRow[]
): Promise<void> => {
  const wb = new ExcelJS.Workbook();

  const ws1 = wb.addWorksheet('Şube Karşılaştırma');
  ws1.addRow(['ŞUBE', 'TOPLAM CİRO', 'SATIN ALIM', 'KASA FARKI', 'NET', 'KAPALI GÜN']);
  ws1.getRow(1).font = { bold: true };

  report.branchComparisons.forEach((b) => {
    ws1.addRow([
      b.branchName,
      b.totalSalesRevenue,
      b.totalPurchaseExpense,
      b.totalCashDifference,
      b.netRevenue,
      b.closedDayCount,
    ]);
  });
  ws1.columns.forEach((col) => {
    col.width = 18;
  });

  const rows = walletRows.length > 0 ? walletRows : report.walletBalances.map((w) => ({ ...w }));

  const ws2 = wb.addWorksheet('Kasa Bakiyeleri');
  ws2.addRow(['ŞUBE', 'NAKİT', 'BANKA', 'TOPLAM']);
  ws2.getRow(1).font = { bold: true };

  rows.forEach((w) => {
    ws2.addRow([w.branchName, w.cashBalance, w.bankBalance, w.totalBalance]);
  });
  ws2.columns.forEach((col) => {
    col.width = 18;
  });

  const ws3 = wb.addWorksheet('Kasa Hareketleri');
  ws3.addRow(['TARİH', 'ŞUBE', 'İŞLEM', 'YÖNTEM', 'TUTAR', 'AÇIKLAMA', 'YAPAN']);
  ws3.getRow(1).font = { bold: true };

  report.walletMovements.forEach((m) => {
    ws3.addRow([
      formatDate(m.transactionDate),
      m.branchName,
      m.transactionTypeLabel,
      m.walletTypeLabel,
      m.amount,
      m.description ?? '-',
      m.createdByName,
    ]);
  });
  ws3.columns.forEach((col) => {
    col.width = 20;
  });

  const buf = await wb.xlsx.writeBuffer();
  saveAs(
    new Blob([buf]),
    `yonetim-paneli-${report.startDate}-${report.endDate}.xlsx`
  );
};

export const exportManagementPdf = async (
  report: ManagementReport,
  walletRows: WalletBalanceExportRow[]
): Promise<void> => {
  const doc = new jsPDF({ orientation: 'landscape' });
  const font = await pdfFontName(doc);

  doc.setFont(font, 'normal');
  doc.setFontSize(16);
  doc.text('Yönetim Paneli', 14, 20);
  doc.setFontSize(11);
  doc.text(`${formatDate(report.startDate)} — ${formatDate(report.endDate)}`, 14, 28);

  autoTable(doc, {
    startY: 35,
    head: [['Şube', 'Toplam Ciro', 'Satın Alım', 'Kasa Farkı', 'Net']],
    body: report.branchComparisons.map((b) => [
      b.branchName,
      formatCurrency(b.totalSalesRevenue),
      formatCurrency(b.totalPurchaseExpense),
      formatCurrency(b.totalCashDifference),
      formatCurrency(b.netRevenue),
    ]),
    theme: 'striped',
    styles: { font, fontSize: 8 },
    headStyles: { font },
  });

  const rows = walletRows.length > 0 ? walletRows : report.walletBalances.map((w) => ({ ...w }));

  autoTable(doc, {
    startY: (doc as any).lastAutoTable.finalY + 10,
    head: [['Şube', 'Nakit', 'Banka', 'Toplam']],
    body: rows.map((w) => [
      w.branchName,
      formatCurrency(w.cashBalance),
      formatCurrency(w.bankBalance),
      formatCurrency(w.totalBalance),
    ]),
    theme: 'striped',
    styles: { font, fontSize: 8 },
    headStyles: { font },
  });

  doc.save(`yonetim-paneli-${report.startDate}-${report.endDate}.pdf`);
};
