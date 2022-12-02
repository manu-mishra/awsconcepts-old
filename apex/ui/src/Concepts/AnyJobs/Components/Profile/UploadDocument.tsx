import { Auth } from 'aws-amplify';
import { applicationConfig } from './../../../../configuration/AmplifyConfig'
import axios from 'axios';
import { useState } from 'react';
import { getTheme, IStackStyles, IStackTokens, PrimaryButton, Stack, TextField } from '@fluentui/react';
import { Worker, Viewer } from '@react-pdf-viewer/core';
import React from 'react';


export const UploadDocument = () => {
    const [profileName, setProfileName] = useState<string>('');
    const [fileUrl, setFileUrl] = useState('');
    const viewerRef = React.createRef<HTMLDivElement>();
    const inputFileRef = React.createRef<HTMLInputElement>();
    const handleNameChange = (e: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newvalue: string | undefined) => {
        if (newvalue)
            setProfileName(newvalue);
        else
            setProfileName('')
    }
    const onFileSelected = (e: React.ChangeEvent<HTMLInputElement>) => {
        const files = e.target.files;
        if (files?.length && files.length > 0) {
            setFileUrl(URL.createObjectURL(files[0]));
            setProfileName(files[0].name);
            console.log(fileUrl);
        }
    };
    function btnCLicked() {
        if (inputFileRef.current && inputFileRef.current.files ) 
        {
        fileUpload(inputFileRef.current.files[0]);
        }
    }
    const fileUpload = async (file: any) => {
        if (viewerRef.current && viewerRef.current?.textContent) {
            const formData = new FormData();
            formData.append('file', file);
            formData.append('FileTextContent', viewerRef.current?.textContent);
            const session = await Auth.currentSession();
            const token = session.getIdToken().getJwtToken();
            console.log(applicationConfig.API.endpoints[0].endpoint);
            const url = applicationConfig.API.endpoints[0].endpoint + '/Applicants/ProfileDocuments';
            return await axios.post(url, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                    Authorization: `Bearer ${token}`,
                },
            });
        }

    };
    let theme = getTheme();

    const smallSpacingToken: IStackTokens = {
        childrenGap: 's1',
        padding: 's1',
    };
    const mainStackStyles: IStackStyles = {
        root: {
            boxShadow: theme.effects.elevation4,
            background: theme.palette.neutralLight,
            marginBottom: '1px'
        },
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
            <Stack horizontalAlign='stretch' styles={mainStackStyles} tokens={smallSpacingToken}>
                <Stack verticalAlign='start' horizontalAlign='space-evenly' tokens={smallSpacingToken} styles={childStackStyles}>
                    <TextField id={'name'} label="Profile Name" value={profileName} onChange={handleNameChange} />
                    <input ref={inputFileRef} type="file" accept=".pdf" onChange={onFileSelected} />
                    <PrimaryButton text="Upload" onClick={btnCLicked} />
                </Stack>
                <Stack horizontalAlign='space-evenly' styles={childStackStyles}>
                    <Worker workerUrl="https://unpkg.com/pdfjs-dist@3.0.279/build/pdf.worker.min.js"></Worker>
                    <div >
                        {fileUrl ? (
                            <div>
                                <div ref={viewerRef}>
                                    <Viewer fileUrl={fileUrl} />
                                </div>
                            </div>
                        ) : (
                            <div >

                            </div>
                        )}
                    </div>
                </Stack>

            </Stack>
        </>
    )
}