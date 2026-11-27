namespace Usuarios.Application.DTOs.Preferencias
{
    public class PreferenciasCategoriaDTO
    {
        public Guid Id { get; set; }
        public string CategoriaCodigo { get; set; } = string.Empty;
        public int Afinidad { get; set; }
    }
}
