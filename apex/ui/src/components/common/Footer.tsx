import { AnimationClassNames, getTheme, IStackStyles, IStackTokens, Stack } from '@fluentui/react';
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
export function Footer() {
  return (
    <Stack enableScopedSelectors horizontal horizontalAlign="center" tokens={smallSpacingToken} styles={stackStyles} className={AnimationClassNames.slideRightIn400}>
      <span>Copyright © {new Date().getFullYear()} awsconcepts.com</span>
    </Stack>

  )
}