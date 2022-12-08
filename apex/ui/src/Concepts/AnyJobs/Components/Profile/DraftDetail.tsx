import { API } from 'aws-amplify';
import React from 'react';
import { useState } from 'react'
import { useParams } from "react-router";
import { ProfileDocument, ProfileDraft } from '../../Model/ApplicantsModel';
import { EmailPicker } from './EmailPicker';
import { NamePicker } from './NamePicker';
import { PhoneNumberPicker } from './PhoneNumberPicker';

export const DraftDetail = () => {

  const { id } = useParams();
  const [profileDraft, setProfileDraft] = useState<ProfileDraft>();
  const [profileDocument, setProfileDocument] = useState<ProfileDocument>();

  React.useEffect(() => {
    if (id !== undefined) {
      const callApi = async () => {
        let respProfile = await API.get('api', '/Applicants/ProfileDrafts/' + id, {
          responseType: 'json'
        }) as ProfileDraft;

        setProfileDraft(respProfile);
        let responseDoc = await API.get('api', '/Applicants/ProfileDocuments/' + respProfile.profileDocumentId, {
          responseType: 'json'
        });
        setProfileDocument(responseDoc as ProfileDocument);

      }
      callApi().catch(console.error);
    }
  }, [id]);
  return (
    <div>
      <NamePicker analysis={profileDocument?.analysis}></NamePicker>
      <EmailPicker analysis={profileDocument?.analysis}></EmailPicker>
      <PhoneNumberPicker analysis={profileDocument?.analysis}></PhoneNumberPicker>
      <p>{JSON.stringify(profileDraft)}</p>
      <p>{JSON.stringify(profileDocument)}</p>
    </div>
  )
}
