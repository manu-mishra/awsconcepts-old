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
    { serviceName: 'AWS Web Application Firewall (WAF)', configuration: '30 Million requests per month', cost: 84.00 },
    { serviceName: 'Amazon DynamoD', configuration: 'On Demand, 5 million reads, 15 million reads (with back up and data streams) ', cost: 2342.05 },
    { serviceName: 'AWS Shield', configuration: 'For all resources', cost: 3128.00},
    { serviceName: 'Amazon Simple Storage Service (S3)', configuration: '3.5 TB', cost: 52.15},
    { serviceName: 'AWS Lambda', configuration: '22 Million requests per month', cost: 67.73 },
    { serviceName: 'Amazon API Gateway', configuration: '15 Million requests per month', cost: 15.00},
    { serviceName: 'Amazon CloudFront', configuration: '30 Million requests per month', cost: 127.04},
    { serviceName: 'Business Support Plan', configuration: '24/7 phone and email access, less than 1 hour response time', cost: 991.59 },
    { serviceName: 'Amazon OpenSearch Service', configuration: 'Highly available (3 data nodes and 3 master nodes) ', cost: 4099.96 }
  ];
  const [ServiceList, setServiceList] = useState<Estimate[]>(serviceList);

  const _columns: IColumn[] = [
    {
      key: 'serviceName', name: 'Service Name', fieldName: 'serviceName', minWidth: 300, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick, isRowHeader: true,
      isSorted: false,
      isSortedDescending: false,
      sortAscendingAriaLabel: 'Sorted A to Z',
      sortDescendingAriaLabel: 'Sorted Z to A',
    },
    {
      key: 'configuration', name: 'Configuration', fieldName: 'configuration', minWidth: 400, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick, isRowHeader: true,
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
  function _renderItemColumn(item: Estimate, index: number|undefined, column: IColumn|undefined) {
    const fieldContent = item[column?.fieldName as keyof Estimate] as string;
    switch (column?.key) {
      case 'cost':
        return <span>{fieldContent} USD</span>
  
      default:
        return <span>{fieldContent}</span>;
    }
  }
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
          <PrimaryButton iconProps={{ iconName: "NavigateExternalInline" }} onClick={() => window.open("https://calculator.aws/#/estimate?id=1693e63cec25ffa7214ef39bf031bb447cd761c4")}>Modify Your Copy</PrimaryButton>
        </Stack>
        <Label>1 million request per day with 40% cache hit ratio.</Label>
        <DetailsList items={ServiceList} columns={_columns} setKey="set" layoutMode={DetailsListLayoutMode.justified} selectionMode={SelectionMode.none} 
        onRenderItemColumn={_renderItemColumn}/>
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
