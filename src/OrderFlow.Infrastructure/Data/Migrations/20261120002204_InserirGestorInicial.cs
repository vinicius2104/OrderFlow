using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderFlow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InserirGestorInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Nome", "Perfil", "SenhaHash" },
                values: new object[] { 1L, true, new DateTime(2026, 11, 19, 0, 0, 0, 0, DateTimeKind.Utc), "admin@orderflow.com", "Administrador", "GESTOR", "AQAAAAIAAYagAAAAECblr9Ez20LGhWMAoOpWxNLMpi2boEOzS1T5W2fQf7NF+4bCDdlZ88mUdhY2IXl9Aw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
