using System;
using System.Threading.Tasks;
using HotChan.DataBase;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;
using HotChan.DataBase.Models.Requests;
using HotChan.DataBase.Models.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HotChanApi.Services;
using System.Collections.Generic;

namespace HotChanApi.Controller;

[AllowAnonymous]
public class AuthController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly TokenValidationParameters _tokenValidationParameters;
	private readonly HotChanContext _hotChanContext;
	private IConfiguration _configuration { get; set; }
	private readonly ITokenService _tokenService;
	public AuthController(
		IConfiguration configuration,
		UserManager<User> userManager,
		SignInManager<User> signInManager,
		TokenValidationParameters tokenValidationParameters,
		HotChanContext hotChanContext,
		ITokenService tokenService
)
	{
		this._userManager = userManager;
		this._signInManager = signInManager;
		this._configuration = configuration;
		this._tokenValidationParameters = tokenValidationParameters;
		this ._hotChanContext = hotChanContext;
		this._tokenService = tokenService;
	}

	//[HttpPost]
	//public async Task<IActionResult> Login(UserAuth userAuth)
	//{
	//	DateTime jwtDate = DateTime.Now;

	//	User user = await _userManager.FindByEmailAsync(userAuth.UserMail);
	//	if(user.Id != Guid.Empty)
	//	{
	//		var pwAuth = await _signInManager.CheckPasswordSignInAsync(user, userAuth.Key, false);
	//		if (pwAuth.Succeeded)
	//		{
	//			return Ok(new
	//			{
	//				token = _tokenService.GenerateJwtToken(user, jwtDate),
	//				unixTimeExpiresAt = new DateTimeOffset(jwtDate).ToUnixTimeMilliseconds(),
	//			});
	//		}
	//		else
	//			return BadRequest("Login Error");
	//	}
	//	else
	//	{
	//		user.Email = userAuth.UserMail;
	//		user.UserName = userAuth.UserName;
	//		var result = _userManager.CreateAsync(user, userAuth.Key);

	//		if (result.IsCompletedSuccessfully)
	//			return CreatedAtRoute("GetUser", new { controller = "Users", id = user.Id });
	//		else
	//			return BadRequest(result.Exception);
	//	}
	//}

	[HttpGet]
	[Authorize(AuthenticationSchemes = "Asymmetric")] // Use the "Asymmetric" authentication scheme
	public IActionResult ValidateTokenAsymmetric()
	{
		return Ok();
	}

	[HttpPost]
	[Route("RefreshToken")]
	public async Task<IActionResult> RefreshToken(TokenRequest tokenRequest)
	{
		if (!string.IsNullOrWhiteSpace(tokenRequest.Token) && !string.IsNullOrWhiteSpace(tokenRequest.RefreshToken))
		{
			var res = await _tokenService.VerifyToken(tokenRequest);

			if (res == null)
			{
				return BadRequest(new RegistrationResponse()
				{
					Errors = new List<string>() { "Invalid tokens" },
					Success = false
				});
			}

			return Ok(res);
		}

		return BadRequest(new RegistrationResponse()
		{
			Errors = new List<string>() { "Invalid payload" },
			Success = false
		});
	}


}
