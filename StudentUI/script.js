const baseURL = "https://localhost:44308/api/student";

async function getAllStudents() {
    try {
        const res = await axios.get(baseURL);
        return res.data || [];
    } catch (error) {
        console.error(error);
        alert("❌ Error fetching students");
        return [];
    }
}

async function getStudentById(id) {
    try {
        const res = await axios.get(`${baseURL}/${id}`);
        return res.data;
    } catch (error) {
        alert("❌ Student not found");
        return null;
    }
}

async function addStudent(student) {
    try {
        console.log("Sending student:", student);

        const res = await axios.post(baseURL, student);

        console.log("Response:", res);

        alert("✅ Student added successfully!");
        return true;

    } catch (error) {
        console.error(error);

        if (error.response) {
            alert("❌ " + error.response.data);
        } else {
            alert("❌ Server error");
        }

        return false;
    }
}

async function updateStudent(id, student) {
    try {
        await axios.put(`${baseURL}/${id}`, student);
        alert("✏️ Student updated");

        window.location.href = "viewStudents.html";

    } catch (error) {
        alert("❌ Error updating student");
    }
}

async function deleteStudentById(id) {
    try {
        await axios.delete(`${baseURL}/${id}`);
        alert("🗑️ Student deleted");
        return true;
    } catch (error) {
        alert("❌ Error deleting student");
        return false;
    }
}

function goToEdit(id) {
    window.location.href = `updateStudent.html?id=${id}`;
}

function goToDelete(id) {
    window.location.href = `deleteStudent.html?id=${id}`;
}

async function renderStudentList(containerId) {
    const list = document.getElementById(containerId);
    list.innerHTML = "<p>Loading...</p>";

    try {
        const students = await getAllStudents();

        list.innerHTML = "";

        if (!students || students.length === 0) {
            list.innerHTML = "<p>No students found</p>";
            return;
        }

        // 🔥 SORT BY ID
        students.sort((a, b) => a.id - b.id);

        students.forEach(s => {
            list.innerHTML += `
                <div class="student">
                    <div><strong>ID:</strong> ${s.id}</div>
                    <div><strong>Name:</strong> ${s.name}</div>
                    <div><strong>Roll No:</strong> ${s.rollNumber}</div>
                    <div><strong>Course:</strong> ${s.course}</div>

                    <div class="actions">
                        <button onclick="goToEdit(${s.id})">✏️ Edit</button>
                        <button class="delete-btn" onclick="goToDelete(${s.id})">🗑️ Delete</button>
                    </div>
                </div>
            `;
        });

    } catch (error) {
        console.error(error);
        list.innerHTML = "<p style='color:red;'>Error loading students</p>";
    }
}