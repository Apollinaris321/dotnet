using Todo.Models;
using TodoApi.Models;

namespace TodoApi.Services;

public interface IProfileService
{
    public Task<Boolean> Delete(Profile profile);
    public Task<Profile> Create(Profile profile);
    public Task<Profile> GetById(long id);
    public Task<List<Blog>> GetBlogsById(long id);
    public Task<List<Comment>> GetCommentsById(long id);
}