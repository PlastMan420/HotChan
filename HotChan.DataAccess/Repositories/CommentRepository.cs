using AutoMapper;
using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotChan.DataAccess.Repositories
{
    public class CommentRepository
    {
        private readonly HotChanContext _db;
        private readonly IMapper _mapper;

        public CommentRepository(HotChanContext dataContext, IMapper mapper)
        {
            _db = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> AddComment(Guid userId, Guid postId, Comment comment)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if(user == null)
            {
                return false;
            }

            var thread = await _db.Posts.Where(x => x.PostId == postId).Select(x => x.Thread).FirstOrDefaultAsync();

            comment.Thread = thread;
            comment.User = user;
            comment.Score = 0;
            comment.CreatedOn = DateTime.UtcNow;
            comment.LastModified = DateTime.UtcNow;
            comment.IsModified = false;

            return true;
        }
    }
}
