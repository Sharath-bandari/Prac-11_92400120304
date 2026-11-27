using System.ComponentModel.DataAnnotations;

namespace SmartCampusPortal.Models;

public class CampusNotice
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Title { get; set; } = "";

    [Required, StringLength(1000)]
    public string Message { get; set; } = "";

    public DateTime PostedOn { get; set; } = DateTime.UtcNow;
}
