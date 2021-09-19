using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
		DateTime jwtDate = DateTime.Now;

		User user = await _userManager.FindByEmailAsync(userAuth.UserMail);
		if(user.Id != Guid.Empty)
		{
			var pwAuth = await _signInManager.CheckPasswordSignInAsync(user, userAuth.Key, false);
			if (pwAuth.Succeeded)
			{
				return Ok(new
				{
					token = GenerateJwtToken(user, jwtDate),
					unixTimeExpiresAt = new DateTimeOffset(jwtDate).ToUnixTimeMilliseconds(),
				});
			}
			else
				return BadRequest("Login Error");
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

	private async Task<string> GenerateJwtToken(User user, DateTime jwtDate)
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

		using RSA rsa = RSA.Create();
		rsa.ImportRSAPrivateKey( // Convert the loaded key from base64 to bytes.
				source: Convert.FromBase64String(_configuration["jwt:rs512-private"]), // Use the private key to sign tokens
				bytesRead: out int _); // Discard the out variable

		var signingCredentials = new SigningCredentials(
						key: new RsaSecurityKey(rsa),
						algorithm: SecurityAlgorithms.RsaSha512 // Important to use RSA version of the SHA algo 
					);

		var jwt = new JwtSecurityToken(
				audience: "jwt-test",
				issuer: "jwt-test",
				claims: claims,
				notBefore: jwtDate,
				expires: jwtDate.AddDays(3),
				signingCredentials: signingCredentials
			);

		string token = new JwtSecurityTokenHandler().WriteToken(jwt);
		return token;
	}

	[HttpGet]
	[Authorize(AuthenticationSchemes = "Asymmetric")] // Use the "Asymmetric" authentication scheme
	public IActionResult ValidateTokenAsymmetric()
	{
		return Ok();
	}
}
