using System.ComponentModel.DataAnnotations;

namespace Todo.Models;

public class Follower
{
    public int Id { get; set; }
    
    public int? OwnerProfileId { get; set; }
    public Profile OwnerProfile { get; set; }
    
    // user is following ...
    public int? FollowingId { get; set; }
    public Profile FollowingProfile { get; set; }   
}