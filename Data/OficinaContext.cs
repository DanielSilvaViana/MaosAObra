using MaosAObra.Models;
using Microsoft.EntityFrameworkCore;

namespace MaosAObra.Data
{
    public class OficinaContext : DbContext
    {
        public OficinaContext(DbContextOptions<OficinaContext> options) : base(options) { }

        public DbSet<OrcamentoModel> Orcamentos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DbSet<EnderecoModel> Enderecos { get; set; } 



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrcamentoModel>()
                .Property(o => o.Valor)
                .HasPrecision(10, 2); // Define precisão para valores monetários
        }

    }
}
