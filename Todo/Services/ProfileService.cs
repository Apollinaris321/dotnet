using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Dto;
using Todo.Models;
using TodoApi.Models;

namespace TodoApi.Services;

public class ProfileService : IProfileService
{
    private readonly DataContext _context;

    public ProfileService(DataContext context)
    {
        _context = context;
    }
    
    public Task<bool> Delete(Profile profile)
    {
        throw new NotImplementedException();
    }

    public async Task<Profile> Create(ProfileDto profileDto)
    {
        var profile = new Profile{
                Email = profileDto.Email,
                Password = profileDto.Password,
                Username = profileDto.Username
            };
        
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }

    public async Task<Profile?> GetById(long id)
    {
        var profile = await _context.Profiles
            .FirstOrDefaultAsync(p => p.Id == id);
        return profile ?? null;
    }

    public Task<List<Blog>> GetBlogsById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Comment>> GetCommentsById(long id)
    {
        throw new NotImplementedException();
    }
}