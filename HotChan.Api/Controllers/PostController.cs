using HotChan.DataAccess.Repositories;
using HotChan.DataBase.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotChan.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly PostRepository _postRepo;
    public PostController(PostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    [AllowAnonymous]
    [HttpGet("GetPost/{postId}")]
    public async Task<IActionResult> GetPost(Guid postId)
    {
        if (postId == Guid.Empty)
            return BadRequest();

        var post = await _postRepo.GetPostById(postId);
        return post != null ? Ok(post) : NotFound();
    }

    [AllowAnonymous]
    [HttpGet("catalog")]
    public async Task<IActionResult> Catalog()
    {
        var post = await _postRepo.GetPostsforCatalog();
        return Ok(post);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok("success");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePost(HttpRequest request)
    {
        var newPost = await request.ReadFromJsonAsync<PostDialogueDto>();
        if (newPost == null)
            return BadRequest();

        var result = await _postRepo.CreatePost(newPost);

        return Ok(result);
    }
}
