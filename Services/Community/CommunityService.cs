namespace ThreadsBackend.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThreadsBackend.DTOs.Community;
using ThreadsBackend.Data;
using ThreadsBackend.Models;

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
            listCommunitiesQuery = listCommunitiesQuery.Where(c => c.Name.Contains(query.SearchTerm));
        }

        return await listCommunitiesQuery
            .Select(c => this._mapper.Map<CommunityDTO>(c))
            .ToListAsync();
    }

    public async Task<CommunityDTO> GetCommunity(string id)
    {
        this._logger.LogInformation($"Get Community - id: {id}");

        var community = await this._context.Communities.Include(c => c.Threads).FirstOrDefaultAsync(c => c.Id == id);
        if (community is null)
        {
            throw new BadHttpRequestException("User not found");
        }

        return this._mapper.Map<CommunityDTO>(community);
    }

    public async Task<CommunityDTO> AddMemberToCommunity(string communityId, string userId)
    {
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

        community.Members.Add(user);
        this._context.Communities.Update(community);
        await this._context.SaveChangesAsync();

        return this._mapper.Map<CommunityDTO>(community);
    }
}