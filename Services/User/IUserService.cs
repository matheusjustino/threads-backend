namespace ThreadsBackend.Services;

using ThreadsBackend.DTOs.Thread;
using ThreadsBackend.DTOs.User;

public interface IUserService
{
    Task<List<UserDTO>> ListUsers(ListUsersQueryDTO query);

    Task<UserDTO> GetUser(string userId);

    Task<UserDTO> UpdateUser(string userId, UpdateUserDTO data);

    Task<List<ThreadDTO>> GetUserActivity(string userId);
}