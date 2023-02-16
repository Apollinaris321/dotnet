using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using TodoApi.Models;

namespace Todo.Models;

public class Blog
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Title { get; set; }
    
    [Required]
    public long? ProfileId { get; set; }
    
    [Required]
    [StringLength(250, MinimumLength = 1)]
    public string Text { get; set; }
    
    public List<Comment> Comments { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Blog()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}