using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Todo.Dto;
using Todo.Services;
using TodoApi.Services;

namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowerController : ControllerBase
{
    private readonly IFollowerService _followerService;

    public FollowerController(IFollowerService followerService)
    {
        _followerService = followerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var followers = await _followerService.GetAll();
        return Ok(followers);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var followers = await _followerService.GetById(id);
        return Ok(followers);
    }
    
    //given a profile id return all the blogs posted by people who this 
    // profile is following
    [HttpGet]
    [Route("{id}/blogs")]
    public async Task<IActionResult> GetFollowingBlogs(int id)
    {
        var followers = await _followerService.GetFollowingBlogs(id);
        return Ok(followers);
    }

    [HttpGet]
    [Route("{id}/following")]
    public async Task<IActionResult> GetFollowingProfiles(int id)
    {
        var following = await _followerService.GetFollowing(id);
        return Ok(following);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(FollowerDto followerDto)
    {
        var follower = await _followerService.Create(followerDto);
        return Ok(follower);
    }
}