using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services;

public class TaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskDto>> GetAllTasksAsync()
    {
        var items = await _context.Tasks.ToListAsync();
        return items.Select(MapToDto).ToList();
    }

    public async Task<TaskDto?> GetTaskByIdAsync(int id)
    {
        var item = await _context.Tasks.FindAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto)
    {
        var item = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            DueDate = dto.DueDate
        };
        _context.Tasks.Add(item);
        await _context.SaveChangesAsync();
        return MapToDto(item);
    }

    public async Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto dto)
    {
        var item = await _context.Tasks.FindAsync(id);
        if (item is null)
            return null;

        item.Title = dto.Title;
        item.Description = dto.Description;
        item.Status = dto.Status;
        item.DueDate = dto.DueDate;
        await _context.SaveChangesAsync();
        return MapToDto(item);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var item = await _context.Tasks.FindAsync(id);
        if (item is null)
            return false;

        _context.Tasks.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    private static TaskDto MapToDto(TaskItem item)
    {
        return new TaskDto
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Status = item.Status,
            CreatedAt = item.CreatedAt,
            DueDate = item.DueDate
        };
    }
}
