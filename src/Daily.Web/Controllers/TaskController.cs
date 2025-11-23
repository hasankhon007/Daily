using Daily.Service.Services.DailyTasks;
using Daily.Service.Services.DailyTasks.Models;
using Microsoft.AspNetCore.Mvc;

namespace Daily.Web.Controllers;

public class TaskController(ITaskService taskService) : Controller
{
    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await taskService.GetAllAsync();
            return View(result);
        }
        catch (Exception ex)
        {
            ViewBag.ExceptionMessage = ex.Message;
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(TaskCreateModel model)
    {
        try
        {
            await taskService.CreateAsync(model);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            ViewBag.ExceptionMessage = ex.Message;
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var result = await taskService.GetAsync(id);
            return View(result);
        }
        catch (Exception ex)
        {
            ViewBag.ExceptionMessage = ex.Message;
            return View();
        }
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, TaskUpdateModel model)
    {
        try
        {
            await taskService.UpdateAsync(id, model);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.ExceptionMessage = ex.Message;
            return View();
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await taskService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.ExceptionMessage = ex.Message;
            return RedirectToAction("Index");
        }
    }
}
