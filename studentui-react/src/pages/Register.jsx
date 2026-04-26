import { useState } from "react";
import API from "../services/api";
import { useNavigate } from "react-router-dom";

export default function Register() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const register = async () => {
    if (!userName || !password) {
      alert("All fields required ⚠️");
      return;
    }

    try {
      await API.post("/auth/register", {
        userName,
        password
      });

      alert("Registered successfully ✅");
      navigate("/"); // go to login
    } catch (err) {
      alert(err.response?.data || "Registration failed ❌");
    }
  };

  return (
    <div className="container">
  <div className="card">
      <h2>📝 Register</h2>

      <input
        placeholder="Username"
        value={userName}
        onChange={(e) => setUserName(e.target.value)}
      />

      <br /><br />

      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />

      <br /><br />

      <button onClick={register}>Register</button>
    </div>
    </div>
  );
}