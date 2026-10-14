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
    public DbSet<PreferenciasUsuario> PreferenciasUsuarios { get; set; }
    public DbSet<PreferenciasCategoria> PreferenciasCategorias { get; set; }
    public DbSet<RestriccionesUsuario> RestriccionesUsuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.GoogleId)
                .IsUnique();
        });

        modelBuilder.Entity<PreferenciasUsuario>()
            .HasIndex(p => p.UsuarioId)
            .IsUnique();

        modelBuilder.Entity<RestriccionesUsuario>()
            .HasIndex(r => r.UsuarioId)
            .IsUnique();

        // Carga de datos de prueba
        modelBuilder.Seed();
    }
}
