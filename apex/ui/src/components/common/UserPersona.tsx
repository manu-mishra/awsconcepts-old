import { AmplifyUser } from "@aws-amplify/ui"

import { useNavigate } from 'react-router-dom';
import { ContextualMenu, IContextualMenuItem, Persona, PersonaSize, Stack } from "@fluentui/react"
import { ActionButton, IconButton, PrimaryButton } from '@fluentui/react/lib/Button';
import { useAuthenticator, Button, Heading, View } from '@aws-amplify/ui-react';

import React, { useState, useEffect } from "react"
import { IIconProps } from '@fluentui/react';

export const UserPersona = () => {

  const { route, signOut, user } = useAuthenticator((context) => [
    context.route,
    context.signOut,
    context.user
  ]);
  const navigate = useNavigate();

  const linkRef = React.useRef(null);
  const [showContextualMenu, setShowContextualMenu] = React.useState(false);
  const onShowContextualMenu = React.useCallback((ev: React.MouseEvent<HTMLElement>) => {
    ev.preventDefault(); // don't navigate
    setShowContextualMenu(true);
  }, []);
  const onHideContextualMenu = React.useCallback(() => setShowContextualMenu(false), []);

  function invokeSignIn() {
    navigate('/login');
  }
  const signinIcon: IIconProps = { iconName: 'AddFriend' }
  const menuItems: IContextualMenuItem[] = [
    {
      key: 'signOut',
      text: 'Sign Out',
      onClick: () => signOut(),
    }
  ];
  return (
    <Stack horizontal tokens={{ childrenGap: 10 }}>
      <View>
        {route === 'authenticated' ?
          <div>
            <Persona ref={linkRef} text={user?.attributes?.nickname}
              onClick={onShowContextualMenu} size={PersonaSize.size24} />
            <ContextualMenu
              items={menuItems}
              hidden={!showContextualMenu}
              target={linkRef}
              onItemClick={onHideContextualMenu}
              onDismiss={onHideContextualMenu}
            />
          </div>
          : <ActionButton allowDisabledFocus
            onClick={invokeSignIn}>Sign-In</ActionButton>}
      </View>
    </Stack>
  )
}

