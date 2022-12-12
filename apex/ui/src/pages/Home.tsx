// components/Home.js

import { DocumentCard, DocumentCardImage, ImageFit, DocumentCardDetails, DocumentCardTitle, DocumentCardActivity, IDocumentCardStyles, IDocumentCardActivityPerson, IIconProps, Stack, IStackStyles, Panel, PanelType, getTheme } from '@fluentui/react';
import CSS from 'csstype';
import { useNavigate } from 'react-router-dom';
import { useBoolean } from '@fluentui/react-hooks';
export function Home() {
  const navigate = useNavigate();
  const people: IDocumentCardActivityPerson[] = [
    { name: 'Manu Mishra', profileImageSrc: '', initials: 'MM' },
  ];
  
  const architectureIconProps: IIconProps = {
    iconName: 'ProductVariant',
    styles: { root: { color: '#0078d4', fontSize: '120px', width: '120px', height: '120px' } },
  };
  const newsSearchIconProps: IIconProps = {
    iconName: 'NewsSearch',
    styles: { root: { color: '#0078d4', fontSize: '120px', width: '120px', height: '120px' } },
  };
  const wordSearchIconProps: IIconProps = {
    iconName: 'SearchAndApps',
    styles: { root: { color: '#0078d4', fontSize: '120px', width: '120px', height: '120px' } },
  };
  const authenticationIconProps: IIconProps = {
    iconName: 'AuthenticatorApp',
    styles: { root: { color: '#0078d4', fontSize: '120px', width: '120px', height: '120px' } },
  };
  const documentAnalysisIconProps: IIconProps = {
    iconName: 'AnalyticsReport',
    styles: { root: { color: '#0078d4', fontSize: '120px', width: '120px', height: '120px' } },
  };
  const cardStyles: IDocumentCardStyles = {
    root: { display: 'inline-block', marginRight: 20, marginBottom: 20, width: 320 },
  };
  let theme = getTheme();
  const stackStyles: Partial<IStackStyles> = {
    root: {
      backgroundColor: 'white',
      boxShadow: theme.effects.elevation4,
    },
  };
  const maincomponent : CSS.Properties = {
    minHeight: '85vh',
    backgroundColor: 'white'
    
  }
  const [isPanel1Open, { setTrue: openPanel1, setFalse: dismissPanel1 }] = useBoolean(false);
  return (
    <>
    <div style={maincomponent}>
    <Stack styles={stackStyles} horizontal horizontalAlign='center' verticalFill verticalAlign='center'>
    <DocumentCard
        aria-label={'App architecture' }
        styles={cardStyles}
        onClick={()=>openPanel1()}
      >
        <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={architectureIconProps}/>
        <DocumentCardDetails>
          <DocumentCardTitle title="Application Architecture" shouldTruncate />
          <DocumentCardTitle
            title="Click to view"
            shouldTruncate
            showAsSecondaryTitle 
          />
        </DocumentCardDetails>
        <DocumentCardActivity activity="Modified Dec 12, 2022" people={people.slice(0, 3)} />
      </DocumentCard>
      <DocumentCard
        styles={cardStyles}
        onClick={()=>navigate('/anyjobs/')}
      >
        <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={wordSearchIconProps}/>
        <DocumentCardDetails>
          <DocumentCardTitle title="Full Text Search" shouldTruncate />
          <DocumentCardTitle
            title="Example showing advance full text search capabilities with AWS open search."
            shouldTruncate
            showAsSecondaryTitle 
          />
        </DocumentCardDetails>
        <DocumentCardActivity activity="Modified Dec 12, 2022" people={people.slice(0, 3)} />
      </DocumentCard>
      
      <DocumentCard
        styles={cardStyles}
        onClick={()=>navigate('/anyjobs/jobs/searchparaphrase/')}
      >
        <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={newsSearchIconProps}/>
        <DocumentCardDetails>
          <DocumentCardTitle title="Praphrase Search" shouldTruncate />
          <DocumentCardTitle
            title="Example showing advance Praphrase search capabilities with AWS open search."
            shouldTruncate
            showAsSecondaryTitle 
          />
        </DocumentCardDetails>
        <DocumentCardActivity activity="Modified Dec 12, 2022" people={people.slice(0, 3)} />
      </DocumentCard>
    </Stack>
 
    <Stack styles={stackStyles} horizontal horizontalAlign='center' verticalFill verticalAlign='center'>
    
      <DocumentCard styles={cardStyles} onClick={()=>navigate('/login')} >
        <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={authenticationIconProps}/>
        <DocumentCardDetails>
          <DocumentCardTitle title="Zero knowledge proof - User SignIn" shouldTruncate />
          <DocumentCardTitle
            title="Authenticate without sending password to Aws Cognito using SRP(secure remote password)"
            shouldTruncate
            showAsSecondaryTitle 
          />
        </DocumentCardDetails>
        <DocumentCardActivity activity="Modified Dec 12, 2022" people={people.slice(0, 3)} />
      </DocumentCard>
            
      <DocumentCard
        styles={cardStyles}
        onClick={()=>navigate('/anyjobs/profiles/drafts/new')}
      >
        <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={documentAnalysisIconProps}/>
        <DocumentCardDetails>
          <DocumentCardTitle title="Document text Analysis" shouldTruncate />
          <DocumentCardTitle
            title="Example extracting meaningfull insights from document"
            shouldTruncate
            showAsSecondaryTitle 
          />
        </DocumentCardDetails>
        <DocumentCardActivity activity="Modified Dec 12, 2022" people={people.slice(0, 3)} />
      </DocumentCard>
    </Stack>
    </div>
    <Panel
                isLightDismiss
                isOpen={isPanel1Open}
                type={PanelType.large}
                closeButtonAriaLabel="Close"
                headerText={'Application Architecture'}
                onDismiss={dismissPanel1}
            >
                <Stack styles={stackStyles} verticalFill horizontalAlign='stretch' >
                  <img src= '/AwsConceptsArchitectureDiagram.png' alt ='aws concepts architecture' style={{paddingBottom:'2vw',paddingTop:'2vw', paddingRight:'1vw', paddingLeft:'1vw'}}/>
                </Stack>
            </Panel>
    </>
  );
}