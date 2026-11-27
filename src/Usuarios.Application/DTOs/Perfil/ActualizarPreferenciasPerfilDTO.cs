using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs.Perfil
{
    public class ActualizarPreferenciasPerfilDTO
    {
        public RitmoVisita RitmoVisitaPreferido { get; set; }

        public List<TipoTransporte> TransportesPreferidos { get; set; } = new();

        public List<ActualizarPreferenciaCategoriaDTO> Categorias { get; set; } = new();
    }
}