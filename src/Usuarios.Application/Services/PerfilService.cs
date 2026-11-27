using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Perfil;
using Usuarios.Application.DTOs.Preferencias;
using Usuarios.Application.DTOs.Restricciones;
using Usuarios.Application.Interfaces.Repositories;
using Usuarios.Application.Interfaces.Services;
using Usuarios.Domain;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;
using Usuarios.Domain.Exceptions;

namespace Usuarios.Application.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPreferenciasRepository _preferenciasRepository;
        private readonly IRestriccionesRepository _restriccionesRepository;
        public PerfilService(IPreferenciasRepository preferenciasRepository, IRestriccionesRepository restriccionesRepository)
        {
            _preferenciasRepository = preferenciasRepository;
            _restriccionesRepository = restriccionesRepository;
        }

        public async Task<PreferenciasUsuarioDTO> ActualizarPreferenciasByUsuarioId(
            Guid usuarioId,
            ActualizarPreferenciasUsuarioDTO dto
        )
        {
            var preferencias = await _preferenciasRepository.GetByUsuarioId(usuarioId);

            if (preferencias is null)
            {
                throw new NotFoundException($"Preferencias del usuario con ID {usuarioId} no encontradas");
            }

            // actualizar los campos si se envian en el dto, si no, dejar valores existente
            if (dto.RitmoVisitaPreferido is not null)
            {
                if (!Enum.IsDefined(typeof(RitmoVisita), dto.RitmoVisitaPreferido))
                {
                    throw new DominioException($"El tipo de RitmoVisita '{dto.RitmoVisitaPreferido}' no es válido.");
                }

                preferencias.RitmoVisitaPreferido = dto.RitmoVisitaPreferido.Value;
            }

            // si se envía una lista, reemplazarla por la existente
            if (dto.TransportesPreferidos is not null)
            {
                if (dto.TransportesPreferidos.Count <= 0)
                {
                    throw new DominioException($"Debe existir al menos un método de transporte preferido");
                }

                if (dto.TransportesPreferidos.Any(d => !Enum.IsDefined(typeof(TipoTransporte), d)))
                {
                    throw new DominioException("Uno o más tipos de transporte no son válidos.");
                }

                preferencias.TransportesPreferidos = dto.TransportesPreferidos;
            }

            if (dto.Categorias is not null)
            {
                foreach (var preferenciaCategoria in dto.Categorias)
                {
                    if (string.IsNullOrWhiteSpace(preferenciaCategoria.CategoriaCodigo) ||
                        !CategoriasLugar.Codigos.Contains(preferenciaCategoria.CategoriaCodigo))
                        throw new DominioException($"El código de categoría '{preferenciaCategoria.CategoriaCodigo}' no es válido.");

                    if (preferenciaCategoria.Afinidad < -2 || preferenciaCategoria.Afinidad > 2)
                        throw new DominioException($"El valor de afinidad para una categoría debe estar entre -2 y 2");

                    var existente = preferencias.Categorias
                        .FirstOrDefault(c => c.CategoriaCodigo == preferenciaCategoria.CategoriaCodigo);

                    if (existente is null)
                    {
                        // si la categoría recibida no existe, agregarla
                        var nuevaPreferenciaCategoria = new PreferenciasCategoria
                        {
                            PreferenciasUsuarioId = preferencias.Id,
                            CategoriaCodigo = preferenciaCategoria.CategoriaCodigo,
                            Afinidad = preferenciaCategoria.Afinidad
                        };

                        await _preferenciasRepository.AddPreferenciaCategoria(nuevaPreferenciaCategoria);
                    }
                    else
                    {
                        // si ya existe, actualizar su valor de afinidad.
                        existente.Afinidad = preferenciaCategoria.Afinidad;
                    }
                }
            }
            await _preferenciasRepository.SaveChangesAsync();

            return MapPreferenciaUsuarioDTO(preferencias);
        }

        public async Task<RestriccionesUsuarioDTO> ActualizarRestriccionesByUsuarioId(Guid usuarioId, ActualizarRestriccionesUsuarioDTO dto)
        {
            var restricciones = await _restriccionesRepository.GetByUsuarioId(usuarioId);

            if (restricciones is null)
            {
                throw new NotFoundException($"Restricciones del usuario con ID {usuarioId} no encontradas");
            }

            // actualizar los campos si se envian en el dto, si no, dejar valores existente
            if (dto.RequiereRecorridoSinEscalones is not null)
                restricciones.RequiereRecorridoSinEscalones = dto.RequiereRecorridoSinEscalones.Value;

            if (dto.RequiereBanoAccesible is not null)
                restricciones.RequiereBanoAccesible = dto.RequiereBanoAccesible.Value;

            // si se envía una lista, reemplazarla por la existente
            if (dto.Dieta is not null)
            {
                if (dto.Dieta.Any(d => !Enum.IsDefined(typeof(TipoDieta), d)))
                {
                    throw new DominioException("Uno o más tipos de dietas no son válidos.");
                }

                restricciones.Dieta = dto.Dieta;
            }

            await _restriccionesRepository.SaveChangesAsync();

            return MapRestriccionesUsuarioDTO(restricciones);
        }

        public async Task ActualizarPerfilByUsuarioId(
            Guid usuarioId,
            ActualizarPerfilUsuarioDTO dto)
        {
            var preferencias = await _preferenciasRepository.GetByUsuarioId(usuarioId);
            var restricciones = await _restriccionesRepository.GetByUsuarioId(usuarioId);

            if (preferencias is null)
                throw new NotFoundException(
                    $"Preferencias del usuario con ID {usuarioId} no encontradas");

            if (restricciones is null)
                throw new NotFoundException(
                    $"Restricciones del usuario con ID {usuarioId} no encontradas");

            // Validaciones
            ValidarPreferenciasPerfil(dto.Preferencias);
            ValidarRestriccionesPerfil(dto.Restricciones);

            // Actualizar preferencias generales
            preferencias.RitmoVisitaPreferido =
                dto.Preferencias.RitmoVisitaPreferido;

            preferencias.TransportesPreferidos =
                dto.Preferencias.TransportesPreferidos;

            // Actualizar afinidades
            foreach (var categoriaDto in dto.Preferencias.Categorias)
            {
                var existente = preferencias.Categorias
                    .First(c => c.CategoriaCodigo == categoriaDto.CategoriaCodigo);

                existente.Afinidad = categoriaDto.Afinidad;
            }

            // Actualizar restricciones
            restricciones.Dieta = dto.Restricciones.Dieta;
            restricciones.RequiereRecorridoSinEscalones =
                dto.Restricciones.RequiereRecorridoSinEscalones;
            restricciones.RequiereBanoAccesible =
                dto.Restricciones.RequiereBanoAccesible;

            await _preferenciasRepository.SaveChangesAsync();
        }
        // --- Validaciones ---

        private static void ValidarPreferenciasPerfil(
            ActualizarPreferenciasPerfilDTO dto)
        {
            if (!Enum.IsDefined(typeof(RitmoVisita), dto.RitmoVisitaPreferido))
                throw new DominioException("El ritmo de visita no es válido.");

            if (dto.TransportesPreferidos is null ||
                dto.TransportesPreferidos.Count == 0)
                throw new DominioException(
                    "Debe existir al menos un método de transporte preferido.");

            if (dto.TransportesPreferidos.Any(
                t => !Enum.IsDefined(typeof(TipoTransporte), t)))
                throw new DominioException(
                    "Uno o más tipos de transporte no son válidos.");

            if (dto.Categorias is null)
                throw new DominioException("Las categorías son obligatorias.");

            foreach (var categoria in dto.Categorias)
            {
                if (string.IsNullOrWhiteSpace(categoria.CategoriaCodigo) ||
                    !CategoriasLugar.Codigos.Contains(categoria.CategoriaCodigo))
                    throw new DominioException(
                        $"El código de categoría '{categoria.CategoriaCodigo}' no es válido.");

                if (categoria.Afinidad < -2 || categoria.Afinidad > 2)
                    throw new DominioException(
                        "El valor de afinidad debe estar entre -2 y 2.");
            }

            // Exigir exactamente las categorías definidas en el dominio.
            var codigosRecibidos = dto.Categorias
                .Select(c => c.CategoriaCodigo)
                .ToList();

            if (codigosRecibidos.Distinct().Count() != codigosRecibidos.Count ||
                !CategoriasLugar.Codigos.ToHashSet()
                    .SetEquals(codigosRecibidos))
            {
                throw new DominioException(
                    "Debe enviarse exactamente una preferencia por cada categoría.");
            }
        }

        private static void ValidarRestriccionesPerfil(
            ActualizarRestriccionesPerfilDTO dto)
        {
            if (dto.Dieta is null)
                throw new DominioException("La lista de dietas es obligatoria.");

            if (dto.Dieta.Any(d => !Enum.IsDefined(typeof(TipoDieta), d)))
                throw new DominioException(
                    "Uno o más tipos de dieta no son válidos.");
        }


        // --- Helpers --- 

        private static PreferenciasUsuarioDTO MapPreferenciaUsuarioDTO(PreferenciasUsuario p)
        {
            return new PreferenciasUsuarioDTO
            {
                Id = p.Id,
                RitmoVisitaPreferido = p.RitmoVisitaPreferido,
                TransportesPreferidos = p.TransportesPreferidos,
                Categorias = p.Categorias
                    .Select(p => MapPreferenciaCategoriaDTO(p))
                    .ToList()
            };
        }

        private static PreferenciasCategoriaDTO MapPreferenciaCategoriaDTO(PreferenciasCategoria p)
        {
            return new PreferenciasCategoriaDTO
            {
                Id = p.Id,
                CategoriaCodigo = p.CategoriaCodigo,
                Afinidad = p.Afinidad
            };
        }
        private static RestriccionesUsuarioDTO MapRestriccionesUsuarioDTO(RestriccionesUsuario r)
        {
            return new RestriccionesUsuarioDTO
            {
                Id = r.Id,
                Dieta = r.Dieta,
                RequiereRecorridoSinEscalones = r.RequiereRecorridoSinEscalones,
                RequiereBanoAccesible = r.RequiereBanoAccesible
            };
        }
    }
}
