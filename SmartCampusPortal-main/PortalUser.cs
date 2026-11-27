using System.ComponentModel.DataAnnotations;

namespace SmartCampusPortal.Models;

public class PortalUser
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string FullName { get; set; } = "";

    [Required, EmailAddress, StringLength(120)]
    public string Email { get; set; } = "";

    [Required]
    public string PasswordHash { get; set; } = "";

    [Required]
    public string Role { get; set; } = "Student";

    [StringLength(30)]
    public string StudentNumber { get; set; } = "";
}
