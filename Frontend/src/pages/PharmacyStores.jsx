import { useEffect, useState } from "react";

function PharmacyStores() {
  const [stores, setStores] = useState([]); 
  const [searchName, setSearchName] = useState(""); 
  
  
  const [newName, setNewName] = useState("");
  const [newAddress, setNewAddress] = useState(""); 
  const [isSubmitting, setIsSubmitting] = useState(false);

  const token = localStorage.getItem("token");

  
  const fetchStores = () => {
    fetch("https://localhost:7173/api/pharmacystores", {
      headers: { "Authorization": `Bearer ${token}` }
    })
    .then(res => res.json())
    .then(data => setStores(Array.isArray(data) ? data : []))
    .catch(err => console.error("Greška pri učitavanju:", err));
  };

  useEffect(() => { fetchStores(); }, []);

  
  const handleSearchByName = async (name) => {
    setSearchName(name);
    
    if (name.trim() === "") {
      fetchStores(); 
      return;
    }

    try {
      
      const res = await fetch(`https://localhost:7173/api/pharmacystores/search/${name}`, {
        headers: { "Authorization": `Bearer ${token}` }
      });
      
      if (res.ok) {
        const data = await res.json();
        setStores(data); 
      }
    } catch (err) {
      console.error("Greška pri pretrazi:", err);
    }
  };

  // Dodavanje nove apoteke
  const handleCreate = async (e) => {
    if (e) e.preventDefault();
    if (!newName || !newAddress) return alert("Popunite i naziv i adresu!");

    setIsSubmitting(true);
    try {
      const res = await fetch("https://localhost:7173/api/pharmacystores/Create", {
        method: "POST",
        headers: { 
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}` 
        },
        body: JSON.stringify({ 
          name: newName,
          address: newAddress 
        })
      });

      if (res.ok) {
        alert("Uspešno dodata apoteka!");
        setNewName("");
        setNewAddress("");
        setSearchName(""); 
        fetchStores();
      }
    } catch (err) {
      console.error(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div style={pageContainerStyle}>
      <h2 style={{ textShadow: "2px 2px 4px black", marginBottom: "30px" }}>Pharmacy Store Control</h2>

      {/* Sekcija za dodavanje */}
      <div style={cardStyle}>
        <h3 style={{ color: "#4ade80", marginTop: 0 }}>Dodaj Novu Apoteku</h3>
        <div style={{ display: "flex", flexDirection: "column", gap: "10px" }}>
          <input 
            style={inputStyle} 
            value={newName} 
            onChange={(e) => setNewName(e.target.value)} 
            placeholder="Naziv nove apoteke" 
          />
          <input 
            style={inputStyle} 
            value={newAddress} 
            onChange={(e) => setNewAddress(e.target.value)} 
            placeholder="Adresa (Ulica, Grad, Zemlja)" 
          />
          <button 
            style={{ ...btnStyle, opacity: isSubmitting ? 0.7 : 1 }} 
            onClick={handleCreate}
            disabled={isSubmitting}
          >
            {isSubmitting ? "Slanje..." : "SAČUVAJ"}
          </button>
        </div>
      </div>

      {/* Sekcija za pretragu PO NAZIVU */}
      <div style={cardStyle}>
        <h3 style={{ color: "#4ade80", marginTop: 0 }}>Pretraži po nazivu</h3>
        <div style={{ position: "relative" }}>
          <input 
            style={inputStyle} 
            type="text" 
            value={searchName} 
            onChange={(e) => handleSearchByName(e.target.value)} 
            placeholder="Unesite naziv (npr. 'z' za Zemun)..." 
          />
        </div>
        {searchName && (
          <p style={{ fontSize: "0.8rem", marginTop: "10px", color: "#4ade80" }}>
            Pronađeno rezultata: {stores.length}
          </p>
        )}
      </div>

      {/* Lista apoteka */}
      <div style={cardStyle}>
        <h3 style={{ color: "#4ade80", marginTop: 0 }}>
          {searchName ? "Rezultati pretrage" : "Sve Apoteke"}
        </h3>
        <div style={{ maxHeight: "300px", overflowY: "auto" }}>
          {stores.length > 0 ? (
            <ul style={{ listStyle: "none", padding: 0 }}>
              {stores.map(s => (
                <li key={s.idPharmacy} style={listItemStyle}>
                  <div style={{ textAlign: "left" }}>
                    <strong>{s.name}</strong><br/>
                    <small style={{ color: "#ccc" }}>{s.address}</small>
                  </div>
                </li>
              ))}
            </ul>
          ) : (
            <p>Nema rezultata za "{searchName}"</p>
          )}
        </div>
      </div>
    </div>
  );
}


const pageContainerStyle = { padding: "40px", color: "white", textAlign: "center", minHeight: "100vh", background: "linear-gradient(rgba(0,0,0,0.7), rgba(0,0,0,0.7)), url('/img/pharmacy_background.jpg')", backgroundSize: "cover", backgroundAttachment: "fixed" };
const cardStyle = { background: "rgba(255,255,255,0.15)", backdropFilter: "blur(10px)", padding: "25px", borderRadius: "15px", marginBottom: "25px", maxWidth: "500px", margin: "20px auto", border: "1px solid rgba(255,255,255,0.1)" };
const inputStyle = { padding: "12px", borderRadius: "8px", border: "none", backgroundColor: "rgba(0,0,0,0.5)", color: "white", outline: "none", width: "100%", boxSizing: "border-box" };
const btnStyle = { padding: "12px 25px", borderRadius: "8px", border: "none", cursor: "pointer", backgroundColor: "#4ade80", color: "#1a1a1a", fontWeight: "bold" };
const listItemStyle = { display: "flex", justifyContent: "space-between", alignItems: "center", borderBottom: "1px solid rgba(255,255,255,0.1)", padding: "12px 5px" };

export default PharmacyStores;