using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Interfaces.Repositories;
using Usuarios.Domain.Entities;
using Usuarios.Infrastructure.Data;

namespace Usuarios.Infrastructure.Repositories
{
    public class RestriccionesRepository : IRestriccionesRepository
    {
        private readonly ApplicationDbContext _context;
        public RestriccionesRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(RestriccionesUsuario restriccionesUsuario)
        {
            await _context.AddAsync(restriccionesUsuario);
        }

        public async Task<RestriccionesUsuario?> GetByUsuarioId(Guid usuarioId)
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
