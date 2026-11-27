using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs.Perfil
{
    public class ActualizarRestriccionesPerfilDTO
    {
        public List<TipoDieta> Dieta { get; set; } = new();

        public bool RequiereRecorridoSinEscalones { get; set; }

        public bool RequiereBanoAccesible { get; set; }
    }
}