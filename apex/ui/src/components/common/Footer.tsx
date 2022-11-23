import { AnimationClassNames } from '@fluentui/react';

export function Footer() {
  return (
    <p className={AnimationClassNames.fadeIn500}>Copyright © {new Date().getFullYear()} awsconcepts.com</p>
  )
}