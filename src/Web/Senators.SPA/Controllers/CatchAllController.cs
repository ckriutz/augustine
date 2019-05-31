using Microsoft.AspNetCore.Mvc;

namespace Augustine.Senators.SPA.Controllers
{
    public class CatchAllController : Controller
    {
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}