namespace ThreadsBackend.Api.Domain.DTOs.CommunityMember;

using System.Text.Json;
using ThreadsBackend.Api.Domain.DTOs.Community;
using ThreadsBackend.Api.Domain.DTOs.User;

public class CommunityMemberDTO
{
    public string Id { get; set; }

    public string CommunityId { get; set; }

    public CommunityDTO Community { get; set; }

    public string MemberId { get; set; }

    public UserDTO Member { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}