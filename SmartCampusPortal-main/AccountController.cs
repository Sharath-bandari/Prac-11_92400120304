using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Data;
using SmartCampusPortal.Models;
using SmartCampusPortal.Services;

namespace SmartCampusPortal.Controllers;

public class AccountController : Controller
{
    private readonly CampusDbContext _db;
    public AccountController(CampusDbContext db) => _db = db;

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var email = model.Email.Trim().ToLower();
        if (await _db.Users.AnyAsync(u => u.Email == email))
        {
            ModelState.AddModelError("Email", "This email is already registered.");
            return View(model);
        }

        var user = new PortalUser
        {
            FullName = model.FullName.Trim(),
            Email = email,
            PasswordHash = PasswordService.Hash(model.Password),
            StudentNumber = model.StudentNumber.Trim(),
            Role = "Student"
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        SetSession(user);
        return RedirectToAction("Index", "Student");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email.Trim().ToLower());
        if (user == null || !PasswordService.Verify(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }
        SetSession(user);
        return RedirectToAction(user.Role == "Admin" ? "Index" : "Index", user.Role == "Admin" ? "Admin" : "Student");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    private void SetSession(PortalUser user)
    {
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.FullName);
        HttpContext.Session.SetString("UserRole", user.Role);
    }
}
