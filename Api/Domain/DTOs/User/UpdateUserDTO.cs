namespace ThreadsBackend.Api.Domain.DTOs.User;

using System.Text.Json;

public class UpdateUserDTO
{
    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Bio { get; set; }

    public IFormFile? ProfilePhoto { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}