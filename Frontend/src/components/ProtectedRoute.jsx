import { Navigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode"; 

export const ProtectedRoute = ({ children, roleRequired }) => {
  const token = localStorage.getItem("token");

  if (!token) return <Navigate to="/login" />;

  try {
    const decoded = jwtDecode(token);
    const userRole = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (roleRequired && userRole !== roleRequired) {
      return <Navigate to="/" />; 
    }

    return children;
  } catch (error) {
    return <Navigate to="/login" />;
  }
};