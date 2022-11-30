import { CommandBarButton, DetailsListLayoutMode, getTheme, IIconProps, IStackStyles, SelectionMode, ShimmeredDetailsList, Stack } from '@fluentui/react';
import { API } from 'aws-amplify';
import React, { useState } from 'react'
import { ProfileSummary } from "../../Model/ApplicantsModel"

export const ProfileDraftsList = () => {

    const [allProfiles, setAllProfiles] = useState<ProfileSummary[] | null>();
    React.useEffect(() => {
        const callApi = async () => {
            let resp = await API.get('api', '/Applicants/ProfileDrafts', {
                responseType: 'json'
            });

            setAllProfiles(resp as ProfileSummary[]);
        }
        callApi().catch(console.error);
    }, []);

    const _columns = [
        { key: 'column1', name: 'Name', fieldName: 'name', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'column2', name: 'Title', fieldName: 'title', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'column3', name: 'Highlights', fieldName: 'profileHighlights', minWidth: 100, maxWidth: 200, isResizable: true },
    ];
    function _onItemSelected(item: any): void {
        alert(`Item invoked: ${item.name}`);
    };
    let theme = getTheme();
    const addIcon: IIconProps = { iconName: 'Add' };
    const editIcon: IIconProps = { iconName: 'EditNote' };
    const makePrimaryIcon: IIconProps = { iconName: 'PartyLeader' };
    const deleteIcon: IIconProps = { iconName: 'Delete' };
    const stackStyles: Partial<IStackStyles> = { root: { height: 44, backgroundColor:theme.palette.white } };
    return (
        <>
            <Stack horizontal horizontalAlign='center' styles={stackStyles}>
                <CommandBarButton iconProps={addIcon} text="New Profile" />
                <CommandBarButton iconProps={editIcon} text="Edit Profile" />
                <CommandBarButton iconProps={makePrimaryIcon} text="Make Default Profile" />
                <CommandBarButton iconProps={deleteIcon} text="Delete Profile" />
            </Stack>
            <ShimmeredDetailsList
                items={allProfiles || []}
                columns={_columns}
                setKey="set"
                layoutMode={DetailsListLayoutMode.justified}
                selectionMode={SelectionMode.single}
                selectionPreservedOnEmptyClick={true}
                checkButtonAriaLabel="select row"
                enableShimmer={!allProfiles}
                onActiveItemChanged={_onItemSelected}
            />
        </>

    )
}

