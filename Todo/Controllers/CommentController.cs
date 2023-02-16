using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using TodoApi.Models;
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
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentService.GetAll();
        return Ok(comments);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(long id)
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
}
