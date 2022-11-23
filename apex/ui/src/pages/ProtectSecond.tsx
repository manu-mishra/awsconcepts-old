import { useAuthenticator, Heading } from '@aws-amplify/ui-react';
import { API } from 'aws-amplify';
import React from 'react'

export function ProtectedSecond() {
  const { route } = useAuthenticator((context) => [context.route]);
  const [greeting, setGreeting] = React.useState('');
  React.useEffect(() => {
    const callApi= async () => {
      let resp = await API.get('api', '/values', {
        responseType: 'json'
      });
      
      setGreeting( resp[0] +' sdsd ' +resp[1]);
    }
    callApi().catch(console.error);
  }, []);
  const message =
    route === 'authenticated' ?  greeting : 'Loading...';
  return <Heading level={1}>{message}</Heading>;
}