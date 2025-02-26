using MaosAObra.Models;

namespace MaosAObra.Services.SessaoSerive
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuario);
        void RemoverSessao();
    }
}
