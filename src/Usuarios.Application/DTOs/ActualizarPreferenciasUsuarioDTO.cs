using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs
{
    public class ActualizarPreferenciasUsuarioDTO
    {
        public RitmoVisita? RitmoVisitaPreferido { get; set; }

        public List<TipoTransporte>? TransportesPreferidos { get; set; }

        public List<ActualizarPreferenciasCategoriaDTO>? Categorias { get; set; }
    }
}
