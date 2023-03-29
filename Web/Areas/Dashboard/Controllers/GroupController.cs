using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Dashboard.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
