namespace ThreadsBackend.DTOs.Thread;

using System.Text.Json;

public class CreateThreadDTO
{
    public string Text { get; set; }

    public string AuthorId { get; set; }

    public string? CommunityId { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}