// components/Layout.js
import { Outlet } from 'react-router-dom';
import { IStackTokens, IStackStyles, Stack } from '@fluentui/react';
import { Header } from './Header';
import { Footer } from './Footer';

const stackTokens: IStackTokens = { childrenGap: 15 };
const stackStyles: Partial<IStackStyles> = {
  root: {
  },
};
export function Layout() {
  return (
    <>
      <Stack horizontalAlign="stretch" verticalAlign="start" styles={stackStyles} tokens={stackTokens}>
        <Header></Header>
        <Outlet />
        <Stack.Item align="stretch">
          <Footer></Footer>
        </Stack.Item>
      </Stack>


    </>
  );
}