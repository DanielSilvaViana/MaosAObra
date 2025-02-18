using MaosAObra.Enum;
using MaosAObra.Models;
using System.ComponentModel.DataAnnotations;

namespace MaosAObra.DTO.Usuario
{
    public class UsuarioEditarDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome completo!")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "Digite o Usuário!")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Digite o Email!")]
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Selecione Turno!")]
        public OrcamentoModel Endereco { get; set; }
    }
}
