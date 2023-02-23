using System.ComponentModel.DataAnnotations;
using Todo.Models;

namespace TodoApi.Models;

public class Profile
{
    public long Id { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 1)]
    public string Username { get; set; }
    
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 1)]
    public string Password { get; set; }
    
    public List<Comment> Comments { get; set; }
    public List<Blog> Blogs { get; set; }
    
    public DateTime CreatedAt { get; set; } 
    
    public DateTime UpdatedAt { get; set; }
}