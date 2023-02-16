using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Dto;
using Todo.Models;
using TodoApi.Models;

namespace TodoApi.Services;

public class CommentService : ICommentService
{
     private readonly DataContext _context;
 
     public CommentService(DataContext context)
     {
         _context = context;
     }
     
     public async Task<List<Comment>> GetAll()
     {
         var comments = await _context.Comments.ToListAsync();
         return comments;
     }

     public async Task<Comment?> GetById(long id)
     {
         var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
         return comment ?? null;
     }

     public async Task<Comment?> Create(CommentDto commentDto)
     {
         var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == commentDto.BlogId);
         if (blog is null)
         {
             return null;
         }
         
         var comment = new Comment
         {
             ProfileId = commentDto.AuthorId,
             BlogId = blog.Id,
             Text = commentDto.Text,
         };
         _context.Comments.Add(comment);
         await _context.SaveChangesAsync();
         return comment;
     }

     public async Task<bool> Delete(long id)
     {
         var comment = await _context.Comments.FindAsync(id);
         if (comment is null)
         {
             return false;
         }

         _context.Comments.Remove(comment);
         await _context.SaveChangesAsync();
         return true;
     }

     public async Task<Comment?> Update(Comment newComment)
     {
         var oldComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == newComment.Id);
         if (oldComment is null)
         {
             return null;
         }

         // mehr kann man auch nicht ändern was sinn macht
         oldComment.Text = newComment.Text;
         oldComment.UpdatedAt = new DateTime();

         await _context.SaveChangesAsync();
         return oldComment;
     }
}