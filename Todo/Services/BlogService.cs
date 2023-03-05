using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public class BlogService : IBlogService
{
    private readonly DataContext _context;

    public BlogService(DataContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Blog>> GetAll(int page, string order, string date)
    {
        var pageSize = 10;
        var skip = pageSize * (page - 1);
        var take = pageSize;

        var blogs = _context.Blogs
            .Include(b => b.Profile);

        if (order == "new")
        {
            return await blogs
                .OrderBy(b => b.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

        }
        if (order == "popular")
        {
            if (date == "year")
            {
                var year = DateTime.Now.Year;
                return await blogs
                    .Where(b => b.CreatedAt.Year == year)
                    .OrderByDescending(b => b.Likes)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
            if (date == "month")
            {
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                return await blogs
                    .Where(b => b.CreatedAt.Year == year && b.CreatedAt.Month == month)
                    .OrderByDescending(b => b.Likes)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }
        }

        throw new ArgumentException("order was wrong: ", order);
    }

    public async Task<Blog?> GetById(int id)
    {
        var blog = await _context.Blogs
            .Include(b => b.Profile)
            .FirstOrDefaultAsync(b => b.Id == id);

        return blog;
    }

    public async Task<ICollection<Blog>> GetByProfileId(int profileId)
    {
        var blog = await _context.Blogs
            .Include(b => b.Profile)
            .Where(b => b.ProfileId == profileId)
            .ToListAsync();
        
        return blog;
    }

    public async Task<ICollection<Comment>> GetCommentsByBlogId(int blogId, int page, string order)
    {
        var pageSize = 10;
        var skip = pageSize * (page - 1);
        var take = pageSize;
        var comments = _context.Comments
            .Where(c => c.BlogId == blogId)
            .Include(c => c.Profile);
        
        if (order == "new")
        {
            return await comments
                 .OrderBy(c => c.CreatedAt)
                 .Skip(skip)
                 .Take(take)
                 .ToListAsync();
        } 
        if (order == "popular")
        {
            return await comments 
                .OrderByDescending(c => c.Likes)
                .Skip(skip)
                .Take(take)
                .ToListAsync();           
        }

        throw new ArgumentException("wrong order argument", order);
    }
    
    public async Task<Blog> Create(CreateBlogDto blogDto)
    {
        var blog = new Blog
        {
            ProfileId = blogDto.AuthorId,
            Text = blogDto.Text,
            Title = blogDto.Title,
        };
        
        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
        return blog;
    }

    public async Task<Boolean> Delete(int id)
    {
        var deleteBlog = await _context.Blogs
            .Include(b => b.Profile)
            .Include(b => b.BlogVotes)
            .FirstOrDefaultAsync(b => b.Id == id);
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


    public async Task<bool> DislikeBlog(int blogId, int profileId)
    {

        var like = await _context.BlogVotes
            .Include(bv => bv.Profile)
            .Include(bv => bv.Blog)
            .FirstOrDefaultAsync(bv => bv.BlogId == blogId && bv.ProfileId == profileId);

        if (like is null)
        {
            return false;
        }
        
        _context.BlogVotes.Remove(like);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> LikeBlog(int blogId, int profileId)
    {
        var like = new BlogVotes
        {
            ProfileId = profileId,
            BlogId = blogId
        };
        
        _context.BlogVotes.Add(like);
        await _context.SaveChangesAsync();
        return true;
    }
}