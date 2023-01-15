using HotChan.DataBase.Models.Entities;
using HotChan.DataBase.Models.Requests;
using HotChan.DataBase.Models.Results;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotChan.Api.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(
    IConfiguration configuration
    )
    {
        this._configuration = configuration;
    }

    public string GenerateJwtToken(User user, List<UserRole> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        foreach(var role in roles )
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
        }

        var secret = _configuration["JWT:Key"];
        if(string.IsNullOrEmpty(secret))
        {
            throw new Exception("broken token generated");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:JwtExpireDays"]));

        var token = new JwtSecurityToken(
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            claims,
            expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Task<AuthResult> VerifyToken(TokenRequest tokenRequest)
    {
        throw new NotImplementedException();
    }
}
