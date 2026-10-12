using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Enums;

namespace Usuarios.Domain.Entities
{
    public class PreferenciasUsuario
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Required]
        public RitmoVisita RitmoVisitaPreferido { get; set; } = RitmoVisita.Moderado;

        [Required]
        public ICollection<TipoTransporte> TransportesPreferidos { get; set; } = new List<TipoTransporte>() { TipoTransporte.Caminando };
        [Required]
        public ICollection<PreferenciasCategoria> Categorias { get; set; } = new List<PreferenciasCategoria>();
    }
}
