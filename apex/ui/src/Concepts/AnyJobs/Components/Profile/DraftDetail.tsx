import { getTheme, IStackStyles, IStackTokens, Label, PrimaryButton, Stack, TextField } from '@fluentui/react';
import { API } from 'aws-amplify';
import React from 'react';
import { useState } from 'react'
import { useParams } from "react-router";
import RichTextEditor, { EditorValue } from 'react-rte';
import { ProfileDocument, ProfileDraft } from '../../Model/ApplicantsModel';
import { EmailPicker } from './EmailPicker';
import { NamePicker } from './NamePicker';
import { PhoneNumberPicker } from './PhoneNumberPicker';
import { SkillPicker } from './SkillPicker';
import { useNavigate } from 'react-router-dom';


export const DraftDetail = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const [richText, setRichText] = useState<EditorValue>(RichTextEditor.createEmptyValue());
  const [profileDraft, setProfileDraft] = useState<ProfileDraft | undefined>(undefined);
  const [profileDocument, setProfileDocument] = useState<ProfileDocument>();


  function handleHighLightsChange(e: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue: string | undefined) {
    const updatedProfileDraft: ProfileDraft = { ...profileDraft };
    updatedProfileDraft.profileHighlights = newvalue;
    setProfileDraft(updatedProfileDraft);
  }
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
        if (respProfile.profileText)
          setRichText(RichTextEditor.createValueFromString(respProfile.profileText, 'html'))
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
  function handelSkillChange(selectedItems: string[] | undefined) {
    const updatedProfileDraft: ProfileDraft = { ...profileDraft };
    updatedProfileDraft.skills = selectedItems;
    setProfileDraft(updatedProfileDraft);
  }
  function handelNameChange(selectedItems: string[] | undefined) {
    const updatedProfileDraft: ProfileDraft = { ...profileDraft };
    let newValue: string | undefined;
    if (selectedItems && selectedItems[0])
      newValue = selectedItems[0];
    updatedProfileDraft.name = newValue;
    setProfileDraft(updatedProfileDraft);
  }

  const onDocumentTextChange = (value: EditorValue) => {
    setRichText(value);
    let profileState = { ...profileDraft };
    profileState.profileText = richText?.toString('html');
    setProfileDraft(profileState as ProfileDraft);
  };
  async function submit() {
    await API.post('api', '/Applicants/ProfileDrafts', { body: { ...profileDraft } })
      .then((response) => {
        // Add your code here
        setProfileDraft(response as ProfileDraft);
      })
      .catch((error) => {
        console.log(error.response);
      });;

  }
  async function publish() {
    await API.post('api', '/Applicants/ProfileDrafts/'+profileDraft?.id+'/publish', { body: { ...profileDraft } })
      .then((response) => {
        // Add your code here
        navigate('/anyjobs/profiles');
      })
      .catch((error) => {
        console.log(error.response);
      });;

  }
  if (profileDocument && profileDraft) {

    return (
      <Stack horizontalAlign='stretch' styles={mainStackStyles} tokens={smallSpacingToken}>
        <NamePicker analysis={profileDocument?.analysis} handleChange={handelNameChange} profileDraft={profileDraft}></NamePicker>
        <EmailPicker analysis={profileDocument?.analysis}></EmailPicker>
        <PhoneNumberPicker analysis={profileDocument?.analysis}></PhoneNumberPicker>
        <SkillPicker analysis={profileDocument?.analysis} handleChange={handelSkillChange} profileDraft={profileDraft}></SkillPicker>
        <TextField label="Highlights" value={profileDraft.profileHighlights} onChange={handleHighLightsChange} />

        <Label>profileText</Label>
        <div style={{ minHeight: '200px' }}>
          <RichTextEditor editorStyle={{ minHeight: '200px' }} value={richText} onChange={onDocumentTextChange} />
        </div>
        <Stack horizontal horizontalAlign='start' tokens={smallSpacingToken}>
          <PrimaryButton className="ms-welcome__action" iconProps={{ iconName: "Save" }} onClick={submit}>
            Save draft
          </PrimaryButton>
          <PrimaryButton className="ms-welcome__action" iconProps={{ iconName: "Globe" }} onClick={publish}>
            Publish Profile
          </PrimaryButton>
        </Stack>
      </Stack>
    )
  }
  return <label>Loading..</label>
}
