using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Application.DTOs
{
    public class TokenDTO
    {
        public string AccessToken { get; set; } = null!;
        public DateTimeOffset ExpiresAt { get; set; }

    }
}
