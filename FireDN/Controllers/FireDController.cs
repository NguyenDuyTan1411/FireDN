using Microsoft.AspNetCore.Mvc;

namespace FireDN.Controllers
{
    public class FireDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
