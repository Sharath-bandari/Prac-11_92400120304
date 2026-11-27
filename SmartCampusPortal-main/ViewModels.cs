using System.ComponentModel.DataAnnotations;

namespace SmartCampusPortal.Models;

public class RegisterViewModel
{
    [Required, StringLength(80)]
    public string FullName { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, MinLength(8), DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required]
    public string StudentNumber { get; set; } = "";
}

public class LoginViewModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = "";
}

public class RequestViewModel
{
    [Required, StringLength(100)]
    public string Title { get; set; } = "";

    [Required]
    public string Category { get; set; } = "Maintenance";

    [Required, StringLength(1000)]
    public string Description { get; set; } = "";
}
