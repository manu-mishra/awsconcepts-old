import { getTheme, IStackStyles, PrimaryButton, Stack } from '@fluentui/react'
import { API } from 'aws-amplify'
import { useState } from 'react'

export const RaiseError = () => {
    
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
                    <p>Total {errorCount} errors raised!</p> :
                    (<div ></div>)
            }
        </Stack>
    )
}
