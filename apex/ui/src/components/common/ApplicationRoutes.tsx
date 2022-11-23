import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home } from "../../pages/Home";
import { Protected } from "../../pages/Protected";
import { ProtectedSecond } from "../../pages/ProtectSecond";
import { Login } from "../authentication/Login";
import { RequireAuth } from "../authentication/RequireAuth";
import { Layout } from "./Layout";

export function ApplicationRoutes() {
    return (
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route
              path="/protected"
              element={
                <RequireAuth>
                  <Protected />
                </RequireAuth>
              }
            />
            <Route
              path="/protected2"
              element={
                <RequireAuth>
                  <ProtectedSecond />
                </RequireAuth>
              }
            />
            <Route path="/login" element={<Login />} />
          </Route>
        </Routes>
      </BrowserRouter>
    );
  }