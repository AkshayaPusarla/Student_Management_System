using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using StudentRepository;

namespace StudentTimerFunction
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;

        public Function1(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Timer triggered at: {DateTime.Now}");

            try
            {
                var students = _context.Students.ToList();

                if (students.Count == 0)
                {
                    _logger.LogInformation("No students found 📭");
                    return;
                }

                _logger.LogInformation($"Total Students: {students.Count}");

                foreach (var s in students)
                {
                    _logger.LogInformation(
                        $"ID: {s.Id}, Name: {s.Name}, Roll: {s.RollNumber}, Course: {s.Course}"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
        }
    }
}