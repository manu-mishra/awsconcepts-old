import { Routes, Route } from "react-router-dom";
import { JobApplicants } from "./Components/Organization/JobApplicants";
import { JobDetails } from "./Components/Organization/JobDetails";
import { JobList } from "./Components/Organization/JobList";
import { NewJob } from "./Components/Organization/NewJob";
import { NewOrganization } from "./Components/Organization/NewOrganization";
import { OrganizationDetail } from "./Components/Organization/OrganizationDetail";
import { OrganizationList } from "./Components/Organization/OrganizationList";
import { DraftDetail } from "./Components/Profile/DraftDetail";
import { ProfileDocumentsDetail } from "./Components/Profile/ProfileDocumentsDetail";
import { ProfileDocumentsList } from "./Components/Profile/ProfileDocumentsList";
import { ProfileDraftsList } from "./Components/Profile/ProfileDraftsList";
import { ProfilesList } from "./Components/Profile/ProfilesList";
import { UploadDocument } from "./Components/Profile/UploadDocument";
import Home from './Pages/Home';
import RouterLayout from "./RouterLayout";


const AnyJobsRoutes = () => (
    <>
        <Routes>
            <Route path="/" element={<RouterLayout />}>
                <Route path="/" element={<Home />} />
                <Route path="/home" element={<Home />} />
                <Route path="/profiles" element={<ProfilesList />} />
                <Route path="/profiles/documents/" element={<ProfileDocumentsList />} />
                <Route path="/profiles/documents/:id" element={<ProfileDocumentsDetail />} />
                <Route path="/profiles/drafts/" element={<ProfileDraftsList />} />
                <Route path="/profiles/drafts/new" element={<UploadDocument />} />
                <Route path="/profiles/drafts/:id" element={<DraftDetail />} />
                <Route path="/organizations" element={<OrganizationList />} />
                <Route path='/organizations/new' element={<NewOrganization/>} ></Route>
                <Route path='/organizations/:id' element={<OrganizationDetail/>} ></Route>
                <Route path='/organizations/:id' element={<OrganizationDetail/>} ></Route>
                <Route path='/organizations/:orgId/jobs' element={<JobList/>} ></Route>
                <Route path='/organizations/:orgId/jobs/new' element={<NewJob/>} ></Route>
                <Route path='/organizations/:orgId/jobs/:jobId' element={<JobDetails/>} ></Route>
                <Route path='/organizations/:orgId/jobs/:jobId/applications' element={<JobApplicants/>} ></Route>
            </Route>
        </Routes>
    </>
)

export default AnyJobsRoutes