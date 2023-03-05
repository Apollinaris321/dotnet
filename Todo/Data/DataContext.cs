using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Todo.Dto;
using Todo.Models;

namespace Todo.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("getutcdate()");
        
        modelBuilder.Entity<Blog>()
            .Property(b => b.UpdatedAt)
            .HasDefaultValueSql("getutcdate()");
                
        modelBuilder.Entity<Comment>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("getutcdate()");
        
        modelBuilder.Entity<Comment>()
            .Property(c => c.UpdatedAt)
            .HasDefaultValueSql("getutcdate()");
        
        modelBuilder.Entity<Profile>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("getutcdate()");
        
        modelBuilder.Entity<Profile>()
            .Property(c => c.UpdatedAt)
            .HasDefaultValueSql("getutcdate()");
         
        modelBuilder.Entity<CommentVotes>()
            .ToTable(tb => tb.HasTrigger("UpdateCommentLikesInsert"));        
        
        modelBuilder.Entity<CommentVotes>()
            .ToTable(tb => tb.HasTrigger("UpdateCommentLikesDelete")); 
                
        modelBuilder.Entity<BlogVotes>()
            .ToTable(tb => tb.HasTrigger("UpdateBlogLikesDelete")); 
                        
        modelBuilder.Entity<BlogVotes>()
            .ToTable(tb => tb.HasTrigger("UpdateBlogLikesInsert"));

        modelBuilder.Entity<Follower>()
            .HasOne(f => f.OwnerProfile)
            .WithMany(p => p.Following)
            .HasForeignKey(f => f.OwnerProfileId)
            .OnDelete(DeleteBehavior.Restrict)
            ;
        
         modelBuilder.Entity<Follower>()
             .HasOne(f => f.FollowingProfile)
             .WithMany(p => p.Followers)
             .HasForeignKey(f => f.FollowingId);       
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<CommentVotes> CommentVotes { get; set; }
    public DbSet<BlogVotes> BlogVotes { get; set; }
    public DbSet<Follower> Followers { get; set; }
}