using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuarios.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InicializarCategoriasUsuariosExistentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444446"),
                column: "Afinidad",
                value: 2);

            migrationBuilder.Sql("""
                INSERT INTO "PreferenciasCategorias"
                    ("Id", "PreferenciasUsuarioId", "CategoriaCodigo", "Afinidad")
                SELECT
                    gen_random_uuid(),
                    pu."Id",
                    categoria.codigo,
                    0
                FROM "PreferenciasUsuarios" pu
                CROSS JOIN (
                    VALUES
                        ('cafe_especialidad'),
                        ('museos_galerias'),
                        ('parques_aire_libre'),
                        ('librerias_diseno'),
                        ('bares_vermu'),
                        ('gastronomia_clasica'),
                        ('cocteleria_autor')
                ) AS categoria(codigo)
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM "PreferenciasCategorias" pc
                    WHERE pc."PreferenciasUsuarioId" = pu."Id"
                      AND pc."CategoriaCodigo" = categoria.codigo
                );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PreferenciasCategorias",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444446"),
                column: "Afinidad",
                value: -1);

            // no hacer método DOWN correspondiente a setteo de valores de afinidad para 
            // no resetear valores de otros usuarios
        }
    }
}
