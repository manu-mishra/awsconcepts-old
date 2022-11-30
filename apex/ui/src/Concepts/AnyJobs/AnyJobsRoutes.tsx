import { Routes, Route } from "react-router-dom";
import { NewProfileDrafts } from "./Components/Profile/NewProfileDrafts";
import { ProfileDraftsList } from "./Components/Profile/ProfileDraftsList";
import { ProfilesList } from "./Components/Profile/ProfilesList";
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
                <Route path="/profiles/drafts/new" element={<NewProfileDrafts />} />
                <Route path="/organizations" element={<Organizations />} />
            </Route>
        </Routes>
    </>
)

export default AnyJobsRoutes