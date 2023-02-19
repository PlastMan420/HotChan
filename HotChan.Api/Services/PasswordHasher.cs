using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace HotChan.Api.Services;

public class PasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
{
    private readonly IConfiguration _config;

    public PasswordHasher(IConfiguration config)
    {
        _config = config;
    }

    // pepper key salt //
    public string HashPassword(TUser user, string saltedPassword)
    {
        //It’s expected that this password hash contains everything required to validate the password.
        //In other words, it should include any versioning information, the salt, and, of course, the password hash itself.

        // the password paramter is salted.

        string pepperedKey = ApplyPepper(saltedPassword);

        return Argon2.Hash(pepperedKey);
    }

    public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string saltedPassword)
    {
        // the saltedPassword parameter is salted
        string pepperedKey = ApplyPepper(saltedPassword);

        var isValid = Argon2.Verify(hashedPassword, pepperedKey.ToString());

        return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }

    private string ApplyPepper(string saltedKey)
    {
        string pepper = _config["auth:pepper"];

        var pepperedKey = new StringBuilder(pepper);
        return pepperedKey.Append(saltedKey).ToString();
    }
}
