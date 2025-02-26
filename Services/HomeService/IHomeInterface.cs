using MaosAObra.DTO.Home;
using MaosAObra.Models;

namespace MaosAObra.Services.HomeService
{
    public interface IHomeInterface
    {
        Task<RespostaModel<UsuarioModel>> RealizarLogin(LoginDTO loginDto);

    }
}
