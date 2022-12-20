import { getTheme, IStackStyles, PrimaryButton, Stack } from '@fluentui/react'
import { API } from 'aws-amplify'
import { useState } from 'react'
import { useAuthenticator } from '@aws-amplify/ui-react';

export const RaiseError = () => {
    const { route,  user } = useAuthenticator((context) => [
        context.route,
        context.user
      ]);

  let theme = getTheme();
    async function RaiseError() {
        const callApi = async () => {
            await API.get('api', '/health/error', {
                responseType: 'json'
            });
        }
        callApi().catch(() => {
            console.log(errorCount + 'errors');
            setErrorCount(errorCount + 1);
        });
    }
    const stackStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation8,
            background: theme.palette.white,
            margin: '20px',
            padding: '20px'
        },
    };
    const [errorCount, setErrorCount] = useState<number>(0)
    return (
        <Stack styles={stackStyles}>

            <PrimaryButton onClick={() => RaiseError()}>Raise Errors</PrimaryButton>
            {
                (errorCount > 0) ?
                    <div>
                        <p>Total {errorCount} errors raised!</p>
                        <p>
                            <strong>search x-ray for</strong> <br></br>
                            annotation.domainOperation = "Application.Common.Test.RaiseErrorCommand"<br></br>
                            {route === 'authenticated' ?
                            <span> annotation.user="{user?.attributes?.sub}"</span>
                            :<span></span>}
                        </p>
                    </div> :
                    (<div ></div>)
            }
        </Stack>
    )
}
