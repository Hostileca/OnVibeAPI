using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.Token;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Implementations;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public TokensReadDto GenerateTokens(User user, CancellationToken cancellationToken)
    {
        return new TokensReadDto
        {
            AccessToken = GenerateAccessToken(user),
            RefreshToken = GenerateRefreshToken()
        };
    }
    
    private Token GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenExpires = DateTime.UtcNow.AddMinutes(GetJwtSetting<double>("AccessTokenExpiresInMinutes"));
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetJwtSetting<string>("Key")));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: tokenExpires,
            issuer: GetJwtSetting<string>("Issuer"),
            signingCredentials: signingCredentials);

        return new Token
        {
            Value = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Expiration = tokenExpires
        };
    }

    private Token GenerateRefreshToken()
    {
        return new Token
        {
            Value = Guid.NewGuid().ToString(),
            Expiration = DateTime.UtcNow.AddMinutes(GetJwtSetting<double>("RefreshTokenExpiresInMinutes"))
        };
    }
    
    private T GetJwtSetting<T>(string key) => configuration.GetValue<T>($"JwtSettings:{key}");
}