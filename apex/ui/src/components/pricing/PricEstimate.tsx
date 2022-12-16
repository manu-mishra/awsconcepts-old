// components/Home.js

import { DocumentCard, DocumentCardImage, ImageFit, DocumentCardDetails, DocumentCardTitle, DocumentCardActivity, IDocumentCardStyles, IDocumentCardActivityPerson, IIconProps, Stack, IStackStyles, Panel, PanelType, getTheme } from '@fluentui/react';
import CSS from 'csstype';
import {Worker, Viewer} from '@react-pdf-viewer/core';

import React from 'react';
export default function PricEstimate() {

  
  const maincomponent: CSS.Properties = {
    display: 'flex',
    flexWrap: 'wrap',
    justifyContent: 'center',
    height: '85vh',
    backgroundColor: 'white',
    alignItems: 'center'

  }

  return (
    <>
      <Worker workerUrl="https://unpkg.com/pdfjs-dist@3.0.279/build/pdf.worker.min.js"></Worker>
      <div style={maincomponent}>
            <Viewer fileUrl={'/PricingEstimate.pdf'} />
      </div>

    </>
  );
}
