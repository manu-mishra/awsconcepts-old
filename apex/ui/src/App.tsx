import React from 'react';
import './App.css';
import { Authenticator } from '@aws-amplify/ui-react';
import '@aws-amplify/ui-react/styles.css';
import { ApplicationRoutes } from './components/common/ApplicationRoutes'
export const App: React.FunctionComponent = () => {
  return (
    <Authenticator.Provider>
      <ApplicationRoutes />
    </Authenticator.Provider>

  );
};


