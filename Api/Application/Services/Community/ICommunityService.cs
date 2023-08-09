namespace ThreadsBackend.Api.Application.Services;

using ThreadsBackend.Api.Domain.DTOs.Community;

public interface ICommunityService
{
    Task<CommunityDTO> CreateCommunity(CreateCommunityDTO data);

    Task<List<CommunityDTO>> ListCommunities(ListCommunitiesQueryDTO query);

    Task<CommunityDTO> GetCommunity(string id);

    Task<CommunityDTO> AddMemberToCommunity(string communityId, string userId);
}