using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs.Restricciones
{
    public class RestriccionesUsuarioDTO
    {
        public Guid Id { get; set; }
        public ICollection<TipoDieta> Dieta { get; set; } = new List<TipoDieta>();
        public bool RequiereRecorridoSinEscalones { get; set; }
        public bool RequiereBanoAccesible { get; set; }
    }
}
