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
}