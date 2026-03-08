import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";

function Patients() {
  const [patients, setPatients] = useState([]);
  const [loading, setLoading] = useState(true);
  
  
  const [newFirstName, setNewFirstName] = useState("");
  const [newLastName, setNewLastName] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  const token = localStorage.getItem("token");
  const isAdmin = token ? jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "admin" : false;

  const fetchPatients = () => {
    setLoading(true);
    fetch("https://localhost:7173/api/patients", {
      headers: { "Authorization": `Bearer ${token}` }
    })
    .then(res => res.json())
    .then(data => {
      setPatients(Array.isArray(data) ? data : []);
      setLoading(false);
    })
    .catch(() => setLoading(false));
  };

  useEffect(() => {
    fetchPatients();
  }, [token]);

  
  const handleAddPatient = async (e) => {
    e.preventDefault();
    if (!newFirstName || !newLastName) return alert("Popunite oba polja!");

    setIsSubmitting(true);
    try {
      const response = await fetch("https://localhost:7173/api/patients/Create", {
        method: "POST",
        headers: { 
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}` 
        },
        body: JSON.stringify({
          firstName: newFirstName,
          lastName: newLastName
        })
      });

      if (response.ok) {
        alert("Pacijent uspešno dodat!");
        setNewFirstName("");
        setNewLastName("");
        fetchPatients(); 
      } else {
        alert("Greška pri dodavanju.");
      }
    } catch (err) {
      console.error(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  const deletePatient = (id) => {
    if (!window.confirm("Obrisati pacijenta?")) return;
    fetch(`https://localhost:7173/api/patients/${id}`, {
      method: "DELETE",
      headers: { "Authorization": `Bearer ${token}` }
    }).then(res => {
      if (res.ok) setPatients(patients.filter(p => (p.idPatient || p.id) !== id));
    });
  };

  return (
    <div style={{ padding: "40px", textAlign: "center", color: "white" }}>
      <h2 style={{ textShadow: "2px 2px 4px black" }}>Upravljanje Pacijentima</h2>

      {/* SEKCIJA ZA DODAVANJE - Samo za Admina */}
      {isAdmin && (
        <div style={{ background: "rgba(74, 222, 128, 0.1)", border: "1px solid #4ade80", padding: "20px", borderRadius: "15px", maxWidth: "500px", margin: "0 auto 30px auto" }}>
          <h3 style={{ marginTop: 0, color: "#4ade80" }}>Dodaj novog pacijenta</h3>
          <form onSubmit={handleAddPatient} style={{ display: "flex", gap: "10px", flexDirection: "column" }}>
            <input 
              style={inputStyle} 
              placeholder="Ime" 
              value={newFirstName}
              onChange={(e) => setNewFirstName(e.target.value)}
            />
            <input 
              style={inputStyle} 
              placeholder="Prezime" 
              value={newLastName}
              onChange={(e) => setNewLastName(e.target.value)}
            />
            <button 
              type="submit" 
              disabled={isSubmitting}
              style={{ ...buttonStyle, backgroundColor: "#4ade80" }}
            >
              {isSubmitting ? "Slanje..." : "SAČUVAJ PACIJENTA"}
            </button>
          </form>
        </div>
      )}

      <div style={{ background: "rgba(255, 255, 255, 0.15)", backdropFilter: "blur(5px)", padding: "20px", borderRadius: "15px", maxWidth: "500px", margin: "0 auto" }}>
        {loading ? (
          <p>Učitavanje...</p>
        ) : patients.length === 0 ? (
          <p>Nema pacijenata u bazi.</p>
        ) : (
          <ul style={{ listStyle: "none", padding: 0 }}>
            {patients.map(p => (
              <li key={p.idPatient || p.id} style={{ padding: "12px", borderBottom: "1px solid rgba(255,255,255,0.2)", display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                <span>{p.firstName || p.FirstName} {p.lastName || p.LastName}</span>
                {isAdmin && (
                  <button onClick={() => deletePatient(p.idPatient || p.id)} style={deleteBtnStyle}>Obriši</button>
                )}
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}


const inputStyle = {
  padding: "10px",
  borderRadius: "5px",
  border: "none",
  outline: "none"
};

const buttonStyle = {
  padding: "10px",
  border: "none",
  borderRadius: "5px",
  fontWeight: "bold",
  cursor: "pointer",
  color: "black"
};

const deleteBtnStyle = {
  color: "#ff4d4d",
  border: "1px solid #ff4d4d",
  padding: "4px 8px",
  borderRadius: "4px",
  background: "none",
  cursor: "pointer",
  fontSize: "0.8rem"
};

export default Patients;