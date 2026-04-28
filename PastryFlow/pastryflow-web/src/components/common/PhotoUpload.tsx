import React, { useRef, useState } from 'react';
import { Button, Space, Image, message, Typography } from 'antd';
import { CameraOutlined, FolderOpenOutlined, DeleteOutlined, PictureOutlined } from '@ant-design/icons';

const { Text } = Typography;

const ALLOWED_TYPES = ['image/jpeg', 'image/jpg', 'image/png', 'image/webp', 'image/heic'];

interface PhotoUploadProps {
  value?: File | string | null;        // File = yeni seçilmiş, string = mevcut URL
  onChange?: (file: File | null) => void;
  maxSizeMB?: number;                  // default: 10
  disabled?: boolean;
  required?: boolean;
  placeholder?: string;                // default: "Fotoğraf yükleyin"
  showPreview?: boolean;               // default: true
  style?: React.CSSProperties;
}

const PhotoUpload: React.FC<PhotoUploadProps> = ({
  value,
  onChange,
  maxSizeMB = 10,
  disabled = false,
  required = false,
  placeholder = 'Fotoğraf yükleyin',
  showPreview = true,
  style,
}) => {
  const cameraInputRef = useRef<HTMLInputElement>(null);
  const galleryInputRef = useRef<HTMLInputElement>(null);
  const [previewUrl, setPreviewUrl] = useState<string | null>(null);

  // value değiştiğinde preview güncelle
  React.useEffect(() => {
    if (value instanceof File) {
      const url = URL.createObjectURL(value);
      setPreviewUrl(url);
      return () => URL.revokeObjectURL(url);
    } else if (typeof value === 'string' && value) {
      // Backend static path mapping: /uploads... -> prepend baseURL if needed
      // but usually /uploads is handled by proxy or full URL
      setPreviewUrl(value);
    } else {
      setPreviewUrl(null);
    }
  }, [value]);

  const handleFileSelect = (file: File) => {
    // Format kontrolü
    if (!ALLOWED_TYPES.includes(file.type)) {
      message.error('Geçersiz dosya formatı. JPG, PNG, WebP veya HEIC yükleyin.');
      return;
    }
    // Boyut kontrolü
    if (file.size > maxSizeMB * 1024 * 1024) {
      message.error(`Dosya boyutu ${maxSizeMB} MB'dan büyük olamaz.`);
      return;
    }
    onChange?.(file);
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) handleFileSelect(file);
    // Input'u resetle ki aynı dosya tekrar seçilebilsin
    e.target.value = '';
  };

  const handleRemove = () => {
    onChange?.(null);
  };

  return (
    <div style={{ ...style }}>
      {/* Gizli input'lar */}
      <input
        ref={cameraInputRef}
        type="file"
        accept="image/*"
        capture="environment"
        style={{ display: 'none' }}
        onChange={handleInputChange}
        disabled={disabled}
      />
      <input
        ref={galleryInputRef}
        type="file"
        accept="image/*"
        style={{ display: 'none' }}
        onChange={handleInputChange}
        disabled={disabled}
      />

      {/* Önizleme */}
      {showPreview && previewUrl ? (
        <div style={{ position: 'relative', marginBottom: 8, textAlign: 'center' }}>
          <Image
            src={previewUrl}
            alt="Önizleme"
            style={{ maxWidth: '100%', maxHeight: 200, objectFit: 'contain', borderRadius: 8 }}
            preview={{ mask: 'Büyüt' }}
          />
          {!disabled && (
            <Button
              type="text"
              danger
              icon={<DeleteOutlined />}
              size="small"
              onClick={handleRemove}
              style={{ position: 'absolute', top: 4, right: 4, background: 'rgba(255,255,255,0.8)' }}
            />
          )}
        </div>
      ) : showPreview ? (
        <div
          style={{
            border: '1px dashed #d9d9d9',
            borderRadius: 8,
            padding: 24,
            textAlign: 'center',
            marginBottom: 8,
            background: '#fafafa',
          }}
        >
          <PictureOutlined style={{ fontSize: 32, color: '#bfbfbf' }} />
          <div>
            <Text type="secondary">{placeholder}</Text>
            {required && <Text type="danger"> *</Text>}
          </div>
        </div>
      ) : null}

      {/* Butonlar */}
      {!disabled && (
        <Space wrap style={{ width: '100%', justifyContent: 'center' }}>
          <Button
            icon={<CameraOutlined />}
            onClick={() => cameraInputRef.current?.click()}
          >
            Kamera ile Çek
          </Button>
          <Button
            icon={<FolderOpenOutlined />}
            onClick={() => galleryInputRef.current?.click()}
          >
            Galeriden Seç
          </Button>
        </Space>
      )}
    </div>
  );
};

export default PhotoUpload;
