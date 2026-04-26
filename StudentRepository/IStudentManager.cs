namespace StudentRepository
{
    public interface IStudentManager
    {
        Task CreateStudent(Student student);
        Task<Student> GetStudentById(int id);
        Task<List<Student>> GetAllStudents();
        Task UpdateStudent(Student student);
        Task DeleteStudent(int id);
    }
}
