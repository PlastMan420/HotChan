using HotChan.DataAccess.Repositories;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;

namespace HotChan.Api.Schema.Data;
public class HotChanMutation
{
    [UseServiceScope]
    public async Task<Post> SubmitJournalPost([Service] PostRepository postRepo, Post post) => await postRepo.CreateJournalPost(post);
    public async Task<int> TogglePostScore([Service] PostRepository postRepo, Guid postId, int score) => await postRepo.ToggleScore(score, postId);
}
