using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedeAgro.Controllers
{
    [Authorize]
    public class NotificacaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
