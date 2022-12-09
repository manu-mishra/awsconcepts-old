import { Stack, PrimaryButton, getTheme, IStackTokens, IStackStyles, TextField} from '@fluentui/react'
import { useState } from 'react';
import { Organization } from '../../Model/OrganizationsModel';
import RichTextEditor, { EditorValue } from 'react-rte';
import { API } from 'aws-amplify';
import { useNavigate } from 'react-router-dom';

export const JobDetails = () => {
    let theme = getTheme();
    const navigate = useNavigate();
    const [organization, setOrganization] = useState<Organization | null>();
    const [richText, setRichText] = useState<EditorValue>(RichTextEditor.createEmptyValue());
    const onNameChange = (e: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue: string | undefined) => {
        let orgState = { ...organization };
        orgState.name = newvalue;
        setOrganization(orgState as Organization);
    }

    const onDetailChange = (value: EditorValue) => {
        setRichText(value);
        let orgState = { ...organization };
        orgState.details = richText?.toString('html');
        setOrganization(orgState as Organization);
    };

    async function btnCLicked() {
        console.log(organization);
        if (organization) {
            await API.put('api', '/Organizations', { body: { ...organization } })
                .then((response) => {
                    navigate('/anyjobs/organizations/'+response.id);
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
                <TextField label="Name" value={organization?.name} onChange={onNameChange} />
                <div style={{ minHeight: '200px' }}>
                    <RichTextEditor editorStyle={{ minHeight: '200px' }} value={richText} onChange={onDetailChange} />
                </div>
                <PrimaryButton text="Submit" onClick={btnCLicked} />
            </Stack>
        </>
    )
}
