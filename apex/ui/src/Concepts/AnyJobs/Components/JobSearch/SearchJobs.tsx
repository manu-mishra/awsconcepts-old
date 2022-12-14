import React from 'react'
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { API } from 'aws-amplify';
import { useParams } from "react-router";
import { JobSummary } from '../../Model/JobsModel';
import { CommandBarButton, DetailsListLayoutMode, getTheme, IColumn, IIconProps, ISearchBoxStyles, IStackStyles, Link, Panel, PanelType, SearchBox, SelectionMode, ShimmeredDetailsList, Stack } from '@fluentui/react';
import { useBoolean } from '@fluentui/react-hooks';
import CSS from 'csstype';

export default function SearchJobs(){

    const { searchText } = useParams();
    const [searchTerm, setSearchTerm] = useState(searchText);
    const [allJobs, setAllJobs] = useState<JobSummary[] | null>();
    const [selectedJob, setSelectedJob] = useState<JobSummary | null>();
    const [isOpen, { setTrue: openPanel, setFalse: dismissPanel }] = useBoolean(false);
    React.useEffect(() => {
        if (searchText) {
            const callApi = async () => {
                let resp = await API.get('api', '/jobs/search/' + searchText, {
                    responseType: 'json'
                });

                setAllJobs(resp as JobSummary[]);
            }
            callApi().catch(console.error);
        }
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
            padding: '10px',
            marginRight: '10px'
        },
    };
  const searchBoxStyles: ISearchBoxStyles = {
    root: {
        
        fontSize: 'calc(20px + 2vw)',
        minHeight: '4vw',
        width:'95vw',
        color:theme.palette.themePrimary,
        
    },
    icon:
    {
      fontSize:'calc(20px + 2vw)', 
    },
    field:{
      paddingLeft:'4vw',   
      color:theme.palette.themePrimary,
      selectors: {
        ':placeholder-shown': {
        },
        ':focus': {
          paddingLeft:'0'
        }
      } 
    }
};
    return (
        <>
            <Stack styles={stackStyles}>
                <SearchBox placeholder="Search Jobs" underlined={true} onChange={(e, value) => setSearchTerm(value)} value={searchTerm}
                    onSearch={(newValue: any) => navigate('/anyjobs/jobs/search/' + newValue)} styles={searchBoxStyles}></SearchBox>
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


