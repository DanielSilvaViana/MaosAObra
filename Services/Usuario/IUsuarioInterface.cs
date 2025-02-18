using MaosAObra.DTO.Usuario;
using MaosAObra.Models;

namespace MaosAObra.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<List<UsuarioModel>> BuscarUsuarios(int? id);
        Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioCriacaoDTO usuarioCriacaoDTO);
        Task<UsuarioCriacaoDTO> Cadastrar(UsuarioCriacaoDTO usuarioCriacaoDTO);
        Task<UsuarioModel> BuscarUsuarioPorId(int? id);
        Task<UsuarioModel> MudarSituacaoUsuario(int id);
        Task<UsuarioModel> Editar(UsuarioEditarDTO usuarioEditarDto);
    }
}
