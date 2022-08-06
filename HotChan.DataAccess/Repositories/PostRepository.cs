using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotChan.DataAccess.Repositories;

public class PostRepostiry
{
    private readonly HotChanContext _db;

    public PostRepostiry(HotChanContext dataContext)
    {
        _db = dataContext;
    }

    public async Task<Post> GetPostById(Guid postId) => await _db.Posts.FindAsync(postId);
    public async Task<List<Post>> GetPostsforCatalog() => await _db.Posts.Take(10).ToListAsync();
}