using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeAgro.Entidade;
using RedeAgro.Inputs;
using RedeAgro.Intefaces;
using RedeAgro.Models;
using System.ComponentModel.DataAnnotations;

//https://www.yogihosting.com/aspnet-core-identity-mongodb/
namespace RedeAgro.Controllers
{
    public class OperationsController : Controller
    {
        private readonly ICredenciadoService _credenciadoService;
        private UserManager<UserApp> _userManager;
        private RoleManager<RoleApp> _roleManager;

        public OperationsController(
            UserManager<UserApp> userManager,
            RoleManager<RoleApp> roleManager,
            ICredenciadoService credenciadoService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _credenciadoService = credenciadoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                UserApp appUser = new UserApp
                {
                    UserName = user.Name,
                    Email = user.Email,
                    EmailConfirmed = true
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

                //Adding User to Admin Role
                await _userManager.AddToRoleAsync(appUser, "ADMIN");

                if (result.Succeeded) { 
                    ViewBag.Message = "User Created Successfully";

                    //Cadastrar Dados
                    var NovoCredenciado = new CredenciadoInput();
                    NovoCredenciado.Id = appUser.Id;
                    await _credenciadoService.AddAsync(NovoCredenciado);
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole([Required] string name)
        {
            if (ModelState.IsValid)
            {
                RoleApp role = new RoleApp() { Name = name };
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    ViewBag.Message = "Role Created Successfully";
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}
