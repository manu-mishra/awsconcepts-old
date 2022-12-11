import { CommandBarButton, DetailsListLayoutMode, getTheme, IColumn, IIconProps, IStackStyles, Link, SelectionMode, Selection, ShimmeredDetailsList, Stack, Panel, PanelType } from '@fluentui/react';
import { API } from 'aws-amplify';
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { useParams } from "react-router";
import { OrganizationJob } from '../../Model/OrganizationsModel';

import { useBoolean } from '@fluentui/react-hooks';
let theme = getTheme();
export const JobList = () => {
    const { orgId } = useParams();
    const navigate = useNavigate();
    const [selection] = useState<Selection>(new Selection());
    const _columns = [
        { key: 'title', name: 'title', fieldName: 'title', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'description', name: 'description', fieldName: 'description', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'applications', name: 'applications', fieldName: 'id', minWidth: 100, maxWidth: 200, isResizable: true },
       ];
    const [allOrganizationsJobs, setAllOrganizationsJobs] = useState<OrganizationJob[] | null>();
    const [selectedOrganizationsJob, setSelectedOrganizationsJob] = useState<OrganizationJob | null>();
    const [isOpen, { setTrue: openPanel, setFalse: dismissPanel }] = useBoolean(false);
    React.useEffect(() => {

        const callApi = async () => {
            let resp = await API.get('api', '/Organizations/'+orgId+'/jobs', {
                responseType: 'json'
            });

            setAllOrganizationsJobs(resp as OrganizationJob[]);
        }
        callApi().catch(console.error);
    }, [orgId]);
    function showDetails(org: OrganizationJob): void {
        setSelectedOrganizationsJob(org);
        openPanel();
    }
    function _renderItemColumn(item: OrganizationJob, index: number | undefined, column: IColumn | undefined) {
        const fieldContent = item[column?.fieldName as keyof OrganizationJob] as string;
        switch (column?.key) {
            case 'title':
                return <Link onClick={() => navigate('/anyjobs/organizations/'+orgId+'/jobs/' + item.id)} to={'/anyjobs/organizations/'+orgId+'/jobs/' + item.id}>{fieldContent}</Link>
            case 'description':
                return <CommandBarButton iconProps={showDetailsIcon} text="View" onClick={() => { showDetails(item) }} />
            case 'applications':
                return <CommandBarButton iconProps={showJobsApplicants} text="Applications" onClick={() => navigate('/anyjobs/organizations/'+orgId+'/jobs/' + item.id+'/applications')} />
            default:
                return <span>{fieldContent}</span>;
        }
    }
    async function DeleteSelected() {
        let itemsToDelete = selection.getSelection() as OrganizationJob[];
        console.log(itemsToDelete);
        itemsToDelete.forEach((value) => {
            const callApi = async () => {
                let job = value as OrganizationJob
                await API.del('api', '/Organizations/' + orgId+'/jobs/'+job.id, {
                    responseType: 'json'
                });
            }
            callApi().catch(console.error);

        });
        let newItems = allOrganizationsJobs?.filter(function (el) {
            return !itemsToDelete.includes(el);
        });
        setAllOrganizationsJobs(newItems);
    }
    const addIcon: IIconProps = { iconName: 'Add' };
    const deleteIcon: IIconProps = { iconName: 'Delete' };
    const showDetailsIcon: IIconProps = { iconName: 'Articles' };
    const showJobsApplicants: IIconProps = { iconName: 'D365TalentLearn' };

    const stackStyles: Partial<IStackStyles> = { root: { height: 44, backgroundColor: theme.palette.white } };

    function createMarkup(markup: string) { return { __html: markup }; };
    return (
        <>
            <Stack horizontal horizontalAlign='center' styles={stackStyles}>
                <CommandBarButton iconProps={addIcon} text="New Job" onClick={() => navigate('/anyjobs/Organizations/'+orgId+'/jobs/new')} />
                <CommandBarButton iconProps={deleteIcon} text="Delete Selected Jobs" onClick={DeleteSelected} />
            </Stack>
            <ShimmeredDetailsList
                items={allOrganizationsJobs || []}
                columns={_columns}
                setKey="set"
                layoutMode={DetailsListLayoutMode.justified}
                selectionMode={SelectionMode.multiple}
                selectionPreservedOnEmptyClick={true}
                selection = {selection}
                enableShimmer={!allOrganizationsJobs}
                onRenderItemColumn={_renderItemColumn}
            />
            <Panel
                isLightDismiss
                isOpen={isOpen}
                type={PanelType.large}
                closeButtonAriaLabel="Close"
                headerText={selectedOrganizationsJob?.title}
                onDismiss={dismissPanel}
            >
                {selectedOrganizationsJob &&
                    <div dangerouslySetInnerHTML={createMarkup(selectedOrganizationsJob.description)}></div>
                }
            </Panel>
        </>

    )
}
