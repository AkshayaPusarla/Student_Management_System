using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StudentRepository;
using StudentRepository.Controllers;
using System.Threading.Tasks;
using System.Linq;

public class StudentControllerTests
{
    [Fact]
    public async Task CreateStudent_ReturnsOk()
    {
        var mock = new Mock<IStudentManager>();
        var controller = new StudentController(mock.Object);

        var student = new Student
        {
            Name = "Test",
            RollNumber = "101",
            Course = "CSE"
        };

        var result = await controller.CreateStudent(student);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetStudent_NotFound_Returns404()
    {
        var mock = new Mock<IStudentManager>();

        mock.Setup(x => x.GetStudentById(It.IsAny<int>()))
            .ThrowsAsync(new StudentNotFoundException("Not found"));

        var controller = new StudentController(mock.Object);

        var result = await controller.GetStudent(1);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}