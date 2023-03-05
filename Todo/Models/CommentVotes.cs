namespace Todo.Models;

public class CommentVotes
{
    public int Id { get; set; }
    
    public int? ProfileId { get; set; }
    public Profile Profile { get; set; }
    
    public int? CommentId { get; set; }
    public Comment Comment { get; set; }
}