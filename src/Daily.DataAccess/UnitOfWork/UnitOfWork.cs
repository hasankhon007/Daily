using Daily.DataAccess.Contexts;
using Daily.DataAccess.Repositories;
using Daily.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Daily.DataAccess.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public IRepository<DailyTask> DailyTaskRepository { get; } = new Repository<DailyTask>(context);


    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await context.Database.BeginTransactionAsync();
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}
