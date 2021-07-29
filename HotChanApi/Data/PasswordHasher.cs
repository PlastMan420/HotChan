using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace HotChanApi.Data
{
	public class PasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
	{
		private readonly IConfiguration _config;

		public PasswordHasher(IConfiguration config)
		{
			_config = config;
		}

		public string HashPassword(TUser user, string password)
		{
			//It’s expected that this password hash contains everything required to validate the password.
			//In other words, it should include any versioning information, the salt, and, of course, the password hash itself.
			
			// the password paramter is salted.
			string pepper = _config["auth:pepper"];
		
			return Argon2.Hash(pepper + password);
		}

		public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
		{
			// the providedPAssword parameter is salted
			string pepper = _config["auth:pepper"];

			var isValid = Argon2.Verify(hashedPassword, providedPassword + pepper);


			return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
		}
	}

}
