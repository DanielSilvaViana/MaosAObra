using MaosAObra.Enum;
using System.ComponentModel.DataAnnotations;

namespace MaosAObra.DTO.Usuario
{
    public class UsuarioCriacaoDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite um nome Completo")]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o usuário Completo")]

        public string Usuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite a Situação")]
        public bool Situacao { get; set; } = true;

        [Required(ErrorMessage = "Digite um perfil")]
        public PerfilEnum Perfil { get; set; }

        [Required(ErrorMessage = "Digite uma Senha"), MinLength(6, ErrorMessage = "A Senha Deve conter no minimo 6 caractreres!")]

        public string Senha { get; set; }
        [Required(ErrorMessage = "Digite a confirmação da Senha"), Compare("Senha", ErrorMessage = "As Senhas não coincidem")]

        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Digite o Logradouro")]
        public string Logradouro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o Bairro")]

        public string Bairro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o Numero")]

        public string Numero { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o CEP")]

        public string CEP { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o Estado")]

        public string Estado { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;
    }
}
