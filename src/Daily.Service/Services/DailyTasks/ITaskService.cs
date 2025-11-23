using Daily.Domain.Enums;
using Daily.Service.Services.DailyTasks.Models;

namespace Daily.Service.Services.DailyTasks;

public interface ITaskService
{
    Task<TaskViewModel> GetAsync(int id);
    Task<List<TaskViewModel>> GetAllAsync();
    Task CreateAsync(TaskCreateModel model);
    Task UpdateAsync(int id, TaskUpdateModel model);
    Task DeleteAsync(int id);
    Task ChangeStatus(int id, Status status);
}
