import { useState, useEffect } from "react";
import { MapContainer, TileLayer, Marker, Popup, useMap } from 'react-leaflet';
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';
import { jwtDecode } from "jwt-decode";


const markerIcon = new L.Icon({
    iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
    shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
});


function ChangeView({ center }) {
    const map = useMap();
    useEffect(() => {
        if (center) map.setView(center, 15);
    }, [center, map]);
    return null;
}

function Medicines() {
    const [pharmacies, setPharmacies] = useState([]);
    const [selectedPharmacyId, setSelectedPharmacyId] = useState("");
    const [mapCenter, setMapCenter] = useState([44.8125, 20.4612]);
    const [activeTags, setActiveTags] = useState([]);
    const [typedSearch, setTypedSearch] = useState(""); 
    const [medicines, setMedicines] = useState([]);
    const [activeBillId, setActiveBillId] = useState(null);
    const [currentPatient, setCurrentPatient] = useState(null);
    const [manualPatientId, setManualPatientId] = useState("");
    const [billItems, setBillItems] = useState([]);

    const token = localStorage.getItem("token");
    let isAdmin = false;

    if (token) {
        try {
            const decoded = jwtDecode(token);
            isAdmin = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "admin";
        } catch (e) { console.error("Token decode error:", e); }
    }

    const quickFilters = [
        { label: "👄 Grlo", value: "grlo" },
        { label: "🤕 Bol", value: "bol" },
        { label: "🌡️ Temp", value: "temperatura" },
        { label: "🦠 Infekcija", value: "infekcija" },
        { label: "🥛 Stomak", value: "stomak" },
        { label: "☕ Kašalj", value: "kasalj" }
    ];

    
    const closeBillSession = () => {
        setActiveBillId(null);
        setBillItems([]);
        setMedicines([]);
        setTypedSearch("");
        setActiveTags([]);
        setManualPatientId("");
        setSelectedPharmacyId("");
    };

    useEffect(() => {
        const fetchData = async () => {
            if (!token) return;
            try {
                const phRes = await fetch("https://localhost:7173/api/pharmacystores", {
                    headers: { "Authorization": `Bearer ${token}` }
                });
                if (phRes.ok) setPharmacies(await phRes.json());

                const meRes = await fetch("https://localhost:7173/api/patients/me", {
                    headers: { "Authorization": `Bearer ${token}` }
                });
                if (meRes.ok) setCurrentPatient(await meRes.json());
            } catch (err) { console.error("Fetch error:", err); }
        };
        fetchData();
    }, [token]);

    const handlePharmacySelect = (id) => {
        setSelectedPharmacyId(id);
        const pharmacy = pharmacies.find(p => p.idPharmacy.toString() === id.toString());
        if (pharmacy) {
            const lat = parseFloat(String(pharmacy.latitude).replace(',', '.'));
            const lng = parseFloat(String(pharmacy.longitude).replace(',', '.'));
            setMapCenter([lat, lng]);
        }
    };

    const fetchBillItems = async (billId) => {
        try {
            const res = await fetch(`https://localhost:7173/api/billitems/bill/${billId}`, {
                headers: { "Authorization": `Bearer ${token}` }
            });
            if (res.ok) setBillItems(await res.json());
        } catch (err) { console.error("Greška pri učitavanju stavki:", err); }
    };

    const toggleTag = (tagValue) => {
        let updatedTags = activeTags.includes(tagValue) 
            ? activeTags.filter(t => t !== tagValue) 
            : [...activeTags, tagValue];
        setActiveTags(updatedTags);
        handleSearchMedicines(updatedTags, typedSearch);
    };

    const handleSearchMedicines = async (tags = activeTags, text = typedSearch) => {
        const combinedQuery = [...tags, text].filter(x => x.trim() !== "").join(" ");
        if (!combinedQuery.trim()) { setMedicines([]); return; }
        try {
            const res = await fetch(`https://localhost:7173/api/medicines/search/${encodeURIComponent(combinedQuery)}`, {
                headers: { "Authorization": `Bearer ${token}` }
            });
            if (res.ok) {
                const data = await res.json();
                setMedicines(Array.isArray(data) ? data : [data]);
            }
        } catch (err) { setMedicines([]); }
    };

    const openNewBill = async () => {
        const patientIdToUse = isAdmin && manualPatientId ? parseInt(manualPatientId) : currentPatient?.idPatient;
        if (!patientIdToUse || !selectedPharmacyId) return alert("Izaberite apoteku i proverite pacijenta!");

        try {
            const res = await fetch("https://localhost:7173/api/bills/Create", {
                method: "POST",
                headers: { "Content-Type": "application/json", "Authorization": `Bearer ${token}` },
                body: JSON.stringify({
                    date: new Date().toISOString(),
                    totalPrice: 0,
                    idPharmacy: parseInt(selectedPharmacyId),
                    idPatient: patientIdToUse
                })
            });

            if (res.ok) {
                const data = await res.json();
                const newBillId = data.id || data.idBill;
                setActiveBillId(newBillId);
                setBillItems([]); 
                alert("Račun uspešno otvoren!");
            }
        } catch (err) { alert("Greška pri otvaranju računa."); }
    };

    const addToBill = async (medicine) => {
        if (!activeBillId) return alert("Otvorite račun prvo!");
        
        const itemData = {
            idBill: parseInt(activeBillId),
            idMedicine: medicine.idMedicine,
            price: medicine.price,
            description: medicine.name,
            quantity: 1
        };

        try {
            const res = await fetch("https://localhost:7173/api/billitems/Create", {
                method: "POST",
                headers: { 
                    "Content-Type": "application/json", 
                    "Authorization": `Bearer ${token}` 
                },
                body: JSON.stringify(itemData)
            });

            if (res.ok) {
                
                const newItem = {
                    description: medicine.name,
                    price: medicine.price,
                    quantity: 1
                };
                setBillItems(prev => [...prev, newItem]);
                alert(`Lek ${medicine.name} dodat.`);
            } else {
                const errorData = await res.json();
                alert("Greška: " + JSON.stringify(errorData.errors));
            }
        } catch (err) { console.error("Network error:", err); }
    };

    const calculateTotal = () => billItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);

    return (
        <div style={{ padding: "40px", textAlign: "center", color: "white", minHeight: "100vh", background: "#1a1a1a" }}>
            <h2 style={{ color: "#4ade80", marginBottom: "20px" }}>Apoteke i Izdavanje Lekova</h2>

            {/* MAPA */}
            <div style={{ height: "350px", marginBottom: "30px", borderRadius: "15px", overflow: "hidden", border: "2px solid #4ade80" }}>
                <MapContainer center={mapCenter} zoom={12} style={{ height: "100%", width: "100%" }}>
                    <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                    <ChangeView center={mapCenter} />
                    {pharmacies.map((p) => (
                        <Marker 
                            key={p.idPharmacy} 
                            position={[parseFloat(String(p.latitude).replace(',','.')), parseFloat(String(p.longitude).replace(',','.'))]} 
                            icon={markerIcon}
                            eventHandlers={{ click: () => handlePharmacySelect(p.idPharmacy.toString()) }}
                        >
                            <Popup>{p.name}</Popup>
                        </Marker>
                    ))}
                </MapContainer>
            </div>

            <div style={{ display: "flex", gap: "20px", justifyContent: "center", flexWrap: "wrap", alignItems: "flex-start" }}>
                
                {/* 1. UPRAVLJANJE RAČUNOM */}
                <div style={containerStyle}>
                    <h3 style={{ color: "#4ade80", marginBottom: "15px" }}>1. Upravljanje računom</h3>
                    {!activeBillId ? (
                        <>
                            <select 
                                style={{...inputStyle, marginBottom: "15px"}} 
                                value={selectedPharmacyId} 
                                onChange={(e) => handlePharmacySelect(e.target.value)}
                            >
                                <option value="">-- Izaberi apoteku --</option>
                                {pharmacies.map(p => <option key={p.idPharmacy} value={p.idPharmacy}>{p.name}</option>)}
                            </select>
                            <div style={profileBoxStyle}>
                                <span style={{ fontSize: "12px", color: "#4ade80" }}>Profil pacijenta:</span><br/>
                                <strong>{currentPatient ? `${currentPatient.firstName} ${currentPatient.lastName}` : "Profil pacijenta nije učitan"}</strong>
                            </div>
                            {isAdmin && <input style={{...inputStyle, marginTop: "10px"}} type="text" placeholder="Admin: ID pacijenta" value={manualPatientId} onChange={e => setManualPatientId(e.target.value)} />}
                            <button style={{...buttonStyle, marginTop: "15px"}} onClick={openNewBill}>Otvori račun</button>
                        </>
                    ) : (
                        <div style={activeBillStyle}>
                            <p style={{color: "#4ade80", fontWeight: "bold"}}>RAČUN # {activeBillId} JE AKTIVAN</p>
                            <button 
                                style={{...buttonStyle, background: "#ef4444", color: "white"}} 
                                onClick={closeBillSession}
                            >
                                Zatvori sesiju
                            </button>
                        </div>
                    )}
                </div>

                {/* 2. DODAVANJE LEKOVA */}
                <div style={containerStyle}>
                    <h3 style={{ color: "#4ade80", marginBottom: "15px" }}>2. Dodavanje lekova</h3>
                    <div style={{ display: "flex", gap: "6px", marginBottom: "15px", flexWrap: "wrap" }}>
                        {quickFilters.map(f => (
                            <button key={f.value} onClick={() => toggleTag(f.value)} style={{ ...filterBadgeStyle, background: activeTags.includes(f.value) ? "#4ade80" : "#1a1a1a", color: activeTags.includes(f.value) ? "#1a1a1a" : "#4ade80" }}>
                                {f.label}
                            </button>
                        ))}
                    </div>
                    <input style={inputStyle} type="text" placeholder="Ime leka..." value={typedSearch} onChange={e => { setTypedSearch(e.target.value); handleSearchMedicines(activeTags, e.target.value); }} />
                    
                    <div style={{ marginTop: "15px", maxHeight: "250px", overflowY: "auto" }}>
                        {medicines.map(m => (
                            <div key={m.idMedicine} style={itemStyle}>
                                <div><b>{m.name}</b><br/><small style={{color: "#4ade80"}}>{m.price} RSD</small></div>
                                <button onClick={() => addToBill(m)} style={{ ...miniButtonStyle, background: activeBillId ? "#4ade80" : "#555", cursor: activeBillId ? "pointer" : "not-allowed" }} disabled={!activeBillId}>Dodaj</button>
                            </div>
                        ))}
                    </div>
                </div>

                {/* 3. VAŠA KORPA */}
                <div style={{...containerStyle, borderColor: "#4ade80", borderStyle: "dashed"}}>
                    <h3 style={{ color: "#4ade80", marginBottom: "15px" }}>3. Vaša korpa</h3>
                    {billItems.length > 0 ? (
                        <>
                            <div style={{ maxHeight: "250px", overflowY: "auto", marginBottom: "15px" }}>
                                {billItems.map((item, idx) => (
                                    <div key={idx} style={{...itemStyle, borderBottom: "1px solid #333"}}>
                                        <div style={{fontSize: "13px"}}>
                                            <b>{item.description}</b> <br/>
                                            <span style={{color: "#888"}}>{item.quantity}x {item.price} RSD</span>
                                        </div>
                                        <span style={{fontWeight: "bold"}}>{item.quantity * item.price} RSD</span>
                                    </div>
                                ))}
                            </div>
                            <div style={{ borderTop: "2px solid #4ade80", paddingTop: "15px", textAlign: "right" }}>
                                <span style={{ fontSize: "14px", color: "#888" }}>UKUPNO ZA NAPLATU:</span>
                                <div style={{ fontSize: "24px", fontWeight: "bold", color: "#4ade80" }}>{calculateTotal()} RSD</div>
                                <button 
                                    style={{...buttonStyle, marginTop: "10px"}} 
                                    onClick={() => {
                                        alert("Račun uspešno plaćen i zatvoren!");
                                        closeBillSession();
                                    }}
                                >
                                    Finalizuj i Plati
                                </button>
                            </div>
                        </>
                    ) : (
                        <div style={{padding: "40px 0", color: "#666", textAlign: "center"}}>
                            <p>Korpa je prazna.</p>
                            <small>Otvorite račun i dodajte lekove.</small>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}


const containerStyle = { background: "#262626", padding: "20px", borderRadius: "15px", border: "1px solid #444", width: "350px", textAlign: "left", minHeight: "450px" };
const inputStyle = { padding: "12px", borderRadius: "8px", border: "1px solid #444", width: "100%", background: "#1a1a1a", color: "white", outline: "none" };
const buttonStyle = { padding: "12px", background: "#4ade80", border: "none", borderRadius: "8px", fontWeight: "bold", cursor: "pointer", width: "100%", color: "#1a1a1a" };
const miniButtonStyle = { padding: "6px 12px", border: "none", borderRadius: "6px", color: "#1a1a1a", fontWeight: "bold" };
const itemStyle = { display: "flex", justifyContent: "space-between", alignItems: "center", padding: "10px 0" };
const profileBoxStyle = { background: "#333", padding: "15px", borderRadius: "10px", border: "1px solid #444", textAlign: "center", marginTop: "10px" };
const activeBillStyle = { padding: "20px", border: "2px dashed #4ade80", borderRadius: "10px", textAlign: "center" };
const filterBadgeStyle = { padding: "6px 10px", background: "#1a1a1a", border: "1px solid #4ade80", borderRadius: "20px", color: "#4ade80", fontSize: "11px", cursor: "pointer", fontWeight: "bold" };

export default Medicines;