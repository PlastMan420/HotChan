using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotChan.DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HotChanApi.Controller;

[AllowAnonymous]
public class AuthController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private IConfiguration _configuration { get; set; }

	public AuthController(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager)
	{
		this._userManager = userManager;
		this._signInManager = signInManager;
		this._configuration = configuration;
	}

	[HttpPost]
	public async Task<IActionResult> Login(UserAuth userAuth)
	{
		User user = await _userManager.FindByEmailAsync(userAuth.UserMail);
		if(user.Id != Guid.Empty)
		{
			var pwAuth = await _signInManager.CheckPasswordSignInAsync(user, userAuth.Key, false);
			if (pwAuth.Succeeded)
			{
				return Ok(new
				{
					token = GenerateJwtToken(user)
				});
			}
		}
		else
		{
			user.Email = userAuth.UserMail;
			user.UserName = userAuth.UserName;
			var result = _userManager.CreateAsync(user, userAuth.Key);

			if (result.IsCompletedSuccessfully)
				return CreatedAtRoute("GetUser", new { controller = "Users", id = user.Id });
			else
				return BadRequest(result.Exception);
		}
	}

	private async Task<string> GenerateJwtToken(User user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.UserName),
		};

		var roles = await _userManager.GetRolesAsync(user);

		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		var key = new AsymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:rs512-public"]));

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.Now.AddDays(1),
			SigningCredentials = creds
		};

		var tokenHandler = new JwtSecurityTokenHandler();

		var token = tokenHandler.CreateToken(tokenDescriptor);

		//var user = _mapper.Map<UserForListDto>(userFromRepo);
	}
}
