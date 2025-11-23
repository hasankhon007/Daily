using Daily.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DailyTask = Daily.Domain.Entities.DailyTask;

namespace Daily.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<DailyTask> DailyTasks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DailyTask>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
        });
    }
}

