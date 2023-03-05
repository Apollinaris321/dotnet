using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using Todo.Dto;

namespace Todo.Models;

public class Blog
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Title { get; set; }
    
    
    [Required]
    [StringLength(250, MinimumLength = 1)]
    public string Text { get; set; }
    
    public int? ProfileId { get; set; }
    public Profile Profile { get; set; }
    public ICollection<Comment> Comments { get; set; }
    
    [JsonIgnore]
    public ICollection<BlogVotes> BlogVotes { get; set; }

    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Blog()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}