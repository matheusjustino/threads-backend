namespace ThreadsBackend.Api.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public class Community : Entity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Username { get; set; }

    public string Image { get; set; }

    public string Bio { get; set; }

    [Required]
    public string CreatedById { get; set; }

    [Required]
    public User CreatedBy { get; set; }

    public List<CommunityMember> Members { get; set; }

    public List<Thread> Threads { get; set; }
}