using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Dto;
using Todo.Models;

namespace TodoApi.Services;

public class BlogVoteService : IBlogVoteService
{
    private readonly DataContext _context;

    public BlogVoteService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Delete(int id)
    {
        var vote = await _context.BlogVotes
            .Include(v => v.Blog)
            .Include(v => v.Profile)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vote is null)
        {
            return false;
        }

        _context.BlogVotes.Remove(vote);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BlogVotes> Create(BlogVoteDto blogVoteDto)
    {
        var vote = new BlogVotes
        {
            ProfileId = blogVoteDto.ProfileId,
            BlogId = blogVoteDto.BlogId,
        };

        _context.Add(vote);

        await _context.SaveChangesAsync();
        return vote;
    }

    public Task<BlogVotes> Update(BlogVotes blogVotes)
    {
        throw new NotImplementedException();
    }

    public async Task<BlogVotes?> GetById(int id)
    {
        var vote = await _context.BlogVotes
            .Include(v => v.Blog)
            .FirstOrDefaultAsync(v => v.Id == id);
        return vote ?? null;
    }

    //which blogs did this person like?
    public async Task<List<BlogVotes>> GetByProfileId(int profileId)
    {
        return await _context.BlogVotes
            .Include(v => v.Blog)
            .Include(v => v.Profile)
            .Where(v => v.ProfileId == profileId)
            .ToListAsync();
    }

    // who liked this blog ?
    public async Task<List<BlogVotes>> GetByBlogId(int blogId)
    {
        return await _context.BlogVotes
            .Include(v => v.Blog)
            .Include(v => v.Profile)
            .Where(v => v.BlogId == blogId)
            .ToListAsync();
    }

    public async Task<List<BlogVotes>> GetAll()
    {
        return await _context.BlogVotes
            .Include(v => v.Blog)
            .ToListAsync();
    }
}