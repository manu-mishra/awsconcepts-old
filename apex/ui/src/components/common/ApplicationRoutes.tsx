import { BrowserRouter, Routes, Route } from "react-router-dom";
import AnyJobsRoutes from "../../Concepts/AnyJobs/AnyJobsRoutes";
import { Home } from "../../pages/Home";
import { Login } from "../authentication/Login";
import { RequireAuth } from "../authentication/RequireAuth";
import { Layout } from "./Layout";

export function ApplicationRoutes() {
    return (
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route path='/anyjobs/*' element={<RequireAuth><AnyJobsRoutes/></RequireAuth>} ></Route>
            <Route path="/login" element={<Login />} />
          </Route>
        </Routes>
      </BrowserRouter>
    );
  }