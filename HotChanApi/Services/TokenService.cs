using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using HotChan.DataBase.Models.Requests;
using HotChan.DataBase.Models.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HotChanApi.Services;

public class TokenService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly HotChanContext _hotChanContext;
    private IConfiguration _configuration { get; set; }

    public TokenService(
    IConfiguration configuration,
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    TokenValidationParameters tokenValidationParameters,
    HotChanContext hotChanContext
    )
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._configuration = configuration;
        this._tokenValidationParameters = tokenValidationParameters;
        this._hotChanContext = hotChanContext;
    }

    private string GenerateJwtToken(User user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserName)
        };

        roles.ForEach(role =>
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        });

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:JwtExpireDays"]));

        var token = new JwtSecurityToken(
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
