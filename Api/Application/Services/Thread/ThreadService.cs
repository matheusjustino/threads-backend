namespace ThreadsBackend.Api.Application.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThreadsBackend.Api.Infrastructure.Persistence;
using ThreadsBackend.Api.Domain.DTOs.Community;
using ThreadsBackend.Api.Domain.DTOs.Thread;
using ThreadsBackend.Api.Domain.DTOs.User;
using ThreadsBackend.Api.Domain.Entities;

public class ThreadService : IThreadService
{
    private readonly ILogger<ThreadService> _logger;

    private readonly AppDbContext _context;

    private readonly IMapper _mapper;

    public ThreadService(ILogger<ThreadService> logger, AppDbContext context, IMapper mapper)
    {
        this._logger = logger;
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<ThreadDTO> CreateThread(CreateThreadDTO data)
    {
        this._logger.LogInformation($"Create Thread - data: {data.ToString()}");

        var author = await this._context.Users.FirstOrDefaultAsync(u => u.Id == data.AuthorId);
        if (author is null)
        {
            throw new BadHttpRequestException("Author not found");
        }

        var thread = new Thread
        {
            Text = data.Text,
            AuthorId = data.AuthorId,
            Author = author,
            CommunityId = data.CommunityId ?? null,
        };

        await this._context.AddAsync(thread);
        await this._context.SaveChangesAsync();

        return this._mapper.Map<ThreadDTO>(thread);
    }

    public async Task<List<ThreadDTO>> ListThreads(ListThreadsQueryDTO query)
    {
        this._logger.LogInformation($"List Threads - query: {query.ToString()}");

        var listThreadsQuery = this._context.Threads
            .OrderByDescending(t => t.CreatedAt)
            .Skip(query.Skip * query.Take)
            .Take(query.Take)
            .Where(t => t.ParentThreadId == null);
        if (!string.IsNullOrEmpty(query.CommunityId))
        {
            listThreadsQuery = listThreadsQuery.Where(t => t.CommunityId == query.CommunityId);
        }

        return await listThreadsQuery
            .Select(t => new ThreadDTO
            {
                Id = t.Id,
                Text = t.Text,
                ParentThreadId = t.ParentThreadId,
                AuthorId = t.AuthorId,
                Author = new UserDTO
                {
                  Id = t.Author.Id,
                  Name = t.Author.Name,
                  Username = t.Author.Username,
                  ProfilePhoto = t.Author.ProfilePhoto,
                },
                CommunityId = t.CommunityId,
                Community = t.Community != null ? new CommunityDTO
                {
                    Id = t.Community.Id,
                    Name = t.Community.Name,
                    Username = t.Community.Username,
                    Image = t.Community.Image,
                }
                : null,
                CreatedAt = t.CreatedAt,
                CommentsCount = t.Comments.Count,
            })
            .ToListAsync();
    }

    public async Task<ThreadDTO> GetThread(Guid threadId)
    {
        this._logger.LogInformation($"Get Thread - threadId: {threadId}");

        var thread = await this._context.Threads
            .Select(t => new ThreadDTO
            {
                Id = t.Id,
                Text = t.Text,
                ParentThreadId = t.ParentThreadId,
                AuthorId = t.AuthorId,
                Author = new UserDTO
                {
                    Id = t.Author.Id,
                    Name = t.Author.Name,
                    Username = t.Author.Username,
                    ProfilePhoto = t.Author.ProfilePhoto,
                },
                CommunityId = t.CommunityId,
                Community = t.Community != null ? new CommunityDTO
                    {
                        Id = t.Community.Id,
                        Name = t.Community.Name,
                        Username = t.Community.Username,
                        Image = t.Community.Image,
                    }
                    : null,
                CreatedAt = t.CreatedAt,
                Comments = t.Comments.Select(tc => new ThreadDTO
                {
                    Id = tc.Id,
                    Text = tc.Text,
                    ParentThreadId = tc.ParentThreadId,
                    AuthorId = tc.AuthorId,
                    Author = new UserDTO
                    {
                        Id = tc.Author.Id,
                        Name = tc.Author.Name,
                        Username = tc.Author.Username,
                        ProfilePhoto = tc.Author.ProfilePhoto,
                    },
                    CommunityId = tc.CommunityId,
                    Community = tc.Community != null ? new CommunityDTO
                        {
                            Id = tc.Community.Id,
                            Name = tc.Community.Name,
                            Username = tc.Community.Username,
                            Image = tc.Community.Image,
                        }
                        : null,
                    CreatedAt = tc.CreatedAt,
                    CommentsCount = tc.Comments.Count,
                }).ToList(),
            })
            .FirstOrDefaultAsync(t => t.Id == threadId);
        if (thread is null)
        {
            throw new BadHttpRequestException("Thread not found");
        }

        return thread;
    }

    public async Task<ThreadDTO> AddCommentToThread(AddCommentDTO data)
    {
        this._logger.LogInformation($"Add Comment To Thread - data: {data.ToString()}");

        var parentThread = await this._context.Threads.FirstOrDefaultAsync(t => t.Id == data.ThreadId);
        if (parentThread is null)
        {
            throw new BadHttpRequestException("Thread not found");
        }

        var author = await this._context.Users.FirstOrDefaultAsync(u => u.Id == data.UserId);
        if (author is null)
        {
            throw new BadHttpRequestException("Author not found");
        }

        var commentThread = new Thread
        {
            Text = data.Text,
            AuthorId = data.UserId,
            Author = author,
            ParentThreadId = data.ThreadId,
        };

        parentThread.Comments.Add(commentThread);
        this._context.Threads.Update(parentThread);
        await this._context.AddAsync(commentThread);
        await this._context.SaveChangesAsync();

        return this._mapper.Map<ThreadDTO>(commentThread);
    }
}