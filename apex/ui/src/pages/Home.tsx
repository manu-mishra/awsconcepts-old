// components/Home.js

import { Pivot, PivotItem, DocumentCard, DocumentCardImage, ImageFit, DocumentCardDetails, DocumentCardTitle, DocumentCardActivity, IDocumentCardStyles, IDocumentCardActivityPerson, IIconProps, Stack, IStackStyles, Panel, PanelType, getTheme } from '@fluentui/react';
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
  const costEstimateIconProps: IIconProps = {
    iconName: 'Money',
    styles: { root: { color: '#0078d4', fontSize: '120px', width: '120px', height: '120px' } },
  };
  let theme = getTheme();
  const cardStyles: IDocumentCardStyles = {
    root: {
      display: 'inline-block', marginTop: 10, marginLeft: 20, marginBottom: 20, width: 400,
      boxShadow: theme.effects.elevation8,
      borderRadius: theme.effects.roundedCorner4
    },
  };
  const stackStyles: Partial<IStackStyles> = {
    root: {
      backgroundColor: 'white',
    },
  };
  const maincomponent: CSS.Properties = {
    display: 'flex',
    flexWrap: 'wrap',
    justifyContent: 'center',
    minHeight: '85vh',
    backgroundColor: 'white',
    alignItems: 'center'

  }
  const menucomponent: CSS.Properties = {
    display: 'flex',
    flexWrap: 'wrap',
    justifyContent: 'center',
    maxWidth: '1100px',
    minHeight: '85vh',
    backgroundColor: 'white',
    alignItems: 'center'

  }
  const [isPanel1Open, { setTrue: openPanel1, setFalse: dismissPanel1 }] = useBoolean(false);
  return (
    <>
      <div style={maincomponent}>
        <div style={menucomponent}>
 
          <DocumentCard
            styles={cardStyles}
            onClick={() => navigate('/anyjobs/')}
          >
            <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={wordSearchIconProps} />
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
            onClick={() => navigate('/anyjobs/jobs/searchparaphrase/')}
          >
            <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={newsSearchIconProps} />
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
          <DocumentCard styles={cardStyles} onClick={() => navigate('/login')} >
            <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={authenticationIconProps} />
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
            onClick={() => navigate('/anyjobs/profiles/drafts/new')}
          >
            <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={documentAnalysisIconProps} />
            <DocumentCardDetails>
              <DocumentCardTitle title="Comprehend text Analysis" shouldTruncate />
              <DocumentCardTitle
                title="Example extracting meaningfull insights from document using COmprehend"
                shouldTruncate
                showAsSecondaryTitle
              />
            </DocumentCardDetails>
            <DocumentCardActivity activity="Modified Dec 12, 2022" people={people.slice(0, 3)} />
          </DocumentCard>

          <DocumentCard aria-label={'App architecture (POC)'} styles={cardStyles} onClick={() => openPanel1()}>
            <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={architectureIconProps} />
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
            aria-label={'Cost Estimate'}
            styles={cardStyles}
            onClick={() => navigate('/costestimate')}
          >
            <DocumentCardImage height={200} imageFit={ImageFit.center} iconProps={costEstimateIconProps} />
            <DocumentCardDetails>
              <DocumentCardTitle title="Cost Estimate" shouldTruncate />
              <DocumentCardTitle
                title="Click to view"
                shouldTruncate
                showAsSecondaryTitle
              />
            </DocumentCardDetails>
            <DocumentCardActivity activity="Modified Dec 12, 2022" people={people.slice(0, 3)} />
          </DocumentCard>

        </div>
      </div>
      <Panel
        isLightDismiss
        isOpen={isPanel1Open}
        type={PanelType.extraLarge}
        closeButtonAriaLabel="Close"
        headerText={'Application Architecture'}
        onDismiss={dismissPanel1}>
        <Stack styles={stackStyles} verticalFill horizontalAlign='stretch' >
          <Pivot aria-label="Document Analysis">
            <PivotItem
              headerText="Current State"
              headerButtonProps={{
                'data-order': 1,
                'data-title': 'Current Architecture',
              }}
            >
              <img src='/ProductionArchitecture.png' alt='aws concepts architecture' style={{ height:'80vh', width:'80vw', paddingBottom: '1vw', paddingTop: '1vw', paddingRight: '1vw', paddingLeft: '1vw' }} />
            </PivotItem>
            <PivotItem headerText="Future Possibilities">
              <img src='/FutureArchitecture.png' alt='aws concepts architecture' style={{ height:'80vh', width:'80vw', paddingBottom: '1vw', paddingTop: '1vw', paddingRight: '1vw', paddingLeft: '1vw' }} />
            </PivotItem>
          </Pivot>
        </Stack>
      </Panel>
    </>
  );
}