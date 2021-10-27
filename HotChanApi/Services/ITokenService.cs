using HotChan.DataBase.Models.Entities;
using HotChan.DataBase.Models.Requests;
using HotChan.DataBase.Models.Results;
using System;
using System.Threading.Tasks;

namespace HotChanApi.Services
{
    public interface ITokenService
    {
        public Task<AuthResult> GenerateJwtToken(User user, DateTime jwtDate);
        public Task<AuthResult> VerifyToken(TokenRequest tokenRequest);

    }
}
