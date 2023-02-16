using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace Todo.Dto;

public class BlogDto
{
    [Microsoft.Build.Framework.Required]
    [StringLength(50, MinimumLength = 1)]
    public string Title { get; set; }
    
    [Microsoft.Build.Framework.Required]
    public long AuthorId { get; set; }
    
    [Microsoft.Build.Framework.Required]
    [StringLength(250, MinimumLength = 1)]
    public string Text { get; set; }
}