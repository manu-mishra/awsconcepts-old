import { ActionButton, AnimationClassNames, getTheme, IStackStyles, IStackTokens, Stack } from '@fluentui/react'
import { useNavigate } from 'react-router-dom';

const CommandBar = () => {
    const navigate = useNavigate();
    let theme = getTheme();
    const stackStyles: IStackStyles = {
        root: {
            
            borderWidth: '10px',
            borderBottomColor: theme.palette.themeDark,
            marginBottom: '10px'
            
        },
    };
    const smallSpacingToken: IStackTokens = {
        childrenGap: 's1',
    };
    return (
        <>
        {/* <Stack enableScopedSelectors horizontal horizontalAlign="space-between" verticalAlign="start" styles={stackStyles} className={AnimationClassNames.slideRightIn400}> */}
            <Stack horizontal tokens={smallSpacingToken} styles={stackStyles} className={AnimationClassNames.slideRightIn400}>
                <ActionButton allowDisabledFocus onClick={() => navigate('/anyjobs/home')}>Home</ActionButton>
                <ActionButton allowDisabledFocus onClick={() => navigate('/anyjobs/Organizations')}>My Organizations</ActionButton>
                <ActionButton allowDisabledFocus onClick={() => navigate('/anyjobs/Profiles')}>My Profiles</ActionButton>
            </Stack>
        {/* </Stack> */}
        </>
    )
}

export default CommandBar