namespace Usuarios.Application.DTOs.Usuarios
{
    public class UsuarioResumenDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
    }
}
