using HotChan.Api.Schema.Types;
using HotChan.DataAccess.Repositories;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;
using HotChocolate.Authorization;
using System.Security.Claims;

namespace HotChan.Api.Schema.Data;
public class HotChanQuery
{
    public HotChanQuery() { }

    public Task<List<Post>> GetPostCatalog([Service] PostRepository postRepo) => postRepo.GetPostsforCatalog();
    public Task<Post> GetPost([Service] PostRepository postRepo, Guid postId) => postRepo.GetPostById(postId);

    public async Task<string> Login([Service] UserRepository usrRepo, UserLoginDto login) => await usrRepo.Login(login);

    [Authorize]
    public async Task<User> GetUserDetailsPageData([Service] UserRepository usrRepo, Guid userId) => await usrRepo.GetUserById(userId);

    //public User GetMe(ClaimsPrincipal claimsPrincipal)
    //{
    //    var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    //}
}
