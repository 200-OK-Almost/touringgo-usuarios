using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs.Preferencias
{
    public class PreferenciasUsuarioDTO
    {
        public Guid Id { get; set; }
        public RitmoVisita RitmoVisitaPreferido { get; set; } = RitmoVisita.Moderado;
        public ICollection<TipoTransporte> TransportesPreferidos { get; set; } = new List<TipoTransporte>();
        public ICollection<PreferenciasCategoriaDTO> Categorias { get; set; } = new List<PreferenciasCategoriaDTO>();
    }
}
