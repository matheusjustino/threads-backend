namespace ThreadsBackend.Api.Domain.Entities;

using System.Text.Json;

public class CommunityMember : Entity
{
    public string CommunityId { get; set; }

    public Community Community { get; set; }

    public string MemberId { get; set; }

    public User Member { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
