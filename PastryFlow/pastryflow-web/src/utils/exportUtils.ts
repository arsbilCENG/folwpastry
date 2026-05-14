import ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import type {
  DailySummaryReport,
  PeriodSummaryReport,
  ManagementReport,
  ProductionReport,
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

export const exportProductionReportPdf = async (
  report: ProductionReport,
  categoryFilter?: string
): Promise<void> => {
  const doc = new jsPDF({ orientation: 'landscape' });
  const font = await pdfFontName(doc);

  const filteredRows = categoryFilter
    ? report.rows.filter(r => r.categoryName === categoryFilter)
    : report.rows;

  const categoryLabel = categoryFilter ?? 'Tüm Kategoriler';
  // Turkish-safe uppercase
  const title = `ÜRETİM RAPORU — ${categoryLabel.toLocaleUpperCase('tr-TR')}`;

  // Başlık - Kalın font yüklü olmadığı için normal kullanıyoruz (encoding bozulmaması için)
  doc.setFont(font, 'normal');
  doc.setFontSize(18);
  doc.text(title, 14, 18);
  
  doc.setFontSize(11);
  doc.text(report.productionBranchName, 14, 26);
  doc.text(
    `Rapor Tarihi: ${formatDate(report.reportDate)} | Talep Tarihi: ${formatDate(report.demandDate)}`,
    14, 33
  );

  // Tablo başlıkları
  const head = [
    ['Kategori', 'Ürün', 'Birim',
     ...report.salesBranches.map(b => b.branchName),
     'TOPLAM']
  ];

  // Tablo satırları
  const body = filteredRows.map(row => [
    row.categoryName,
    row.productName,
    row.unit,
    ...report.salesBranches.map(b =>
      (row.branchQuantities[b.branchId] ?? 0).toString()
    ),
    row.totalQuantity.toString()
  ]);

  const totalQty = filteredRows.reduce((sum, r) => sum + r.totalQuantity, 0);

  // Toplam satırı
  const totalRow = [
    'TOPLAM', '', '',
    ...report.salesBranches.map(b =>
      filteredRows
        .reduce((sum, r) => sum + (r.branchQuantities[b.branchId] ?? 0), 0)
        .toString()
    ),
    totalQty.toString()
  ];

  autoTable(doc, {
    startY: 40,
    head,
    body: [...body, totalRow],
    theme: 'striped',
    styles: { 
      font, 
      halign: 'center' // İçerikleri ortalıyoruz
    },
    headStyles: {
      fillColor: [41, 128, 185],
      textColor: 255,
      fontStyle: 'normal', // Bold font yüklenmediği için normal (Turkish fix)
      halign: 'center',
      font
    },
    columnStyles: {
      0: { cellWidth: 30 },
      1: { cellWidth: 40 },
      2: { cellWidth: 15 },
    },
    didParseCell: (data) => {
      // Toplam satırı görsel ayrımı
      if (data.row.index === body.length) {
        data.cell.styles.fillColor = [236, 240, 241];
      }
    },
    foot: [[
      `Toplam ${filteredRows.length} ürün çeşidi`,
      '', '',
      ...report.salesBranches.map(() => ''),
      `${totalQty} adet`
    ]],
    footStyles: { 
      font, 
      fontStyle: 'normal',
      halign: 'center' 
    }
  });

  const fileName = categoryFilter
    ? `uretim-raporu-${safeFilePart(report.productionBranchName)}-${safeFilePart(categoryFilter)}-${report.reportDate}.pdf`
    : `uretim-raporu-${safeFilePart(report.productionBranchName)}-${report.reportDate}.pdf`;

  doc.save(fileName);
};

export const exportProductionReportExcel = async (
  report: ProductionReport,
  categoryFilter?: string
): Promise<void> => {
  const wb = new ExcelJS.Workbook();
  const sheetName = categoryFilter ? categoryFilter.substring(0, 31) : 'Üretim Raporu';
  const ws = wb.addWorksheet(sheetName);

  const filteredRows = categoryFilter
    ? report.rows.filter(r => r.categoryName === categoryFilter)
    : report.rows;

  const colCount = 3 + report.salesBranches.length + 1;

  // Başlık
  ws.mergeCells(1, 1, 1, colCount);
  ws.getCell('A1').value = categoryFilter 
    ? `ÜRETİM RAPORU — ${categoryFilter.toLocaleUpperCase('tr-TR')}` 
    : 'ÜRETİM RAPORU';
  ws.getCell('A1').font = { bold: true, size: 16 };
  ws.getCell('A1').alignment = { horizontal: 'center' };

  ws.mergeCells(2, 1, 2, colCount);
  ws.getCell('A2').value = report.productionBranchName;
  ws.getCell('A2').font = { bold: true, size: 13 };
  ws.getCell('A2').alignment = { horizontal: 'center' };

  ws.mergeCells(3, 1, 3, colCount);
  ws.getCell('A3').value =
    `Rapor: ${formatDate(report.reportDate)} | Talep: ${formatDate(report.demandDate)}`;
  ws.getCell('A3').alignment = { horizontal: 'center' };

  ws.addRow([]);

  // Header satırı
  const headerValues = ['Kategori', 'Ürün', 'Birim', ...report.salesBranches.map(b => b.branchName), 'TOPLAM'];
  const headerRow = ws.addRow(headerValues);
  headerRow.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'FF2980B9' } };
  headerRow.eachCell(cell => {
    cell.font = { bold: true, color: { argb: 'FFFFFFFF' } };
    cell.alignment = { horizontal: 'center' };
  });

  // Veri satırları
  filteredRows.forEach(row => {
    const r = ws.addRow([
      row.categoryName,
      row.productName,
      row.unit,
      ...report.salesBranches.map(b => row.branchQuantities[b.branchId] ?? 0),
      row.totalQuantity
    ]);
    r.eachCell(cell => {
      cell.alignment = { horizontal: 'center' }; // İçerikleri ortalıyoruz
    });
    r.getCell(colCount).font = { bold: true };
  });

  // Toplam satırı
  const totalQty = filteredRows.reduce((sum, r) => sum + r.totalQuantity, 0);
  const totalRow = ws.addRow([
    'TOPLAM', '', '',
    ...report.salesBranches.map(b => filteredRows.reduce((sum, r) => sum + (r.branchQuantities[b.branchId] ?? 0), 0)),
    totalQty
  ]);
  totalRow.font = { bold: true };
  totalRow.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'FFECF0F1' } };
  totalRow.eachCell(cell => {
    cell.alignment = { horizontal: 'center' };
  });

  // Kolon genişlikleri
  ws.getColumn(1).width = 20;
  ws.getColumn(2).width = 35;
  ws.getColumn(3).width = 10;
  report.salesBranches.forEach((_, i) => ws.getColumn(4 + i).width = 18);
  ws.getColumn(colCount).width = 12;

  const buf = await wb.xlsx.writeBuffer();
  const fileName = categoryFilter
    ? `uretim-raporu-${safeFilePart(report.productionBranchName)}-${safeFilePart(categoryFilter)}-${report.reportDate}.xlsx`
    : `uretim-raporu-${safeFilePart(report.productionBranchName)}-${report.reportDate}.xlsx`;

  saveAs(new Blob([buf]), fileName);
};

