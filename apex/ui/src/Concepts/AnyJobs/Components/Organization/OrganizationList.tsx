import { CommandBarButton, DetailsListLayoutMode, getTheme, IIconProps, IStackStyles, SelectionMode, ShimmeredDetailsList, Stack } from '@fluentui/react';
import { API } from 'aws-amplify';
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { Organization } from '../../Model/OrganizationsModel';
let theme = getTheme();
export const OrganizationList = () => {
    const navigate = useNavigate();
    const _columns = [
        { key: 'column1', name: 'Name', fieldName: 'name', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'column2', name: 'Detail', fieldName: 'detail', minWidth: 100, maxWidth: 200, isResizable: true },
    ];
    const [allOrganizations, setAllOrganizations] = useState<Organization[] | null>();
    React.useEffect(() => {

        const callApi = async () => {
            let resp = await API.get('api', '/Organizations', {
                responseType: 'json'
            });

            setAllOrganizations(resp as Organization[]);
        }
        callApi().catch(console.error);
    }, []);

    function _onItemSelected(item: Organization): void {
        navigate('/anyjobs/Organizations/'+item.id);
    };
    const addIcon: IIconProps = { iconName: 'Add' };
    const deleteIcon: IIconProps = { iconName: 'Delete' };
    const stackStyles: Partial<IStackStyles> = { root: { height: 44, backgroundColor: theme.palette.white } };
    return (
        <>
            <Stack horizontal horizontalAlign='center' styles={stackStyles}>
                <CommandBarButton iconProps={addIcon} text="New Organization" onClick={() => navigate('/anyjobs/Organizations/new')} />
                <CommandBarButton iconProps={deleteIcon} text="Delete Organization" />
            </Stack>
            <ShimmeredDetailsList
                items={allOrganizations || []}
                columns={_columns}
                setKey="set"
                selectionMode={SelectionMode.single}
                layoutMode={DetailsListLayoutMode.justified}
                selectionPreservedOnEmptyClick={true}
                enableShimmer={!allOrganizations}
                onActiveItemChanged={_onItemSelected}
                
            />
        </>

    )
}
