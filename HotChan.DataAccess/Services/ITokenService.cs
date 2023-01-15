using HotChan.DataBase.Models.Entities;
using HotChan.DataBase.Models.Requests;
using HotChan.DataBase.Models.Results;

namespace HotChan.DataAccess.Services;

public interface ITokenService
{
    public string GenerateJwtToken(User user);
    public Task<AuthResult> VerifyToken(TokenRequest tokenRequest);
}
