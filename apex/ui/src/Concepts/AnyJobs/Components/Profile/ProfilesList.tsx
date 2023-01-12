
import { CommandBarButton, DetailsListLayoutMode, getTheme, IIconProps, IStackStyles, SelectionMode, ShimmeredDetailsList, Stack } from '@fluentui/react';
import { API } from 'aws-amplify';
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { ProfileSummary } from "../../Model/ApplicantsModel"

let theme = getTheme();
export default  function ProfilesList (){
    const navigate = useNavigate();
    const _columns = [
        { key: 'column1', name: 'Name', fieldName: 'name', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'column2', name: 'Title', fieldName: 'title', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'column3', name: 'Highlights', fieldName: 'profileHighlights', minWidth: 100, maxWidth: 200, isResizable: true },
    ];
    const [allProfiles, setAllProfiles] = useState<ProfileSummary[] | null>();
    React.useEffect(() => {

        const callApi = async () => {
            let resp = await API.get('api', '/Applicants/Profiles/', {
                responseType: 'json'
            });

            setAllProfiles(resp as ProfileSummary[]);
        }
        callApi().catch(console.error);
    }, []);

    function _onItemSelected(item: any): void {
        console.log(item);
    };
    const addIcon: IIconProps = { iconName: 'Add' };
    const makePrimaryIcon: IIconProps = { iconName: 'PartyLeader' };
    const deleteIcon: IIconProps = { iconName: 'Delete' };
    const stackStyles: Partial<IStackStyles> = { root: { height: 44, backgroundColor: theme.palette.white } };
    return (
        <>
            <Stack horizontal horizontalAlign='center' styles={stackStyles}>
                <CommandBarButton iconProps={addIcon} text="News Profile" onClick={() => navigate('/anyjobs/profiles/drafts/new')} />
                <CommandBarButton iconProps={makePrimaryIcon} text="Make Default Profile" />
                <CommandBarButton iconProps={deleteIcon} text="Delete Profile" />
            </Stack>
            <ShimmeredDetailsList
                items={allProfiles || []}
                columns={_columns}
                setKey="set"
                selectionMode={SelectionMode.multiple}
                layoutMode={DetailsListLayoutMode.justified}
                selectionPreservedOnEmptyClick={true}
                enableShimmer={!allProfiles}
                onActiveItemChanged={_onItemSelected}
            />
        </>

    )
}
