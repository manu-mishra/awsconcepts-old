import { Stack, PrimaryButton, getTheme, IStackTokens, IStackStyles, TextField} from '@fluentui/react'
import { useState } from 'react';
import { useParams } from "react-router";
import { OrganizationJob } from '../../Model/OrganizationsModel';
import RichTextEditor, { EditorValue } from 'react-rte';
import { API } from 'aws-amplify';
import { useNavigate } from 'react-router-dom';

export const NewJob = () => {
    const { orgId } = useParams();
    let theme = getTheme();
    const navigate = useNavigate();
    const [organizationsJob, setOrganizationJob] = useState<OrganizationJob | null>();
    const [richText, setRichText] = useState<EditorValue>(RichTextEditor.createEmptyValue());
    const onTitleChange = (e: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue: string | undefined) => {
        let jobState = { ...organizationsJob };
        jobState.title = newvalue;
        setOrganizationJob(jobState as OrganizationJob);
    }

    const onDetailChange = (value: EditorValue) => {
        setRichText(value);
        let jobState = { ...organizationsJob };
        jobState.description = richText?.toString('html');
        setOrganizationJob(jobState as OrganizationJob);
    };

    async function btnCLicked() {
        console.log(organizationsJob);
        if (organizationsJob) {
            await API.put('api', '/Organizations/'+orgId+'/jobs', { body: { ...organizationsJob } })
                .then((response) => {
                    navigate('/anyjobs/organizations/'+orgId+'/jobs/'+response.id);
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
                <h3>Create New Organization</h3>
                <TextField label="Title" value={organizationsJob?.title} onChange={onTitleChange} />
                <div style={{ minHeight: '200px' }}>
                    <RichTextEditor editorStyle={{ minHeight: '200px' }} value={richText} onChange={onDetailChange} />
                </div>
                
                <PrimaryButton text="Submit" onClick={btnCLicked} />
            </Stack>
        </>
    )
}
