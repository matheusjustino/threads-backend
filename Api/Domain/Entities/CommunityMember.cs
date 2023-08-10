namespace ThreadsBackend.Api.Domain.Entities;

public class CommunityMember : Entity
{
    public string CommunityId { get; set; }

    public Community Community { get; set; }

    public string MemberId { get; set; }

    public User Member { get; set; }
}
