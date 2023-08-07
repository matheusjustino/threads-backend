namespace ThreadsBackend.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThreadsBackend.Data;
using ThreadsBackend.DTOs.Community;
using ThreadsBackend.DTOs.User;
using ThreadsBackend.DTOs.Thread;
using ThreadsBackend.Models;

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

        return await this._context.Threads
            .OrderByDescending(t => t.CreatedAt)
            .Skip(query.Skip * query.Take)
            .Take(query.Take)
            .Where(t => t.ParentThreadId == null)
            .Include(t => t.Author)
            .Include(t => t.Community)
            .Select(t => this._mapper.Map<ThreadDTO>(t))
            .ToListAsync();
    }

    public async Task<ThreadDTO> GetThread(Guid threadId)
    {
        this._logger.LogInformation($"Get Thread - threadId: {threadId}");

        var thread = await this._context.Threads
            .Include(t => t.Author)
            .Include(t => t.Community)
            .Include(t => t.Comments.OrderByDescending(c => c.CreatedAt))
            .FirstOrDefaultAsync(t => t.Id == threadId);
        if (thread is null)
        {
            throw new BadHttpRequestException("Thread not found");
        }

        await this._context.Entry(thread)
            .Collection(t => t.Comments)
            .Query()
            .Include(t => t.Author)
            .LoadAsync();

        return this._mapper.Map<ThreadDTO>(thread);
    }

    public async Task<ThreadDTO> AddCommentToThread(AddCommentDTO data)
    {
        this._logger.LogInformation($"Add Comment To Thread - data: {data.ToString()}");

        var originalThread = await this._context.Threads.FirstOrDefaultAsync(t => t.Id == data.ThreadId);
        if (originalThread is null)
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

        originalThread.Comments.Add(commentThread);
        this._context.Threads.Update(originalThread);
        await this._context.AddAsync(commentThread);
        await this._context.SaveChangesAsync();

        return this._mapper.Map<ThreadDTO>(commentThread);
    }

    public async Task<GetUserThreadsResponseDTO> GetUserThreads(string id)
    {
        this._logger.LogInformation($"Get User Threads - id: {id}");

        var user = await this._context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null)
        {
            throw new BadHttpRequestException("User not found");
        }

        var userThreads = await this._context.Threads
            .Where(t => t.AuthorId == user.Id)
            .Include(t => t.Author)
            .Include(t => t.Community)
            .Include(t => t.Comments).ThenInclude(c => c.Author)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        var response = new GetUserThreadsResponseDTO
        {
            Profile = this._mapper.Map<UserDTO>(user),
            Threads = this._mapper.Map<List<ThreadDTO>>(userThreads),
        };

        return response;
    }

    public async Task<GetCommunityThreadsResponseDTO> GetCommunityThreads(string id)
    {
        this._logger.LogInformation($"Get Community Threads - id: {id}");

        var community = await this._context.Communities
            .Include(c => c.CreatedBy)
            .Include(c => c.Members)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (community is null)
        {
            throw new BadHttpRequestException("Community not found");
        }

        var communityThreads = await this._context.Threads
            .Where(t => t.CommunityId == id)
            .Include(t => t.Author)
            // .Include(t => t.Comments).ThenInclude(c => c.Author)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        var response = new GetCommunityThreadsResponseDTO
        {
            Profile = this._mapper.Map<CommunityDTO>(community),
            Threads = this._mapper.Map<List<ThreadDTO>>(communityThreads),
        };

        return response;
    }
}