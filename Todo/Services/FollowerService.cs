using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public class FollowerService : IFollowerService
{
    private readonly DataContext _context;

    public FollowerService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<Follower>> GetAll()
    {
        var followers = await _context.Followers.ToListAsync();
        return followers;
        throw new NotImplementedException();
    }

    public async Task<List<Follower>> GetById(int profileId)
    {
        var followers = await _context.Followers
            .Where(f => f.OwnerProfileId == profileId)
            .ToListAsync();
        
        return followers;
    }

    public async Task<List<Blog>> GetFollowingBlogs(int profileId)
    {
        var followers = await _context.Blogs
            .Where(b => _context.Followers
                .Any(f => 
                    f.OwnerProfileId == profileId 
                    && f.FollowingId == b.ProfileId
                    && f.FollowingId != null))
            .ToListAsync();
        return followers;
    }

    public async Task<List<Profile>> GetFollowing(int ownerId)
    {
        var following = await _context.Profiles
            .Where(p => _context.Followers
                .Any(f => 
                    f.OwnerProfileId == ownerId &&
                    f.FollowingId == p.Id &&
                    p.Id != null))
            .ToListAsync();
        return following;
    }

    public async Task<Follower> Create(FollowerDto followerDto)
    {
        var follower = new Follower
        {
            OwnerProfileId = followerDto.ProfileId,
            FollowingId = followerDto.FollowingId,
        };

        _context.Followers.Add(follower);
        await _context.SaveChangesAsync();
        return follower;
    }
}