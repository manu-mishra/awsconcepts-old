// components/Layout.js
import { Outlet } from 'react-router-dom';
import { IStackTokens, IStackStyles, Stack } from '@fluentui/react';
import { Header } from './Header';
import { Footer } from './Footer';

const stackTokens: IStackTokens = { childrenGap: 0 };
const stackStyles: Partial<IStackStyles> = {
  root: {
  },
};
export function Layout() {
  return (
    <>
      <Stack horizontalAlign="stretch" verticalAlign="start" styles={stackStyles} tokens={stackTokens}>
        <Header></Header>
        <div style={{height:"85vh"}}>
         <Outlet />
        </div>
        <Stack.Item align="stretch">
          <Footer></Footer>
        </Stack.Item>
      </Stack>


    </>
  );
}