using AutoMapper;
using HotChan.DataBase.Models.Entities;
using HotChan.DataBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using HotChan.DataBase.Models.Dtos;
using System.Text;

namespace HotChan.DataAccess.Repositories;

public class UserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    //private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly HotChanContext _hotChanContext;
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleManager;
    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, HotChanContext hotChanContext, IMapper mapper, RoleManager<Role> roleManager)
    {
        _hotChanContext = hotChanContext;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }


    public async Task<User?> Login(UserAuth userAuth)
    {
        DateTime jwtDate = DateTime.Now;

        User user = await _userManager.FindByEmailAsync(userAuth.UserMail);

        if (user == null || user.Id == Guid.Empty)
        {
            var msg = "User not found";
           // _logger.LogError(msg);
            return null;
        }

        else
        {
            var pwAuth = await _signInManager.CheckPasswordSignInAsync(user, userAuth.Key, false);
            if (pwAuth.Succeeded)
            {
                return user;
            }
            else
            {
                var msg = "Login Error";
                //_logger.LogError(msg);
                return null;
            }
        }
    }

    public async Task<bool> Register(UserAuth userAuth)
    {
        // create user
        User user = new();

        user.Email = userAuth.UserMail;
        user.UserName = userAuth.UserName;
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.Id = Guid.NewGuid();
        user.Salt = "asdsdfasdf";
        var result = _userManager.CreateAsync(user, userAuth.Key);

        //register role
        //user.UserRole = _roleManager.Roles.FirstOrDefault(x => x.eRoleId == Enums.eRole.User);

        var newUserFromDb = await _userManager.FindByIdAsync(user.Id.ToString());
        await _userManager.AddToRoleAsync(newUserFromDb, "USER");

        return result.IsCompletedSuccessfully;
    }


    public async Task<User> GetUserById(Guid userId)
    {
        return await _hotChanContext.Users.FindAsync(userId);
    }
}
