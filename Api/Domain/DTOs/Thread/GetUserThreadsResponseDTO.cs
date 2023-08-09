using ThreadsBackend.Api.Domain.DTOs.Community;

namespace ThreadsBackend.Api.Domain.DTOs.Thread;

using ThreadsBackend.Api.Domain.DTOs.User;

public class GetUserThreadsResponseDTO
{
    public CommunityDTO Profile { get; set; }

    public List<ThreadDTO> Threads { get; set; }
}