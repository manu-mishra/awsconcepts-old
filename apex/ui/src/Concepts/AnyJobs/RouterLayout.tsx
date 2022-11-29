import { Grid } from '@aws-amplify/ui-react';
import { AnimationClassNames, } from '@fluentui/react';
import { Outlet } from 'react-router-dom';
import CommandBar from './Components/CommandBar';


const RouterLayout = () => {
    return (
        <>
            <CommandBar />
            <Grid borderRadius={'medium'} border={'ThreeDDarkShadow'} margin={'relative.small'} backgroundColor={'white'} className={AnimationClassNames.slideRightIn400}>
                <Outlet />
            </Grid>
        </>
    );
}

export default RouterLayout