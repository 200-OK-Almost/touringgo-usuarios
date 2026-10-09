using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Usuarios.Application.DTOs;
using Usuarios.Application.Interfaces;
using Usuarios.Application.Services;
namespace SubastaYa.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;

        public AuthController(IUsuarioService usuarioService, IAuthService authService)
        {
            _usuarioService = usuarioService;
            _authService = authService;
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

            var principal = result.Principal;

            var googleId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = principal.FindFirstValue(ClaimTypes.Email);

            // Validate the required claims BEFORE using them.
            if (string.IsNullOrWhiteSpace(googleId))
            {
                return Unauthorized("Google did not provide a valid identifier.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return Unauthorized("Google did not provide an email address.");
            }

            var dto = new GoogleUsuarioInfoDTO
            {
                GoogleId = googleId,
                Email = email,
                Nombre = principal.FindFirstValue(ClaimTypes.GivenName)
            };

            var response = await _authService.LoginWithGoogleAsync(dto);

            return Ok(response);
        }
    }
}
