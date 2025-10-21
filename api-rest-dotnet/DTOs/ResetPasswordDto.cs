using System.ComponentModel.DataAnnotations;

namespace api_rest_dotnet.DTOs
{
  public class ResetPasswordDto
  {
    [Required(ErrorMessage = "Token is required")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "ConfirmPassword is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public required string ConfirmPassword { get; set; }
  }
}
