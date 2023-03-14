using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
