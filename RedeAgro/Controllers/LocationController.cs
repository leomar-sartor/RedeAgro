using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeAgro.Entidade;
using RedeAgro.Inputs;
using RedeAgro.Intefaces;
using RedeAgro.Models;
using RedeAgro.Models.ViewModels;
using RedeAgro.Services;
using System.Data;
using System.Globalization;

namespace RedeAgro.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class LocationController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private UserManager<UserApp> _userManager;
        protected readonly UserApp? _user;
        private readonly ICredenciadoService _credenciadoService;

        //CRUD
        ////https://medium.com/net-core/build-a-web-app-with-asp-net-core-and-mongodb-f9fcd61cb04f
        public LocationController(
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


        public IActionResult Form()
        {
            var location = new MinhaLocalizacao();
            location.UserId = _user.Id;
            location.Latitude = _user.Local?.Coordinates?.FirstOrDefault() ?? 0;
            location.Longitude = _user.Local?.Coordinates?.Skip(1)?.FirstOrDefault() ?? 0;

            return View(location);
        }

        [HttpPost]
        public async Task<IActionResult> Form(MinhaLocalizacao form)
        {
            bool error = false;

            Double resultLatitude;
            var latitude = HttpContext.Request.Form["Latitude"].ToString().Replace(".", ",");
            if (Double.TryParse(latitude, out resultLatitude))
            {
                resultLatitude = Double.Parse(latitude);
            }
            else
            {
                error = true;
            }

            Double resultLongitude;
            var longitude = HttpContext.Request.Form["Longitude"].ToString().Replace(".", ",");
            if (Double.TryParse(longitude, out resultLongitude))
            {
                resultLongitude = Double.Parse(longitude);
            }
            else
            {
                error = true;
            }

            if (error)
            {
                TempData["Error"] = true;
            }
            else
            {
                var jatenhoLocal = _user.Local is not null;

                if (jatenhoLocal)
                {
                    var meuLocal = _user.Local;

                    if(meuLocal.Coordinates is not null)
                    {
                        if (meuLocal.Coordinates.Count > 0)
                        {
                            meuLocal.Coordinates.Clear();
                        }
                    }

                    meuLocal.Type = "Point";
                    meuLocal.Coordinates = new List<double>() { resultLatitude, resultLongitude };

                    _user.Local = meuLocal;

                    var result = await _userManager.UpdateAsync(_user);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Erro ao atualizar Usuário");
                    }
                    else
                    {
                        var input = new CredenciadoInput();
                        input.Id = _user.Id;
                        input.Location = new Local()
                        {
                            Type = "Point",
                            Coordinates = new List<double>() { resultLatitude, resultLongitude }
                        };

                        await _credenciadoService.UpdateAsync(_user.Id, input);
                    }
                }
                else
                {
                    var novoLocal = new Local();

                    novoLocal.Coordinates.Clear();
                    novoLocal.Coordinates.Add(resultLatitude);
                    novoLocal.Coordinates.Add(resultLongitude);

                    _user.Local = novoLocal;

                    var result = await _userManager.UpdateAsync(_user);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Erro ao atualizar Usuário");
                    }
                    else
                    {
                        var input = new CredenciadoInput();
                        input.Id = _user.Id;
                        input.Location = new Local()
                        {
                            Type = "Point",
                            Coordinates = new List<double>() { resultLatitude, resultLongitude }
                        };

                        await _credenciadoService.UpdateAsync(_user.Id, input);
                    }
                }
            }

            return RedirectToAction("Form");
        }
    }
}
