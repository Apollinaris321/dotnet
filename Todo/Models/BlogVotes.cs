namespace Todo.Models;

public class BlogVotes
{
     public int Id { get; set; }
     
     public int? ProfileId { get; set; }
     public Profile Profile { get; set; }
     
     public int? BlogId { get; set; }
     public Blog Blog { get; set; }   
}