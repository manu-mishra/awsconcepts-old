import { ActionButton, AnimationClassNames, getTheme, IStackStyles, IStackTokens, Stack } from '@fluentui/react'
import { useNavigate } from 'react-router-dom';
import { AppLogo } from './AppLogo';
import { UserPersona } from './UserPersona';

export const AppCommandBar = () => {
    const navigate = useNavigate();
    let theme = getTheme();
    const stackStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation8,
        },
    };
    const smallSpacingToken: IStackTokens = {
        childrenGap: 's1',
        padding: 's1',
    };

    //AnimationClassNames.fadeIn500
    return (
            <Stack enableScopedSelectors horizontal horizontalAlign="space-between" verticalAlign="center" tokens={smallSpacingToken} verticalFill styles={stackStyles} className={AnimationClassNames.slideRightIn400}>
                <AppLogo></AppLogo>
                <Stack horizontal tokens={smallSpacingToken}>
                    <ActionButton allowDisabledFocus onClick={() => navigate('/')}>Home</ActionButton>
                    <ActionButton allowDisabledFocus onClick={() => navigate('/protected')}>My Jobs</ActionButton>
                    <ActionButton allowDisabledFocus onClick={() => navigate('/protected2')}>My Jobs</ActionButton>
                </Stack>
                <UserPersona></UserPersona>
            </Stack>
    )
}

