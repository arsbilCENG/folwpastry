import ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

// ============ EXCEL EXPORT ============

interface ExcelColumn {
  header: string;
  key: string;
  width?: number;
  style?: Partial<ExcelJS.Style>;
}

interface ExcelExportOptions {
  fileName: string;
  sheetName: string;
  columns: ExcelColumn[];
  data: Record<string, any>[];
  title?: string;
  subtitle?: string;
}

export const exportToExcel = async (options: ExcelExportOptions): Promise<void> => {
  const { fileName, sheetName, columns, data, title, subtitle } = options;
  
  const workbook = new ExcelJS.Workbook();
  workbook.creator = 'PastryFlow';
  workbook.created = new Date();
  
  const worksheet = workbook.addWorksheet(sheetName);
  
  let startRow = 1;
  
  // Başlık satırı
  if (title) {
    const titleRow = worksheet.addRow([title]);
    titleRow.font = { size: 16, bold: true };
    titleRow.alignment = { horizontal: 'center' };
    worksheet.mergeCells(startRow, 1, startRow, columns.length);
    startRow++;
  }
  
  // Alt başlık
  if (subtitle) {
    const subtitleRow = worksheet.addRow([subtitle]);
    subtitleRow.font = { size: 11, italic: true, color: { argb: '666666' } };
    subtitleRow.alignment = { horizontal: 'center' };
    worksheet.mergeCells(startRow, 1, startRow, columns.length);
    startRow++;
  }
  
  // Boş satır
  if (title || subtitle) {
    worksheet.addRow([]);
    startRow++;
  }
  
  // Kolon tanımları
  worksheet.columns = columns.map(col => ({
    key: col.key,
    width: col.width || 15,
  }));
  
  // Header satırını manuel ekle
  const headerValues = columns.map(col => col.header);
  const headerRow = worksheet.addRow(headerValues);
  headerRow.font = { bold: true, color: { argb: 'FFFFFF' } };
  headerRow.fill = {
    type: 'pattern',
    pattern: 'solid',
    fgColor: { argb: '1890FF' }, // Ant Design primary blue
  };
  headerRow.alignment = { horizontal: 'center', vertical: 'middle' };
  headerRow.height = 28;
  
  // Veri satırları
  data.forEach((row, index) => {
    const values = columns.map(col => row[col.key] ?? '');
    const dataRow = worksheet.addRow(values);
    
    // Zebra striping
    if (index % 2 === 1) {
      dataRow.fill = {
        type: 'pattern',
        pattern: 'solid',
        fgColor: { argb: 'F5F5F5' },
      };
    }
    
    dataRow.alignment = { vertical: 'middle' };
  });
  
  // Border ekle
  worksheet.eachRow((row, rowNumber) => {
    if (rowNumber >= startRow) {
      row.eachCell((cell) => {
        cell.border = {
          top: { style: 'thin' },
          left: { style: 'thin' },
          bottom: { style: 'thin' },
          right: { style: 'thin' },
        };
      });
    }
  });
  
  // Auto-filter
  if (data.length > 0) {
    const lastRow = worksheet.rowCount;
    const headerRowNumber = startRow + (title || subtitle ? 2 : 0); // Corrected header row calculation
    worksheet.autoFilter = {
      from: { row: headerRowNumber, column: 1 },
      to: { row: lastRow, column: columns.length },
    };
  }
  
  // Dosyayı kaydet
  const buffer = await workbook.xlsx.writeBuffer();
  const blob = new Blob([buffer], { 
    type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' 
  });
  saveAs(blob, `${fileName}.xlsx`);
};

// ============ PDF EXPORT ============

interface PdfColumn {
  header: string;
  dataKey: string;
  halign?: 'left' | 'center' | 'right';
}

interface PdfExportOptions {
  fileName: string;
  title: string;
  subtitle?: string;
  columns: PdfColumn[];
  data: Record<string, any>[];
  orientation?: 'portrait' | 'landscape';
  footerText?: string;
}

// Türkçe font yükleme
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

export const exportToPdf = async (options: PdfExportOptions): Promise<void> => {
  const { fileName, title, subtitle, columns, data, orientation = 'landscape', footerText } = options;
  
  const doc = new jsPDF({ orientation, unit: 'mm', format: 'a4' });
  
  // Türkçe font yükle
  const hasTurkishFont = await loadTurkishFont(doc);
  const fontName = hasTurkishFont ? 'Roboto' : 'helvetica';
  
  const pageWidth = doc.internal.pageSize.getWidth();
  
  // Başlık
  doc.setFont(fontName, 'normal');
  doc.setFontSize(18);
  doc.setTextColor(24, 144, 255); // Ant Design primary blue
  doc.text(title, pageWidth / 2, 15, { align: 'center' });
  
  // Alt başlık
  let yPosition = 22;
  if (subtitle) {
    doc.setFontSize(10);
    doc.setTextColor(102, 102, 102);
    doc.text(subtitle, pageWidth / 2, yPosition, { align: 'center' });
    yPosition += 8;
  }
  
  // Oluşturma tarihi
  doc.setFontSize(8);
  doc.setTextColor(153, 153, 153);
  const now = new Date().toLocaleString('tr-TR');
  doc.text(`Oluşturma Tarihi: ${now}`, pageWidth / 2, yPosition, { align: 'center' });
  yPosition += 5;
  
  // Tablo
  const tableColumns = columns.map(col => ({
    header: col.header,
    dataKey: col.dataKey,
  }));
  
  const tableData = data.map(row => {
    const newRow: Record<string, any> = {};
    columns.forEach(col => {
      newRow[col.dataKey] = row[col.dataKey] ?? '-';
    });
    return newRow;
  });
  
  autoTable(doc, {
    startY: yPosition,
    columns: tableColumns,
    body: tableData,
    styles: {
      font: fontName,
      fontSize: 8,
      cellPadding: 3,
      lineColor: [220, 220, 220],
      lineWidth: 0.1,
    },
    headStyles: {
      fillColor: [24, 144, 255],
      textColor: [255, 255, 255],
      fontStyle: 'bold',
      halign: 'center',
    },
    alternateRowStyles: {
      fillColor: [245, 245, 245],
    },
    columnStyles: columns.reduce((acc, col, index) => {
      if (col.halign) {
        acc[index] = { halign: col.halign as any };
      }
      return acc;
    }, {} as Record<number, any>),
    margin: { top: 10, right: 10, bottom: 20, left: 10 },
    didDrawPage: (data: any) => {
      // Footer
      const pageCount = doc.getNumberOfPages();
      const currentPage = data.pageNumber;
      doc.setFontSize(8);
      doc.setTextColor(153, 153, 153);
      doc.setFont(fontName, 'normal');
      
      const footerY = doc.internal.pageSize.getHeight() - 10;
      doc.text(
        `Sayfa ${currentPage} / ${pageCount}`,
        pageWidth / 2,
        footerY,
        { align: 'center' }
      );
      
      if (footerText) {
        doc.text(footerText, 10, footerY);
      }
      
      doc.text('PastryFlow', pageWidth - 10, footerY, { align: 'right' });
    },
  });
  
  doc.save(`${fileName}.pdf`);
};

// ============ HELPER: Tarih formatla ============
export const formatDateForExport = (date: string): string => {
  return new Date(date).toLocaleDateString('tr-TR');
};

export const formatDateRangeForExport = (startDate: string, endDate: string): string => {
  return `${formatDateForExport(startDate)} - ${formatDateForExport(endDate)}`;
};
