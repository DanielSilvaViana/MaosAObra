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
            { "Assentamento de piso", 50 },  // R$50 por m�
            { "Constru��o de parede", 80 },  // R$80 por m�
            { "Reboco", 40 },  // R$40 por m�
            { "Pintura", 30 }   // R$30 por m�
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
            // Regra 1: Verificar se o servi�o � v�lido
            if (!precosServicos.ContainsKey(orcamento.TipoServico))
            {
                ModelState.AddModelError("TipoServico", "Servi�o inv�lido! Escolha um servi�o dispon�vel.");
                return View(orcamento);
            }

            // Calcular o or�amento com base na metragem
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
                return BadRequest("Or�amento n�o encontrado ou j� confirmado.");
            }

            // Regra 2: Verificar disponibilidade do pedreiro (um servi�o por dia)
            if (_context.Orcamentos.Any(o => o.DataAgendamento == data))
            {
                return BadRequest("Data indispon�vel! Escolha outro dia.");
            }

            orcamento.Confirmado = true;
            orcamento.DataAgendamento = data;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

