using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;

namespace Usuarios.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ICollection<UsuarioDTO>> GetAllUsuarios();
        Task<UsuarioDTO?> GetUsuarioById(Guid id);

    }
}
