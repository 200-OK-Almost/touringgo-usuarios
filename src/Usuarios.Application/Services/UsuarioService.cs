using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;
using Usuarios.Domain.Exceptions;

namespace Usuarios.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<ICollection<UsuarioDTO>> GetAllUsuarios()
        {
            return await _usuarioRepository.GetAllUsuarios();
        }

        public async Task<UsuarioDTO?> GetUsuarioById(Guid id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
                throw new NotFoundException($"Usuario con ID {id} no encontrado");

            return usuario;
        }
    }
}
