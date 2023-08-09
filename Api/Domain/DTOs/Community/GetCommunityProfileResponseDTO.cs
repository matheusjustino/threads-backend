namespace ThreadsBackend.Api.Domain.DTOs.Community;

using ThreadsBackend.Api.Domain.DTOs.Thread;

public class GetCommunityProfileResponseDTO
{
    public CommunityDTO Profile { get; set; }

    public List<ThreadDTO> Threads { get; set; }
}