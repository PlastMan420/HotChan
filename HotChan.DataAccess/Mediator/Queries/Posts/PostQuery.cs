using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotChan.DataAccess.mediator.Queries.Posts;

public class PostQuery : IRequest<Post>
{
    public Guid postId { get; set; }
    public PostQuery(Guid postId)
    {
        this.postId = postId;
    }
}

public class PostQueryHandler : IRequestHandler<PostQuery, Post>
{
    private readonly HotChanContext _context;

    public PostQueryHandler(HotChanContext context)
    {
        _context = context;
    }

    public async Task<Post> Handle(PostQuery request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == request.postId);
        return post ?? new Post() { PostId = Guid.Empty };
    }
}
