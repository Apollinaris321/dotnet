using Todo.Dto;
using Todo.Models;

namespace TodoApi.Services;

public interface IBlogVoteService
{
      public Task<Boolean> Delete(int id);
      public Task<BlogVotes> Create(BlogVoteDto blogVoteDto);
      public Task<BlogVotes> Update(BlogVotes blogVotes);
      public Task<BlogVotes?> GetById(int id);
      // what did someone like?
      public Task<List<BlogVotes>> GetByProfileId(int profileId);
      // who liked this ? 
      public Task<List<BlogVotes>> GetByBlogId(int blogId);
      public Task<List<BlogVotes>> GetAll();   
}