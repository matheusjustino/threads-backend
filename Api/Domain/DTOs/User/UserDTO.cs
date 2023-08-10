namespace ThreadsBackend.Api.Domain.DTOs.User;

using ThreadsBackend.Api.Domain.DTOs.Community;
using ThreadsBackend.Api.Domain.DTOs.Thread;

public class UserDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string Bio { get; set; }

    public string ProfilePhoto { get; set; }

    public bool Onboarded { get; set; }

    public List<ThreadDTO> Threads { get; set; } = new ();

    public List<CommunityDTO> Communities { get; set; } = new ();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}