import React from 'react'
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { API } from 'aws-amplify';
import { useParams } from "react-router";
import { JobSummary } from '../../Model/JobsModel';
import { CommandBarButton, DetailsListLayoutMode, IColumn, IIconProps, Link, Panel, PanelType, SelectionMode, ShimmeredDetailsList } from '@fluentui/react';
import { useBoolean } from '@fluentui/react-hooks';
import CSS from 'csstype';

export const SearchJobs = () => {

    const { searchText } = useParams();
    const [allJobs, setAllJobs] = useState<JobSummary[] | null>();
    const [selectedJob, setSelectedJob] = useState<JobSummary | null>();
    const [isOpen, { setTrue: openPanel, setFalse: dismissPanel }] = useBoolean(false);
    React.useEffect(() => {

        const callApi = async () => {
            let resp = await API.get('api', '/jobs/search/' + searchText, {
                responseType: 'json'
            });

            setAllJobs(resp as JobSummary[]);
        }
        callApi().catch(console.error);
    }, [searchText]);
    const navigate = useNavigate();
    const _columns = [
        { key: 'title', name: 'Title', fieldName: 'title', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'description', name: 'Description', fieldName: 'description', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'showDetail', name: 'details', fieldName: 'description', minWidth: 100, maxWidth: 200, isResizable: true }
    ];
    const showDetailsIcon: IIconProps = { iconName: 'Articles' };
    function _renderItemColumn(item: JobSummary, index: number | undefined, column: IColumn | undefined) {
        const fieldContent = item[column?.fieldName as keyof JobSummary] as string;
        switch (column?.key) {
            case 'title':
                return <Link onClick={() => navigate('/anyjobs/organizations/' + item.organizationId + '/jobs/' + item.id)} to={'/anyjobs/organizations/' + item.organizationId + '/jobs/' + item.id}>{fieldContent}</Link>
            case 'description':
                return <><div style={truncate} >{fieldContent}</div></>
                case 'showDetail':
                    return <><CommandBarButton iconProps={showDetailsIcon} text="View" onClick={() => { showDetails(item) }} /></>
                default:
                return <span>{fieldContent}</span>;
        }
    }
    function showDetails(org: JobSummary): void {
        setSelectedJob(org);
        openPanel();
    }
    function createMarkup(markup: string) { return { __html: markup }; };
    const truncate: CSS.Properties = {
        whiteSpace: 'nowrap',
        overflow: 'hidden',
        textOverflow: 'ellipsis',
    }
    return (
        <>
            <ShimmeredDetailsList
                items={allJobs || []}
                columns={_columns}
                setKey="set"
                layoutMode={DetailsListLayoutMode.justified}
                selectionMode={SelectionMode.none}
                selectionPreservedOnEmptyClick={true}
                enableShimmer={!allJobs}
                onRenderItemColumn={_renderItemColumn}
            />
            <Panel
                isLightDismiss
                isOpen={isOpen}
                type={PanelType.large}
                closeButtonAriaLabel="Close"
                headerText={selectedJob?.title}
                onDismiss={dismissPanel}
            >
                {selectedJob &&
                    <div dangerouslySetInnerHTML={createMarkup(selectedJob.description)}></div>
                }
            </Panel></>
    )
}


