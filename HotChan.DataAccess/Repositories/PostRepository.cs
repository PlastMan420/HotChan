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

    public async Task<Post> CreateJournalPost(Post newPost)
    {
        //var post = _mapper.Map<Post>(newPost);

        _db.Posts.Add(newPost);

        try
        {
            await _db.SaveChangesAsync();
            return newPost;
        }
        catch (Exception e)
        {
            // _logger.LogError(e.Message);
            throw e;
        }
    }

    public async Task<int> ToggleScore(int score, Guid postId)
    {
        bool isNew = false;
        var postScoreRecord = _db.PostScores.Where(x => x.postId == postId).FirstOrDefault();

        if(postScoreRecord == null) {
            isNew = true;
            postScoreRecord = new PostScore();

            postScoreRecord.post = await GetPostById(postId);
            postScoreRecord.postId = postId;
            //postScoreRecord.user = await _db.Users.FindAsync(userId);
            postScoreRecord.user = new User() { Id = Guid.Empty };
            postScoreRecord.userId = Guid.Empty;
        }

        postScoreRecord.score = postScoreRecord.score == score ? 0 : score;

        if(isNew)
        {
            _db.Add(postScoreRecord);
        }
        else
        {
            _db.Update(postScoreRecord);
        }
        await _db.SaveChangesAsync(true);

        return postScoreRecord.score;
    }
}