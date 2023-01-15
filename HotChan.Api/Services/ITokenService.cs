using HotChan.DataBase.Models.Entities;
using HotChan.DataBase.Models.Requests;
using HotChan.DataBase.Models.Results;

namespace HotChan.Api.Services;

public interface ITokenService
{
    public string GenerateJwtToken(User user, List<UserRole> roles);
    public Task<AuthResult> VerifyToken(TokenRequest tokenRequest);
}
