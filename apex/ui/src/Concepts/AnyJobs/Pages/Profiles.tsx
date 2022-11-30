import * as React from 'react';
import { IStyleSet, Label, ILabelStyles, Pivot, PivotItem } from '@fluentui/react';
import { ProfilesList } from '../Components/Profile/ProfilesList';
import { ProfileDraftsList } from '../Components/Profile/ProfileDraftsList';

const labelStyles: Partial<IStyleSet<ILabelStyles>> = {
  root: { marginTop: 10 },
};

export const Profiles = () => {
  return (
    <Pivot aria-label="Basic Pivot Example">
      <PivotItem
        headerText="My Profiles"
        headerButtonProps={{
          'data-order': 1,
          'data-title': 'My Files Title',
        }}
      >
        <ProfilesList></ProfilesList>
      </PivotItem>
      <PivotItem headerText="Draft Profiles">
        <ProfileDraftsList/>
      </PivotItem>
      <PivotItem headerText="Shared with me">
        <Label styles={labelStyles}>Pivot #3</Label>
      </PivotItem>
    </Pivot>
  );
}
