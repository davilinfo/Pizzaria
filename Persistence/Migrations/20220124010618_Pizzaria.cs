using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Pizzaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endereco = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "Sabor",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    valor = table.Column<double>(type: "float", nullable: false),
                    disponivel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sabor", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCliente = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.codigo);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_CodigoCliente",
                        column: x => x.CodigoCliente,
                        principalTable: "Cliente",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    valor = table.Column<double>(type: "float", nullable: false),
                    PedidoCodigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.codigo);
                    table.ForeignKey(
                        name: "FK_Pizza_Pedido_PedidoCodigo",
                        column: x => x.PedidoCodigo,
                        principalTable: "Pedido",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PizzaSabor",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPizza = table.Column<int>(type: "int", nullable: false),
                    CodigoSabor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaSabor", x => x.codigo);
                    table.ForeignKey(
                        name: "FK_PizzaSabor_Pizza_CodigoPizza",
                        column: x => x.CodigoPizza,
                        principalTable: "Pizza",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaSabor_Sabor_CodigoSabor",
                        column: x => x.CodigoSabor,
                        principalTable: "Sabor",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_CodigoCliente",
                table: "Pedido",
                column: "CodigoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_PedidoCodigo",
                table: "Pizza",
                column: "PedidoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaSabor_CodigoPizza",
                table: "PizzaSabor",
                column: "CodigoPizza");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaSabor_CodigoSabor",
                table: "PizzaSabor",
                column: "CodigoSabor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaSabor");

            migrationBuilder.DropTable(
                name: "Pizza");

            migrationBuilder.DropTable(
                name: "Sabor");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
