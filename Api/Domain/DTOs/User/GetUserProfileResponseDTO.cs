namespace ThreadsBackend.Api.Domain.DTOs.User;

using ThreadsBackend.Api.Domain.DTOs.Thread;

public class GetUserProfileResponseDTO
{
    public UserDTO Profile { get; set; }

    public List<ThreadDTO> Threads { get; set; }
}