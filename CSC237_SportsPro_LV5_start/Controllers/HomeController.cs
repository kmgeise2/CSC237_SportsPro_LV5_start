using Microsoft.AspNetCore.Mvc;

namespace CSC237_SportsPro_LV5_start.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }
    }
}