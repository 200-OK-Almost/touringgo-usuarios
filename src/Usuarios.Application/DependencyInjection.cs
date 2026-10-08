using Microsoft.Extensions.DependencyInjection;
using Usuarios.Application.Interfaces;
using Usuarios.Application.Services;

namespace Usuarios.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Registrar servicios 
        services.AddScoped<IUsuarioService, UsuarioService>();

        return services;
    }
}