using Daily.DataAccess.Repositories;
using Daily.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Daily.DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<DailyTask> DailyTaskRepository { get; }

    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
