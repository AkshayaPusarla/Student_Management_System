namespace StudentRepository
{
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException(string message) : base(message)
        {
        }
    }
}