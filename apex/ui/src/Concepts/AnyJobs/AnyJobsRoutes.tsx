import { Routes, Route } from "react-router-dom";
import { ProfileDraftsList } from "./Components/Profile/ProfileDraftsList";
import { ProfilesList } from "./Components/Profile/ProfilesList";
import { UploadDocument } from "./Components/Profile/UploadDocument";
import Home from './Pages/Home';
import { Organizations } from "./Pages/Organizations";
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
                <Route path="/organizations" element={<Organizations />} />
            </Route>
        </Routes>
    </>
)

export default AnyJobsRoutes