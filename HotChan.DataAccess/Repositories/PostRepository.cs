using AutoMapper;
using HotChan.DataBase;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotChan.DataAccess.Repositories;

public class PostRepostiry
{
    private readonly HotChanContext _db;
    private readonly IMapper _mapper;
    //private readonly ILogger _logger;

    public PostRepostiry(HotChanContext dataContext, IMapper mapper)
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
            return false;
        }
    }
}