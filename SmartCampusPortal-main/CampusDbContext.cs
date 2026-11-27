using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Data;

public class CampusDbContext : DbContext
{
    public CampusDbContext(DbContextOptions<CampusDbContext> options) : base(options) { }

    public DbSet<PortalUser> Users => Set<PortalUser>();
    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
    public DbSet<CampusNotice> Notices => Set<CampusNotice>();
}
