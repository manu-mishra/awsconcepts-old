import { PrimaryButton } from '@fluentui/react';
import { Worker, Viewer } from '@react-pdf-viewer/core';
import React from 'react';

export const PdfViewer = () => {
    const [url, setUrl] = React.useState('');
    const viewerRef = React.createRef<HTMLDivElement>();
    const viewerRefCopy = React.createRef<HTMLParagraphElement>();
    // Handle the `onChange` event of the `file` input
    const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const files = e.target.files;
        if (files?.length && files.length > 0)
            setUrl(URL.createObjectURL(files[0]));
    };
    function btnCLicked() {
        var p=viewerRefCopy.current;
        if(viewerRef.current && viewerRef.current?.textContent)
        {
            var doc =document.createTextNode(viewerRef.current?.textContent);
            p?.appendChild(doc);
        }
            
        //console.log(viewerRef.current?.innerHTML);
        
    }
    return (
        <>
        <p ref = {viewerRefCopy}></p>
            <Worker workerUrl="https://unpkg.com/pdfjs-dist@3.0.279/build/pdf.worker.min.js">
                ...
            </Worker>
            
            <div>
                <input type="file" accept=".pdf" onChange={onChange} />
                <PrimaryButton onClick={btnCLicked}></PrimaryButton>
                <div style={{ height: '750px' }}>
                    {url ? (
                        <div
                            style={{
                                border: '1px solid rgba(0, 0, 0, 0.3)',
                                height: '100%',
                            }}
                        >
                            <div ref={viewerRef}><Viewer fileUrl={url} /></div>

                        </div>
                    ) : (
                        <div
                            style={{
                                alignItems: 'center',
                                border: '2px dashed rgba(0, 0, 0, .3)',
                                display: 'flex',
                                fontSize: '2rem',
                                height: '100%',
                                justifyContent: 'center',
                                width: '100%',
                            }}
                        >
                            Preview area
                        </div>
                    )}
                </div>
            </div>
        </>


    )
}
