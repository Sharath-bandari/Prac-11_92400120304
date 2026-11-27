using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Data;

namespace SmartCampusPortal.Controllers;

public class HomeController : Controller
{
    private readonly CampusDbContext _db;
    public HomeController(CampusDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        ViewBag.Notices = await _db.Notices.OrderByDescending(n => n.PostedOn).Take(4).ToListAsync();
        return View();
    }

    public IActionResult Error() => View();
}
