using System.ComponentModel.DataAnnotations;

namespace Todo.Dto;

public class BlogPatchDto
{
     [Key]
     public int Id { get; set; }
     
     [Required]
     [StringLength(50, MinimumLength = 1)]
     public string Title { get; set; }
     
     [Required]
     public int AuthorId { get; set; }
     
     [Required]
     [StringLength(250, MinimumLength = 1)]
     public string Text { get; set; }   
}