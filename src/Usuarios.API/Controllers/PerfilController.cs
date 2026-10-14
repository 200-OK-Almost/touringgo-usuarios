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
    [Route("api/me")]
    public class PerfilController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;

        public PerfilController(IUsuarioService usuarioService, ITokenService tokenService, IWebHostEnvironment environment, IPerfilService perfilService)
        {
            _usuarioService = usuarioService;
            _perfilService = perfilService;
        }

        [Authorize]
        [HttpGet]
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
        [HttpGet("perfil")]
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

        [Authorize]
        [HttpPatch("preferencias")]
        public async Task<IActionResult> ActualizarPreferencias(
            [FromBody] ActualizarPreferenciasUsuarioDTO dto)
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(claim, out var usuarioId))
                return Unauthorized();

            var preferencias = await _perfilService.ActualizarPreferenciasByUsuarioId(usuarioId, dto);

            return Ok(preferencias);
        }

        [Authorize]
        [HttpPatch("restricciones")]
        public async Task<IActionResult> ActualizarRestricciones(
            [FromBody] ActualizarRestriccionesUsuarioDTO dto)
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(claim, out var usuarioId))
                return Unauthorized();

            var restricciones = await _perfilService.ActualizarRestriccionesByUsuarioId(usuarioId, dto);

            return Ok(restricciones);
        }
    }
}
