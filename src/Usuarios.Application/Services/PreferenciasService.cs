using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;

namespace Usuarios.Application.Services
{
    public class PreferenciasService: IPreferenciasService
    {
        private readonly IPreferenciasRepository _preferenciasRepository;
        public PreferenciasService(IPreferenciasRepository preferenciasRepository)
        {
            _preferenciasRepository = preferenciasRepository;
        }
        
    }
}
