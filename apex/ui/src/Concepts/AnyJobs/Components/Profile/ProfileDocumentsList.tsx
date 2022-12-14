import { CommandBarButton, DetailsListLayoutMode, getTheme, IIconProps, IStackStyles, 
    SelectionMode, ShimmeredDetailsList, Stack, Selection, Link, IColumn } from '@fluentui/react';
import { API } from 'aws-amplify';
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import {  ProfileDocumentSummary } from "../../Model/ApplicantsModel"

export default function  ProfileDocumentsList() {

    const [selection] = useState<Selection >(new Selection());
    const [allProfileDocuments, setAllProfileDocuments] = useState<ProfileDocumentSummary[] | null>();
    React.useEffect(() => {
        loadData();
    }, []);

function loadData()
{
    const callApi = async () => {
        let resp = await API.get('api', '/Applicants/ProfileDocuments', {
            responseType: 'json'
        });

        setAllProfileDocuments(resp as ProfileDocumentSummary[]);
    }
    callApi().catch(console.error);
}

    const _columns = [
        { key: 'name', name: 'Name', fieldName: 'name', minWidth: 100, maxWidth: 200, isResizable: true },
     ];
    const navigate = useNavigate();
   
    function _renderItemColumn(item: ProfileDocumentSummary, index: number|undefined, column: IColumn|undefined) {
        const fieldContent = item[column?.fieldName as keyof ProfileDocumentSummary] as string;
        switch (column?.key) {
          case 'name':
            return <Link onClick={() => navigate('/anyjobs/profiles/documents/'+item.id)} to={"/anyjobs/profiles/documents/"+item.id}>{fieldContent}</Link>
          default:
            return <span>{fieldContent}</span>;
        }
      }
    async function DeleteSelected(){
        let itemsToDelete=selection.getSelection() as ProfileDocumentSummary[];
        itemsToDelete.forEach((value)=>
        {
            const callApi = async () => {
                let profileSummary= value as ProfileDocumentSummary
                await API.del('api', '/Applicants/ProfileDocuments/'+profileSummary.id, {
                    responseType: 'json'
                });
            }
            callApi().catch(console.error);

        });
        let newItems = allProfileDocuments?.filter(function( el ) {
            return !itemsToDelete.includes( el );
          });
        console.log(newItems);
        setAllProfileDocuments(newItems);
    }
    let theme = getTheme();
    
    const deleteIcon: IIconProps = { iconName: 'Delete' };
    const stackStyles: Partial<IStackStyles> = { root: { height: 44, backgroundColor:theme.palette.white } };
    return (
        <>
            <Stack horizontal horizontalAlign='center' styles={stackStyles}>
                <CommandBarButton iconProps={deleteIcon} text="Delete Document" onClick={DeleteSelected}/>
            </Stack>
            <ShimmeredDetailsList
                items={allProfileDocuments || []}
                columns={_columns}
                setKey="set"
                layoutMode={DetailsListLayoutMode.justified}
                selectionMode={SelectionMode.multiple}
                selectionPreservedOnEmptyClick={true}
                selection = {selection}
                enableShimmer={!allProfileDocuments}
                onRenderItemColumn={_renderItemColumn}
            />
        </>

    )
}

