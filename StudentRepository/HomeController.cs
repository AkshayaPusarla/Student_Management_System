using Microsoft.AspNetCore.Mvc;

namespace StudentRepository
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
