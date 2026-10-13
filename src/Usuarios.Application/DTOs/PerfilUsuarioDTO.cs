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
    public class PerfilUsuarioDTO
    {
        public UsuarioDTO Usuario { get; set; } = null!;
        public PreferenciasUsuarioDTO Preferencias { get; set; } = null!;
        public RestriccionesUsuarioDTO Restricciones { get; set; } = null!;
    }
}
