using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Todo.Data;
using Todo.Dto;
using Todo.Models;

namespace Todo.Services;

public class CommentService : ICommentService
{
     private readonly DataContext _context;
 
     public CommentService(DataContext context)
     {
         _context = context;
     }

     public async Task<List<Comment>> GetAll(int skip, int page, string order)
     {
         var comments = new List<Comment>();
         switch (order)
         {
             case "new": 
                 comments = await _context.Comments
                     .Include(c => c.Profile)
                     .OrderBy(c => c.CreatedAt)
                     .Skip(skip)
                     .Take(page)
                     .ToListAsync();
                 break;
             case "old": 
                 comments = await _context.Comments
                     .Include(c => c.Profile)
                     .OrderByDescending(c => c.CreatedAt)
                     .Skip(skip)
                     .Take(page)
                     .ToListAsync();
                 break;
         }
         return comments;
     }

     public async Task<Comment?> GetById(int id)
     {
         var comment = await _context.Comments
             .Include(c => c.Profile)
             .FirstOrDefaultAsync(c => c.Id == id);
         return comment ?? null;
     }

     public async Task<ICollection<Comment>> GetByProfileId(
         int profileId,
         int page,
         string order
         )
     {
         var pageSize = 10; 
         var skip = pageSize * (page - 1);
         var take = pageSize;
         var comments = _context.Comments
             .Where(c => c.ProfileId == profileId)
             .Include(c => c.Profile);
         
         if (order == "new")
         {
             return await comments
                  .OrderBy(c => c.CreatedAt)
                  .Skip(skip)
                  .Take(take)
                  .ToListAsync();
         } 
         if (order == "popular")
         {
             return await comments 
                 .OrderByDescending(c => c.Likes)
                 .Skip(skip)
                 .Take(take)
                 .ToListAsync();           
         }
         throw new ArgumentException("wrong order argument", order);
     }

     public async Task<ICollection<Comment>> GetByBlogId(int blogId, int page, string order)
     { 
         var pageSize = 10; 
         var skip = pageSize * (page - 1);
         var take = pageSize;
         var comments = _context.Comments
             .Where(c => c.BlogId == blogId)
             .Include(c => c.Profile);
         
         if (order == "new")
         {
             return await comments
                  .OrderBy(c => c.CreatedAt)
                  .Skip(skip)
                  .Take(take)
                  .ToListAsync();
         } 
         if (order == "popular")
         {
             return await comments 
                 .OrderByDescending(c => c.Likes)
                 .Skip(skip)
                 .Take(take)
                 .ToListAsync();           
         }
         
         throw new ArgumentException("wrong order argument", order);
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

     public async Task<bool> Delete(int id)
     {
         var comment = await _context.Comments
             .Include(c => c.CommentVotes)
             .FirstOrDefaultAsync(c => c.Id == id);
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

         oldComment.Text = newComment.Text;
         oldComment.UpdatedAt = new DateTime();

         await _context.SaveChangesAsync();
         return oldComment;
     }
     
     public async Task<bool> LikeComment(int commentId, int profileId)
     {
         var like = new CommentVotes
         {
             ProfileId = profileId,
             CommentId = commentId
         };

         _context.CommentVotes.Add(like);
         await _context.SaveChangesAsync();
         return true;
     }

     public async  Task<bool> DislikeComment(int commentId, int profileId)
     {
         var like = await _context.CommentVotes
             .FirstOrDefaultAsync(cv =>
                 cv.ProfileId == profileId &&
                 cv.CommentId == commentId);
         if (like is null)
         {
             return false;
         }

         _context.CommentVotes.Remove(like);
         await _context.SaveChangesAsync();
         return true;
     }
}
