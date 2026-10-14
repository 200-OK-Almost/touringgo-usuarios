using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.DTOs.Usuarios;
using Usuarios.Application.Interfaces.Services;

namespace Usuarios.Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        public AuthService(IUsuarioService usuarioService, ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }
        public async Task<LoginResponseDTO> LoginWithGoogleAsync(GoogleUsuarioInfoDTO googleInfo)
        {
            var usuario = await _usuarioService.GetOrCreateUsuarioGoogleAsync(googleInfo);

            var token = _tokenService.GenerateToken(usuario);

            return new LoginResponseDTO
            {
                AccessToken = token.AccessToken,
                ExpiresAt = token.ExpiresAt,
                Usuario = usuario
            };
        }
    }
}
