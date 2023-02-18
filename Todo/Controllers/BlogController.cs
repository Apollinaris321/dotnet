using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using Todo.Models;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var blogs = await _blogservice.GetAll();
        return Ok(blogs);   
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var blog = await _blogservice.GetById(id);
        if (blog is null)
        {
            return NotFound();
        }
        
        return Ok(blog);   
    }
    
    [HttpGet]
    [Route("{id}/comments")]
    public async Task<IActionResult> GetCommentsById(long id)
    {
        var blog = await _blogservice.GetBlogComments(id);
        if (blog is null)
        {
            return NotFound();
        }
        
        return Ok(blog);   
    }
        
    [HttpPost]
    public async Task<IActionResult> Create(BlogDto blogDto)
    {
        var blog = await _blogservice.Create(blogDto);
        return Ok(blog);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _blogservice.Delete(id);
        if (response is false)
        {
            return NotFound();
        }
        return Ok(response);
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