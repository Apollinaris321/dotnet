using System.Net;
using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using Todo.Services;
using TodoApi.Services;

namespace Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(ProfileDto profileDto)
    {
        var result = await _profileService.Login(profileDto);
        if (result.Length == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("{profileId}/RemoveFollower/{followerId}")]
    public async Task<IActionResult> RemoveFollower(int profileId, int followerId)
    {
        var result = await _profileService.RemoveFollower(profileId, followerId);
        return Ok();
    }
    
    [HttpPost]
    [Route("{profileId}/AddFollower/{followerId}")]
    public async Task<IActionResult> AddFollower(int profileId, int followerId)
    {
        var result = await _profileService.AddFollower(profileId, followerId);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}/blogs")]
    public async Task<IActionResult> GetFollowingBlogs(
        int profileId,
        [FromQuery] int page,
        [FromQuery] string order
        )
    {
        var blogs = await _profileService
            .GetFollowingBlogs(profileId, order, page);
        return Ok(blogs);
    }

    [HttpGet]
    [Route("{id}/following")]
    public async Task<IActionResult> GetFollowing(int profileId)
    {
        var following = await _profileService.GetFollowing(profileId);
        return Ok(following);
    }
    
    [HttpGet]
    [Route("{id}/followers")]
    public async Task<IActionResult> GetFollewers(int profileId)
    {
        var followers = await _profileService.GetFollowers(profileId);
        return Ok(followers);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var profiles = await _profileService.GetAll();
        return Ok(profiles);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profile = await _profileService.GetById(id);
        if (profile is null)
        {
            return NotFound();
        }

        return Ok(profile);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProfileDto profileDto)
    {
        var result = await _profileService.Create(profileDto);
        if (result is null)
        {
            return Content($"Failed to create new Profile '{profileDto}'");
        }

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _profileService.Delete(id);
        if (result)
        {
            return Ok(result);
        }
        else
        {
            return NotFound(result);
        }
    }

    [HttpGet]
    [Route("comments")]
    public async Task<IActionResult> GetComments(int id)
    {
        var comments = await _profileService.GetCommentsById(id);
        return Ok(comments);
    }
}