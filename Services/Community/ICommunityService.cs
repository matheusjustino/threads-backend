namespace ThreadsBackend.Services;

using ThreadsBackend.DTOs.Community;

public interface ICommunityService
{
    Task<CommunityDTO> CreateCommunity(CreateCommunityDTO data);
}