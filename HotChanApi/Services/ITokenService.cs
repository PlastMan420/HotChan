using HotChan.DataBase.Models.Entities;
using HotChan.DataBase.Models.Requests;
using HotChan.DataBase.Models.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotChanApi.Services
{
    public interface ITokenService
    {
        public Task<AuthResult> GenerateJwtToken(User user, List<string> roles);
        public Task<AuthResult> VerifyToken(TokenRequest tokenRequest);

    }
}
