using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeAgro.Entidade;
using RedeAgro.Intefaces;
using RedeAgro.Models;
using RedeAgro.Models.ViewModels;
using RedeAgro.Object;
using RedeAgro.Repositories;
using System.Data;
using System.Xml.Linq;
using System.Linq;

namespace RedeAgro.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class CredenciadosController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private UserManager<UserApp> _userManager;
        protected readonly UserApp? _user;
        private readonly ICredenciadoService _credenciadoService;

        public CredenciadosController(
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

        public IActionResult Index(CredenciadoFiltro filtro = null)
        {
            if (filtro == null)
                filtro = new CredenciadoFiltro();


            return View(filtro);
        }

        //BuscarCredenciados
        public async Task<IActionResult> BuscarCredenciadosAsync(double latitude, double longitude, int distance, string nome)
        {
            //var fake = new DadosFake();
            //var dados = fake.GetDados();
            //var jsonFake = Json(dados);

            //nome = "Teatro São Pedro",
            //endereco = "Teste",
            //local = new List<double>() { -51.23040500, -30.03211600 },
            //setor = "Poder Público"

            //IEnumerable<CredenciadoLocationValueObject> retorno = await _credenciadoService.GetProximityAsync(latitude, longitude, distance, nome);

            var retorno = await _credenciadoService
                .GetProximityAsync(latitude, longitude, distance, nome);

            var result = retorno.Select(m => new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = m.Location.Coordinates,
                setor = "Poder Público"
            }).ToList(); 


            var json = Json(result);

            return json;
        }
    }
}
