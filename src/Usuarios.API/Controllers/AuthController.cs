using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Usuarios;
using Usuarios.Application.Interfaces.Services;
using Usuarios.Application.Services;
namespace SubastaYa.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;
        private readonly IAuthCodeStore _authCodeStore;
        private readonly IConfiguration _configuration;

        public AuthController(IUsuarioService usuarioService, IAuthService authService, IAuthCodeStore authCodeStore, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _authService = authService;
            _authCodeStore = authCodeStore;
            _configuration = configuration;
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
            string code = _authCodeStore.Create(response);

            string frontendUrl = _configuration["Frontend:BaseUrl"] 
                                ?? throw new InvalidOperationException("Frontend:BaseUrl is not configured.");

            string redirectUrl = $"{frontendUrl.TrimEnd('/')}/auth/callback?code={Uri.EscapeDataString(code)}";

            return Redirect(redirectUrl);
        }
        public sealed record ExchangeCodeRequest(string Code);

        [HttpPost("exchange")]
        public async Task<IActionResult> Exchange(
            [FromBody] ExchangeCodeRequest request)
        {
            var authResponse = _authCodeStore.Consume(request.Code);

            if (authResponse is null)
                return Unauthorized();

            return Ok(authResponse);
        }
    }
}
