using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;

namespace Usuarios.Application.DTOs.Preferencias
{
    public class PreferenciasCategoriaDTO
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public int Afinidad { get; set; }
    }
}
