using Daily.Domain.Entities;
using Daily.Domain.Enums;

namespace Daily.Service.Services.DailyTasks.Models;

public class TaskUpdateModel
{
    public string Title { get; set; }
    public string Description { get; set; }

}