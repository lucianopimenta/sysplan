using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SysPlan.Data.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filme",
                columns: table => new
                {
                    codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_criacao = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    data_atualizacao = table.Column<DateTime>(nullable: true),
                    catalogo = table.Column<string>(maxLength: 100, nullable: false),
                    nome = table.Column<string>(maxLength: 50, nullable: false),
                    ano = table.Column<int>(nullable: false),
                    imagem = table.Column<string>(nullable: false),
                    slogan = table.Column<string>(maxLength: 500, nullable: false),
                    visao_geral = table.Column<string>(nullable: false),
                    classificacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_filme", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_criacao = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    data_atualizacao = table.Column<DateTime>(nullable: true),
                    login = table.Column<string>(maxLength: 100, nullable: false),
                    senha = table.Column<string>(maxLength: 50, nullable: false),
                    ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filme");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
