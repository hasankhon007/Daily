using Daily.DataAccess.UnitOfWork;
using Daily.Domain.Entities;
using Daily.Domain.Enums;
using Daily.Service.Exceptions;
using Daily.Service.Extensions;
using Daily.Service.Services.DailyTasks.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Daily.Service.Services.DailyTasks;

public class TaskService(IUnitOfWork unitOfWork,
    IValidator<TaskCreateModel> taskcreatevalidator,
    IValidator<TaskUpdateModel> taskupdatemodelvalidator) : ITaskService
{
    public async Task CreateAsync(TaskCreateModel model)
    {
        await taskcreatevalidator.EnsureValidatedAsync(model);
        var existTask = await unitOfWork.DailyTaskRepository
            .SelectAsync(t => t.Title == model.Title);
        if (existTask is not null)
            throw new InvalidOperationException("A task with the same title already exists for this user.");

        unitOfWork.DailyTaskRepository.Insert(
            new DailyTask
            {
                Title = model.Title,
                Description = model.Description,
                Task_Status = model.Task_Status
            });
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, TaskUpdateModel model)
    {
        await taskupdatemodelvalidator.EnsureValidatedAsync(model);

        var task = await unitOfWork.DailyTaskRepository.SelectAsync(x => x.Id == id)
            ?? throw new NotFoundException("This task is not found");

        task.Title = model.Title;
        task.Description = model.Description;

        unitOfWork.DailyTaskRepository.Update(task);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var task = await unitOfWork.DailyTaskRepository.SelectAsync(x => x.Id == id)
            ?? throw new NotFoundException("This task is not found");

        unitOfWork.DailyTaskRepository.MarkAsDeleted(task);
        await unitOfWork.SaveAsync();
    }

    public async Task<List<TaskViewModel>> GetAllAsync()
    {
        var tasks = unitOfWork.DailyTaskRepository.SelectAllAsQueryable();

        return await tasks.Select(t => new TaskViewModel
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Task_Status = t.Task_Status
        }).ToListAsync();
    }

    public async  Task<TaskViewModel> GetAsync(int id)
    {
        var task = await unitOfWork.DailyTaskRepository.SelectAsync(x => x.Id == id)
            ?? throw new NotFoundException("This task is not found");

        return new TaskViewModel
        {
            Id = id,
            Title = task.Title,
            Description = task.Description,
            Task_Status = task.Task_Status
        };
    }

    public async Task ChangeStatus(int id, Status status)
    {
        var task = await unitOfWork.DailyTaskRepository.SelectAsync(t => t.Id == id)
            ?? throw new NotFoundException("Task is not found");

        task.Task_Status = status;
        unitOfWork.DailyTaskRepository.Update(task);
        unitOfWork.SaveAsync();
    }
}
