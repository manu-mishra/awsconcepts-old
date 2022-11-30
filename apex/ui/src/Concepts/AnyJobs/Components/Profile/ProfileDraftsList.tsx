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
        console.log(item);
    };
    let theme = getTheme();
    const deleteIcon: IIconProps = { iconName: 'Delete' };
    const stackStyles: Partial<IStackStyles> = { root: { height: 44, backgroundColor:theme.palette.white } };
    return (
        <>
            <Stack horizontal horizontalAlign='center' styles={stackStyles}>
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

