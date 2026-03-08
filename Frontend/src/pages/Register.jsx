import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Register() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleRegister = async () => {
    try {
      const res = await fetch("https://localhost:7173/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ firstName, lastName, email, password })
      });

      const data = await res.json();

      if (res.ok && data.succeeded) {
        alert("Registracija uspešna! Sada se možete ulogovati.");
        navigate("/login");
      } else {
        const errorMsg = data.errors ? data.errors.join("\n") : "Neuspešna registracija";
        alert("Greška pri registraciji:\n" + errorMsg);
      }
    } catch (err) {
      alert("Došlo je do greške prilikom povezivanja sa serverom.");
    }
  };

  return (
    <div style={pageStyle}>
      <div style={cardStyle}>
        <h2 style={{ color: "#4ade80", marginBottom: "30px", fontSize: "2rem" }}>Registracija</h2>
        
        <div style={formStyle}>
          <input 
            style={inputStyle} 
            placeholder="Ime" 
            onChange={e => setFirstName(e.target.value)} 
          />
          <input 
            style={inputStyle} 
            placeholder="Prezime" 
            onChange={e => setLastName(e.target.value)} 
          />
          <input 
            style={inputStyle} 
            placeholder="Email" 
            onChange={e => setEmail(e.target.value)} 
          />
          <input 
            style={inputStyle} 
            type="password" 
            placeholder="Lozinka" 
            onChange={e => setPassword(e.target.value)} 
          />
          
          <button onClick={handleRegister} style={btnStyle}>
            REGISTRUJ SE
          </button>
        </div>

        <p 
          onClick={() => navigate("/login")} 
          style={{ cursor: 'pointer', color: '#4ade80', marginTop: '20px', fontSize: '0.9rem' }}
        >
          Već imate nalog? <b>Ulogujte se.</b>
        </p>
      </div>
    </div>
  );
}


const pageStyle = { 
  display: "flex", 
  justifyContent: "center", 
  alignItems: "center", 
  height: "100vh", 
  background: "#121212", 
  color: "white" 
};

const cardStyle = { 
  background: "rgba(255,255,255,0.05)", 
  padding: "50px", 
  borderRadius: "20px", 
  backdropFilter: "blur(12px)", 
  width: "400px", 
  textAlign: "center",
  boxShadow: "0 8px 32px 0 rgba(0, 0, 0, 0.8)",
  border: "1px solid rgba(255, 255, 255, 0.1)"
};

const formStyle = { 
  display: "flex", 
  flexDirection: "column", 
  gap: "15px" 
};

const inputStyle = { 
  padding: "16px", 
  borderRadius: "10px", 
  border: "1px solid #333", 
  background: "rgba(0,0,0,0.4)", 
  color: "white", 
  outline: "none",
  fontSize: "1.1rem" 
};

const btnStyle = { 
  padding: "16px", 
  borderRadius: "10px", 
  border: "none", 
  background: "#4ade80", 
  fontWeight: "bold", 
  cursor: "pointer", 
  color: "#000",
  fontSize: "1rem",
  marginTop: "15px",
  transition: "0.3s"
};

export default Register;