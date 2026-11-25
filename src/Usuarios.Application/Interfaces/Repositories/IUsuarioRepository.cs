using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.DTOs.Usuarios;
using Usuarios.Domain.Entities;

namespace Usuarios.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<ICollection<Usuario>> GetAllUsuarios();
        Task<Usuario?> GetUsuarioById(Guid id);
        Task<Usuario?> GetUsuarioByGoogleId(string googleId);
        Task<Usuario> Add(Usuario usuario);
        public Task SaveChangesAsync();
        Task<List<UsuarioResumenDTO>> ObtenerResumenesAsync(
            IEnumerable<Guid> usuariosIds,
            CancellationToken cancellationToken = default);
    }
}
