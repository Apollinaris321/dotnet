using System.Net;
using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using TodoApi.Models;
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

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(long id)
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
}