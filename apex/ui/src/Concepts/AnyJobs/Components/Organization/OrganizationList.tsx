import { CommandBarButton, DetailsListLayoutMode, getTheme, IColumn, IIconProps, IStackStyles, Link, SelectionMode, Selection, ShimmeredDetailsList, Stack, Panel, PanelType } from '@fluentui/react';
import { API } from 'aws-amplify';
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { Organization } from '../../Model/OrganizationsModel';

import { useBoolean } from '@fluentui/react-hooks';
let theme = getTheme();
export default function OrganizationList()  {
    const navigate = useNavigate();
    const [selection] = useState<Selection>(new Selection());
    const _columns = [
        { key: 'name', name: 'Name', fieldName: 'name', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'details', name: 'Details', fieldName: 'name', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'jobs', name: 'View Jobs', fieldName: 'id', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'newjobs', name: 'New Job', fieldName: 'id', minWidth: 100, maxWidth: 200, isResizable: true },
    ];
    const [allOrganizations, setAllOrganizations] = useState<Organization[] | null>();
    const [selectedOrganizations, setSelectedOrganizations] = useState<Organization | null>();
    const [isOpen, { setTrue: openPanel, setFalse: dismissPanel }] = useBoolean(false);
    React.useEffect(() => {

        const callApi = async () => {
            let resp = await API.get('api', '/Organizations', {
                responseType: 'json'
            });

            setAllOrganizations(resp as Organization[]);
        }
        callApi().catch(console.error);
    }, []);
    function showDetails(org: Organization): void {
        setSelectedOrganizations(org);
        openPanel();
    }
    function _renderItemColumn(item: Organization, index: number | undefined, column: IColumn | undefined) {
        const fieldContent = item[column?.fieldName as keyof Organization] as string;
        switch (column?.key) {
            case 'name':
                return <Link onClick={() => navigate('/anyjobs/Organizations/' + item.id)} to={"/anyjobs/profiles/documents/" + item.id}>{fieldContent}</Link>
            case 'details':
                return <CommandBarButton iconProps={showDetailsIcon} text="View" onClick={() => { showDetails(item) }} />
            case 'jobs':
                return <CommandBarButton iconProps={showJobsIcon} text="Jobs" onClick={() => navigate('/anyjobs/Organizations/' + item.id + '/jobs')} />
            case 'newjobs':
                return <CommandBarButton iconProps={newJobIcon} text="New Job" onClick={() => navigate('/anyjobs/Organizations/' + item.id + '/jobs/new')} />
            default:
                return <span>{fieldContent}</span>;
        }
    }
    async function DeleteSelected() {
        let itemsToDelete = selection.getSelection() as Organization[];
        itemsToDelete.forEach((value) => {
            const callApi = async () => {
                let org = value as Organization
                await API.del('api', '/Organizations/' + org.id, {
                    responseType: 'json'
                });
            }
            callApi().catch(console.error);

        });
        let newItems = allOrganizations?.filter(function (el) {
            return !itemsToDelete.includes(el);
        });
        console.log(newItems);
        setAllOrganizations(newItems);
    }
    const addIcon: IIconProps = { iconName: 'Add' };
    const deleteIcon: IIconProps = { iconName: 'Delete' };
    const showDetailsIcon: IIconProps = { iconName: 'PageArrowRight' };
    const showJobsIcon: IIconProps = { iconName: 'ReportLibrary' };
    const newJobIcon: IIconProps = { iconName: 'Add' };

    const stackStyles: Partial<IStackStyles> = { root: { height: 44, backgroundColor: theme.palette.white } };

    function createMarkup(markup: string) { return { __html: markup }; };
    return (
        <>
            <Stack horizontal horizontalAlign='center' styles={stackStyles}>
                <CommandBarButton iconProps={addIcon} text="New Organization" onClick={() => navigate('/anyjobs/Organizations/new')} />
                <CommandBarButton iconProps={deleteIcon} text="Delete Organization" onClick={DeleteSelected} />
            </Stack>
            <ShimmeredDetailsList
                items={allOrganizations || []}
                columns={_columns}
                setKey="set"
                selectionMode={SelectionMode.single}
                layoutMode={DetailsListLayoutMode.justified}
                selectionPreservedOnEmptyClick={true}
                enableShimmer={!allOrganizations}
                onRenderItemColumn={_renderItemColumn}
            />
            <Panel
                isLightDismiss
                isOpen={isOpen}
                type={PanelType.large}
                closeButtonAriaLabel="Close"
                headerText={selectedOrganizations?.name}
                onDismiss={dismissPanel}
            >
                {selectedOrganizations &&
                    <div dangerouslySetInnerHTML={createMarkup(selectedOrganizations.details)}></div>
                }
            </Panel>
        </>

    )
}
