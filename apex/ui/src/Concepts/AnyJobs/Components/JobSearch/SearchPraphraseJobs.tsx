import React from 'react'
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { API } from 'aws-amplify';
import { useParams } from "react-router";
import { JobSummary } from '../../Model/JobsModel';
import { CommandBarButton, DetailsListLayoutMode, getTheme, IColumn, IIconProps, IStackStyles, Link, Panel, PanelType, SearchBox, SelectionMode, ShimmeredDetailsList, Stack } from '@fluentui/react';
import { useBoolean } from '@fluentui/react-hooks';
import CSS from 'csstype';

export default function SearchPraphraseJobs(){

    const { searchText } = useParams();
    const[searchTerm, setSearchTerm]=useState(searchText);
    const [allJobs, setAllJobs] = useState<JobSummary[] | null>();
    const [selectedJob, setSelectedJob] = useState<JobSummary | null>();
    const [isOpen, { setTrue: openPanel, setFalse: dismissPanel }] = useBoolean(false);
    React.useEffect(() => {

        const callApi = async () => {
            if(searchText)
            {
                let resp = await API.get('api', '/jobs/searchparaphrase/' + searchText, {
                    responseType: 'json'
                });
    
                setAllJobs(resp as JobSummary[]);
            }
            else
                setAllJobs([]);
        }
        callApi().catch(console.error);
    }, [searchText]);

    
    const navigate = useNavigate();
    const _columns = [
        { key: 'title', name: 'Search Results', fieldName: 'title', minWidth: 100, maxWidth: 200, isResizable: true }
    ];
    const showDetailsIcon: IIconProps = { iconName: 'Articles' };
    function _renderItemColumn(item: JobSummary, index: number | undefined, column: IColumn | undefined) {
        const fieldContent = item[column?.fieldName as keyof JobSummary] as string;
        switch (column?.key) {
            case 'title':
                return <>
                    <Stack styles={stackRowStyles}>
                        <Link style={jobTitle} onClick={() => navigate('/anyjobs/organizations/' + item.organizationId + '/jobs/' + item.id)} to={'/anyjobs/organizations/' + item.organizationId + '/jobs/' + item.id}>{fieldContent}</Link>
                        <Stack verticalAlign='start' horizontalAlign='stretch' horizontal>
                            <div style={truncate}>{item.description}</div>
                            <CommandBarButton iconProps={showDetailsIcon} text="View" onClick={() => { showDetails(item) }} />
                        </Stack>
                    </Stack>
                </>
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
        width: '80vw',
        fontSize: '15px',
    }

    const jobTitle: CSS.Properties = {
        fontSize: '25px',
    }
    let theme = getTheme();
    const stackStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation4,
            backgroundColor: 'white',
            
        },
    };
    const stackRowStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation4,
            backgroundColor: 'white',
            padding:'10px',
            marginRight:'10px'
        },
    };
    return (
        <>
        <Stack styles={stackStyles}>
            <SearchBox placeholder="Search Jobs" underlined={true} onChange={(e,value)=> setSearchTerm(value)} value={searchTerm} 
            onSearch={(newValue:any)=>navigate('/anyjobs/jobs/searchparaphrase/'+newValue )}></SearchBox>
        </Stack>
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


