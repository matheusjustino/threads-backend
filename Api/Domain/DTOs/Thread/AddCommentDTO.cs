namespace ThreadsBackend.Api.Domain.DTOs.Thread;

using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class AddCommentDTO
{
    [Required]
    public string Text { get; set; }

    [Required]
    public Guid ThreadId { get; set; }

    [Required]
    public string UserId { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}