using Usuarios.Application.DTOs.Preferencias;
using Usuarios.Application.DTOs.Restricciones;

namespace Usuarios.Application.DTOs.Perfil
{
    public class ActualizarPerfilUsuarioDTO
    {
        public ActualizarPreferenciasPerfilDTO Preferencias { get; set; } = new();
        public ActualizarRestriccionesPerfilDTO Restricciones { get; set; } = new();
    }
}