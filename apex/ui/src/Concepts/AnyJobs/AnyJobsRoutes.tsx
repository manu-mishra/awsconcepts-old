import React from "react";
import { Routes, Route } from "react-router-dom";

const SearchJobs = React.lazy(()=> import('./Components/JobSearch/SearchJobs'));
const SearchPraphraseJobs = React.lazy(()=> import('./Components/JobSearch/SearchPraphraseJobs'));
const JobApplicants = React.lazy(()=> import('./Components/Organization/JobApplicants'));
const JobDetails = React.lazy(()=> import('./Components/Organization/JobDetails'));
const JobList = React.lazy(()=> import('./Components/Organization/JobList'));
const NewJob = React.lazy(()=> import('./Components/Organization/NewJob'));
const NewOrganization = React.lazy(()=> import('./Components/Organization/NewOrganization'));
const OrganizationDetail = React.lazy(()=> import('./Components/Organization/OrganizationDetail'));
const OrganizationList = React.lazy(()=> import('./Components/Organization/OrganizationList'));
const DraftDetail = React.lazy(()=> import('./Components/Profile/DraftDetail'));
const ProfileDocumentsDetail = React.lazy(()=> import('./Components/Profile/ProfileDocumentsDetail'));
const ProfileDocumentsList = React.lazy(()=> import('./Components/Profile/ProfileDocumentsList'));
const ProfileDraftsList = React.lazy(()=> import('./Components/Profile/ProfileDraftsList'));
const ProfilesList = React.lazy(()=> import('./Components/Profile/ProfilesList'));
const UploadDocument = React.lazy(()=> import('./Components/Profile/UploadDocument'));
const Home = React.lazy(()=> import('./Pages/Home'));
const RouterLayout = React.lazy(()=> import('./RouterLayout'));

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
                <Route path="/jobs/search" element={<SearchJobs />} />
                <Route path="/jobs/search/:searchText" element={<SearchJobs />} />
                <Route path="/jobs/searchparaphrase" element={<SearchPraphraseJobs />} />
                <Route path="/jobs/searchparaphrase/:searchText" element={<SearchPraphraseJobs />} />
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