using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public interface ICommentService
{
    public Task<List<Comment>> GetAll(int skip, int page, string order);
    public Task<Comment?> GetById(int id);
    public Task<ICollection<Comment>> GetByProfileId(int profileId, int page, string order);
    public Task<ICollection<Comment>> GetByBlogId(int blogId, int page, string order);
    public Task<Comment?> Create(CommentDto commentDto);
    public Task<Boolean> Delete(int id);
    public Task<Comment?> Update(Comment newComment);
    public Task<bool> LikeComment(int commentId, int profileId);
    public Task<bool> DislikeComment(int commentId, int profileId);
}