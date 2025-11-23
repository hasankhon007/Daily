using Daily.Service.Services.DailyTasks.Models;
using FluentValidation;

namespace Daily.Service.Validators.Tasks;

public class TaskCreateModelValidator : AbstractValidator<TaskCreateModel>
{
    public TaskCreateModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}
