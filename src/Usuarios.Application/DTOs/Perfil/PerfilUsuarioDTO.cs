using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Preferencias;
using Usuarios.Application.DTOs.Restricciones;
using Usuarios.Application.DTOs.Usuarios;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs.Perfil
{
    public class PerfilUsuarioDTO
    {
        public UsuarioDTO Usuario { get; set; } = null!;
        public PreferenciasUsuarioDTO Preferencias { get; set; } = null!;
        public RestriccionesUsuarioDTO Restricciones { get; set; } = null!;
    }
}
