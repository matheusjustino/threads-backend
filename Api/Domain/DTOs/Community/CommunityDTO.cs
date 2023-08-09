namespace ThreadsBackend.Api.Domain.DTOs.Community;

using ThreadsBackend.Api.Domain.DTOs.Thread;
using ThreadsBackend.Api.Domain.DTOs.User;

public class CommunityDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string Image { get; set; }

    public string Bio { get; set; }

    public string CreatedById { get; set; }

    public UserDTO CreatedBy { get; set; }

    public List<UserDTO> Members { get; set; }

    public List<ThreadDTO> Threads { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}