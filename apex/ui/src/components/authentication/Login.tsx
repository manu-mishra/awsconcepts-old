import { useEffect } from "react";

import { Authenticator, useAuthenticator, View } from '@aws-amplify/ui-react';
import '@aws-amplify/ui-react/styles.css';

import { useNavigate, useLocation } from 'react-router';
import { AnimationClassNames, getTheme, ILabelStyles, IStackStyles, Label, Stack } from "@fluentui/react";

export default function Login() {
  const { route } = useAuthenticator((context) => [context.route]);
  const location = useLocation();
  const navigate = useNavigate();
  let from = location.state?.from?.pathname || '/';
  useEffect(() => {
    if (route === 'authenticated') {
      navigate(from, { replace: true });
    }
  }, [route, navigate, from]);
  let theme = getTheme();
    const stackStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation4,
            background: theme.palette.white,
            marginBottom: '10px',
            fontSize:'20px'
        },
    };
    const labelStyles: ILabelStyles = {
      root: {
          fontSize:'20px'
      },
  };
  return (
    <View className="auth-wrapper">
      <Stack horizontalAlign="center" styles={stackStyles} className={AnimationClassNames.slideRightIn400}>
        <Label styles={labelStyles} required>I wont send you emails, but i do expect authenticity, so please create a account!</Label>
      </Stack>
      <Authenticator loginMechanisms={['email']} signUpAttributes={['nickname']}></Authenticator>
    </View>
  );
}