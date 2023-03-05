using Microsoft.Build.Framework;
using Todo.Models;

namespace Todo.Dto;

public class ProfileDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}

public class ProfileLoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}