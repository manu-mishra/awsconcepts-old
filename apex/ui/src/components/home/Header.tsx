import React from 'react'
import { API } from 'aws-amplify';

export function Header () {
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
  return (
    <div> {greeting} </div>
  )
}