using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Usuarios.Application.DTOs.Usuarios;
using Usuarios.Application.Interfaces.Services;

namespace SubastaYa.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public AuthController(
            IUsuarioService usuarioService,
            IAuthService authService,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _usuarioService = usuarioService;
            _authService = authService;
            _configuration = configuration;
            _environment = environment;
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
        /// Método ejecutado cuando el usuario completa la autenticación con éxito en Google.
        /// Emite una HttpOnly Cookie con el JWT generado y redirige al frontend.
        /// </summary>
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

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = !_environment.IsDevelopment(),
                SameSite = SameSiteMode.Lax,
                Expires = response.ExpiresAt,
                Path = "/"
            };

            Response.Cookies.Append("access_token", response.AccessToken, cookieOptions);

            string frontendUrl = _configuration["Frontend:BaseUrl"] 
                                ?? throw new InvalidOperationException("Frontend:BaseUrl is not configured.");

            string redirectUrl = $"{frontendUrl.TrimEnd('/')}/auth/callback";

            return Redirect(redirectUrl);
        }

        /// <summary>
        /// Obtiene la información del usuario autenticado a partir del JWT enviado en la cookie o header.
        /// Usado por el frontend para validar y persistir la sesión.
        /// </summary>
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

        /// <summary>
        /// Cierra la sesión eliminando la cookie HttpOnly.
        /// </summary>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token", new CookieOptions
            {
                HttpOnly = true,
                Secure = !_environment.IsDevelopment(),
                SameSite = SameSiteMode.Lax,
                Path = "/"
            });

            return NoContent();
        }
    }
}
