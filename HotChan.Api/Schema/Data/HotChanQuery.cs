using HotChan.Api.Schema.Types;
using HotChan.DataAccess.Repositories;
using HotChan.DataBase.Models.Entities;

namespace HotChan.Api.Schema.Data;
public class HotChanQuery
{
    public HotChanQuery() { }

    public Task<List<Post>> GetPostCatalog([Service] PostRepository postRepo) => postRepo.GetPostsforCatalog();
    public Task<Post> GetPost([Service] PostRepository postRepo, Guid postId) => postRepo.GetPostById(postId);
}
