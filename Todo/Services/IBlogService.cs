using Microsoft.AspNetCore.Http.HttpResults;
using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public interface IBlogService
{
    public Task<ICollection<Blog>> GetAll(int page, string order, string date);
    public Task<Blog?> GetById(int id);
    public Task<ICollection<Blog>> GetByProfileId(int profileId);
    public Task<Blog> Create(CreateBlogDto blogDto);
    public Task<Boolean> Delete(int id);
    public Task<Blog?> Update(BlogPatchDto blogDto);
    public Task<bool> LikeBlog(int blogId,int profileId);
    public Task<bool> DislikeBlog(int blogId, int profileId);
}                                     
                                      
                                      
                                      
                                      
                                      