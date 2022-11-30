import { Stack, PrimaryButton, TextField } from '@fluentui/react';
import React, { useState } from 'react'
import { ProfileDraft } from "../../Model/ApplicantsModel"
import { PdfViewer } from '../PdfViewer';
import { SkillPicker } from '../SkillPicker';

export const NewProfileDrafts = () => {
    const [profileDraft, setProfileDraft] = useState<ProfileDraft | undefined>(undefined)

    function _handleNameChange(e:React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue:string | undefined){
        const updatedProfileDraft : ProfileDraft={...profileDraft};
        updatedProfileDraft.name = newvalue;
        setProfileDraft(updatedProfileDraft);
    }
    function _handleHighLightsChange(e:React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue:string | undefined){
        const updatedProfileDraft : ProfileDraft={...profileDraft};
        updatedProfileDraft.profileHighlights = newvalue;
        setProfileDraft(updatedProfileDraft);
    }
    

    return (
        <>
            <Stack>
                <TextField id={'name'} label="Profile Name" value={profileDraft?.name} onChange={_handleNameChange} />
                <TextField label="Profile Highlights" value={profileDraft?.profileHighlights} onChange={_handleHighLightsChange} />
                <SkillPicker></SkillPicker>
                <PdfViewer></PdfViewer>
                <PrimaryButton className="ms-welcome__action" iconProps={{ iconName: "ChevronRight" }}>
                    Sign In
                </PrimaryButton>
            </Stack>
        </>

    )
}

