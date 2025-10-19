using System.ComponentModel.DataAnnotations;
using api_rest_dotnet.Enums;

namespace api_rest_dotnet.DTOs
{
  public class UpdateUserDto
  {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Department is required")]
    public DepartmentEnum Department { get; set; }

    [Required(ErrorMessage = "Shift is required")]
    public ShiftEnum Shift { get; set;}
  }
}
