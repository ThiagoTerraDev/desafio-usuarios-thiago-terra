namespace api_rest_dotnet.Services.PasswordService
{
  public interface IPasswordService
  {
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
  }
}
