// components/Layout.js
import React from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import { useAuthenticator, Button, Heading, View } from '@aws-amplify/ui-react';
import { IStackTokens, IStackStyles, Stack } from '@fluentui/react';
import { Header } from './Header';
import { Footer } from './Footer';

const stackTokens: IStackTokens = { childrenGap: 15 };
const stackStyles: Partial<IStackStyles> = {
  root: {
  },
};

export function Layout() {
  const { route, signOut } = useAuthenticator((context) => [
    context.route,
    context.signOut,
  ]);
  const navigate = useNavigate();

  function logOut() {
    signOut();
    navigate('/login');
  }
  return (
    <>
      <Stack horizontalAlign="stretch" 
      verticalAlign="start" styles={stackStyles} tokens={stackTokens}>
        <Header></Header>
        <Outlet />
        <Stack.Item align="stretch">
        <Footer></Footer>
        </Stack.Item>
      </Stack>


    </>
  );
}