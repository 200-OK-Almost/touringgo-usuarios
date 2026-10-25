
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Usuarios.Application.DTOs.Usuarios;
using Usuarios.Application.Interfaces.Services;

public sealed class InMemoryAuthCodeStore : IAuthCodeStore
{
    private static readonly TimeSpan CodeLifetime =
        TimeSpan.FromSeconds(60);

    private readonly ConcurrentDictionary<string, CodeEntry> _codes = new();

    public string Create(LoginResponseDTO response)
    {
        // Remove expired entries to prevent unredeemed codes
        // from accumulating indefinitely.
        RemoveExpiredCodes();

        while (true)
        {
            string code = GenerateCode();

            var entry = new CodeEntry(
                response,
                DateTimeOffset.UtcNow.Add(CodeLifetime));

            if (_codes.TryAdd(code, entry))
                return code;
        }
    }

    public LoginResponseDTO? Consume(string code)
    {
        // TryRemove makes redemption single-use and atomic.
        if (!_codes.TryRemove(code, out var entry))
            return null;

        if (entry.ExpiresAt <= DateTimeOffset.UtcNow)
            return null;

        return entry.Response;
    }

    private void RemoveExpiredCodes()
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var pair in _codes)
        {
            if (pair.Value.ExpiresAt <= now)
                _codes.TryRemove(pair.Key, out _);
        }
    }

    private static string GenerateCode()
    {
        byte[] randomBytes = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(randomBytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    private sealed record CodeEntry(
        LoginResponseDTO Response,
        DateTimeOffset ExpiresAt);
}
