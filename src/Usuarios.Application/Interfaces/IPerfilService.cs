using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;

namespace Usuarios.Application.Interfaces
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
