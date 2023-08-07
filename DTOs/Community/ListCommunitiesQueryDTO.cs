namespace ThreadsBackend.DTOs.Community;

using System.Text.Json;

public class ListCommunitiesQueryDTO
{
    public string? SearchTerm { get; set; }

    public int Skip { get; set; } = 0;

    public int Take { get; set; } = 10;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}