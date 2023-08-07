namespace ThreadsBackend.DTOs.Thread;

using ThreadsBackend.DTOs.Community;

public class GetCommunityThreadsResponseDTO
{
    public CommunityDTO Profile { get; set; }

    public List<ThreadDTO> Threads { get; set; }
}