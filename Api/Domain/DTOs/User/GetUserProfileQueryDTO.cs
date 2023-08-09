namespace ThreadsBackend.Api.Domain.DTOs.User;

using System.Text.Json;
using ThreadsBackend.Api.Domain.Enums;

public class GetUserProfileQueryDTO
{
    public UserProfileEnum ProfileTab { get; set; } = UserProfileEnum.THREADS;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}