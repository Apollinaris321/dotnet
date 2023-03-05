using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public interface IProfileService
{
    public Task<List<Profile>> GetAll();
    public Task<Profile?> GetById(int id);
    public Task<Profile> Create(ProfileDto profileDto);
    public Task<Profile> Update(ProfileDto profileDto);
    public Task<Boolean> Delete(int id);
    public Task<string> Login(ProfileDto profileDto);
    public Task<ICollection<Comment>> GetCommentsById(int id);
    public Task<Follower> AddFollower(int profileId, int followerId);
    public Task<bool> RemoveFollower(int profileId, int followerId);
    public Task<ICollection<Blog>> GetFollowingBlogs(int profileId, string order, int page);
    public Task<ICollection<Profile>> GetFollowers(int profileId);
    public Task<ICollection<Profile>> GetFollowing(int profileId);
}