using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Usuarios;

namespace Usuarios.Application.Interfaces.Services
{
    public interface ITokenService
    {
        TokenDTO GenerateToken(UsuarioDTO usuario);
    }
}
