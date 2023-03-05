using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using Todo.Data;
using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public class ProfileService : IProfileService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public ProfileService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
 
    public async Task<string> Login(ProfileDto profileDto)
    {
        Profile? profile = await _context.Profiles
            .FirstOrDefaultAsync(p => p.Username == profileDto.Username);
        if (profile is null)
        {
            return string.Empty;
        }

        if (BCrypt.Net.BCrypt.Verify(profileDto.Password, profile.Password))
        {
            string token = CreateToken(profile);
            return token;
        }

        return string.Empty;
    }

    public string CreateToken(Profile profile)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, profile.Username),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("JWT:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt; 
    }
    
    public async Task<List<Profile>> GetAll()
    {
        var profiles = await _context.Profiles.ToListAsync();
        return profiles;
    }
    
    public async Task<bool> Delete(int id)
    {
        var result = await _context.Profiles
            .Include(p => p.Comments)
            .Include(p => p.Blogs)
            .Include(p => p.CommentVotes)
            .Include(p => p.BlogVotes)
            .Include(p => p.Followers)
            .Include(p => p.Following)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (result is null)
        {
            return false;
        }

        _context.Profiles.Remove(result);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Profile> Create(ProfileDto profileDto)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(profileDto.Password);
        
        var profile = new Profile{
                Email = profileDto.Email,
                Password = passwordHash,
                Username = profileDto.Username
            };
        
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }

    public Task<Profile> Update(ProfileDto profileDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Profile?> GetById(int id)
    {
        var profile = await _context.Profiles
            .FirstOrDefaultAsync(p => p.Id == id);
        return profile ?? null;
    }

    public Task<List<Blog>> GetBlogsById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Comment>> GetCommentsById(int id)
    {
        var comments = await _context.Comments
            .Include(c => c.Profile)
            .Where(c => c.ProfileId == id)
            .ToListAsync();

        return comments;
    }

    public async Task<Follower> AddFollower(int profileId, int followerId)
    {
        var follower = new Follower
        {
            OwnerProfileId = profileId,
            FollowingId = followerId,
        };
        
        _context.Followers.Add(follower);
        await _context.SaveChangesAsync();
        return follower;
    }

    public async Task<bool> RemoveFollower(int profileId, int followerId)
    {
        var follower = await _context.Followers
            .Include(f => f.OwnerProfile)
            .Include(f => f.FollowingProfile)
            .FirstOrDefaultAsync(f => followerId == followerId && f.OwnerProfileId == profileId);

        if (follower is null)
        {
            return false;
        }
        
        _context.Followers.Remove(follower);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task<ICollection<Blog>> GetFollowingBlogs(int profileId, string order, int page)
    {
        var blogs = await _context.Blogs
            .Where(b => _context.Followers
                .Any(f =>
                    f.OwnerProfileId == profileId &&
                    f.FollowingId == b.ProfileId &&
                    f.FollowingId != null))
            .OrderBy(b => b.CreatedAt)
            .ToListAsync();
        return blogs;
    }

    public async Task<ICollection<Profile>> GetFollowers(int profileId)
    {
        var following = await _context.Profiles
            .Where(p => _context.Followers
                .Any(f => 
                    f.OwnerProfileId == p.Id &&
                    f.FollowingId == profileId &&
                    p.Id != null))
            .ToListAsync();
        return following;
    }

    public async Task<ICollection<Profile>> GetFollowing(int profileId)
    {
        var following = await _context.Profiles
            .Where(p => _context.Followers
                .Any(f => 
                    f.OwnerProfileId == profileId &&
                    f.FollowingId == p.Id &&
                    p.Id != null))
            .ToListAsync();
        return following;
    }
}