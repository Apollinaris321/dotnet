using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Todo.Models;

namespace TodoApi.Models;

public class Comment
{
    public long Id { get; set; }
    
    public long? ProfileId { get; set; }
    
    public long BlogId { get; set; }
    
    [StringLength(250, MinimumLength = 1)]
    public string Text { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}