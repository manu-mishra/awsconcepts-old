import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home } from "../../pages/Home";
import { Layout } from "./Layout";
import React, { Suspense } from "react";
import PricEstimate from "../pricing/PricEstimate";
import { RaiseError } from "../../pages/RaiseError";

const RequireAuth = React.lazy(()=> import('../authentication/RequireAuth'));
const Login = React.lazy(()=> import('../authentication/Login'));
const AnyJobsRoutes = React.lazy(()=> import('../../Concepts/AnyJobs/AnyJobsRoutes'));


export function ApplicationRoutes() {
    return (
      <Suspense fallback={<div>Loading...</div>}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route path='/costestimate' element={<PricEstimate />} />
            <Route path='/raiseerror' element={<RaiseError />} />          
            <Route path='/anyjobs/*' element={<RequireAuth><AnyJobsRoutes/></RequireAuth>} ></Route>
            <Route path="/login" element={<Login />} />
          </Route>
        </Routes>
      </BrowserRouter>
      </Suspense>
    );
  }