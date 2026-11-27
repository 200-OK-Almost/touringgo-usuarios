using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs.Perfil
{
    public class ActualizarPreferenciaCategoriaDTO
    {
        public string CategoriaCodigo { get; set; } = string.Empty;
        public int Afinidad { get; set; }
    }
}