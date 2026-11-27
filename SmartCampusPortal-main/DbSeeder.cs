using SmartCampusPortal.Models;
using SmartCampusPortal.Services;

namespace SmartCampusPortal.Data;

public static class DbSeeder
{
    public static void Seed(CampusDbContext db)
    {
        if (!db.Users.Any())
        {
            db.Users.AddRange(
                new PortalUser { FullName = "Campus Administrator", Email = "admin@campus.com", PasswordHash = PasswordService.Hash("Admin@123"), Role = "Admin", StudentNumber = "ADMIN" },
                new PortalUser { FullName = "Jayanth", Email = "student@campus.com", PasswordHash = PasswordService.Hash("Student@123"), Role = "Student", StudentNumber = "STU001" }
            );
        }

        if (!db.Notices.Any())
        {
            db.Notices.AddRange(
                new CampusNotice { Title = "Welcome to Smart Campus", Message = "Use this portal to submit and track campus service requests.", PostedOn = DateTime.UtcNow },
                new CampusNotice { Title = "Library Timings", Message = "The library is open from 9:00 AM to 6:00 PM on working days.", PostedOn = DateTime.UtcNow }
            );
        }
        db.SaveChanges();
    }
}
