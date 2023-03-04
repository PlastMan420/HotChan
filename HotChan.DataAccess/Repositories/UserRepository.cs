using AutoMapper;
using HotChan.DataBase.Models.Entities;
using HotChan.DataBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using HotChan.DataBase.Models.Dtos;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HotChan.DataAccess.Repositories;

public class UserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    //private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly HotChanContext _hotChanContext;
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _configuration;
    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, HotChanContext hotChanContext, IMapper mapper, RoleManager<Role> roleManager, IConfiguration configuration)
    {
        _hotChanContext = hotChanContext;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [AllowAnonymous]
    public async Task<string> Login(UserLoginDto userAuth)
    {
        DateTime jwtDate = DateTime.Now;

        User? user = await _userManager.FindByNameAsync(userAuth.Name);

        if (user == null || user.Id == Guid.Empty)
        {
            var msg = "User not found";
           // _logger.LogError(msg);
            return null;
        }
        var saltedKey = new StringBuilder(userAuth.Key);
        saltedKey.Append(user.Salt);

        var pwAuth = await _signInManager.CheckPasswordSignInAsync(user, saltedKey.ToString(), false);

        if (pwAuth.Succeeded)
        {
            return await LoginData(user);
        }
        else
        {
            var msg = "Login Error";
            //_logger.LogError(msg);
            return null;
        }
    }

    public async Task<string> Register(UserRegisterFormDto userAuth)
    {
        var normalisedName = userAuth.UserName.ToUpper();
        var isDuplicate = await _userManager.FindByNameAsync(normalisedName);

        if(isDuplicate != null)
        {
            return "DUP";
        }

        // create user
        User user = new User();

        user.Email = userAuth.UserMail;
        user.UserName = userAuth.UserName;
        user.Salt = Nanoid.Nanoid.Generate(size: 20);
        user.RegisterationDate = DateTime.UtcNow;

        var pepperKey = new StringBuilder(userAuth.Key);
        pepperKey.Append(user.Salt);

        try
        {
            var result = await _userManager.CreateAsync(user, pepperKey.ToString());

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }
            
            var newUserFromDb = await _userManager.FindByNameAsync(normalisedName);
            await _userManager.AddToRoleAsync(newUserFromDb, "User");

            return await LoginData(newUserFromDb);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        
        

        //register role
        //user.UserRole = _roleManager.Roles.FirstOrDefault(x => x.eRoleId == Enums.eRole.User);

    }


    public async Task<User> GetUserById(Guid userId)
    {
        User user = await _hotChanContext.Users.Where(x => x.Id == userId).Select(x =>
            new User()
            {
                Id = x.Id,
                UserName = x.UserName,
                Avatar = x.Avatar,
                Posts = x.Posts,
                RegisterationDate = x.RegisterationDate,
            }
        ).FirstOrDefaultAsync();

        if (user == null) { return null; }

        return user;
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

        var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    private async Task<string> LoginData(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
            {
                // jti claim is used by refresh tokens.
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),

            };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GetToken(authClaims);
        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}
