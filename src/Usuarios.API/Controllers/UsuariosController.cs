using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;
namespace SubastaYa.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly IWebHostEnvironment _environment;

        public UsuariosController(IUsuarioService usuarioService, ITokenService tokenService, IWebHostEnvironment environment)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioById(Guid id)
        {
            var usuario = await _usuarioService.GetUsuarioById(id);
            return Ok(usuario);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var usuario = await _usuarioService.GetUsuarioById(userId);

            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }

        [Authorize]
        [HttpGet("me/perfil")]
        public async Task<IActionResult> GerPefilCurrentUser()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var usuarioPerfil = await _usuarioService.GetPerfilUsuarioByUsuarioId(userId);

            if (usuarioPerfil is null)
                return NotFound();

            return Ok(usuarioPerfil);
        }

        /// <summary>
        /// Endpoint para obtener un token de autenticación de un usuario fijo de prueba.
        /// Solo para desarrollo.
        /// </summary>
        /// <returns></returns>
        [HttpPost("dev/auth/token")]
        public async Task<ActionResult> EmitDevTestToken()
        {
            // No registrar endpoint fuera de desarrollo.
            if (!_environment.IsDevelopment())
                return NotFound();

            var usuarioDevId = Guid.Parse("11111111-1111-1111-1111-111111111112");
            var usuario = await _usuarioService.GetUsuarioById(usuarioDevId);

            var token = _tokenService.GenerateToken(usuario);

            return Ok(token);
        }
    }
}
