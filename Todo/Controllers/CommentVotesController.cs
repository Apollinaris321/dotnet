using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using Todo.Services;
using TodoApi.Services;

namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentVotesController : ControllerBase
{
    private readonly ICommentVotesService _commentVotesService;

    public CommentVotesController(ICommentVotesService commentVotesService)
    {
        _commentVotesService = commentVotesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var votes = await _commentVotesService.GetAll();
        return Ok(votes);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _commentVotesService.Delete(id);
        return result ? Ok() : NotFound();
    }
 
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vote = await _commentVotesService.GetById(id);
        return vote is null ? NotFound() : Ok(vote);
    }
     
    [HttpGet]
    [Route("/comment/{id}")]
    public async Task<IActionResult> GetByCommentId(int id)
    {
        var vote = await _commentVotesService.GetByCommentId(id);
        return vote is null ? NotFound() : Ok(vote);
    }   
    
    [HttpGet]
    [Route("/profile/{id}")]
    public async Task<IActionResult> GetByProfileId(int id)
    {
        var vote = await _commentVotesService.GetByProfileId(id);
        return vote is null ? NotFound() : Ok(vote);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CommentVoteDto commentVoteDto)
    {
        var vote = await _commentVotesService.Create(commentVoteDto);
        return vote is null ? NotFound() : Ok(vote);
    }
}