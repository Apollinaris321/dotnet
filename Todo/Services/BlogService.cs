using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace TodoApi.Services;

public class BlogService : IBlogService
{
    private readonly DataContext _context;

    public BlogService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<Blog>> GetAll()
    {
        var blogs = await _context.Blogs
            .Include(b => b.Comments)
            .ToListAsync();
        return blogs;
    }

    public async Task<Blog?> GetById(long id)
    {
        var blog = await _context.Blogs
            .FirstOrDefaultAsync(b => b.Id == id);
        return blog ?? null;
    }

    public async Task<Blog?> GetBlogComments(long id)
    {
        var blog = await _context.Blogs
            .Include(b => b.Comments)
            .FirstOrDefaultAsync(b => b.Id == id);
        return blog ?? null;
    }

    public async Task<Blog> CreateBlog(Blog blog)
    {
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
        return blog;
    }

    public async Task<Boolean> DeleteBlog(long id)
    {
        var deleteBlog = await _context.Blogs.FindAsync(id);
        if (deleteBlog is null)
        {
            return false;
        }

        _context.Blogs.Remove(deleteBlog);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Blog?> UpdateBlog(Blog blog)
    {
        var oldBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == blog.Id);
        if (oldBlog is null)
        {
            return null;
        }

        oldBlog.Text = blog.Text;
        oldBlog.Title = blog.Title;
        oldBlog.ProfileId = blog.ProfileId;

        await _context.SaveChangesAsync();
        return oldBlog;
    }
}