using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedeAgro.Entidade;
using RedeAgro.Inputs;
using RedeAgro.Intefaces;
using RedeAgro.Models;
using RedeAgro.Models.ViewModels;
using System.Data;

namespace RedeAgro.Controllers
{

    [Authorize]
    public class InformacoesController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private UserManager<UserApp> _userManager;
        protected readonly UserApp? _user;
        private readonly ICredenciadoService _credenciadoService;

        public InformacoesController(
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

        public async Task<IActionResult> Form(Guid Id)
        {
            var info = new MinhasInformacoes();
            info.UserId = _user.Id;

            var minhaInfo = await _credenciadoService.GetByIdAsync(info.UserId);

            if (minhaInfo == null)
            {
                info.Email = _user.Email;
                info.Nome = _user.UserName;
            }
            else
            {
                info.Telefone = minhaInfo.Telefone;
                info.Email = minhaInfo.Email;
                info.Nome = minhaInfo.Nome;

                info.Servicos = minhaInfo.Servicos;
            }

            return View(info);
        }

        [HttpPost]
        public async Task<IActionResult> Form(MinhasInformacoes form)
        {
            
            if (!ModelState.IsValid)
                return View(form);

            var input = new CredenciadoInput();
            input.Id = form.UserId;
            input.Telefone = form.Telefone;
            input.Email = form.Email;
            input.Nome = form.Nome;

            input.Servicos = new List<Entidade.Servico>();
            input.Location = _user.Local;

            foreach (var servico in form.Servicos)
            {
                var service = new Servico();
                service.Descricao = servico.Descricao;
                service.UserId = form.UserId;

                input.Servicos.Add(service);
            }

            var res = await _credenciadoService.UpdateAsync(form.UserId, input);

            return RedirectToAction("Form", new { Id = res.Id });
        }

        public async Task<ActionResult> AdicionarServico(MinhasInformacoes info)
        {
            ModelState.Clear();

            info.Servicos.Add(new Entidade.Servico());

            return PartialView("_Servicos", info);
        }

        public async Task<ActionResult> RemoverServico(MinhasInformacoes info)
        {
            ModelState.Clear();

            var sequencial = HttpContext.Request.Form["Sequencial"];

            info.Servicos.RemoveAt(Int32.Parse(sequencial));

            return PartialView("_Servicos", info);
        }
    }
}
