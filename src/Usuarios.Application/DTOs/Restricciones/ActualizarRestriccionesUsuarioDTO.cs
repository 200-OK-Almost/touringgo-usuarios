using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs.Restricciones
{
    public class ActualizarRestriccionesUsuarioDTO
    {
        public List<TipoDieta>? Dieta { get; set; }
        public bool? RequiereRecorridoSinEscalones { get; set; }
        public bool? RequiereBanoAccesible { get; set; }
    }
}
