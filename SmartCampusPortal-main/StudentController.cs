using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Data;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Controllers;

public class StudentController : Controller
{
    private readonly CampusDbContext _db;
    public StudentController(CampusDbContext db) => _db = db;

    private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

    public async Task<IActionResult> Index()
    {
        if (CurrentUserId == null || HttpContext.Session.GetString("UserRole") != "Student")
            return RedirectToAction("Login", "Account");

        ViewBag.Total = await _db.ServiceRequests.CountAsync(r => r.UserId == CurrentUserId);
        ViewBag.Pending = await _db.ServiceRequests.CountAsync(r => r.UserId == CurrentUserId && r.Status == "Pending");
        ViewBag.Completed = await _db.ServiceRequests.CountAsync(r => r.UserId == CurrentUserId && r.Status == "Completed");
        ViewBag.Notices = await _db.Notices.OrderByDescending(n => n.PostedOn).Take(3).ToListAsync();
        return View();
    }

    [HttpGet]
    public IActionResult CreateRequest()
    {
        if (CurrentUserId == null) return RedirectToAction("Login", "Account");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRequest(RequestViewModel model)
    {
        if (CurrentUserId == null) return RedirectToAction("Login", "Account");
        if (!ModelState.IsValid) return View(model);

        _db.ServiceRequests.Add(new ServiceRequest
        {
            UserId = CurrentUserId.Value,
            Title = model.Title,
            Category = model.Category,
            Description = model.Description
        });
        await _db.SaveChangesAsync();
        TempData["Message"] = "Your service request has been submitted.";
        return RedirectToAction(nameof(MyRequests));
    }

    public async Task<IActionResult> MyRequests()
    {
        if (CurrentUserId == null) return RedirectToAction("Login", "Account");
        var requests = await _db.ServiceRequests
            .Where(r => r.UserId == CurrentUserId)
            .OrderByDescending(r => r.CreatedOn).ToListAsync();
        return View(requests);
    }

    public async Task<IActionResult> Notices()
    {
        if (CurrentUserId == null) return RedirectToAction("Login", "Account");
        return View(await _db.Notices.OrderByDescending(n => n.PostedOn).ToListAsync());
    }
}
