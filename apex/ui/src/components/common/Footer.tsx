import { AnimationClassNames, getTheme, IStackStyles, IStackTokens, Stack } from '@fluentui/react';
import CSS from 'csstype';
let theme = getTheme();
const stackStyles: IStackStyles = {
  root: {
    boxShadow: theme.effects.elevation8,
    marginLeft: "10px",
    marginRight: "10px",
  },
};
const smallSpacingToken: IStackTokens = {
  childrenGap: 's1',
  padding: 's1',
};
const copyright: CSS.Properties = {
 color: theme.palette.themePrimary
}
export function Footer() {
  return (
    <Stack enableScopedSelectors horizontal horizontalAlign="center" tokens={smallSpacingToken} styles={stackStyles} className={AnimationClassNames.slideRightIn400}>
      <span style={copyright}>Copyright © {new Date().getFullYear()} awsconcepts.com </span> 
      <span>Concepts listed here are from the author and has no relation to do with author's past and current employers.</span> 
    </Stack>

  )
}