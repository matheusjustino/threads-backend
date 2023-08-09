namespace ThreadsBackend.Api.Application.Services;

using ThreadsBackend.Api.Domain.DTOs.Thread;

public interface IThreadService
{
    Task<ThreadDTO> CreateThread(CreateThreadDTO data);

    Task<List<ThreadDTO>> ListThreads(ListThreadsQueryDTO query);

    Task<ThreadDTO> GetThread(Guid threadId);

    Task<ThreadDTO> AddCommentToThread(AddCommentDTO data);
}