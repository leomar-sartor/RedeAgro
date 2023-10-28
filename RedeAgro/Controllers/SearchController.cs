using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeAgro.Intefaces;
using RedeAgro.Models;
using RedeAgro.Models.Filtros;
using System.Data;

namespace RedeAgro.Controllers
{
    [Authorize(Roles = "USER, ADMIN")]
    public class SearchController : Controller
    {
        private readonly ICredenciadoService _credenciadoService;
        
        public SearchController(ICredenciadoService credenciadoService)
        {
            _credenciadoService = credenciadoService;
        }

        [Route("Search/Index/{pageindex?}")]
        public async Task<IActionResult> Index(SearchFilter? filtro = null, int? pageindex = 1)
        {
            if (filtro == null)
                filtro = new SearchFilter();

            var total = await _credenciadoService.GetAllAsync();
            var itens = await _credenciadoService.GetAllAsyncPaginator(pageindex?? 1);
            filtro.Itens = itens.ToList();
            filtro.PageIndex = pageindex ?? 1;
            filtro.TotalPages = (int)Math.Ceiling(total.Count() / (double)10);

            return View(filtro);
        }
    }
}
