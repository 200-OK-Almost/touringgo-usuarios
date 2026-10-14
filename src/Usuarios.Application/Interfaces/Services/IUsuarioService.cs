using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Perfil;
using Usuarios.Application.DTOs.Usuarios;

namespace Usuarios.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<ICollection<UsuarioDTO>> GetAllUsuarios();
        Task<UsuarioDTO> GetUsuarioById(Guid id);
        Task<UsuarioDTO> GetOrCreateUsuarioGoogleAsync(GoogleUsuarioInfoDTO dto);
        Task<PerfilUsuarioDTO> GetPerfilUsuarioByUsuarioId(Guid usuarioId);

    }
}
