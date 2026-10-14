using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Interfaces;
using Usuarios.Domain.Entities;
using Usuarios.Infrastructure.Data;

namespace Usuarios.Infrastructure.Repositories
{
    public class PreferenciasRepository : IPreferenciasRepository
    {
        private readonly ApplicationDbContext _context;
        public PreferenciasRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PreferenciasUsuario> AddPreferencias(PreferenciasUsuario preferenciasUsuario)
        {
            await _context.AddAsync(preferenciasUsuario);

            return preferenciasUsuario;
        }

        public async Task<RestriccionesUsuario> AddRestricciones(RestriccionesUsuario restriccionesUsuario)
        {
            await _context.AddAsync(restriccionesUsuario);

            return restriccionesUsuario;
        }

        public async Task<PreferenciasCategoria> AddPreferenciaCategoria(PreferenciasCategoria preferenciasCategoria)
        {
            await _context.AddAsync(preferenciasCategoria);
            return preferenciasCategoria;
        }

        public async Task<PreferenciasUsuario?> GetPreferenciasUsuarioByUsuarioId(Guid usuarioId)
        {
            return await _context.PreferenciasUsuarios
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync();
        }

        public async Task<RestriccionesUsuario?> GetRestriccionesUsuarioByUsuarioId(Guid usuarioId)
        {
            return await _context.RestriccionesUsuarios
                .Where(p => p.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
