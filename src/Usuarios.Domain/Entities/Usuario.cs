using System.ComponentModel.DataAnnotations;

namespace Usuarios.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string GoogleId { get; set; } = null!;
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        [Required]
        public DateTimeOffset FechaCreacion { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UltimoAcceso { get; set; }
    }
}
