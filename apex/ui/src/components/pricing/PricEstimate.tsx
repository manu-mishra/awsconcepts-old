// components/Home.js

import CSS from 'csstype';
import { Worker, Viewer } from '@react-pdf-viewer/core';

import { DetailsList, DetailsListLayoutMode, getTheme, IColumn, IStackStyles, IStackTokens, Label, Panel, PanelType, PrimaryButton, SelectionMode, Stack } from '@fluentui/react';
import { useBoolean } from '@fluentui/react-hooks';
import { useState } from 'react';
import { Estimate } from './Estimate';
export default function PricEstimate() {

  let theme = getTheme();

  const maincomponent: CSS.Properties = {
    margin: '20px',
    justifyContent: 'center',
    height: '90vh',
    backgroundColor: 'white',
    alignItems: 'center',
    boxShadow: theme.effects.elevation4,
  }
  const smallSpacingToken: IStackTokens = {
    childrenGap: 's1',
    padding: 's1',
  };
  const stackStyles: IStackStyles = {
    root: {
      boxShadow: theme.effects.elevation8,
      background: theme.palette.white,
      margin: '20px',
      padding: '20px'
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
      const newItems = _copyAndSort(serviceList, currColumn.fieldName!, currColumn.isSortedDescending);
      setColumns(newColumns);
      setServiceList(newItems);
    }
  };
  const serviceList: Estimate[] = [
    { serviceName: 'AWS Web Application Firewall (WAF)', configuration: '', cost: '' },
    { serviceName: 'Amazon DynamoD', configuration: '', cost: '665.80 USD' },
    { serviceName: 'AWS Shield', configuration: '', cost: '3,051.32 USD' },
    { serviceName: 'Amazon Simple Storage Service (S3)', configuration: '', cost: '' },
    { serviceName: 'AWS Lambda', configuration: '', cost: '' },
    { serviceName: 'Amazon API Gateway', configuration: '', cost: '' },
    { serviceName: 'Amazon CloudFront', configuration: '', cost: '' },
    { serviceName: 'Business Support Plan', configuration: '', cost: '' },
    { serviceName: 'Amazon OpenSearch Service', configuration: '', cost: '' }
  ];
  const [ServiceList, setServiceList] = useState<Estimate[]>(serviceList);

  const _columns: IColumn[] = [
    {
      key: 'serviceName', name: 'Service Name', fieldName: 'serviceName', minWidth: 100, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick, isRowHeader: true,
      isSorted: false,
      isSortedDescending: false,
      sortAscendingAriaLabel: 'Sorted A to Z',
      sortDescendingAriaLabel: 'Sorted Z to A',
    },
    {
      key: 'configuration', name: 'Configuration', fieldName: 'configuration', minWidth: 100, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick, isRowHeader: true,
      isSorted: false,
      isSortedDescending: false,
      sortAscendingAriaLabel: 'Sorted A to Z',
      sortDescendingAriaLabel: 'Sorted Z to A',
    },
    {
      key: 'cost', name: 'Cost', fieldName: 'cost', minWidth: 100, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick, isRowHeader: true,
      isSorted: false,
      isSortedDescending: false,
      sortAscendingAriaLabel: 'Sorted A to Z',
      sortDescendingAriaLabel: 'Sorted Z to A',
    },
  ];
  const [columns, setColumns] = useState<IColumn[]>(_columns);

  function _copyAndSort<T>(items: T[], columnKey: string, isSortedDescending?: boolean): T[] {
    const key = columnKey as keyof T;
    return items.slice(0).sort((a: T, b: T) => ((isSortedDescending ? a[key] < b[key] : a[key] > b[key]) ? 1 : -1));
  }

  const [isPanel1Open, { setTrue: openPanel1, setFalse: dismissPanel1 }] = useBoolean(false);
  return (
    <>
      <Stack styles={stackStyles}>
        <Stack horizontal verticalAlign='baseline' tokens={smallSpacingToken} >
          <h1>Price Estimate</h1>
          <PrimaryButton iconProps={{ iconName: "PDF" }} onClick={() => openPanel1()}>Show PDF</PrimaryButton>
        </Stack>
        <Label>1 million request per day with 40% cache hit ratio.</Label>
        <DetailsList items={ServiceList} columns={_columns} setKey="set" layoutMode={DetailsListLayoutMode.justified} selectionMode={SelectionMode.none} />
      </Stack >


      <Panel
        isLightDismiss
        isOpen={isPanel1Open}
        type={PanelType.extraLarge}
        closeButtonAriaLabel="Close"
        headerText={'Cost Estimate'}
        onDismiss={dismissPanel1}>
        <Worker workerUrl="https://unpkg.com/pdfjs-dist@3.0.279/build/pdf.worker.min.js"></Worker>
        <div style={maincomponent}>
          <Viewer fileUrl={'/PricingEstimate.pdf'} />
        </div>
      </Panel>


    </>
  );
}
