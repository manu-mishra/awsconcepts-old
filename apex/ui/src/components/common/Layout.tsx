import { Outlet } from 'react-router-dom';
import { IStackTokens, IStackStyles, Stack, StackItem } from '@fluentui/react';
import { Header } from './Header';
import { Footer } from './Footer';

const stackTokens: IStackTokens = { childrenGap: 0 };
const stackStyles: Partial<IStackStyles> = {
  root: {
    backgroundColor:'#faf9f8'
  },
};
export function Layout() {
  return (
    <>
      <Stack verticalFill horizontalAlign="stretch" verticalAlign="stretch" styles={stackStyles} tokens={stackTokens}>
        <Header></Header>
        <StackItem >
         <Outlet />
         </StackItem>
        <Stack.Item align='auto'>
          <Footer></Footer>
        </Stack.Item>
      </Stack>
    </>
  );
}