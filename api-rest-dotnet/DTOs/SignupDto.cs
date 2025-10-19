using System.ComponentModel.DataAnnotations;
using api_rest_dotnet.Enums;

namespace api_rest_dotnet.DTOs
{
  public class SignupDto
  {
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "ConfirmPassword is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Department is required")]
    public DepartmentEnum Department { get; set; }

    [Required(ErrorMessage = "Shift is required")]
    public ShiftEnum Shift { get; set; }
  }
}
