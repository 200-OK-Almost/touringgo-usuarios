using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Usuarios;
using Usuarios.Application.Interfaces.Services;
using Usuarios.Application.Services;
namespace Usuarios.API.Controllers
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

        /// <summary>
        /// Endpoint para obtener un token de autenticación de un usuario fijo de prueba.
        /// Solo para desarrollo.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
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

        [HttpPost("batch")]
        public async Task<ActionResult<List<UsuarioResumenDTO>>> ObtenerPerfiles(
            [FromBody] ObtenerUsuariosResumenRequest request,
            CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioService.ObtenerResumenesAsync(
                request.UsuariosIds,
                cancellationToken);

            return Ok(usuarios);
        }

        [Authorize(Policy = "RequiereUsuarioId")]
        [HttpPatch("me/encuesta")]
        public async Task<IActionResult> CompletarEncuesta()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdValue, out var userId))
                return Unauthorized();

            await _usuarioService.CompletarEncuestaAsync(userId);

            return NoContent();
        }
    }
}
