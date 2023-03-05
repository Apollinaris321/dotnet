using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Todo.Models;

namespace Todo.Models;

public class Profile
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 1)]
    public string Username { get; set; }
    
    [EmailAddress]
    [Required]
    [JsonIgnore]
    public string Email { get; set; }
    
    [Required]
    [StringLength(60, MinimumLength = 1)]
    [JsonIgnore]
    public string Password { get; set; }
    
    [JsonIgnore]
    public ICollection<Comment> Comments { get; set; }
    [JsonIgnore]
    public ICollection<Blog> Blogs { get; set; }
    [JsonIgnore]
    public ICollection<CommentVotes> CommentVotes { get; set; }
    [JsonIgnore]
    public ICollection<BlogVotes> BlogVotes { get; set; }
    
    [JsonIgnore]
    public ICollection<Follower> Following { get; set; } 
    [JsonIgnore]
    public ICollection<Follower> Followers { get; set; } 
    
    public DateTime CreatedAt { get; set; } 
    
    public DateTime UpdatedAt { get; set; }
}