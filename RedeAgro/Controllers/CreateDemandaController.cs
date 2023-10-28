using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedeAgro.Controllers
{
    [Authorize]
    public class CreateDemandaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
