using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public class CommentVotesService : ICommentVotesService
{
    private readonly DataContext _context;

    public CommentVotesService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Delete(int id)
    {
        var vote = await _context.CommentVotes
            .Include(v => v.Profile)
            .Include(v => v.Comment)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vote is null)
        {
            return false;
        }

        _context.CommentVotes.Remove(vote);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CommentVotes> Create(CommentVoteDto commentVoteDto)
    {
        var vote = new CommentVotes
        {
            ProfileId = commentVoteDto.ProfileId,
            CommentId = commentVoteDto.CommentId,
        };
        
        _context.Add(vote);

        await _context.SaveChangesAsync();
        return vote;
    }

    public Task<CommentVotes> Update(CommentVotes commentVote)
    {
        throw new NotImplementedException();
    }

    public async Task<CommentVotes?> GetById(int id)
    {
        var vote = await _context.CommentVotes
            .Include(v => v.Comment)
            .FirstOrDefaultAsync(v => v.Id == id);
        return vote ?? null;
    }

    public async Task<List<CommentVotes>> GetAll()
    {
        return await _context.CommentVotes
            .Include(v => v.Comment)
            .ToListAsync();
    }

    public async Task<List<CommentVotes>> GetByProfileId(int profileId)
    {
        return await _context.CommentVotes
            .Include(v => v.Comment)
            .Include(v => v.Profile)
            .Where(v => v.ProfileId == profileId)
            .ToListAsync();
    }

    public async Task<List<CommentVotes>> GetByCommentId(int commentId)
    {
        return await _context.CommentVotes
            .Include(v => v.Comment)
            .Include(v => v.Profile)
            .Where(v => v.CommentId == commentId)
            .ToListAsync();
    }
}