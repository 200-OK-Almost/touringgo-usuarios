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
    public class RestriccionesUsuarioDTO
    {
        public Guid Id { get; set; }
        public ICollection<TipoDieta> Dieta { get; set; } = new List<TipoDieta>();
        public bool RequiereRecorridoSinEscalones { get; set; }
        public bool RequiereBanoAccesible { get; set; }
    }
}
