using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace SubastaYa.Data
{
    public static class SeedDataExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // 1. Usuarios existentes
            var baseDate = new DateTimeOffset(
                2026, 10, 8, 12, 0, 0, TimeSpan.Zero);

            var usuarioTestId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var devTestId = Guid.Parse("11111111-1111-1111-1111-111111111112");

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = usuarioTestId,
                    Nombre = "Usuario Test",
                    GoogleId = "user-test-google-id",
                    Email = "usuario@test.com",
                    FechaCreacion = baseDate,
                    UltimoAcceso = baseDate,
                },
                new Usuario
                {
                    Id = devTestId,
                    Nombre = "Dev Test",
                    GoogleId = "dev-test-google-id",
                    Email = "dev@test.com",
                    FechaCreacion = baseDate,
                    UltimoAcceso = baseDate,
                }
            );

            // 2. IDs de preferencias
            var preferenciasUsuarioTestId = Guid.Parse("22222222-2222-2222-2222-222222222221");

            var preferenciasDevTestId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            modelBuilder.Entity<PreferenciasUsuario>().HasData(
                new PreferenciasUsuario
                {
                    Id = preferenciasUsuarioTestId,
                    UsuarioId = usuarioTestId,
                    RitmoVisitaPreferido = RitmoVisita.Relajado,
                    TransportesPreferidos = new List<TipoTransporte>
                    {
                        TipoTransporte.Caminando,
                        TipoTransporte.TransportePublico
                    }
                },
                new PreferenciasUsuario
                {
                    Id = preferenciasDevTestId,
                    UsuarioId = devTestId,
                    RitmoVisitaPreferido = RitmoVisita.Activo,
                    TransportesPreferidos = new List<TipoTransporte>
                    {
                        TipoTransporte.TransportePublico,
                        TipoTransporte.VehiculoPersonal
                    }
                }
            );

            // 3. Preferencias de categorías
            // Categorías ilustrativas:
            // 333...331 = Historia y patrimonio
            // 333...332 = Gastronomía
            // 333...333 = Museos y arte
            // 333...334 = Parques y naturaleza
            // 333...335 = Fútbol y deportes

            modelBuilder.Entity<PreferenciasCategoria>().HasData(
                // Usuario Test: afinidad alta por historia y gastronomía.
                new PreferenciasCategoria
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444441"),
                    PreferenciasUsuarioId = preferenciasUsuarioTestId,
                    CategoriaId = Guid.Parse("33333333-3333-3333-3333-333333333331"),
                    Afinidad = 5
                },
                new PreferenciasCategoria
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444442"),
                    PreferenciasUsuarioId = preferenciasUsuarioTestId,
                    CategoriaId = Guid.Parse("33333333-3333-3333-3333-333333333332"),
                    Afinidad = 4
                },
                new PreferenciasCategoria
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444443"),
                    PreferenciasUsuarioId = preferenciasUsuarioTestId,
                    CategoriaId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Afinidad = 3
                },

                // Dev Test: afinidad alta por deportes y parques.
                new PreferenciasCategoria
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    PreferenciasUsuarioId = preferenciasDevTestId,
                    CategoriaId = Guid.Parse("33333333-3333-3333-3333-333333333335"),
                    Afinidad = 5
                },
                new PreferenciasCategoria
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444445"),
                    PreferenciasUsuarioId = preferenciasDevTestId,
                    CategoriaId = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                    Afinidad = 4
                },
                new PreferenciasCategoria
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444446"),
                    PreferenciasUsuarioId = preferenciasDevTestId,
                    CategoriaId = Guid.Parse("33333333-3333-3333-3333-333333333331"),
                    Afinidad = 2
                }
            );

            // 4. Restricciones de los usuarios
            modelBuilder.Entity<RestriccionesUsuario>().HasData(
                new
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555551"),
                    UsuarioId = usuarioTestId,
                    RequiereRecorridoSinEscalones = true,
                    RequiereBanoAccesible = true,
                    Dieta = new List<TipoDieta>
                    {
                        TipoDieta.Celiaco,
                        TipoDieta.Vegetariano
                    }
                },
                new
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555552"),
                    UsuarioId = devTestId,
                    RequiereRecorridoSinEscalones = false,
                    RequiereBanoAccesible = false,
                    Dieta = new List<TipoDieta>
                    {
                        TipoDieta.Vegano
                    }
                }
            );
        }
    }
}
