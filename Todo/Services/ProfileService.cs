using Todo.Data;
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

    public Task<Profile> Create(Profile profile)
    {
        throw new NotImplementedException();
    }

    public async Task<Profile> GetById(long id)
    {
        //var profile = await _context.Profi
        throw new NotImplementedException();
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