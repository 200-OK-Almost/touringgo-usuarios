using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Entities;

namespace Usuarios.Application.Interfaces.Repositories
{
    public interface IPreferenciasRepository
    {
        Task<PreferenciasUsuario?> GetByUsuarioId(Guid usuarioId);
        Task AddPreferenciaCategoria(PreferenciasCategoria preferenciasCategoria);
        Task Add(PreferenciasUsuario preferenciasUsuario);
        Task SaveChangesAsync();
    }
}
