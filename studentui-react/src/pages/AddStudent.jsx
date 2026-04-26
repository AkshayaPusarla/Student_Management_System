import { useState } from "react";
import API from "../services/api";
import { useNavigate } from "react-router-dom";

export default function AddStudent() {
  const navigate = useNavigate();

  const [student, setStudent] = useState({
    name: "",
    rollNumber: "",
    course: ""
  });

  // 🔹 ADD FUNCTION
  const add = async () => {
    if (!student.name || !student.rollNumber || !student.course) {
      alert("All fields are required ⚠️");
      return;
    }

    try {
      await API.post("/student", student);

      alert("Student added successfully ✅");

      // 🔥 Redirect to list
      navigate("/students");

    } catch {
      alert("Error adding student ❌");
    }
  };

  return (
    <div>
      {/* 🔝 NAVBAR */}
      <div className="navbar">
        <h3>➕ Add Student</h3>

        <button
          className="back-btn"
          onClick={() => navigate("/students")}
        >
          ⬅ Back
        </button>
      </div>

      {/* 📦 FORM */}
      <div className="container">
        <div className="card">

          <input
            placeholder="Enter Name"
            value={student.name}
            onChange={(e) =>
              setStudent({ ...student, name: e.target.value })
            }
          />

          <input
            placeholder="Enter Roll Number"
            value={student.rollNumber}
            onChange={(e) =>
              setStudent({ ...student, rollNumber: e.target.value })
            }
          />

          <input
            placeholder="Enter Course"
            value={student.course}
            onChange={(e) =>
              setStudent({ ...student, course: e.target.value })
            }
          />

          <button onClick={add}>Add Student</button>

        </div>
      </div>
    </div>
  );
}