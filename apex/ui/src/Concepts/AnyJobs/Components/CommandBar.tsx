import { ActionButton, AnimationClassNames, getTheme, IStackStyles, IStackTokens, Stack } from '@fluentui/react'
import { useNavigate } from 'react-router-dom';

const CommandBar = () => {
    const navigate = useNavigate();
    let theme = getTheme();
    const stackStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation4,
            backgroundColor: 'white'

        },
    };
    const smallSpacingToken: IStackTokens = {
        childrenGap: 's1',
    };
    return (
        <>
            <Stack horizontal horizontalAlign='start' tokens={smallSpacingToken} styles={stackStyles} className={AnimationClassNames.slideRightIn400}>
                <ActionButton allowDisabledFocus onClick={() => navigate('/anyjobs/home')}>Home</ActionButton>
                <ActionButton allowDisabledFocus onClick={() => navigate('/anyjobs/Organizations')}>My Organizations</ActionButton>
                <ActionButton allowDisabledFocus onClick={() => navigate('/anyjobs/Profiles')}>My Profiles</ActionButton>
                <ActionButton allowDisabledFocus onClick={() => navigate('/anyjobs/Profiles/Drafts')}>Draft Profiles</ActionButton>
            </Stack>
        </>
    )
}

export default CommandBar