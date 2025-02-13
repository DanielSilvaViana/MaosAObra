using MaosAObra.Data;
using MaosAObra.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace ProjetoPedreiro.Controllers
{
    public class OrcamentoController : Controller
    {
        private readonly OficinaContext _context;
        private readonly Dictionary<string, double> precosServicos = new Dictionary<string, double>
        {
            { "Assentamento de piso", 50 },  // R$50 por m²
            { "Construção de parede", 80 },  // R$80 por m²
            { "Reboco", 40 },  // R$40 por m²
            { "Pintura", 30 }   // R$30 por m²
        };

        private readonly List<DateTime> datasAgendadas = new List<DateTime>();

        public OrcamentoController(OficinaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orcamentos = _context.Orcamentos.ToList();
            return View(orcamentos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(OrcamentoModel orcamento)
        {
            // Regra 1: Verificar se o serviço é válido
            if (!precosServicos.ContainsKey(orcamento.TipoServico))
            {
                ModelState.AddModelError("TipoServico", "Serviço inválido! Escolha um serviço disponível.");
                return View(orcamento);
            }

            // Calcular o orçamento com base na metragem
            orcamento.Valor = precosServicos[orcamento.TipoServico] * orcamento.Metragem;

            _context.Orcamentos.Add(orcamento);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Confirmar(int id, DateTime data)
        {
            var orcamento = _context.Orcamentos.FirstOrDefault(o => o.Id == id && !o.Confirmado);

            if (orcamento == null)
            {
                return BadRequest("Orçamento não encontrado ou já confirmado.");
            }

            // Regra 2: Verificar disponibilidade do pedreiro (um serviço por dia)
            if (_context.Orcamentos.Any(o => o.DataAgendamento == data))
            {
                return BadRequest("Data indisponível! Escolha outro dia.");
            }

            orcamento.Confirmado = true;
            orcamento.DataAgendamento = data;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

