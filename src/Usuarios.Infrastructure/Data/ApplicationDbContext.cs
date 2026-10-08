using Microsoft.EntityFrameworkCore;
using SubastaYa.Data;
using Usuarios.Domain.Entities;

namespace Usuarios.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
    // Registrar entidades
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.GoogleId)
                .IsUnique();
        });


        // Carga de datos de prueba
        modelBuilder.Seed();
    }
}
