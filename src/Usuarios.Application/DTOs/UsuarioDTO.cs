using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Application.DTOs
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string GoogleId { get; set; } = null!;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public DateTimeOffset FechaCreacion { get; set; }
        public DateTimeOffset UltimoAcceso { get; set; }
    }
}
