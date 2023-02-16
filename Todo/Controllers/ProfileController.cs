using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace Todo.Controllers;

public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task<IActionResult> GetById(long id)
    {
        var profile = await _profileService.GetById(id);
        if (profile is null)
        {
            return NotFound();
        }

        return Ok(profile);
    }
}