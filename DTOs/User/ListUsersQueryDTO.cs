namespace ThreadsBackend.DTOs.User;

using System.Text.Json;

public class ListUsersQueryDTO
{
    public string? UserId { get; set; }

    public string? SearchTerm { get; set; }

    public int Skip { get; set; } = 0;

    public int Take { get; set; } = 10;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}