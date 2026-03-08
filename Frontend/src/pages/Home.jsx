import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

function Home() {
  const navigate = useNavigate();
  const token = localStorage.getItem("token");
  
  let isAdmin = false;
  let displayFullName = "Korisnik";

  if (token) {
    try {
      const decoded = jwtDecode(token);
      isAdmin = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "admin";
      
      const fName = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"] || "";
      const lName = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"] || "";
      
      displayFullName = `${fName} ${lName}`.trim() || "Korisnik";
    } catch (err) {
      console.error("Greška pri dekodovanju:", err);
    }
  }

  const handleLogout = () => {
    localStorage.clear();
    navigate("/login");
  };

  return (
    <div style={pageStyle}>
      <h1 style={{ color: "#4ade80", fontSize: "3rem", textShadow: "2px 2px 10px rgba(74, 222, 128, 0.3)" }}>
        Pharmacy System
      </h1>
      <p style={{ fontSize: "1.5rem", marginBottom: "40px" }}>
        Dobrodošli, <span style={{ color: "#4ade80", fontWeight: "bold" }}>{displayFullName}</span>
      </p>

      <div style={menuContainerStyle}>
        <button style={btnStyle} onClick={() => navigate("/medicines")}>
          🛒 Izdavanje Lekova
        </button>
        
        {isAdmin && (
          <>
            <button style={btnStyle} onClick={() => navigate("/patients")}>
              👥 Pacijenti
            </button>
            <button style={btnStyle} onClick={() => navigate("/pharmacystores")}>
              🏥 Apoteke
            </button>
            <button 
              style={{ ...btnStyle, background: "#facc15", color: "#000" }} 
              onClick={() => navigate("/admin/bills")}
            >
              📑 Upravljanje Računima
            </button>
            <button 
            style={{ ...btnStyle, background: "#3b82f6", color: "#fff" }} 
            onClick={() => navigate("/admin/add-medicine")}
            >
           💊 Dodaj Novi Lek
              </button>
          </>
        )}

        <button style={{ ...btnStyle, background: "#ef4444" }} onClick={handleLogout}>
          🚪 Odjava
        </button>
      </div>
    </div>
  );
}

const pageStyle = { 
    display: "flex", 
    flexDirection: "column", 
    alignItems: "center", 
    justifyContent: "center", 
    height: "100vh", 
    color: "white",
    background: "#121212"
};

const menuContainerStyle = { display: "flex", justifyContent: "center", gap: "20px", flexWrap: "wrap", maxWidth: "800px" };
const btnStyle = { padding: "15px 30px", cursor: "pointer", borderRadius: "12px", border: "none", background: "#4ade80", fontWeight: "bold", color: "black", fontSize: "1rem", transition: "0.3s" };

export default Home;