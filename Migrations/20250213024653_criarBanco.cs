using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaosAObra.Migrations
{
    /// <inheritdoc />
    public partial class criarBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orcamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoServico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metragem = table.Column<double>(type: "float", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    DataAgendamento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamentos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orcamentos");
        }
    }
}
