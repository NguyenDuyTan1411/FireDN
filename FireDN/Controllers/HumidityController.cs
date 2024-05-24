using Microsoft.AspNetCore.Mvc;

namespace FireDN.Controllers
{
    public class HumidityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
