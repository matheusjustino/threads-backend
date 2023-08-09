namespace ThreadsBackend.Api.Domain.DTOs.Community;

using System.Text.Json;
using ThreadsBackend.Api.Domain.Enums;

public class GetCommunityProfileQueryDTO
{
    public CommunityProfileEnum ProfileTab { get; set; } = CommunityProfileEnum.THREADS;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}