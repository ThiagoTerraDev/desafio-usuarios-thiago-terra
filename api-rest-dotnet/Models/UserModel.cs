using System.ComponentModel.DataAnnotations;
using api_rest_dotnet.Enums;

namespace api_rest_dotnet.Models
{
    public class UserModel
    {
      [Key]
      public int Id { get; set; }
      public required string Name { get; set; }
      public required string LastName { get; set; }
      public DepartmentEnum Department { get; set; }
      public bool Active { get; set; }
      public ShiftEnum Shift { get; set; }
      public DateTime CreatedAt { get; set; } = DateTime.Now.ToLocalTime();
      public DateTime UpdatedAt { get; set; } = DateTime.Now.ToLocalTime();
    }
}
