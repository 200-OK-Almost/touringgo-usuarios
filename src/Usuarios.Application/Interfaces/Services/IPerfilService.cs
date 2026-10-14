using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Preferencias;
using Usuarios.Application.DTOs.Restricciones;

namespace Usuarios.Application.Interfaces.Services
{
    public interface IPerfilService
    {
        Task<PreferenciasUsuarioDTO> ActualizarPreferenciasByUsuarioId(
            Guid usuarioId, 
            ActualizarPreferenciasUsuarioDTO dto
        );

        Task<RestriccionesUsuarioDTO> ActualizarRestriccionesByUsuarioId(
            Guid usuarioId, 
            ActualizarRestriccionesUsuarioDTO dto
        );
    }
}
