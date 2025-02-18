using AutoMapper;
using MaosAObra.Data;
using MaosAObra.DTO.Usuario;
using MaosAObra.Models;
using MaosAObra.Services.Autenticacao;
using Microsoft.EntityFrameworkCore;
using System;

namespace MaosAObra.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly OficinaContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IMapper _mapper;

        public UsuarioService(OficinaContext context, IAutenticacaoInterface autenticacaoInterface, IMapper mapper)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int? id)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(endereco => endereco.Endereco).FirstOrDefaultAsync(u => u.Id == id);
                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {
            try
            {
                var registros = new List<UsuarioModel>();

                if (id != null)
                {
                    registros = await _context.Usuarios.Where(cliente => cliente.Perfil == 0)
                        .Include(e => e.Endereco).ToListAsync();
                }
                else
                {
                    registros = await _context.Usuarios.Where(funcionario => funcionario.Perfil != 0)
                        .Include(e => e.Endereco).ToListAsync();
                }

                return registros;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioCriacaoDTO> Cadastrar(UsuarioCriacaoDTO usuarioCriacaoDTO)
        {
            try
            {
                _autenticacaoInterface.CriarSenhaHash(usuarioCriacaoDTO.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    NomeCompleto = usuarioCriacaoDTO.NomeCompleto,
                    Usuario = usuarioCriacaoDTO.Usuario,
                    Email = usuarioCriacaoDTO.NomeCompleto,
                    Perfil = usuarioCriacaoDTO.Perfil,
                    //Turno = usuarioCriacaoDTO.Turno,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                var endereco = new EnderecoModel
                {
                    Logradouro = usuarioCriacaoDTO.Logradouro,
                    Numero = usuarioCriacaoDTO.Numero,
                    Bairro = usuarioCriacaoDTO.Bairro,
                    Estado = usuarioCriacaoDTO.Estado,
                    Complemento = usuarioCriacaoDTO.Complemento,
                    CEP = usuarioCriacaoDTO.CEP,
                    Usuario = usuario
                };
                usuario.Endereco = usuario.Endereco;

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return usuarioCriacaoDTO;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> Editar(UsuarioEditarDTO usuarioEditarDto)
        {
            try
            {
                var usuarioEditarBanco = await _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Id == usuarioEditarDto.Id);
                if (usuarioEditarBanco != null)
                {
                    //usuarioEditarBanco.Turno = usuarioEditarDto.Turno;
                    usuarioEditarBanco.Perfil = usuarioEditarDto.Perfil;
                    usuarioEditarBanco.NomeCompleto = usuarioEditarDto.NomeCompleto;
                    usuarioEditarBanco.Usuario = usuarioEditarDto.Usuario;
                    usuarioEditarBanco.Email = usuarioEditarDto.Email;
                    usuarioEditarBanco.DataAlteracao = DateTime.Now;
                    usuarioEditarBanco.Endereco = _mapper.Map<EnderecoModel>(usuarioEditarDto.Endereco);


                    _context.Update(usuarioEditarBanco);
                    await _context.SaveChangesAsync();

                    return usuarioEditarBanco;
                }

                return usuarioEditarBanco;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> MudarSituacaoUsuario(int id)
        {
            try
            {
                var usuarioMudarSituacao = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Id == id);

                if (usuarioMudarSituacao != null)
                {
                    if (usuarioMudarSituacao.Situacao == true)
                    {
                        usuarioMudarSituacao.Situacao = false;
                        usuarioMudarSituacao.DataAlteracao = DateTime.Now;
                    }
                    else
                    {
                        usuarioMudarSituacao.Situacao = true;
                        usuarioMudarSituacao.DataAlteracao = DateTime.Now;

                    }
                    _context.Update(usuarioMudarSituacao);
                    await _context.SaveChangesAsync();
                    return usuarioMudarSituacao;
                }
                return usuarioMudarSituacao;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioCriacaoDTO usuarioCriacaoDTO)
        {
            try
            {
                var mesmoUsuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Email == usuarioCriacaoDTO.Email ||
                                                                                                                     usuarioBanco.Usuario == usuarioCriacaoDTO.Usuario);

                if (mesmoUsuario == null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
