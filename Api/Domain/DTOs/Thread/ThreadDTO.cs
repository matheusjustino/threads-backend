namespace ThreadsBackend.Api.Domain.DTOs.Thread;

using ThreadsBackend.Api.Domain.DTOs.Community;
using ThreadsBackend.Api.Domain.DTOs.User;

public class ThreadDTO
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public string AuthorId { get; set; }

    public UserDTO Author { get; set; }

    public Guid? ParentThreadId { get; set; }

    public List<ThreadDTO> Comments { get; set; }

    public int? CommentsCount { get; set; } = 0;

    public string? CommunityId { get; set; }

    public CommunityDTO? Community { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}