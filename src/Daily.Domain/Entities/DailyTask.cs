using Daily.Domain.Enums;

namespace Daily.Domain.Entities;

public class DailyTask : Auditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Task_Status { get; set; }
}
