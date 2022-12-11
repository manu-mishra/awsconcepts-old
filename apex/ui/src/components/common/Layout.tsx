import { Outlet } from 'react-router-dom';
import { IStackTokens, IStackStyles, Stack, StackItem } from '@fluentui/react';
import { Header } from './Header';
import { Footer } from './Footer';
import CSS from 'csstype';

const stackTokens: IStackTokens = { childrenGap: 0 };
const stackStyles: Partial<IStackStyles> = {
  root: {
    backgroundColor: '#faf9f8'
  },
};
const wrapperStyle : CSS.Properties = {
  minHeight: '100%',
  marginBottom: '-50px'
}
const pushStyle : CSS.Properties = {
  height: '50px'
}
const footerStyle : CSS.Properties = {
  height: '50px'
}
export function Layout() {
  return (
    <>
    <div style={wrapperStyle}>

    <Stack verticalFill horizontalAlign="stretch" verticalAlign="stretch" styles={stackStyles} tokens={stackTokens}>
        <Header></Header>
        <StackItem >
            <Outlet />
        </StackItem>
      </Stack>

<div style={pushStyle}></div>
</div>
<footer style={footerStyle}>
<Footer></Footer>
</footer>
</>
  );
}