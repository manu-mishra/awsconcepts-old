import { Stack, PrimaryButton, getTheme, IStackTokens, IStackStyles, TextField, MessageBar, MessageBarType } from '@fluentui/react'
import { useState } from 'react';
import { useParams } from "react-router";
import { OrganizationJob } from '../../Model/OrganizationsModel';
import RichTextEditor, { EditorValue } from 'react-rte';
import { API } from 'aws-amplify';
import React from 'react';

export const JobDetails = () => {
    const { orgId, jobId } = useParams();
    let theme = getTheme();
    const [organizationJob, setOrganizationJob] = useState<OrganizationJob | null>();
    const [richText, setRichText] = useState<EditorValue>(RichTextEditor.createEmptyValue());
    const [IsSuccess, setIsSuccess] = React.useState<boolean | undefined>(undefined);
    React.useEffect(() => {
        if (orgId !== undefined && jobId !== undefined) {
            const callApi = async () => {
                let resp = await API.get('api', '/Organizations/' + orgId + '/jobs/' + jobId, {
                    responseType: 'json'
                }) as OrganizationJob;
                setOrganizationJob(resp);
                if (resp.description)
                    setRichText(RichTextEditor.createValueFromString(resp.description, 'html'))
            }
            callApi().catch(console.error);
        }
    }, [orgId, jobId]);

    const onTitleChange = (e: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue: string | undefined) => {
        let objState = { ...organizationJob };
        objState.title = newvalue;
        setOrganizationJob(objState as OrganizationJob);
    }

    const onDetailChange = (value: EditorValue) => {
        setRichText(value);
        let objState = { ...organizationJob };
        objState.description = richText?.toString('html');
        setOrganizationJob(objState as OrganizationJob);
    };

    async function btnCLicked() {
        if (organizationJob) {
            await API.put('api', '/Organizations/' + orgId + '/jobs/', { body: { ...organizationJob } })
                .then((response) => {
                    setIsSuccess(true) ;
                })
                .catch((error) => {
                    console.log(error.response);
                });;
        }
    }

    const smallSpacingToken: IStackTokens = {
        childrenGap: 's1',
        padding: 's1',
    };
    const childStackStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation4,
            background: theme.palette.white,
            marginBottom: '1px'
        },
    };
    return (
        <>
            <Stack verticalAlign='stretch' horizontalAlign='space-evenly' tokens={smallSpacingToken} styles={childStackStyles}>
                {IsSuccess && <MessageBar
                    messageBarType={MessageBarType.success}
                    isMultiline={false}
                    onDismiss={() => { setIsSuccess(undefined) }}
                    dismissButtonAriaLabel="Close"
                    truncated={true}
                    overflowButtonAriaLabel="See more"
                >
                    <b>Record updated succesfully</b>
                </MessageBar>}
                <TextField label="Title" value={organizationJob?.title} onChange={onTitleChange} />
                <div style={{ minHeight: '200px' }}>
                    <RichTextEditor editorStyle={{ minHeight: '200px' }} value={richText} onChange={onDetailChange} />
                </div>
                <PrimaryButton text="Submit" onClick={btnCLicked} />
            </Stack>
        </>
    )
}
