import { useEffect, useState } from "react";
import API from "../services/api";
import { useNavigate, useParams } from "react-router-dom";

export default function UpdateStudent() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [student, setStudent] = useState({
    name: "",
    rollNumber: "",
    course: ""
  });

  // 🔹 LOAD STUDENT DATA
  useEffect(() => {
    loadStudent();
  }, []);

  const loadStudent = async () => {
    try {
      const res = await API.get(`/student/${id}`);
      setStudent(res.data);
    } catch {
      alert("Error loading student ❌");
    }
  };

  // 🔹 UPDATE
  const updateStudent = async () => {
    try {
      await API.put(`/student/${id}`, {
        id: parseInt(id),
        ...student
      });

      alert("Updated successfully ✅");

      // 🔥 REDIRECT TO VIEW PAGE
      navigate("/students");

    } catch {
      alert("Update failed ❌");
    }
  };

  return (
    <div>
      <div className="navbar">
        <h3>✏️ Update Student</h3>
        <button className="back-btn" onClick={() => navigate("/students")}>
          ⬅ Back
        </button>
      </div>

      <div className="container">
        <div className="card">
          <input
            placeholder="Name"
            value={student.name}
            onChange={(e) =>
              setStudent({ ...student, name: e.target.value })
            }
          />

          <input
            placeholder="Roll Number"
            value={student.rollNumber}
            onChange={(e) =>
              setStudent({ ...student, rollNumber: e.target.value })
            }
          />

          <input
            placeholder="Course"
            value={student.course}
            onChange={(e) =>
              setStudent({ ...student, course: e.target.value })
            }
          />

          <button onClick={updateStudent}>Update</button>
        </div>
      </div>
    </div>
  );
}