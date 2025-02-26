using MaosAObra.Data;
using MaosAObra.DTO.Home;
using MaosAObra.Models;
using MaosAObra.Services.Autenticacao;
using Microsoft.EntityFrameworkCore;
using System;

namespace MaosAObra.Services.HomeService
{
    public class HomeService : IHomeInterface
    {
        private readonly OficinaContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;

        public HomeService(OficinaContext context, IAutenticacaoInterface autenticacaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
        }
        public async Task<RespostaModel<UsuarioModel>> RealizarLogin(LoginDTO loginDto)
        {

            RespostaModel<UsuarioModel> resposta = new RespostaModel<UsuarioModel>();
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Email == loginDto.Email);

                if (usuario == null)
                {
                    resposta.Dados = null;
                    resposta.Mensagem = "Credenciais Inválidas";
                    resposta.Status = false;

                    return resposta;
                }

                if (!_autenticacaoInterface.VerificaLogin(loginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    resposta.Dados = null;
                    resposta.Mensagem = "Credenciais Inválidas";
                    resposta.Status = false;
                    return resposta;
                }
                resposta.Dados = usuario;
                resposta.Mensagem = "Login Efetuado com Sucesso!";

                return resposta;
            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

       
    }
}
