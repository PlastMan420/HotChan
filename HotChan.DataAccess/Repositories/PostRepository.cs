using AutoMapper;
using HotChan.DataBase;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotChan.DataAccess.Repositories;

public class PostRepository
{
    private readonly HotChanContext _db;
    private readonly IMapper _mapper;

    public PostRepository(HotChanContext dataContext, IMapper mapper)
    {
        _db = dataContext;
        _mapper = mapper;
    }

    public async Task<Post> GetPostById(Guid postId) => await _db.Posts.FindAsync(postId);
    
    public async Task<List<Post>> GetPostsforCatalog() => await _db.Posts.Take(10).ToListAsync();

    public async Task<bool> CreatePost(PostDialogueDto newPost)
    {
        var post = _mapper.Map<Post>(newPost);
        // get mediaUrl
        // get ThumbnailUrl

        _db.Posts.Add(post);

        try
        {
            await _db.SaveChangesAsync();
            return true;
        }
        catch(Exception e)
        {
           // _logger.LogError(e.Message);
            return false;
        }
    }
}