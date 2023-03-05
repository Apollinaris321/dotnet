using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public interface ICommentVotesService
{
     public Task<Boolean> Delete(int id);
     public Task<CommentVotes> Create(CommentVoteDto commentVoteDto);
     public Task<CommentVotes> Update(CommentVotes commentVote);
     public Task<CommentVotes?> GetById(int id);
     public Task<List<CommentVotes>> GetAll();
     // which comments did this person like?
     public Task<List<CommentVotes>> GetByProfileId(int profileId);
     // who liked this comment?
     public Task<List<CommentVotes>> GetByCommentId(int commentId);
}