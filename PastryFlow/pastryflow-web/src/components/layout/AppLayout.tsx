import React from 'react';
import { Outlet } from 'react-router-dom';

const AppLayout: React.FC = () => {
  return (
    <React.Fragment>
      <Outlet />
    </React.Fragment>
  );
};

export default AppLayout;
