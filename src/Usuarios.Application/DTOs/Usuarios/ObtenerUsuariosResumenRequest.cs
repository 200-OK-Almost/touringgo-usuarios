using System.ComponentModel.DataAnnotations;

namespace Usuarios.Application.DTOs.Usuarios
{
    public class ObtenerUsuariosResumenRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public List<Guid> UsuariosIds { get; set; } = new List<Guid>();
    }
}
