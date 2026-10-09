using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;
using Usuarios.Domain.Entities;
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
            var usuarios = await _usuarioRepository.GetAllUsuarios();
            return usuarios.Select(u => MapUsuarioDTO(u)).ToList();
        }

        public async Task<UsuarioDTO> GetOrCreateUsuarioGoogleAsync(GoogleUsuarioInfoDTO dto)
        {
            var usuario = await _usuarioRepository.GetUsuarioByGoogleId(dto.GoogleId);

            // Si el usuario con Google ID dado no existe, crearlo 
            if (usuario == null)
            {
                usuario = new Usuario
                {
                    GoogleId = dto.GoogleId,
                    Email = dto.Email!,
                    Nombre = dto.Nombre!,
                    FotoUrl = dto.FotoUrl
                };

                await _usuarioRepository.Add(usuario);
            } else
            {
                // si el usuario si existe, actualizar el ultimo acceso
                usuario.UltimoAcceso = DateTimeOffset.UtcNow;
                await _usuarioRepository.SaveChangesAsync();
            }

            return MapUsuarioDTO(usuario!);
        }

        public async Task<UsuarioDTO?> GetUsuarioById(Guid id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
                throw new NotFoundException($"Usuario con ID {id} no encontrado");

            return MapUsuarioDTO(usuario);
        }

        // --- Helpers --- 

        private static UsuarioDTO MapUsuarioDTO(Usuario u)
        {
            return new UsuarioDTO
            {
                Id = u.Id,
                Email = u.Email,
                GoogleId = u.GoogleId,
                Nombre = u.Nombre,
                FotoUrl = u.FotoUrl,
                FechaCreacion = u.FechaCreacion,
                UltimoAcceso = u.UltimoAcceso,
            };
        }
    }
}
