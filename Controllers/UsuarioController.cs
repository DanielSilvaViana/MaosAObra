using AutoMapper;
using MaosAObra.DTO.Usuario;
using MaosAObra.Enum;
using MaosAObra.Models;
using MaosAObra.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace MaosAObra.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioInterface usuarioInterface, IMapper mapper)
        {
            _usuarioInterface = usuarioInterface;
            _mapper = mapper;
        }
        public async Task<ActionResult> Index(int? id)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios(id);

            return View(usuarios);
        }

        [HttpGet]
        public ActionResult Cadastrar(int? id)
        {
            ViewBag.Perfil = PerfilEnum.Administrador;
            ViewBag.id = id;

            if (id != null)
            {
                ViewBag.Perfil = PerfilEnum.Cliente;
            }

            return View();
        }

        [HttpGet]

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id != null)
            {
                var usuario = await _usuarioInterface.BuscarUsuarioPorId(id);
                return View(usuario);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id != null)
            {
                var usuario = await _usuarioInterface.BuscarUsuarioPorId(id);

                var usuarioEditado = new UsuarioEditarDTO
                {
                    NomeCompleto = usuario.NomeCompleto,
                    Email = usuario.Email,
                    Perfil = usuario.Perfil,
                    Id = usuario.Id,
                    Usuario = usuario.Usuario,
                    Endereco = _mapper.Map<OrcamentoModel>(usuario.Endereco)
                };

                if (usuarioEditado.Perfil == PerfilEnum.Cliente)
                {
                    ViewBag.Perfil = PerfilEnum.Cliente;
                }
                else
                {
                    ViewBag.Perfil = PerfilEnum.Administrador;

                }


                return View(usuarioEditado);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<ActionResult> Cadastrar(UsuarioCriacaoDTO usuarioCriacaoDTO)
        {

            if (ModelState.IsValid)
            {
                if (!await _usuarioInterface.VerificaSeExisteUsuarioEEmail(usuarioCriacaoDTO))
                {
                    TempData["MensagemErro"] = "Já Existe email/ usuário Cadastrado!";
                    return View(usuarioCriacaoDTO);
                }

                //Cadastra Usuário
                var usuario = await _usuarioInterface.Cadastrar(usuarioCriacaoDTO);

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

                if (usuario.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }

                return RedirectToAction("Index", "Cliente", new { id = "0" });
            }


            else
            {
                TempData["MensagemErro"] = "Verifique os dados informados!";
                return View(usuarioCriacaoDTO);
            }

            return View();
        }

        [HttpPost]

        public async Task<ActionResult> MudarSituacaoUsuario(UsuarioModel usuarioModel)
        {
            if (usuarioModel.Id != 0 && usuarioModel.Id != null)
            {
                var usuarioBanco = await _usuarioInterface.MudarSituacaoUsuario(usuarioModel.Id);


                if (usuarioBanco.Situacao == true)
                {
                    TempData["MensagemSucesso"] = "Usuário Ativo com Sucesso!";
                }
                else
                {
                    TempData["MensagemSucesso"] = "Inativação realizada com Sucesso!";

                }

                if (usuarioBanco.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                else
                {
                    return RedirectToAction("Index", "Cliente", new { Id = "0" });
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]

        public async Task<ActionResult> Editar(UsuarioEditarDTO usuarioEditarDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Editar(usuarioEditarDto);
                TempData["MensagemSucesso"] = "Edição realizada com Sucesso!";

                if (usuario.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                else
                {
                    return RedirectToAction("Index", "Cliente", new { Id = "0" });

                }
            }
            else
            {
                TempData["MensagemErro"] = "Verifique os dados informados!";
                return View(usuarioEditarDto);
            }
        }
    }
}
