using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Entities;

namespace Usuarios.Application.Interfaces
{
    public interface IPreferenciasRepository
    {
        Task<PreferenciasUsuario?> GetPreferenciasUsuarioByUsuarioId(Guid usuarioId);
        Task<RestriccionesUsuario?> GetRestriccionesUsuarioByUsuarioId(Guid usuarioId);
        Task<PreferenciasCategoria> AddPreferenciaCategoria(PreferenciasCategoria preferenciasCategoria);
        Task<PreferenciasUsuario> AddPreferencias(PreferenciasUsuario preferenciasUsuario);
        Task<RestriccionesUsuario> AddRestricciones(RestriccionesUsuario restriccionesUsuario);
        Task SaveChangesAsync();
    }
}
