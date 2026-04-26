import { useState } from "react";
import API from "../services/api";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const login = async () => {
    try {
      const res = await API.post("/auth/login", {
        userName,
        password
      });

      localStorage.setItem("token", res.data.token);

      alert("Login Successful ✅");

      navigate("/dashboard");
    } catch (err) {
      alert(err.response?.data || "Invalid credentials ❌");
    }
  };

  return (
    <div className="container">
  <div className="card">
      <h2>🔐 Login</h2>

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

      <button onClick={login}>Login</button>

      <br /><br />

      {/* 🔥 REGISTER LINK */}
      <p>
        Don’t have an account?{" "}
        <span
          style={{ color: "blue", cursor: "pointer" }}
          onClick={() => navigate("/register")}
        >
          Register here
        </span>
      </p>
      </div>
    </div>
  );
}