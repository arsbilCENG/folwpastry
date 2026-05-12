import React from 'react';
import ReportsDashboard from '../../components/reports/ReportsDashboard';
import useAuth from '../../hooks/useAuth';

const SalesReports: React.FC = () => {
  const { user } = useAuth();

  return (
    <ReportsDashboard
      pageTitle={`📋 Raporlar — ${user?.branchName || 'Şube'}`}
      branchId={user?.branchId || undefined}
    />
  );
};

export default SalesReports;
