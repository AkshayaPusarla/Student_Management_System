import { BrowserRouter, Routes, Route } from "react-router-dom";

import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import ViewStudents from "./pages/ViewStudents";
import AddStudent from "./pages/AddStudent";
import UpdateStudent from "./pages/UpdateStudent";

import ProtectedRoute from "./pages/ProtectedRoute";
import Register from "./pages/Register";

function App() {
  return (
    <BrowserRouter>
      <Routes>

        {/* 🔓 PUBLIC ROUTE */}
        <Route path="/" element={<Login />} />

        {/* 🔐 PROTECTED ROUTES */}
        <Route
          path="/dashboard"
          element={
            <ProtectedRoute>
              <Dashboard />
            </ProtectedRoute>
          }
        />

        <Route
          path="/students"
          element={
            <ProtectedRoute>
              <ViewStudents />
            </ProtectedRoute>
          }
        />

        <Route
          path="/add"
          element={
            <ProtectedRoute>
              <AddStudent />
            </ProtectedRoute>
          }
        />

        <Route
          path="/update/:id"
          element={
            <ProtectedRoute>
              <UpdateStudent />
            </ProtectedRoute>
          }
        />

        <Route path="/register" element={<Register />} />

      </Routes>
    </BrowserRouter>
  );
}

export default App;