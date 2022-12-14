import { CommandBarButton, DetailsListLayoutMode, getTheme, IIconProps, IStackStyles, 
    SelectionMode, ShimmeredDetailsList, Stack, Selection, Link, IColumn } from '@fluentui/react';
import { API } from 'aws-amplify';
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { ProfileSummary } from "../../Model/ApplicantsModel"

export default function ProfileDraftsList() {

    const [selection] = useState<Selection >(new Selection());
    const [allProfiles, setAllProfiles] = useState<ProfileSummary[] | null>();
    React.useEffect(() => {
        loadData();
    }, []);

function loadData()
{
    const callApi = async () => {
        let resp = await API.get('api', '/Applicants/ProfileDrafts', {
            responseType: 'json'
        });

        setAllProfiles(resp as ProfileSummary[]);
    }
    callApi().catch(console.error);
}

    const _columns = [
        { key: 'name', name: 'Name', fieldName: 'name', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'title', name: 'Title', fieldName: 'title', minWidth: 100, maxWidth: 200, isResizable: true },
        { key: 'highlights', name: 'Highlights', fieldName: 'profileHighlights', minWidth: 100, maxWidth: 200, isResizable: true },
    ];
    const navigate = useNavigate();
   
    function _renderItemColumn(item: ProfileSummary, index: number|undefined, column: IColumn|undefined) {
        const fieldContent = item[column?.fieldName as keyof ProfileSummary] as string;
        switch (column?.key) {
          case 'name':
            return <Link onClick={() => navigate('/anyjobs/profiles/drafts/'+item.id)} to={"/anyjobs/profiles/drafts/"+item.id}>{fieldContent}</Link>
      
          default:
            return <span>{fieldContent}</span>;
        }
      }
    async function DeleteSelected(){
        let itemsToDelete=selection.getSelection() as ProfileSummary[];
        itemsToDelete.forEach((value)=>
        {
            const callApi = async () => {
                let profileSummary= value as ProfileSummary
                await API.del('api', '/Applicants/ProfileDrafts/'+profileSummary.id, {
                    responseType: 'json'
                });
            }
            callApi().catch(console.error);

        });
        let newItems = allProfiles?.filter(function( el ) {
            return !itemsToDelete.includes( el );
          });
        console.log(newItems);
        setAllProfiles(newItems);
        //setAllProfiles()
    }
    let theme = getTheme();
    
    const deleteIcon: IIconProps = { iconName: 'Delete' };
    const stackStyles: Partial<IStackStyles> = { root: { height: 44, backgroundColor:theme.palette.white } };
    return (
        <>
            <Stack horizontal horizontalAlign='center' styles={stackStyles}>
                <CommandBarButton iconProps={deleteIcon} text="Delete Profile" onClick={DeleteSelected}/>
            </Stack>
            <ShimmeredDetailsList
                items={allProfiles || []}
                columns={_columns}
                setKey="set"
                layoutMode={DetailsListLayoutMode.justified}
                selectionMode={SelectionMode.multiple}
                selectionPreservedOnEmptyClick={true}
                selection = {selection}
                enableShimmer={!allProfiles}
                onRenderItemColumn={_renderItemColumn}
            />
        </>

    )
}

