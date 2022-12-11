import { ActionButton, AnimationClassNames, getTheme, IStackStyles, IStackTokens, Stack } from '@fluentui/react'
import { useNavigate } from 'react-router-dom';
import { AppLogo } from './AppLogo';
import { UserPersona } from './UserPersona';

export const AppCommandBar = () => {
    const navigate = useNavigate();
    let theme = getTheme();
    const stackStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation4,
            background: theme.palette.white,
            marginBottom: '1px'
        },
    };
    const smallSpacingToken: IStackTokens = {
        childrenGap: 's1',
        padding: 's1',
    };

    //AnimationClassNames.fadeIn500
    return (
            <Stack enableScopedSelectors horizontal horizontalAlign="space-between" verticalAlign="center" tokens={smallSpacingToken} styles={stackStyles} className={AnimationClassNames.slideRightIn400}>
                <AppLogo></AppLogo>
                <Stack horizontal tokens={smallSpacingToken}>
                    <ActionButton allowDisabledFocus onClick={() => navigate('/')}>Home</ActionButton>
                    <ActionButton allowDisabledFocus onClick={() => navigate('/anyjobs')}>AnyCompany Jobs Concept</ActionButton>
                </Stack>
                <UserPersona></UserPersona>
            </Stack>
    )
}

