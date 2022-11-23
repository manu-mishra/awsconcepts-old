import React from 'react';
import './App.css';
import { API } from 'aws-amplify';

import { Authenticator } from '@aws-amplify/ui-react';
import '@aws-amplify/ui-react/styles.css';

import { ApplicationRoutes } from './components/common/ApplicationRoutes'





export const App: React.FunctionComponent = () => {
  let greeting: string = 'waiting';
  let InvokeApi = async () => {
    console.log(API);
    let resp = await API.get('api', '/values', {
      responseType: 'json'
    });
    greeting = resp[0];
    console.log(resp[0]);
  }
  return (
    <Authenticator.Provider>
      <ApplicationRoutes />
    </Authenticator.Provider>

  );
};


