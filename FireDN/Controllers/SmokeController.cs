using Microsoft.AspNetCore.Mvc;

namespace FireDN.Controllers
{
    public class SmokeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
