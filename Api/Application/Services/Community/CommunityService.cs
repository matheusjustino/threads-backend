namespace ThreadsBackend.Api.Application.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThreadsBackend.Api.Domain.DTOs.Thread;
using ThreadsBackend.Api.Domain.DTOs.User;
using ThreadsBackend.Api.Domain.DTOs.Community;
using ThreadsBackend.Api.Domain.Entities;
using ThreadsBackend.Api.Infrastructure.Persistence;

public class CommunityService : ICommunityService
{
    private readonly ILogger<CommunityService> _logger;

    private readonly AppDbContext _context;

    private readonly IMapper _mapper;

    public CommunityService(ILogger<CommunityService> logger, AppDbContext context, IMapper mapper)
    {
        this._logger = logger;
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<CommunityDTO> CreateCommunity(CreateCommunityDTO data)
    {
        this._logger.LogInformation($"Create Community - data: {data.ToString()}");

        var user = await this._context.Users.FirstOrDefaultAsync(u => u.Id == data.CreatedById);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found");
        }

        var newCommunity = new Community
        {
            Id = data.Id,
            Name = data.Name,
            Username = data.Username,
            Bio = data.Bio,
            Image = data.Image,
            CreatedById = data.CreatedById,
            CreatedBy = user,
        };

        await this._context.AddAsync(newCommunity);
        await this._context.SaveChangesAsync();

        return this._mapper.Map<CommunityDTO>(newCommunity);
    }

    public async Task<List<CommunityDTO>> ListCommunities(ListCommunitiesQueryDTO query)
    {
        this._logger.LogInformation($"List Communities - query: {query.ToString()}");

        var listCommunitiesQuery = this._context.Communities
            .OrderBy(u => u.Name)
            .Skip(query.Skip * query.Take)
            .Take(query.Take);
        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            listCommunitiesQuery = listCommunitiesQuery.Where(c =>
                c.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                c.Username.ToLower().Contains(query.SearchTerm.ToLower()));
        }

        return await listCommunitiesQuery
            .Select(c => this._mapper.Map<CommunityDTO>(c))
            .ToListAsync();
    }

    public async Task AddMemberToCommunity(string communityId, string userId)
    {
        this._logger.LogInformation($"Add Member To Community - communityId: {communityId} - userId: {userId}");

        var community = await this._context.Communities.FirstOrDefaultAsync(c => c.Id == communityId);
        if (community is null)
        {
            throw new BadHttpRequestException("Community not found");
        }

        var user = await this._context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found");
        }

        var communityMember = new CommunityMember
        {
            Id = Guid.NewGuid().ToString(),
            CommunityId = communityId,
            Community = community,
            MemberId = userId,
            Member = user,
        };

        await this._context.AddAsync(communityMember);
        await this._context.SaveChangesAsync();
    }

    public async Task RemoveMemberFromCommunity(string communityId, string userId)
    {
        this._logger.LogInformation($"Remove Member From Community - communityId: {communityId} - userId: {userId}");

        var communityMember =
            await this._context.CommunityMembers.FirstOrDefaultAsync(cm =>
                cm.CommunityId == communityId && cm.MemberId == userId);
        if (communityMember != null)
        {
            this._context.Remove(communityMember);
            await this._context.SaveChangesAsync();
        }
    }

    public async Task<GetCommunityProfileResponseDTO> GetCommunityProfile(string id, GetCommunityProfileQueryDTO query)
    {
        this._logger.LogInformation("Get Community Profile - query: {Query}", query);

        var community = await this._context.Communities
            .Where(c => c.Id == id)
            .Select(c => new Community
            {
                Id = c.Id,
                Name = c.Name,
                Username = c.Username,
                Bio = c.Bio,
                Image = c.Image,
                Members = c.Members.Select(cm => new CommunityMember
                {
                    Id = cm.Id,
                    CommunityId = cm.CommunityId,
                    MemberId = cm.MemberId,
                    Member = new User
                    {
                        Id = cm.Member.Id,
                        Name = cm.Member.Name,
                        Username = cm.Member.Username,
                        ProfilePhoto = cm.Member.ProfilePhoto,
                    },
                }).ToList(),
                CreatedById = c.CreatedById,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            })
            .FirstOrDefaultAsync(c => c.Id == id);
        if (community is null)
        {
            throw new BadHttpRequestException("Community not found");
        }

        var threads = await this._context.Threads
            .Where(t => t.CommunityId == id)
            .Select(t => new ThreadDTO
            {
                Id = t.Id,
                Text = t.Text,
                AuthorId = t.AuthorId,
                Author = new UserDTO
                {
                    Id = t.Author.Id,
                    Name = t.Author.Name,
                    Username = t.Author.Username,
                    ProfilePhoto = t.Author.ProfilePhoto,
                },
                ParentThreadId = t.ParentThreadId,
                CommunityId = t.CommunityId,
                CommentsCount = t.Comments.Count,
                CreatedAt = t.CreatedAt,
            }).ToListAsync();

        return new GetCommunityProfileResponseDTO
        {
            Profile = this._mapper.Map<CommunityDTO>(community),
            Threads = this._mapper.Map<List<ThreadDTO>>(threads),
        };
    }

    public async Task<List<CommunityDTO>> GetSuggestCommunities(GetSuggestCommunitiesQueryDTO query)
    {
        this._logger.LogInformation($"Get Suggest Communities - query: {query.ToString()}");

        if (query.Count is 0)
        {
            return new List<CommunityDTO>();
        }

        var random = new Random();
        var communitiesIds = await this._context.Communities.Select(c => c.Id).ToListAsync();
        var shuffledCommunitiesIds = communitiesIds.OrderBy(_ => random.Next()).ToList();

        var randomCommunities = await this._context.Communities
            .Where(c => shuffledCommunitiesIds.Contains(c.Id))
            .Take(query.Count ?? 4)
            .ToListAsync();

        return this._mapper.Map<List<CommunityDTO>>(randomCommunities);
    }
}