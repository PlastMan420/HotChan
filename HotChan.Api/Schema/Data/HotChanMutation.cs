using HotChan.DataAccess.Repositories;
using HotChan.DataBase.Models.Dtos;
using HotChan.DataBase.Models.Entities;

namespace HotChan.Api.Schema.Data;
public class HotChanMutation
{
    [UseServiceScope]
    public async Task<bool> SubmitJournalPost(PostDialogueDto postDto, [Service] PostRepository postRepo) => await postRepo.CreateJournalPost(postDto);
}
