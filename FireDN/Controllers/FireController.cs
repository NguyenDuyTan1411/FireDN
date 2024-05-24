using Microsoft.AspNetCore.Mvc;

namespace FireDN.Controllers
{
    public class FireController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
