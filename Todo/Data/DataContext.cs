using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Todo.Models;
using TodoApi.Models;

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
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Profile> Profiles { get; set; }
}