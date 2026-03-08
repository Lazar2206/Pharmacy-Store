import { useState } from "react";
import { useNavigate } from "react-router-dom";

function AddMedicine() {
  const [name, setName] = useState("");
  const [price, setPrice] = useState("");
  const [activeTags, setActiveTags] = useState([]); 
  const navigate = useNavigate();
  const token = localStorage.getItem("token");

  
  const availableTags = [
    { label: "Grlo", value: "grlo" },
    { label: "Bol", value: "bol" },
    { label: "Temp", value: "temperatura" },
    { label: "Infekcija", value: "infekcija" },
    { label: "Stomak", value: "stomak" },
    { label: "Kašalj", value: "kasalj" }
  ];

  
  const toggleTag = (tagValue) => {
    if (activeTags.includes(tagValue)) {
      setActiveTags(activeTags.filter(t => t !== tagValue));
    } else {
      setActiveTags([...activeTags, tagValue]);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    
    const tagsString = activeTags.join(", ");

    const command = {
      name: name,
      price: parseFloat(price),
      tags: tagsString
    };

    try {
      const res = await fetch("https://localhost:7173/api/medicines/Create", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify(command)
      });

      if (res.ok) {
        alert(`Uspešno dodat lek: ${name}`);
        
        
        setName("");
        setPrice("");
        setActiveTags([]);
        
      } else {
        const errData = await res.json();
        alert("Greška pri čuvanju: " + (errData.message || "Samo admin ima pristup."));
      }
    } catch (err) {
      console.error("Greška:", err);
      alert("Serverska greška. Proverite da li je backend uključen.");
    }
  };

  return (
    <div style={pageStyle}>
      <div style={cardStyle}>
        <h2 style={{ color: "#4ade80", marginBottom: "10px" }}>💊 Dodaj Novi Lek</h2>
        <p style={{ color: "#888", fontSize: "0.9rem", marginBottom: "25px" }}>
          Unesite podatke i kliknite Sačuvaj za unos u bazu.
        </p>

        <form onSubmit={handleSubmit} style={formStyle}>
          {/* Naziv leka */}
          <input 
            style={inputStyle} 
            placeholder="Naziv leka (npr. Brufen)" 
            value={name} 
            onChange={(e) => setName(e.target.value)} 
            required 
          />

          {/* Cena */}
          <input 
            style={inputStyle} 
            type="number" 
            step="0.01" 
            placeholder="Cena (RSD)" 
            value={price} 
            onChange={(e) => setPrice(e.target.value)} 
            required 
          />

          {/* Selektor tagova */}
          <div style={{ textAlign: "left", marginTop: "10px" }}>
            <label style={{ fontSize: "14px", color: "#4ade80", display: "block", marginBottom: "10px" }}>
              Kategorije (Tagovi):
            </label>
            <div style={{ display: "flex", gap: "8px", flexWrap: "wrap" }}>
              {availableTags.map(f => (
                <button 
                  key={f.value} 
                  type="button" 
                  onClick={() => toggleTag(f.value)} 
                  style={{ 
                    ...filterBadgeStyle, 
                    background: activeTags.includes(f.value) ? "#4ade80" : "rgba(0,0,0,0.3)", 
                    color: activeTags.includes(f.value) ? "#1a1a1a" : "#4ade80",
                    border: activeTags.includes(f.value) ? "1px solid #4ade80" : "1px solid #444",
                    boxShadow: activeTags.includes(f.value) ? "0 0 10px rgba(74, 222, 128, 0.4)" : "none"
                  }}
                >
                  {f.label}
                </button>
              ))}
            </div>
          </div>

          {/* Dugmići za akciju */}
          <div style={{ display: "flex", gap: "10px", marginTop: "25px" }}>
            <button type="submit" style={btnStyle}>SAČUVAJ</button>
            <button 
              type="button" 
              style={{...btnStyle, background: "transparent", border: "1px solid #ef4444", color: "#ef4444"}} 
              onClick={() => navigate("/")}
            >
              ZATVORI
            </button>
          </div>
        </form>
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
  padding: "40px", 
  borderRadius: "20px", 
  backdropFilter: "blur(12px)", 
  width: "450px", 
  textAlign: "center",
  boxShadow: "0 8px 32px 0 rgba(0, 0, 0, 0.8)",
  border: "1px solid rgba(255, 255, 255, 0.1)"
};

const formStyle = { 
  display: "flex", 
  flexDirection: "column", 
  gap: "18px" 
};

const inputStyle = { 
  padding: "14px", 
  borderRadius: "10px", 
  border: "1px solid #333", 
  background: "rgba(0,0,0,0.4)", 
  color: "white", 
  outline: "none",
  fontSize: "1rem"
};

const btnStyle = { 
  padding: "14px", 
  borderRadius: "10px", 
  border: "none", 
  background: "#4ade80", 
  fontWeight: "bold", 
  cursor: "pointer", 
  flex: 1,
  color: "#000",
  fontSize: "1rem",
  transition: "0.2s"
};

const filterBadgeStyle = { 
  padding: "8px 14px", 
  borderRadius: "20px", 
  fontSize: "12px", 
  cursor: "pointer", 
  fontWeight: "bold", 
  transition: "0.3s all ease" 
};

export default AddMedicine;