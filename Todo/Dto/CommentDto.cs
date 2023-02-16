using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Todo.Models;

namespace Todo.Dto;

public class CommentDto
{
     public long AuthorId { get; set; }
     
     public long BlogId { get; set; }
     
     [StringLength(250, MinimumLength = 1)]
     public string Text { get; set; }
}