using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace StudentRepository
{
    public class StudentManager : IStudentManager
    {
        private readonly AppDbContext _context;

        public StudentManager(AppDbContext context)
        {
            _context = context;
        }

        // ✅ CREATE (Stored Procedure)
        public async Task CreateStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");

            if (string.IsNullOrWhiteSpace(student.Name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(student.RollNumber))
                throw new ArgumentException("Roll Number is required");

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddStudent @Name, @RollNumber, @Course",
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@RollNumber", student.RollNumber),
                new SqlParameter("@Course", student.Course)
            );
        }

        // ✅ GET BY ID (Stored Procedure)
        public async Task<Student> GetStudentById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid student ID");

            var result = await _context.Students
                .FromSqlRaw("EXEC GetStudentById @Id",
                    new SqlParameter("@Id", id))
                .ToListAsync();

            var student = result.FirstOrDefault();

            if (student == null)
                throw new StudentNotFoundException($"Student with ID {id} not found");

            return student;
        }

        // ✅ GET ALL (Stored Procedure)
        public async Task<List<Student>> GetAllStudents()
        {
            var students = await _context.Students
                .FromSqlRaw("EXEC GetAllStudents")
                .ToListAsync();

            if (students.Count == 0)
                throw new StudentNotFoundException("No students found");

            return students;
        }

        // ✅ UPDATE (Stored Procedure)
        public async Task UpdateStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (string.IsNullOrWhiteSpace(student.Name))
                throw new ArgumentException("Name is required");

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateStudent @Id, @Name, @RollNumber, @Course",
                new SqlParameter("@Id", student.Id),
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@RollNumber", student.RollNumber),
                new SqlParameter("@Course", student.Course)
            );
        }

        // ✅ DELETE (Stored Procedure)
        public async Task DeleteStudent(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid student ID");

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteStudent @Id",
                new SqlParameter("@Id", id)
            );
        }
    }
}