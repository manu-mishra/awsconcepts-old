import { Stack, PrimaryButton, TextField } from '@fluentui/react';
import React, { useState } from 'react'
import { ProfileDraft } from "../../Model/ApplicantsModel"
import { PdfViewer } from '../PdfViewer';

export const NewProfileDrafts = () => {
    const [profileDraft, setProfileDraft] = useState<ProfileDraft | undefined>(undefined)

    function handleNameChange(e:React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue:string | undefined){
        const updatedProfileDraft : ProfileDraft={...profileDraft};
        updatedProfileDraft.name = newvalue;
        setProfileDraft(updatedProfileDraft);
    }
    function handleHighLightsChange(e:React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue:string | undefined){
        const updatedProfileDraft : ProfileDraft={...profileDraft};
        updatedProfileDraft.profileHighlights = newvalue;
        setProfileDraft(updatedProfileDraft);
    }
    

    return (
        <>
            <Stack>
                <TextField id={'name'} label="Profile Name" value={profileDraft?.name} onChange={handleNameChange} />
                <TextField label="Profile Highlights" value={profileDraft?.profileHighlights} onChange={handleHighLightsChange} />
                
                <PdfViewer></PdfViewer>
                <PrimaryButton className="ms-welcome__action" iconProps={{ iconName: "ChevronRight" }}>
                    Sign In
                </PrimaryButton>
            </Stack>
        </>

    )
}

