using Usuarios.Application.DTOs.Preferencias;
using Usuarios.Application.DTOs.Restricciones;
using Usuarios.Application.DTOs.Usuarios;

namespace Usuarios.Application.DTOs.Perfil
{
    public class PerfilUsuarioDTO
    {
        public UsuarioDTO Usuario { get; set; } = null!;
        public PreferenciasUsuarioDTO Preferencias { get; set; } = null!;
        public RestriccionesUsuarioDTO Restricciones { get; set; } = null!;
    }
}
