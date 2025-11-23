using Daily.Domain.Entities;
using Daily.Domain.Enums;

namespace Daily.Service.Services.DailyTasks.Models;

public class TaskViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Task_Status { get; set; }
}