using Microsoft.Extensions.DependencyInjection;
using Usuarios.Domain.Entities;
using Usuarios.Infrastructure.Data;

namespace Usuarios.UnitTests;

public class UsuariosApiFixture : IDisposable
{
    public static readonly Guid UsuarioId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

    public UsuariosApiFactory Factory { get; } = new();

    public UsuariosApiFixture()
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        db.Usuarios.Add(new Usuario
        {
            Id = UsuarioId,
            Email = "test@test.com",
            GoogleId = "test-google-id",
            Nombre = "Usuario Test"
        });
        db.SaveChanges();
    }

    public HttpClient CreateClient() => Factory.CreateClient();

    public void Dispose() => Factory.Dispose();
}
