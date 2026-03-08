import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const res = await fetch("https://localhost:7173/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
      });

      if (res.ok) {
        const data = await res.json();
        localStorage.setItem("token", data.token);
        navigate("/");
      } else if (res.status === 401) {
        alert("Greška: Pogrešan email ili lozinka!");
      } else {
        alert("Došlo je do greške na serveru.");
      }
    } catch (err) {
      alert("Nije moguće povezivanje sa serverom.");
    }
  };

  const containerStyle = {
    background: "rgba(0, 0, 0, 0.7)", 
    padding: "40px",
    borderRadius: "15px",
    display: "inline-block",
    marginTop: "80px",
    boxShadow: "0 8px 32px rgba(0,0,0,0.3)"
  };

  const inputStyle = {
    padding: "12px",
    width: "280px",
    borderRadius: "8px",
    border: "none",
    marginBottom: "15px",
    fontSize: "16px",
    backgroundColor: "white",
    color: "#333"
  };

  return (
    <div style={{ textAlign: "center", minHeight: "100vh", color: "white" }}>
      <div style={containerStyle}>
        <h2 style={{ marginBottom: "30px", fontSize: "28px" }}>Prijava na sistem</h2>
        
        <div>
          <input 
            placeholder="Email adresa" 
            value={email}
            onChange={e => setEmail(e.target.value)} 
            style={inputStyle}
          />
        </div>

       <div style={{ position: "relative", display: "flex", justifyContent: "center", width: "280px", margin: "0 auto" }}>
  <input 
    type={showPassword ? "text" : "password"} 
    placeholder="Lozinka" 
    value={password}
    onChange={e => setPassword(e.target.value)} 
    style={{ ...inputStyle, width: "100%", marginBottom: "15px" }} 
  />
  <button 
    type="button"
    onClick={() => setShowPassword(!showPassword)}
    style={{
      position: "absolute", 
      right: "10px", 
      top: "12px", 
      background: "none", 
      border: "none", 
      cursor: "pointer", 
      fontSize: "12px", 
      color: "#3498db",
      fontWeight: "bold"
    }}
  >
    {showPassword ? "HIDE" : "SHOW"}
  </button>
</div>

        <br />
        <button 
          onClick={handleLogin}
          style={{ 
            marginTop: "10px", padding: "12px 50px", 
            backgroundColor: "#27ae60", color: "white", 
            border: "none", borderRadius: "8px", 
            cursor: "pointer", fontWeight: "bold", fontSize: "16px" 
          }}
        >
          Prijavi se
        </button>

        <p style={{ marginTop: "20px", fontSize: "14px" }}>
          Nemaš nalog? 
          <span 
            onClick={() => navigate("/register")} 
            style={{ color: "#3498db", cursor: "pointer", marginLeft: "5px", textDecoration: "underline" }}
          >
            Registruj se
          </span>
        </p>
      </div>
    </div>
  );
}

export default Login;