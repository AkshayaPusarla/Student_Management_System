using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using StudentRepository;
using System.Linq;

namespace StudentTimerFunction
{
    public class GetStudentByIdFunction
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public GetStudentByIdFunction(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<GetStudentByIdFunction>();
        }

        [Function("GetStudentById")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("HTTP trigger for GetStudentById");

            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var idStr = query["id"];

            if (!int.TryParse(idStr, out int id))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                badResponse.WriteString("Invalid ID");
                return badResponse;
            }

            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            var response = req.CreateResponse(HttpStatusCode.OK);

            if (student == null)
            {
                response.WriteString("Student not found ❌");
            }
            else
            {
                response.WriteAsJsonAsync(student);
            }

            return response;
        }
    }
}