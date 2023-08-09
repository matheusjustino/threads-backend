namespace ThreadsBackend.Api.Application.Services;

using ThreadsBackend.Api.Domain.DTOs.Thread;
using ThreadsBackend.Api.Domain.DTOs.User;

public interface IUserService
{
    Task<List<UserDTO>> ListUsers(ListUsersQueryDTO query);

    Task<UserDTO> GetUser(string userId);

    Task<UserDTO> UpdateUser(string userId, UpdateUserDTO data);

    Task<List<ThreadDTO>> GetUserActivity(string userId);

    Task<GetUserProfileResponseDTO> GetUserProfile(string userId, GetUserProfileQueryDTO query);
}