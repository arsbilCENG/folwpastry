import React from 'react';
import { Result } from 'antd';
import { EditOutlined } from '@ant-design/icons';

const DayCorrectionPlaceholder: React.FC = () => (
  <Result
    icon={<EditOutlined style={{ color: '#faad14' }} />}
    title="Gün Düzeltme"
    subTitle="Şubelerin geçmişe dönük gün sonu verilerini düzeltme ekranı bir sonraki aşamada aktif olacaktır."
  />
);

export default DayCorrectionPlaceholder;
