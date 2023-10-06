using HotChan.Api.Schema.Types;
using HotChan.DataAccess.Repositories;
using HotChan.DataBase;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotChan.Api.Schema.Data;
public class HotChanQuery
{
    public HotChanQuery() { }

    //public Task<List<Post>> GetPostCatalog([Service] PostRepository postRepo) => postRepo.GetPostsforCatalog();

    [UsePaging(MaxPageSize = 20)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Post> Post(HotChanContext data) => data.Posts.Take(10);

    public async Task<string> Login([Service] UserRepository usrRepo, UserLoginDto login) => await usrRepo.Login(login);

    [Authorize]
    public async Task<User> GetUserDetailsPageData([Service] UserRepository usrRepo, Guid userId) => await usrRepo.GetUserById(userId);

    //public User GetMe(ClaimsPrincipal claimsPrincipal)
    //{
    //    var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    //}
}
