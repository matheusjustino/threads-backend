namespace ThreadsBackend.Api.Domain.DTOs.Thread;

using System.Text.Json;

public class ListThreadsQueryDTO
{
    public string? CommunityId { get; set; }

    public int Skip { get; set; } = 0;

    public int Take { get; set; } = 10;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}