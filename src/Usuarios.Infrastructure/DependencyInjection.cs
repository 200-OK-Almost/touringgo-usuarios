using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Usuarios.Application.Interfaces.Repositories;
using Usuarios.Application.Interfaces.Services;
using Usuarios.Infrastructure.Data;
using Usuarios.Infrastructure.Repositories;

namespace Usuarios.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        // Registrar repositorios
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IPreferenciasRepository, PreferenciasRepository>();
        services.AddScoped<IRestriccionesRepository, RestriccionesRepository>();
        services.AddScoped<ITokenService, JwtTokenService>();

        services.AddSingleton<IAuthCodeStore, InMemoryAuthCodeStore>();
        return services;
    }
}