// Represents a task in the Task Management system.
namespace TaskManagementAPI.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }
}
