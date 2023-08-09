namespace ThreadsBackend.Api.Domain.DTOs.Thread;

using ThreadsBackend.Api.Domain.DTOs.User;

public class GetUserThreadsResponseDTO
{
    public UserDTO Profile { get; set; }

    public List<ThreadDTO> Threads { get; set; }
}