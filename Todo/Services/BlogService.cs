using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Dto;
using Todo.Models;
using TodoApi.Models;

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
            .Include(b => b.Profile)
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

    public async Task<Blog> Create(BlogDto blogDto)
    {
        var blog = new Blog
        {
            ProfileId = blogDto.AuthorId,
            Text = blogDto.Text,
            Title = blogDto.Title,
            Comments = new List<Comment>()
        };
        
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
        return blog;
    }

    public async Task<Boolean> Delete(long id)
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

    public async Task<Blog?> Update(BlogPatchDto blogDto)
    {
        var oldBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == blogDto.Id);
        if (oldBlog is null)
        {
            return null;
        }

        oldBlog.Text = blogDto.Text;
        oldBlog.Title = blogDto.Title;
        oldBlog.ProfileId = blogDto.AuthorId;
        oldBlog.UpdatedAt = new DateTime();

        await _context.SaveChangesAsync();
        return oldBlog;
    }
}