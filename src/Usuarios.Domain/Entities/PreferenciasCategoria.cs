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
        public string CategoriaCodigo { get; set; } = string.Empty; // código compartido con el microservicio de Lugares
        [Required]
        public int Afinidad { get; set; }

    }
}
