using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Todo.Dto;
using Todo.Models;

namespace Todo.Models;

public class Comment
{
    public int Id { get; set; }
    
    public int? ProfileId { get; set; }
    public Profile Profile { get; set; }
    
    public int? BlogId { get; set; }
    
    [StringLength(250, MinimumLength = 1)]
    public string Text { get; set; }
    
    [JsonIgnore]
    public ICollection<CommentVotes> CommentVotes { get; set; }

    public int Likes { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}