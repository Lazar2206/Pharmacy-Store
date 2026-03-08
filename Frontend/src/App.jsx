import { Routes, Route, Navigate } from "react-router-dom";
import Home from "./pages/Home";
import Patients from "./pages/Patients";
import Medicines from "./pages/Medicines";
import Login from "./pages/Login";
import Register from "./pages/Register";
import { ProtectedRoute } from "./components/ProtectedRoute";
import PharmacyStores from "./pages/PharmacyStores";
import AdminBills from "./pages/AdminBills";
import AddMedicine from "./pages/AddMedicine";

function App() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      
      {/* Početna je dostupna svima koji su ulogovani */}
      <Route path="/" element={
        <ProtectedRoute>
          <Home />
        </ProtectedRoute>
      } />
      <Route path="/admin/add-medicine" element={
        <ProtectedRoute roleRequired="admin">
          <AddMedicine />
        </ProtectedRoute>
      } />

      {/* Medicines mogu svi ulogovani */}
      <Route path="/medicines" element={
        <ProtectedRoute>
          <Medicines />
        </ProtectedRoute>
      } />

      {/* Patients može samo Admin */}
      <Route path="/patients" element={
        <ProtectedRoute roleRequired="admin">
          <Patients />
        </ProtectedRoute>
      } />

      {/* PharmacyStores može samo Admin */}
      <Route path="/pharmacystores" element={
        <ProtectedRoute roleRequired="admin">
          <PharmacyStores />
        </ProtectedRoute>
      } />

      {/* NOVO: Admin Dashboard za račune - može samo Admin */}
      <Route path="/admin/bills" element={
        <ProtectedRoute roleRequired="admin">
          <AdminBills />
        </ProtectedRoute>
      } />

      {/* Redirect za nepostojeće rute */}
      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
}
export default App;