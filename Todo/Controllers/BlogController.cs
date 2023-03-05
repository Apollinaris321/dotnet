using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using Todo.Models;
using Todo.Services;
using TodoApi.Services;

namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogservice;
    
    public BlogController(IBlogService blogservice)
    {
        _blogservice = blogservice;
    }

    [HttpPost]
    [Route("{id}/upvote")]
    public async Task<IActionResult> UpvoteBlog(
        [FromQuery] int blogId,
        [FromQuery] int profileId
        )
    {
        await _blogservice.LikeBlog(blogId, profileId);
        return Ok();
    }
 
    [HttpPost]
    [Route("{id}/downvote")]
    public async Task<IActionResult> DislikeBlog(
        [FromQuery] int blogId,
        [FromQuery] int profileId
        )
    {
        var result = await _blogservice.DislikeBlog(blogId, profileId);
        return result ? Ok() : NotFound();
    }   
     
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] string order = "new",
        [FromQuery] string date = "month"
        )
    {
        if (date != "month" && date != "year")
        {
            return BadRequest();
        }
        if (order != "new" && order != "popular")
        {
            return BadRequest();
        }
        
        var blogs = await _blogservice.GetAll(page , order, date);
        return Ok(blogs);   
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var blog = await _blogservice.GetById(id);
        if (blog is null)
        {
            return NotFound();
        }
        
        return Ok(blog);   
    }

        
    [HttpPost]
    public async Task<IActionResult> Create(CreateBlogDto blogDto)
    {
        var blog = await _blogservice.Create(blogDto);
        return Ok(blog);
    }

    [HttpPost]
    [Route("{id}/comments")]
    public async Task<IActionResult> AddComment(CommentDto commentDto)
    {
        
        
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _blogservice.Delete(id);
        if (response is false)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(BlogPatchDto blogDto)
    {
        var response = await _blogservice.Update(blogDto);
        if (response is null)
        {
            return NotFound();
        }
        return Ok(response);
    }

}