namespace ThreadsBackend.Api.Domain.Entities;

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

    public List<Thread>? Threads { get; set; } = new ();

    public List<Community>? Communities { get; set; } = new ();
}