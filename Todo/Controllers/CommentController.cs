using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using Todo.Models;
using Todo.Services;
using TodoApi.Services;

namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    [Route("profile/{profileId}")]
    public async Task<IActionResult> GetCommentsForProfile(
        int profileId,
        [FromQuery] int page,
        [FromQuery] string order
        )
    {
        var blogs = await _commentService.GetByProfileId(profileId, page, order);
        return Ok(blogs);
    }
    
    [HttpGet]
    [Route("blogs/{blogId}")]
    public async Task<IActionResult> GetCommentsForBlog(
        int blogId,
        [FromQuery] int page,
        [FromQuery] string order
        )
    {
        var blogs = await _commentService.GetByBlogId(blogId, page, order);
        return Ok(blogs);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int skip = 0, [FromQuery] string order = "new")
    {
        var pageSize = 10;
        var comments = await _commentService.GetAll(skip , pageSize * page, order);
        return Ok(comments);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comment = await _commentService.GetById(id);
        if (comment is null)
        {
            return NotFound();
        }

        return Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CommentDto commentDto)
    {
        var comment = await _commentService.Create(commentDto);
        if (comment is null)
        {
            // TODO:
            // beseren retrurn wert finden 
            return NotFound();
        }
        return Ok(comment);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Comment newComment)
    {
        var comment = await _commentService.Update(newComment);
        if (comment is null)
        {
            return NotFound();
        }

        return Ok(comment);
    }

    [HttpPost]
    [Route("{commentId}/downvote/{profileId}")]
    public async Task<IActionResult> Downvote(int commentId, int profileId)
    {
        var result = await _commentService.DislikeComment(commentId, profileId);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("{commentId}/upvote/{profileId}")]
    public async Task<IActionResult> Upvote(int commentId, int profileId)
    {
        var result = await _commentService.LikeComment(commentId, profileId);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _commentService.Delete(id);
        if (result)
        {
            return Ok(result);
        }
        return NotFound(result);
    }
}
