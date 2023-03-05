using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using Todo.Models;
using TodoApi.Services;

namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogVotesController : ControllerBase
{
    private readonly IBlogVoteService _blogVoteService;

    public BlogVotesController(IBlogVoteService blogVoteService)
    {
        _blogVoteService = blogVoteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var votes = await _blogVoteService.GetAll();
        return Ok(votes);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _blogVoteService.Delete(id);
        return result ? Ok() : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BlogVoteDto blogVoteDto)
    {
        var vote = await _blogVoteService.Create(blogVoteDto);
        return vote is null ? NotFound() : Ok(vote);
    }

    [HttpGet]
    [Route("blog/{id}")]
    public async Task<IActionResult> GetByBlogId(int id)
    {
        var vote = await _blogVoteService.GetByBlogId(id);
        return vote is null ? NotFound() : Ok(vote);
    }
    
    [HttpGet]
    [Route("profile/{id}")]
    public async Task<IActionResult> GetByProfileId(int id)
    {
        var vote = await _blogVoteService.GetByProfileId(id);
        return vote is null ? NotFound() : Ok(vote);
    }
    
}