using HotChan.DataAccess.Repositories;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace HotChan.Api.Schema.Data;
public class HotChanMutation
{
    [UseServiceScope]
    public async Task<Post> SubmitJournalPost([Service] PostRepository postRepo, Post post) => await postRepo.CreateJournalPost(post);

    [Authorize]
    public async Task<int> TogglePostScore([Service] PostRepository postRepo, Guid postId, int score) => await postRepo.ToggleScore(score, postId);


    [AllowAnonymous]
    public async Task<string> Register([Service] UserRepository usrRepo, UserRegisterFormDto newUser) => await usrRepo.Register(newUser);

    [Authorize]
    public async Task<bool> AddComment([Service] CommentRepository cmntRepo, Guid userId, Guid postId, Comment comment) => await cmntRepo.AddComment(userId, postId, comment);
}
