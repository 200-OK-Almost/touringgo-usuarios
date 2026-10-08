using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace SubastaYa.Data
{
    public static class SeedDataExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            // 1. Usuarios
            var baseDate = new DateTimeOffset(2026, 10, 8, 12, 0, 0, TimeSpan.Zero);
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario 
                { 
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                    Nombre = "Usuario",
                    GoogleId = "user-test-google-id",
                    Apellido = "Test",
                    Email = "usuario@test.com", 
                    FechaCreacion = baseDate,
                    UltimoAcceso = baseDate,
                },
                new Usuario
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    Nombre = "Dev",
                    GoogleId = "dev-test-google-id",
                    Apellido = "Test",
                    Email = "dev@test.com",
                    FechaCreacion = baseDate,
                    UltimoAcceso = baseDate,
                }
            );

        }
    }
}
