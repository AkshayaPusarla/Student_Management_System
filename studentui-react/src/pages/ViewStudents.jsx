import { useEffect, useState } from "react";
import API from "../services/api";
import { useNavigate } from "react-router-dom";

export default function ViewStudents() {
  const [students, setStudents] = useState([]);
  const navigate = useNavigate();

  const loadStudents = async () => {
    const res = await API.get("/student");
    setStudents(res.data);
  };

  useEffect(() => {
    loadStudents();
  }, []);

  return (
    <div>
      <div className="navbar">
        <h3>📋 Students</h3>
        <button onClick={() => navigate("/dashboard")}>⬅ Back</button>
      </div>

      <div className="container">
        <div className="card">

          <button onClick={() => navigate("/add")}>
            ➕ Add Student
          </button>

          {students.map((s) => (
            <div key={s.id} className="student">
              <strong>{s.name}</strong> ({s.rollNumber}) - {s.course}

              <div className="actions">
                <button onClick={() => navigate(`/update/${s.id}`)}>
                  Edit
                </button>

                <button
                  className="delete-btn"
                  onClick={async () => {
                    await API.delete(`/student/${s.id}`);
                    loadStudents(); // 🔥 refresh after delete
                  }}
                >
                  Delete
                </button>
              </div>
            </div>
          ))}

        </div>
      </div>
    </div>
  );
}