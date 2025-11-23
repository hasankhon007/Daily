using Daily.DataAccess.Contexts;
using Daily.DataAccess.Repositories;
using Daily.DataAccess.UnitOfWork;
using Daily.Service.Services.DailyTasks;
using Daily.Service.Validators.Tasks;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddLogging();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDbContext<AppDbContext>(option
    => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection")));

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskCreateModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskUpdateModelValidator>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
