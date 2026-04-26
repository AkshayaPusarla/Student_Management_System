using Xunit;
using Microsoft.EntityFrameworkCore;
using StudentRepository;
using System.Threading.Tasks;
using System.Linq;
using System;

public class StudentManagerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateStudent_ShouldAddStudent()
    {
        // Arrange
        var context = GetDbContext();
        var manager = new StudentManager(context);

        var student = new Student
        {
            Name = "Test",
            RollNumber = "101",
            Course = "CSE"
        };

        // Act
        await manager.CreateStudent(student);

        // Assert
        var students = await context.Students.ToListAsync();
        Assert.Single(students);
    }

    [Fact]
    public async Task GetStudentById_ShouldReturnStudent()
    {
        // Arrange
        var context = GetDbContext();

        var student = new Student
        {
            Name = "Test",
            RollNumber = "101",
            Course = "CSE"
        };

        context.Students.Add(student);
        await context.SaveChangesAsync();

        var manager = new StudentManager(context);

        // Act
        var result = await manager.GetStudentById(student.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task GetStudentById_InvalidId_ShouldThrowException()
    {
        // Arrange
        var context = GetDbContext();
        var manager = new StudentManager(context);

        // Act & Assert
        await Assert.ThrowsAsync<StudentNotFoundException>(() =>
            manager.GetStudentById(999));
    }
}