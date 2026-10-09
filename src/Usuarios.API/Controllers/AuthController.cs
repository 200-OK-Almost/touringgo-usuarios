using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;
namespace SubastaYa.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        [HttpGet("google")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = "/auth/google/callback"
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        /// <summary>
        /// Método ejecutado cuando el usuario completa la autenticación con exito
        /// </summary>
        /// <returns></returns>
        [HttpGet("google/callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded || result.Principal is null)
            {
                return Unauthorized("Google authentication failed.");
            }

            // Leer retorno de google con informacion del usuario (Google ID, nombre, email, etc.)
            var claims = result.Principal.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            });

            GoogleUsuarioInfoDTO dto = new GoogleUsuarioInfoDTO
            {
                GoogleId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier)!,
                Email = result.Principal.FindFirstValue(ClaimTypes.Email),
                Nombre = result.Principal.FindFirstValue(ClaimTypes.Name),

                // La imagen de perfil no esta disponible.
                //FotoUrl = result.Principal.FindFirstValue(ClaimTypes.Picture)
            };

            var usuario = await _usuarioService.GetOrCreateUsuarioGoogleAsync(dto);

            return Ok(usuario);
        }
    }
}
