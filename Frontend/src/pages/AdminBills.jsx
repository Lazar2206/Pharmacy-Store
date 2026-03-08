import { useState, useEffect } from "react";

function AdminBills() {
  const [bills, setBills] = useState([]);
  const [loading, setLoading] = useState(true);
  const token = localStorage.getItem("token");

  
  const fetchBills = async () => {
    try {
      const res = await fetch("https://localhost:7173/api/bills", {
        headers: { "Authorization": `Bearer ${token}` }
      });
      if (res.ok) {
        const data = await res.json();
        setBills(data);
      }
    } catch (err) {
      console.error("Greška pri dohvatanju računa:", err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchBills();
  }, []);

  
  const handleStatusChange = async (id, newStatus) => {
    try {
      const res = await fetch("https://localhost:7173/api/bills/UpdateStatus", {
        method: "PATCH", 
        headers: { 
          "Content-Type": "application/json", 
          "Authorization": `Bearer ${token}` 
        },
        body: JSON.stringify({
          IdBill: id,
          NewStatus: newStatus
        })
      });

      if (res.ok) {
        
        setBills(prev => prev.map(b => b.idBill === id ? { ...b, status: newStatus } : b));
        alert(`Račun #${id} uspešno prebačen u status: ${newStatus}`);
      } else {
        alert("Greška pri promeni statusa na serveru.");
      }
    } catch (err) {
      console.error(err);
    }
  };

  if (loading) return <p style={{ textAlign: "center", color: "white", marginTop: "50px" }}>Učitavanje računa...</p>;

  return (
    <div style={pageWrapperStyle}>
      <h2 style={headerStyle}>Admin Panel - Upravljanje Računima</h2>
      
      <div style={tableContainerStyle}>
        <table style={tableStyle}>
          <thead>
            <tr>
              <th style={thStyle}>ID Računa</th>
              <th style={thStyle}>Datum</th>
              <th style={thStyle}>Ukupna Cena</th>
              <th style={thStyle}>Status</th>
              <th style={thStyle}>Akcije</th>
            </tr>
          </thead>
          <tbody>
            {bills.map((b, index) => (
              <tr key={b.idBill} style={{ 
                ...trStyle, 
                backgroundColor: index % 2 === 0 ? "#2d2d2d" : "#363636" 
              }}>
                <td style={tdStyle}>#{b.idBill}</td>
                <td style={tdStyle}>{new Date(b.date).toLocaleDateString()}</td>
                <td style={tdStyle}>{b.totalPrice} RSD</td>
                <td style={{ ...tdStyle, fontWeight: "bold", color: getStatusColor(b.status) }}>
                  {b.status || "Obrada"}
                </td>
                <td style={tdStyle}>
                  <button 
                    style={actionBtnStyle} 
                    onClick={() => handleStatusChange(b.idBill, "Odobren")}
                    disabled={b.status === "Odobren"}
                  >
                    Odobri
                  </button>
                  <button 
                    style={{ ...actionBtnStyle, background: "#ef4444" }} 
                    onClick={() => handleStatusChange(b.idBill, "Storniran")}
                    disabled={b.status === "Storniran"}
                  >
                    Storniraj
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}


const getStatusColor = (status) => {
  if (status === "Odobren") return "#4ade80"; 
  if (status === "Storniran") return "#ef4444"; 
  return "#facc15"; 
};


const pageWrapperStyle = {
  padding: "40px",
  minHeight: "100vh"
};

const headerStyle = {
  color: "#4ade80",
  textAlign: "center",
  marginBottom: "30px",
  textTransform: "uppercase",
  letterSpacing: "1px"
};

const tableContainerStyle = {
  borderRadius: "12px",
  overflow: "hidden",
  boxShadow: "0 8px 32px rgba(0,0,0,0.5)"
};

const tableStyle = {
  width: "100%",
  borderCollapse: "collapse",
  background: "#2d2d2d", 
  color: "white"
};

const thStyle = {
  padding: "15px",
  background: "#4ade80",
  color: "#1a1a1a",
  fontWeight: "bold",
  textAlign: "center",
  borderBottom: "2px solid #222"
};

const tdStyle = {
  padding: "12px",
  textAlign: "center",
  borderBottom: "1px solid #444"
};

const trStyle = {
  transition: "0.2s ease"
};

const actionBtnStyle = {
  margin: "0 5px",
  padding: "7px 14px",
  border: "none",
  borderRadius: "5px",
  background: "#4ade80",
  color: "#1a1a1a",
  cursor: "pointer",
  fontWeight: "bold",
  transition: "0.2s"
};

export default AdminBills;