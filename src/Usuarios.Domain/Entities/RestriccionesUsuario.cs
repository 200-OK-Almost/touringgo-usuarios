using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Enums;

namespace Usuarios.Domain.Entities
{
    public class RestriccionesUsuario
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Required]
        public ICollection<TipoDieta> Dieta { get; set; } = new List<TipoDieta>();
        [Required]
        public bool RequiereRecorridoSinEscalones { get; set; } = false;
        [Required]
        public bool RequiereBanoAccesible { get; set; } = false;
    }
}
