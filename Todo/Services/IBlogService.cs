using Todo.Models;

namespace TodoApi.Services;

public interface IBlogService
{
    public Task<List<Blog>> GetAll();
    public Task<Blog?> GetById(long id);
    public Task<Blog?> GetBlogComments(long id);
    public Task<Blog> CreateBlog(Blog blog);
    public Task<Boolean> DeleteBlog(long id);
    public Task<Blog?> UpdateBlog(Blog blog);
}