using System.ComponentModel.DataAnnotations;

namespace api_rest_dotnet.DTOs
{
  public class ForgotPasswordDto
  {
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; set; }
  }
}
