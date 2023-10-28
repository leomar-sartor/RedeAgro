using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeAgro.Intefaces;
using RedeAgro.Models;

namespace RedeAgro.Controllers
{
    [Authorize(Roles = "ADMIN, USER")]
    public class SecuredController : Controller
    {
        private readonly ICredenciadoService _credenciadoService;
        private readonly IHttpContextAccessor _contextAccessor;
        private UserManager<UserApp> _userManager;
        protected readonly UserApp? _user;
        public SecuredController(
            IHttpContextAccessor contextAccessor,
            UserManager<UserApp> userManager, 
            ICredenciadoService credenciadoService)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _user = _userManager.Users
                .Where(m => m.NormalizedUserName.ToLower() == contextAccessor.HttpContext.User.Identity.Name.ToLower())
                .FirstOrDefault();
            _credenciadoService = credenciadoService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var credenciado = await _credenciadoService.GetByIdAsync(_user.Id);



            return View();
        }
    }
}
