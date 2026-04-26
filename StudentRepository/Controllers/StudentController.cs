using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StudentRepository.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentManager manager;

        public StudentController(IStudentManager studentManager)
        {
            manager = studentManager;
        }

        // 🔹 GET → Admin + User
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await manager.GetAllStudents();
            return Ok(students);
        }

        // 🔹 GET BY ID → Admin + User
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await manager.GetStudentById(id);
            return Ok(student);
        }

        // 🔴 CREATE → Admin ONLY
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            await manager.CreateStudent(student);
            return Ok("Student Added Successfully");
        }

        // 🔴 UPDATE → Admin ONLY
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            student.Id = id;
            await manager.UpdateStudent(student);
            return Ok("Student Updated Successfully");
        }

        // 🔴 DELETE → Admin ONLY
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await manager.DeleteStudent(id);
            return Ok("Student Deleted Successfully");
        }
    }
}