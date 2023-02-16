using Todo.Dto;
using Todo.Models;
using TodoApi.Models;

namespace TodoApi.Services;

public interface ICommentService
{
    public Task<List<Comment>> GetAll();
    public Task<Comment?> GetById(long id);
    public Task<Comment?> Create(CommentDto commentDto);
    public Task<Boolean> Delete(long id);
    public Task<Comment?> Update(Comment newComment);
}