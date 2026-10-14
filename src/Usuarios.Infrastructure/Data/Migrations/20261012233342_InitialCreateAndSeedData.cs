using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Usuarios.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    GoogleId = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    FotoUrl = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UltimoAcceso = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreferenciasUsuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    RitmoVisitaPreferido = table.Column<int>(type: "integer", nullable: false),
                    TransportesPreferidos = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciasUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferenciasUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestriccionesUsuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Dieta = table.Column<int[]>(type: "integer[]", nullable: false),
                    RequiereRecorridoSinEscalones = table.Column<bool>(type: "boolean", nullable: false),
                    RequiereBanoAccesible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestriccionesUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestriccionesUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferenciasCategorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PreferenciasUsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Afinidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciasCategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferenciasCategorias_PreferenciasUsuarios_PreferenciasUsu~",
                        column: x => x.PreferenciasUsuarioId,
                        principalTable: "PreferenciasUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "FechaCreacion", "FotoUrl", "GoogleId", "Nombre", "UltimoAcceso" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "usuario@test.com", new DateTimeOffset(new DateTime(2026, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "user-test-google-id", "Usuario Test", new DateTimeOffset(new DateTime(2026, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("11111111-1111-1111-1111-111111111112"), "dev@test.com", new DateTimeOffset(new DateTime(2026, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "dev-test-google-id", "Dev Test", new DateTimeOffset(new DateTime(2026, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "PreferenciasUsuarios",
                columns: new[] { "Id", "RitmoVisitaPreferido", "TransportesPreferidos", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222221"), 0, new[] { 0, 1 }, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 2, new[] { 1, 2 }, new Guid("11111111-1111-1111-1111-111111111112") }
                });

            migrationBuilder.InsertData(
                table: "RestriccionesUsuarios",
                columns: new[] { "Id", "Dieta", "RequiereBanoAccesible", "RequiereRecorridoSinEscalones", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555551"), new[] { 0, 1 }, true, true, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("55555555-5555-5555-5555-555555555552"), new[] { 2 }, false, false, new Guid("11111111-1111-1111-1111-111111111112") }
                });

            migrationBuilder.InsertData(
                table: "PreferenciasCategorias",
                columns: new[] { "Id", "Afinidad", "CategoriaId", "PreferenciasUsuarioId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444441"), 5, new Guid("33333333-3333-3333-3333-333333333331"), new Guid("22222222-2222-2222-2222-222222222221") },
                    { new Guid("44444444-4444-4444-4444-444444444442"), 4, new Guid("33333333-3333-3333-3333-333333333332"), new Guid("22222222-2222-2222-2222-222222222221") },
                    { new Guid("44444444-4444-4444-4444-444444444443"), 3, new Guid("33333333-3333-3333-3333-333333333333"), new Guid("22222222-2222-2222-2222-222222222221") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 5, new Guid("33333333-3333-3333-3333-333333333335"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("44444444-4444-4444-4444-444444444445"), 4, new Guid("33333333-3333-3333-3333-333333333334"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("44444444-4444-4444-4444-444444444446"), 2, new Guid("33333333-3333-3333-3333-333333333331"), new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasCategorias_PreferenciasUsuarioId",
                table: "PreferenciasCategorias",
                column: "PreferenciasUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasUsuarios_UsuarioId",
                table: "PreferenciasUsuarios",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestriccionesUsuarios_UsuarioId",
                table: "RestriccionesUsuarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GoogleId",
                table: "Usuarios",
                column: "GoogleId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreferenciasCategorias");

            migrationBuilder.DropTable(
                name: "RestriccionesUsuarios");

            migrationBuilder.DropTable(
                name: "PreferenciasUsuarios");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
