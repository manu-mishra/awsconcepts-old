import { Routes, Route, Outlet } from "react-router-dom";
import CommandBar from "./Components/CommandBar";
import Home from './Pages/Home';
import { Organizations } from "./Pages/Organizations";
import { Profiles } from "./Pages/Profiles";

const AnyJobsRoutes = () => (
    <>
        <CommandBar />
        <Routes>
            <Route path="/" element={<Home></Home>} />
            <Route path="/home" element={<Home/>} />
            <Route path="/profiles" element={<Profiles/>} />
            <Route path="/organizations" element={<Organizations/>} />
        </Routes>

        <Outlet />
    </>
)

export default AnyJobsRoutes