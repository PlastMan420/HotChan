using HotChan.Api.Schema.Types;
using HotChan.DataAccess.Repositories;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HotChan.Api.Schema.Data;
public class HotChanQuery
{
    public HotChanQuery() { }

    public Task<List<Post>> GetPostCatalog([Service] PostRepository postRepo) => postRepo.GetPostsforCatalog();
    public Task<Post> GetPost([Service] PostRepository postRepo, Guid postId) => postRepo.GetPostById(postId);

    [AllowAnonymous]
    public async Task<string> Login([Service] UserRepository usrRepo, UserLoginDto login) => await usrRepo.Login(login);

    //public User GetMe(ClaimsPrincipal claimsPrincipal)
    //{
    //    var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    //}
}
