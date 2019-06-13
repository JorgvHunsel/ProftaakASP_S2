using Microsoft.AspNetCore.Mvc;

namespace ProftaakASP_S2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("../Home/Index");
        }
    }
}