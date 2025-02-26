using MaosAObra.DTO.Home;
using MaosAObra.Services.HomeService;
using MaosAObra.Services.SessaoSerive;
using Microsoft.AspNetCore.Mvc;

namespace MaosAObra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IHomeInterface _homeInterface;

        public HomeController(ISessaoInterface sessaoInterface, IHomeInterface homeInterface)
        {
            _sessaoInterface = sessaoInterface;
            _homeInterface = homeInterface;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string pesquisar = null)
        {
            var usuarioSessao = _sessaoInterface.BuscarSessao();
            if (usuarioSessao != null)
            {
                ViewBag.LayoutPagina = "_Layout";
            }
            else
            {
                ViewBag.LayoutPagina = "_LayoutDeslogada";

            }
            return View();

        }

        [HttpGet]
        public ActionResult Login()
        {
            if (_sessaoInterface.BuscarSessao() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Sair()
        {
            _sessaoInterface.RemoverSessao();
            TempData["MensagemSucesso"] = "Usuário deslogado";
            return RedirectToAction("Login", "Home");

        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
            var usuarioSessao = _sessaoInterface.BuscarSessao();

            if (usuarioSessao != null)
            {
                ViewBag.UsuarioLogado = usuarioSessao.Id;
                ViewBag.LayoutPagina = "_Layout";
            }
            else
            {
                ViewBag.LayoutPagina = "_LayoutDeslogada";

            }
            

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO loginDto)
        {
            if (ModelState.IsValid)
            {
                var login = await _homeInterface.RealizarLogin(loginDto);

                if (login.Status == false)
                {
                    TempData["MensagemErro"] = login.Mensagem;
                    return View(login.Dados);
                }

                if (login.Dados.Situacao == false)
                {
                    TempData["MensagemErro"] = "Procure o suporte para verificar o status da sua conta!";
                    return View(login);
                }

                _sessaoInterface.CriarSessao(login.Dados);
                TempData["MensagemSucesso"] = login.Mensagem;

                return RedirectToAction("Index");
            }
            else
            {
                return View();

            }

        }
    }
}
