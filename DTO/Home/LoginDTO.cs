using System.ComponentModel.DataAnnotations;

namespace MaosAObra.DTO.Home
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Insira o email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insira a Senha!")]
        public string Senha { get; set; }
    }
}
