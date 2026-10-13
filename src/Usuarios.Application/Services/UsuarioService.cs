using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;
using Usuarios.Domain.Exceptions;

namespace Usuarios.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPreferenciasRepository _preferenciasRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, IPreferenciasRepository preferenciasRepository)
        {
            _usuarioRepository = usuarioRepository;
            _preferenciasRepository = preferenciasRepository;
        }
        public async Task<ICollection<UsuarioDTO>> GetAllUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllUsuarios();
            return usuarios.Select(u => MapUsuarioDTO(u)).ToList();
        }

        public async Task<UsuarioDTO> GetOrCreateUsuarioGoogleAsync(GoogleUsuarioInfoDTO dto)
        {
            var usuario = await _usuarioRepository.GetUsuarioByGoogleId(dto.GoogleId);

            // Si el usuario con Google ID dado no existe, crearlo y sus preferencias y restricciones
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

                // los valores por defecto están definidos en las entidades
                var preferencias = new PreferenciasUsuario
                {
                    UsuarioId = usuario.Id
                };
                var restricciones = new RestriccionesUsuario
                {
                    UsuarioId = usuario.Id
                }
                ;
                await _preferenciasRepository.AddPreferencias(preferencias);
                await _preferenciasRepository.AddRestricciones(restricciones);
            } else
            {
                // si el usuario si existe, actualizar el ultimo acceso
                usuario.UltimoAcceso = DateTimeOffset.UtcNow;
            }

            await _usuarioRepository.SaveChangesAsync();

            return MapUsuarioDTO(usuario!);
        }

        public async Task<UsuarioDTO> GetUsuarioById(Guid id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
                throw new NotFoundException($"Usuario con ID {id} no encontrado");

            return MapUsuarioDTO(usuario);
        }

        public async Task<PerfilUsuarioDTO> GetPerfilUsuarioByUsuarioId(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(usuarioId);
            if (usuario == null)
                throw new NotFoundException($"Usuario con ID {usuarioId} no encontrado");

            var preferencias = await _preferenciasRepository.GetPreferenciasUsuarioByUsuarioId(usuarioId);
            if (preferencias == null)
                throw new NotFoundException($"Preferencias del usuario con ID {usuarioId} no encontradas");

            var restricciones = await _preferenciasRepository.GetRestriccionesUsuarioByUsuarioId(usuarioId);
            if (restricciones == null)
                throw new NotFoundException($"Restricciones del usuario con ID {usuarioId} no encontradas");

            return MapPerfilUsuarioDTO(usuario, preferencias, restricciones);
        }

        // --- Helpers --- 

        private static PerfilUsuarioDTO MapPerfilUsuarioDTO(Usuario u, PreferenciasUsuario p, RestriccionesUsuario r)
        {
            return new PerfilUsuarioDTO
            {
                Usuario = MapUsuarioDTO(u),
                Preferencias = MapPreferenciasUsuarioDTO(p),
                Restricciones = MapRestriccionesUsuarioDTO(r)
            };
        }

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

        private static PreferenciasUsuarioDTO MapPreferenciasUsuarioDTO(PreferenciasUsuario p)
        {
            return new PreferenciasUsuarioDTO
            {
                Id = p.Id,
                RitmoVisitaPreferido = p.RitmoVisitaPreferido,
                TransportesPreferidos = p.TransportesPreferidos,
                Categorias = p.Categorias
                    .Select(c => new PreferenciasCategoriaDTO
                    {
                        Id = c.Id,
                        CategoriaId = c.CategoriaId,
                        Afinidad = c.Afinidad
                    }).ToList()
            };
        }

        private static RestriccionesUsuarioDTO MapRestriccionesUsuarioDTO(RestriccionesUsuario r)
        {
            return new RestriccionesUsuarioDTO
            {
                Id = r.Id,
                Dieta = r.Dieta,
                RequiereRecorridoSinEscalones = r.RequiereRecorridoSinEscalones,
                RequiereBanoAccesible = r.RequiereBanoAccesible,
            };
        }
    }
}
