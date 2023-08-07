namespace ThreadsBackend.Services;

using ThreadsBackend.DTOs.Thread;

public interface IThreadService
{
    Task<ThreadDTO> CreateThread(CreateThreadDTO data);

    Task<List<ThreadDTO>> ListThreads(ListThreadsQueryDTO query);

    Task<ThreadDTO> GetThread(Guid threadId);

    Task<ThreadDTO> AddCommentToThread(AddCommentDTO data);

    Task<GetUserThreadsResponseDTO> GetUserThreads(string id);

    Task<GetCommunityThreadsResponseDTO> GetCommunityThreads(string id);
}