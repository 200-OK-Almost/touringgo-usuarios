using System.Net;
using System.Net.Http.Headers;

namespace Usuarios.UnitTests;

public class AutorizacionTests : IClassFixture<UsuariosApiFixture>
{
    private readonly UsuariosApiFixture _fixture;

    public AutorizacionTests(UsuariosApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SinToken_Devuelve401()
    {
        var client = _fixture.CreateClient();

        var response = await client.GetAsync("/api/me");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task TokenValidoSinSub_Devuelve403()
    {
        var client = _fixture.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", TestJwt.GenerarToken(incluirSub: false));

        var response = await client.GetAsync("/api/me");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task TokenValidoConSub_Devuelve200()
    {
        var client = _fixture.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", TestJwt.GenerarToken(
                incluirSub: true,
                sub: UsuariosApiFixture.UsuarioId.ToString()));

        var response = await client.GetAsync("/api/me");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
