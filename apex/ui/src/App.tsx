import React from 'react';
import './App.css';
import { Authenticator } from '@aws-amplify/ui-react';
import { initializeIcons } from '@fluentui/react/lib/Icons';
import '@aws-amplify/ui-react/styles.css';
import { ApplicationRoutes } from './components/common/ApplicationRoutes'
initializeIcons(/* optional base url */);
export const App: React.FunctionComponent = () => {
  return (
    <Authenticator.Provider>
      <ApplicationRoutes />
    </Authenticator.Provider>
  );
};


