using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuarios.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoriasPorCodigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RestriccionesUsuarios_UsuarioId",
                table: "RestriccionesUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_PreferenciasCategorias_PreferenciasUsuarioId",
                table: "PreferenciasCategorias");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "PreferenciasCategorias");

            migrationBuilder.AddColumn<string>(
                name: "CategoriaCodigo",
                table: "PreferenciasCategorias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CategoriaCodigo",
                value: "museos_galerias");

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CategoriaCodigo",
                value: "gastronomia_clasica");

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CategoriaCodigo",
                value: "parques_aire_libre");

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CategoriaCodigo",
                value: "parques_aire_libre");

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444445"),
                column: "CategoriaCodigo",
                value: "bares_vermu");

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444446"),
                column: "CategoriaCodigo",
                value: "museos_galerias");

            migrationBuilder.CreateIndex(
                name: "IX_RestriccionesUsuarios_UsuarioId",
                table: "RestriccionesUsuarios",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasCategorias_PreferenciasUsuarioId_CategoriaCodigo",
                table: "PreferenciasCategorias",
                columns: new[] { "PreferenciasUsuarioId", "CategoriaCodigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RestriccionesUsuarios_UsuarioId",
                table: "RestriccionesUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_PreferenciasCategorias_PreferenciasUsuarioId_CategoriaCodigo",
                table: "PreferenciasCategorias");

            migrationBuilder.DropColumn(
                name: "CategoriaCodigo",
                table: "PreferenciasCategorias");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoriaId",
                table: "PreferenciasCategorias",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "CategoriaId",
                value: new Guid("33333333-3333-3333-3333-333333333331"));

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"),
                column: "CategoriaId",
                value: new Guid("33333333-3333-3333-3333-333333333332"));

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"),
                column: "CategoriaId",
                value: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CategoriaId",
                value: new Guid("33333333-3333-3333-3333-333333333335"));

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444445"),
                column: "CategoriaId",
                value: new Guid("33333333-3333-3333-3333-333333333334"));

            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444446"),
                column: "CategoriaId",
                value: new Guid("33333333-3333-3333-3333-333333333331"));

            migrationBuilder.CreateIndex(
                name: "IX_RestriccionesUsuarios_UsuarioId",
                table: "RestriccionesUsuarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasCategorias_PreferenciasUsuarioId",
                table: "PreferenciasCategorias",
                column: "PreferenciasUsuarioId");
        }
    }
}
