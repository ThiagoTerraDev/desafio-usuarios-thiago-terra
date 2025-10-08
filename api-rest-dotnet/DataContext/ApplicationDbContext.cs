using Microsoft.EntityFrameworkCore;
using api_rest_dotnet.Models;

namespace api_rest_dotnet.DataContext
{
    public class ApplicationDbContext : DbContext
    {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
      {
      }

      public DbSet<UserModel> User { get; set; }
    }
}
