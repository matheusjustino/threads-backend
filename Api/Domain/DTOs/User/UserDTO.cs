namespace ThreadsBackend.Api.Domain.DTOs.User;

public class UserDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string Bio { get; set; }

    public string ProfilePhoto { get; set; }

    public bool Onboarded { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}