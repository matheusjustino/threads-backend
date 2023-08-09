namespace ThreadsBackend.Api.Domain.Entities;

public class Entity
{
    public string Id { get; init; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }
}