export const exportAllCategoriesPdf = (report: ProductionReport): void => {
  const categories = [...new Set(report.rows.map(r => r.categoryName))].sort();
  categories.forEach((category, index) => {
    setTimeout(() => exportProductionReportPdf(report, category), index * 300);
  });
};

export const exportAllCategoriesExcel = async (report: ProductionReport): Promise<void> => {
  const wb = new ExcelJS.Workbook();
  const categories = [...new Set(report.rows.map(r => r.categoryName))].sort();

  const wsAll = wb.addWorksheet('Tümü');
  await addProductionDataToSheet(wsAll, report);

  for (const cat of categories) {
    const ws = wb.addWorksheet(cat.substring(0, 31));
    await addProductionDataToSheet(ws, report, cat);
  }

  const buf = await wb.xlsx.writeBuffer();
  const fileName = `uretim-raporu-${safeFilePart(report.productionBranchName)}-TUM-KATEGORILER-${report.reportDate}.xlsx`;
  saveAs(new Blob([buf]), fileName);
};

const addProductionDataToSheet = async (
  ws: ExcelJS.Worksheet, 
  report: ProductionReport, 
  categoryFilter?: string
) => {
  const filteredRows = categoryFilter
    ? report.rows.filter(r => r.categoryName === categoryFilter)
    : report.rows;

  const colCount = 3 + report.salesBranches.length + 1;

  ws.mergeCells(1, 1, 1, colCount);
  ws.getCell('A1').value = categoryFilter 
    ? `ÜRETİM RAPORU — ${categoryFilter.toLocaleUpperCase('tr-TR')}` 
    : 'ÜRETİM RAPORU';
  ws.getCell('A1').font = { bold: true, size: 16 };
  ws.getCell('A1').alignment = { horizontal: 'center' };

  ws.mergeCells(2, 1, 2, colCount);
  ws.getCell('A2').value = report.productionBranchName;
  ws.getCell('A2').font = { bold: true, size: 13 };
  ws.getCell('A2').alignment = { horizontal: 'center' };

  ws.addRow([]);

  const headerValues = ['Kategori', 'Ürün', 'Birim', ...report.salesBranches.map(b => b.branchName), 'TOPLAM'];
  const headerRow = ws.addRow(headerValues);
  headerRow.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'FF2980B9' } };
  headerRow.eachCell(cell => {
    cell.font = { bold: true, color: { argb: 'FFFFFFFF' } };
    cell.alignment = { horizontal: 'center' };
  });

  filteredRows.forEach(row => {
    const r = ws.addRow([
      row.categoryName,
      row.productName,
      row.unit,
      ...report.salesBranches.map(b => row.branchQuantities[b.branchId] ?? 0),
      row.totalQuantity
    ]);
    r.eachCell(cell => cell.alignment = { horizontal: 'center' });
    r.getCell(colCount).font = { bold: true };
  });

  const totalRow = ws.addRow([
    'TOPLAM', '', '',
    ...report.salesBranches.map(b => filteredRows.reduce((sum, r) => sum + (r.branchQuantities[b.branchId] ?? 0), 0)),
    filteredRows.reduce((sum, r) => sum + r.totalQuantity, 0)
  ]);
  totalRow.font = { bold: true };
  totalRow.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'FFECF0F1' } };
  totalRow.eachCell(cell => cell.alignment = { horizontal: 'center' });

  ws.getColumn(1).width = 20;
  ws.getColumn(2).width = 35;
  ws.getColumn(3).width = 10;
  report.salesBranches.forEach((_, i) => ws.getColumn(4 + i).width = 18);
  ws.getColumn(colCount).width = 12;
};


