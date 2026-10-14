using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;
using Usuarios.Domain.Entities;
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
                preferencias.RitmoVisitaPreferido = dto.RitmoVisitaPreferido.Value;

            // si se envía una lista, reemplazarla por la existente
            if (dto.TransportesPreferidos is not null)
            {
                if (dto.TransportesPreferidos.Count <= 0)
                    throw new DominioException($"Debe existir al menos un método de transporte preferido");

                preferencias.TransportesPreferidos = dto.TransportesPreferidos;
            }

            if (dto.Categorias is not null)
            {
                foreach (var preferenciaCategoria in dto.Categorias)
                {
                    var existente = preferencias.Categorias
                        .FirstOrDefault(c => c.CategoriaId == preferenciaCategoria.CategoriaId);

                    if (preferenciaCategoria.Afinidad < 1 || preferenciaCategoria.Afinidad > 5)
                        throw new DominioException($"El valor de afinidad para una categoría debe estar entre 1 y 5");

                    if (existente is null)
                    {
                        // si la categoría recibida no existe, agregarla
                        var nuevaPreferenciaCategoria = new PreferenciasCategoria
                        {
                            PreferenciasUsuarioId = preferencias.Id,
                            CategoriaId = preferenciaCategoria.CategoriaId,
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
                restricciones.Dieta = dto.Dieta;
            }

            await _restriccionesRepository.SaveChangesAsync();

            return MapRestriccionesUsuarioDTO(restricciones);
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
                CategoriaId = p.CategoriaId,
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
