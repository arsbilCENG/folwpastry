import React from 'react';
import { Select, Space, Spin } from 'antd';
import { useAdminBranches } from '../../hooks/useAdmin';

interface BranchSelectorProps {
  value: string | null;
  onChange: (branchId: string | null) => void;
  showAllOption?: boolean;
  branchTypeFilter?: 'Production' | 'Sales';
  style?: React.CSSProperties;
}

const BranchSelector: React.FC<BranchSelectorProps> = ({
  value,
  onChange,
  showAllOption = true,
  branchTypeFilter,
  style
}) => {
  const { data: branches, isLoading } = useAdminBranches();

  if (isLoading) return <Spin size="small" />;

  const filteredBranches = branchTypeFilter 
    ? branches?.filter(b => b.branchType === branchTypeFilter)
    : branches;

  const options = [];

  if (showAllOption) {
    options.push({
      label: '🏪 Tüm Şubeler',
      value: null
    });
  }

  filteredBranches?.forEach(branch => {
    const cityEmoji = branch.city.toLocaleLowerCase('tr-TR').includes('edirne') ? '🕌' : '🌻';
    options.push({
      label: `${cityEmoji} ${branch.name} (${branch.branchType === 'Production' ? 'Üretim' : 'Satış'})`,
      value: branch.id
    });
  });

  return (
    <Select
      placeholder="Şube Seçin"
      value={value}
      onChange={onChange}
      options={options}
      style={{ minWidth: 200, ...style }}
      allowClear={showAllOption}
    />
  );
};

export default BranchSelector;
