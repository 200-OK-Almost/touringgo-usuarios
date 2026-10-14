using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Enums;

namespace Usuarios.Domain.Entities
{
    public class PreferenciasCategoria
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid PreferenciasUsuarioId { get; set; }
        public PreferenciasUsuario PreferenciasUsuario { get; set; } = null!;

        [Required]
        public Guid CategoriaId { get; set; } // FK al microservicio de lugares
        [Required]
        public int Afinidad { get; set; }

    }
}
