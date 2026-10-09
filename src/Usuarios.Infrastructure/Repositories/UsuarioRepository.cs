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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Usuarios.Infrastructure.Repositories
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Usuario>> GetAllUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioById(Guid id)
        {
            return await _context.Usuarios
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario?> GetUsuarioByGoogleId(string googleId)
        {
            return await _context.Usuarios
                .Where(u => u.GoogleId == googleId)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> Add(Usuario usuario)
        {
            await _context.AddAsync(usuario);
            await SaveChangesAsync();

            return usuario;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
