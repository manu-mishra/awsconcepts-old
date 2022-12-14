import { Pivot, PivotItem, Label, ShimmeredDetailsList, DetailsListLayoutMode, SelectionMode, getTheme, IStackStyles, IStackTokens, Stack, IColumn } from '@fluentui/react';
import { API } from 'aws-amplify';
import React from 'react';
import { useState } from 'react'
import { useParams } from "react-router";
import { ProfileDocument } from '../../Model/ApplicantsModel';

export default function ProfileDocumentsDetail () {

  const { id } = useParams();
  const [profileDocument, setProfileDocument] = useState<ProfileDocument>();

  React.useEffect(() => {
    if (id !== undefined) {
      const callApi = async () => {
        let respProfile = await API.get('api', '/Applicants/ProfileDocuments/' + id, {
          responseType: 'json'
        }) as ProfileDocument;
        setProfileDocument(respProfile);
      }
      callApi().catch(console.error);
    }
  }, [id]);


  let theme = getTheme();
  const smallSpacingToken: IStackTokens = {
    childrenGap: 's1',
    padding: 's1',
  };
  const mainStackStyles: IStackStyles = {
    root: {
      boxShadow: theme.effects.elevation4,
      marginBottom: '1px'
    },
  };
  const _onColumnClick = (ev: React.MouseEvent<HTMLElement>, column: IColumn): void => {
    if (columns) {
      const newColumns: IColumn[] = columns.slice();
      const currColumn: IColumn = newColumns.filter(currCol => column.key === currCol.key)[0];
      newColumns.forEach((newCol: IColumn) => {
        if (newCol === currColumn) {
          currColumn.isSortedDescending = !currColumn.isSortedDescending;
          currColumn.isSorted = true;
        } else {
          newCol.isSorted = false;
          newCol.isSortedDescending = true;
        }
      });
      if (profileDocument && profileDocument.analysis) {
        const newItems = _copyAndSort(profileDocument.analysis, currColumn.fieldName!, currColumn.isSortedDescending);
        profileDocument.analysis = newItems;
        setColumns(newColumns);
        setProfileDocument(profileDocument);
      }

    }
  };
  
  const _columns: IColumn[] = [
    { key: 'text', name: 'Text',   fieldName: 'text', minWidth: 100, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick  ,isRowHeader: true,
        isSorted: false,
        isSortedDescending: false,
        sortAscendingAriaLabel: 'Sorted A to Z',
        sortDescendingAriaLabel: 'Sorted Z to A',},
    { key: 'type', name: 'Type',   fieldName: 'type', minWidth: 100, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick,  isRowHeader: true,
        isSorted: false,
        isSortedDescending: false,
        sortAscendingAriaLabel: 'Sorted A to Z',
        sortDescendingAriaLabel: 'Sorted Z to A',},
    { key: 'score', name: 'Score', fieldName: 'score', minWidth: 100, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick ,isRowHeader: true,
        isSorted: false,
        isSortedDescending: false,
        sortAscendingAriaLabel: 'Sorted A to Z',
        sortDescendingAriaLabel: 'Sorted Z to A',},
  ];
  const [columns, setColumns] = useState<IColumn[]>(_columns);
  
  function _copyAndSort<T>(items: T[], columnKey: string, isSortedDescending?: boolean): T[] {
    const key = columnKey as keyof T;
    return items.slice(0).sort((a: T, b: T) => ((isSortedDescending ? a[key] < b[key] : a[key] > b[key]) ? 1 : -1));
  }
  return (
    <Stack horizontalAlign='stretch' styles={mainStackStyles} tokens={smallSpacingToken}>
      <h1>{profileDocument?.name}</h1>
      <h2>{profileDocument?.size} bytes</h2>
      <Pivot aria-label="Document Analysis">
        <PivotItem
          headerText="Document Text"
          headerButtonProps={{
            'data-order': 1,
            'data-title': 'Document Text Title',
          }}
        >
          <Label >{profileDocument?.documentText}</Label>
        </PivotItem>
        <PivotItem headerText="Document Analysis">
          <ShimmeredDetailsList
            items={profileDocument?.analysis || []}
            columns={_columns}
            setKey="set"
            layoutMode={DetailsListLayoutMode.justified}
            selectionMode={SelectionMode.none}
            enableShimmer={!profileDocument}
          />
        </PivotItem>
      </Pivot>
    </Stack>
  )
}
