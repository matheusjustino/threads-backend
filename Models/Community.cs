namespace ThreadsBackend.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class Community : Entity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Username { get; set; }

    public string Image { get; set; }

    public string Bio { get; set; }

    [Required]
    public string CreatedById { get; set; }

    [Required]
    public User CreatedBy { get; set; }

    public List<User> Members { get; set; }

    public List<Thread> Threads { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}