import { useNavigate } from "react-router-dom";

export default function Dashboard() {
  const navigate = useNavigate();

  const logout = () => {
    localStorage.removeItem("token"); // 🔐 remove token
    alert("Logged out successfully 👋");
    navigate("/"); // 🔄 go to login
  };

  return (
    <div>
  <div className="navbar">
    <h3>🎓 Student System</h3>
    <button onClick={logout}>Logout</button>
  </div>

  <div className="container">
    <div className="card">
      <h2>Dashboard</h2>

      <button onClick={() => navigate("/students")}>
        View Students
      </button>

      <button onClick={() => navigate("/add")}>
        Add Student
      </button>
    </div>
  </div>
</div>
  );
}