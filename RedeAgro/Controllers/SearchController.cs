using Microsoft.AspNetCore.Mvc;

namespace RedeAgro.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
