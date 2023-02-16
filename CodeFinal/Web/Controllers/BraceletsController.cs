using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BraceletsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
