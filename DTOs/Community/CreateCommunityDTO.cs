namespace ThreadsBackend.DTOs.Community;

using System.Text.Json;

public class CreateCommunityDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string Image { get; set; }

    public string Bio { get; set; }

    public string CreatedById { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}