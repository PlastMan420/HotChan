using HotChan.DataBase.Models.Entities;
using HotChan.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HotChan.DataBase.Models.Dtos;
using HotChan.Api.Services;
using AutoMapper;
using HotChan.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace HotChan.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    //private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly HotChanContext _hotChanContext;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly UserRepository _userRepo;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, HotChanContext hotChanContext, ITokenService tokenService, IMapper mapper, UserRepository userRepo)
	{
        _hotChanContext = hotChanContext;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
        _userRepo = userRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto userLogin)
    {
        var userToken = await _userRepo.Login(userLogin);
        
        if (userToken == null)
        {
            return BadRequest("Not Found");
        }

        //var token = _tokenService.GenerateJwtToken(user, user.UserRoles.ToList());
        return Ok(userToken);
    }

    //[HttpPost("register")]
    //public async Task<IActionResult> Register([FromBody]UserRegisterFormDto userAuth)
    //{
    //    var result = await _userRepo.Register(userAuth);

    //    if (result != null)
    //    {
    //        return Ok(result);
    //        //return CreatedAtRoute("GetUser", new { controller = "Users", id = user.Id });
    //    }
    //    else
    //    {
    //        return BadRequest("Registeration Failed");
    //    }
    //}

    [HttpGet]
    public async Task<IActionResult> GetUserInfo(Guid userId)
    {
        User user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return BadRequest("Not Found");
        }

        var userDetails = _mapper.Map<User, UserInfoDto>(user);

        return Ok(userDetails);
    }

    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Popo()
    {
        return Ok(201);
    }
}
