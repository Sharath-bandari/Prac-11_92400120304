using System.ComponentModel.DataAnnotations;

namespace SmartCampusPortal.Models;

public class ServiceRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; } = "";

    [Required]
    public string Category { get; set; } = "Maintenance";

    [Required, StringLength(1000)]
    public string Description { get; set; } = "";

    public string Status { get; set; } = "Pending";
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? AdminRemark { get; set; }
}
