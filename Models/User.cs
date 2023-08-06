namespace ThreadsBackend.Models;

using System.ComponentModel.DataAnnotations;

public class User : Entity
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Bio { get; set; }

    [Required]
    public string ProfilePhoto { get; set; }

    public bool Onboarded { get; set; }
}