import { Routes, Route } from "react-router-dom";
import { NewOrganization } from "./Components/Organization/NewOrganization";
import { OrganizationDetail } from "./Components/Organization/OrganizationDetail";
import { OrganizationList } from "./Components/Organization/OrganizationList";
import { DraftDetail } from "./Components/Profile/DraftDetail";
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
                <Route path="/profiles/drafts/" element={<ProfileDraftsList />} />
                <Route path="/profiles/drafts/new" element={<UploadDocument />} />
                <Route path="/profiles/drafts/*" element={<DraftDetail />} />
                <Route path="/organizations" element={<OrganizationList />} />
                <Route path="/organizations/new" element={<NewOrganization />} />
                <Route path="/organizations/*" element={<OrganizationDetail />} />
            </Route>
        </Routes>
    </>
)

export default AnyJobsRoutes