using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;
using Usuarios.Domain.Entities;
using Usuarios.Infrastructure.Data;

namespace Usuarios.Infrastructure.Repositories
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<UsuarioDTO>> GetAllUsuarios()
        {
            return await _context.Usuarios
                .Select(u => MapUsuarioDTO(u)).ToListAsync();
        }

        public async Task<UsuarioDTO?> GetUsuarioById(Guid id)
        {
            return await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => MapUsuarioDTO(u))
                .FirstOrDefaultAsync();
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
                Apellido = u.Apellido,
                FotoUrl = u.FotoUrl,
                FechaCreacion = u.FechaCreacion,
                UltimoAcceso = u.UltimoAcceso,
            };
        }
    }
}
