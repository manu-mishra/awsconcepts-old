// components/Home.js

import CSS from 'csstype';
import { Worker, Viewer } from '@react-pdf-viewer/core';

import { ActionButton, DetailsList, DetailsListLayoutMode, getTheme, IColumn, IStackStyles, IStackTokens, Label, Panel, PanelType, PrimaryButton, SelectionMode, Stack } from '@fluentui/react';
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
    { serviceName: 'AWS Web Application Firewall (WAF)', configuration: '30 Million requests per month', cost: 84.00, productUrl:"https://aws.amazon.com/waf/", limitsUrl:"https://docs.aws.amazon.com/waf/latest/developerguide/limits.html", faq:"https://aws.amazon.com/waf/faqs/" },
    { serviceName: 'Amazon DynamoD', configuration: 'On Demand, 5 million writes, 15 million reads (with back up and data streams) ', cost: 1086.05, productUrl:"https://aws.amazon.com/dynamodb/", limitsUrl:"https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/ServiceQuotas.html", faq:"https://aws.amazon.com/dynamodb/faqs/" },
    { serviceName: 'AWS Shield', configuration: 'For all resources', cost: 3128.00, productUrl:"https://aws.amazon.com/shield/", limitsUrl:"https://docs.aws.amazon.com/waf/latest/developerguide/shield-limits.html", faq:"https://aws.amazon.com/shield/faqs/"},
    { serviceName: 'Amazon Simple Storage Service (S3)', configuration: '3.5 TB', cost: 52.15, productUrl:"https://aws.amazon.com/s3/", limitsUrl:"https://docs.aws.amazon.com/AmazonS3/latest/userguide/BucketRestrictions.html", faq:"https://aws.amazon.com/s3/faqs/?nc=sn&loc=7"},
    { serviceName: 'AWS Lambda', configuration: '22 Million requests per month', cost: 67.73, productUrl:"https://aws.amazon.com/lambda/", limitsUrl:"https://docs.aws.amazon.com/lambda/latest/dg/gettingstarted-limits.html", faq:"https://aws.amazon.com/lambda/faqs/" },
    { serviceName: 'Amazon API Gateway', configuration: '15 Million requests per month', cost: 15.00, productUrl:"https://aws.amazon.com/api-gateway/", limitsUrl:"https://docs.aws.amazon.com/apigateway/latest/developerguide/limits.html", faq:"https://aws.amazon.com/api-gateway/faqs/"},
    { serviceName: 'Amazon CloudFront', configuration: '30 Million requests per month', cost: 127.04, productUrl:"https://aws.amazon.com/cloudfront/", limitsUrl:"https://docs.aws.amazon.com/AmazonCloudFront/latest/DeveloperGuide/cloudfront-limits.html", faq:"https://aws.amazon.com/cloudfront/faqs/?nc=sn&loc=5&dn=2"},
    { serviceName: 'Business Support Plan', configuration: '24/7 phone and email access, less than 1 hour response time', cost: 991.59 , productUrl:"https://aws.amazon.com/premiumsupport/plans/business/", limitsUrl:"https://aws.amazon.com/premiumsupport/plans/", faq:"https://aws.amazon.com/premiumsupport/faqs/?nc=sn&loc=6"},
    { serviceName: 'Amazon OpenSearch Service', configuration: 'Highly available (3 data nodes and 3 master nodes) ', cost: 4099.96 , productUrl:"https://aws.amazon.com/opensearch-service/", limitsUrl:"https://docs.aws.amazon.com/opensearch-service/latest/developerguide/limits.html", faq:"https://aws.amazon.com/opensearch-service/faqs/"}
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
    {
      key: 'more', name: 'More Information', fieldName: 'cost', minWidth: 100, maxWidth: 200, isResizable: true, onColumnClick: _onColumnClick, isRowHeader: true,
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
      case 'more':
        return <Stack horizontal>
          <ActionButton iconProps={{ iconName: "Globe" }} onClick={() => window.open(item.productUrl)}>Product Link</ActionButton>
          <ActionButton iconProps={{ iconName: "ReportWarning" }} onClick={() => window.open(item.limitsUrl)}>Limits and Quotas</ActionButton>
          <ActionButton iconProps={{ iconName: "StatusCircleQuestionMark" }} onClick={() => window.open(item.faq)}>FAQs</ActionButton>
        </Stack>
  
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
          <PrimaryButton iconProps={{ iconName: "NavigateExternalInline" }} onClick={() => window.open("https://calculator.aws/#/estimate?id=efe2bbda6220f3b05466555484c86192acd4efe5")}>Modify Your Copy</PrimaryButton>
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
