using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Data;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Controllers;

public class AdminController : Controller
{
    private readonly CampusDbContext _db;
    public AdminController(CampusDbContext db) => _db = db;

    private bool IsAdmin => HttpContext.Session.GetString("UserRole") == "Admin";

    public async Task<IActionResult> Index()
    {
        if (!IsAdmin) return RedirectToAction("Login", "Account");
        ViewBag.Total = await _db.ServiceRequests.CountAsync();
        ViewBag.Pending = await _db.ServiceRequests.CountAsync(r => r.Status == "Pending");
        ViewBag.Completed = await _db.ServiceRequests.CountAsync(r => r.Status == "Completed");
        return View(await _db.ServiceRequests.OrderByDescending(r => r.CreatedOn).ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string status, string? remark)
    {
        if (!IsAdmin) return RedirectToAction("Login", "Account");
        var request = await _db.ServiceRequests.FindAsync(id);
        if (request == null) return NotFound();
        if (new[] { "Pending", "In Progress", "Completed" }.Contains(status))
        {
            request.Status = status;
            request.AdminRemark = remark;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult CreateNotice()
    {
        if (!IsAdmin) return RedirectToAction("Login", "Account");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNotice(CampusNotice notice)
    {
        if (!IsAdmin) return RedirectToAction("Login", "Account");
        if (!ModelState.IsValid) return View(notice);
        notice.PostedOn = DateTime.UtcNow;
        _db.Notices.Add(notice);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
