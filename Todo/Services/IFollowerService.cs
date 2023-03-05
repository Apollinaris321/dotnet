using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public interface IFollowerService
{
    public Task<List<Follower>> GetAll();
    public Task<List<Follower>> GetById(int profileId);
    public Task<List<Blog>> GetFollowingBlogs(int profileId);
    public Task<Follower> Create(FollowerDto followerDto);
    public Task<List<Profile>> GetFollowing(int ownerId);
}
