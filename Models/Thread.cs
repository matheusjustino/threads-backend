namespace ThreadsBackend.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class Thread : Entity
{
    public Guid Id { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public string AuthorId { get; set; }

    [Required]
    public User Author { get; set; }

    public Guid? ParentThreadId { get; set; }

    public List<Thread> Comments { get; set; } = new ();

    public string? CommunityId { get; set; }

    public Community? Community { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}