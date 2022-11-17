import React from 'react';
import { Stack, IStackTokens, IStackStyles} from '@fluentui/react';
import './App.css';
import { Amplify, API } from 'aws-amplify';

import { Authenticator } from '@aws-amplify/ui-react';
import '@aws-amplify/ui-react/styles.css';
import { applicationConfig } from './configuration/AmplifyConfig'
import {Header} from './components/home/Header'

Amplify.configure(applicationConfig);
const stackTokens: IStackTokens = { childrenGap: 15 };
const stackStyles: Partial<IStackStyles> = {
  root: {
    width: '960px',
    margin: '0 auto',
    textAlign: 'center',
    color: '#605e5c',
  },
};

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
    <Stack horizontalAlign="center" verticalAlign="center" verticalFill styles={stackStyles} tokens={stackTokens}>
      <Header></Header>
      <Authenticator loginMechanisms={['email']} signUpAttributes={['nickname']}>

        {({ signOut, user }) => (
          <main>
            <h1>Hello {user?.attributes?.nickname} {greeting}</h1>
            <button onClick={signOut}>Sign out</button>
            <button onClick={InvokeApi}>Invoke API </button>
          </main>
        )}
      </Authenticator>
    </Stack>
  );
};

