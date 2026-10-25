using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Usuarios;

namespace Usuarios.Application.Interfaces.Services
{
    public interface IAuthCodeStore
    {
        string Create(LoginResponseDTO response);
        LoginResponseDTO? Consume(string code);
    }
}
