using Todo.Dto;
using Todo.Models;

namespace TodoApi.Services;

public interface IBlogService
{
    public Task<List<Blog>> GetAll();
    public Task<Blog?> GetById(long id);
    public Task<Blog?> GetBlogComments(long id);
    public Task<Blog> Create(BlogDto blogDto);
    public Task<Boolean> Delete(long id);
    public Task<Blog?> Update(BlogPatchDto blogDto);
}