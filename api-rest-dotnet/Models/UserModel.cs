using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using api_rest_dotnet.Enums;

namespace api_rest_dotnet.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserModel
    {
      [Key]
      public int Id { get; set; }
      public required string Name { get; set; }
      public required string LastName { get; set; }
      public required string Email { get; set; }
      public required string PasswordHash { get; set; }
      public required string PasswordSalt { get; set; }      
      public string? ResetPasswordToken { get; set; }      
      public DateTime? ResetPasswordTokenExpires { get; set; }
      public DateTime TokenCreatedAt { get; set; } = DateTime.UtcNow;
      public DepartmentEnum Department { get; set; }
      public bool Active { get; set; }
      public ShiftEnum Shift { get; set; }
      public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
