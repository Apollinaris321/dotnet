using System.ComponentModel.DataAnnotations;

namespace Todo.Dto;

public class BlogPatchDto
{
     [Key]
     public long Id { get; set; }
     
     [Required]
     [StringLength(50, MinimumLength = 1)]
     public string Title { get; set; }
     
     [Required]
     public long AuthorId { get; set; }
     
     [Required]
     [StringLength(250, MinimumLength = 1)]
     public string Text { get; set; }   
}