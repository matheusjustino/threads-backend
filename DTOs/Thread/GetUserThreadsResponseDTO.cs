namespace ThreadsBackend.DTOs.Thread;

using ThreadsBackend.DTOs.User;

public class GetUserThreadsResponseDTO
{
    public UserDTO Profile { get; set; }

    public List<ThreadDTO> Threads { get; set; }
}