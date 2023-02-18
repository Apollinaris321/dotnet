using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace Todo.Dto;

public class BlogDto
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Title { get; set; }
    
    [Required]
    public long AuthorId { get; set; }
    
    [Required]
    [StringLength(250, MinimumLength = 1)]
    public string Text { get; set; }
}