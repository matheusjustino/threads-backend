namespace ThreadsBackend.Api.Domain.DTOs.Thread;

using ThreadsBackend.Api.Domain.DTOs.Community;

public class GetCommunityThreadsResponseDTO
{
    public CommunityDTO Profile { get; set; }

    public List<ThreadDTO> Threads { get; set; }
}