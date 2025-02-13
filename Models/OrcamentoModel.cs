using System.ComponentModel.DataAnnotations;

namespace MaosAObra.Models
{
    public class OrcamentoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string ClienteNome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O serviço é obrigatório.")]
        public string TipoServico { get; set; }

        [Required(ErrorMessage = "A metragem é obrigatória.")]
        [Range(1, double.MaxValue, ErrorMessage = "A metragem deve ser maior que 0.")]
        public double Metragem { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Endereco { get; set; }

        public double Valor { get; set; } // Calculado automaticamente

        public bool Confirmado { get; set; }

        public DateTime? DataAgendamento { get; set; }
    }
}
