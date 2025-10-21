using Xunit;
using api_rest_dotnet.Services.PasswordService;

namespace api_rest_dotnet.Tests
{
  public class PasswordServiceTests
  {
    private readonly IPasswordService _passwordService;

    public PasswordServiceTests()
    {
      _passwordService = new PasswordService();
    }

    [Fact]
    public void HashPassword_ShouldReturnNonEmptyString()
    {
      var password = "MySecurePassword123!";

      var hashedPassword = _passwordService.HashPassword(password);

      Assert.NotNull(hashedPassword);
      Assert.NotEmpty(hashedPassword);
    }

    [Fact]
    public void HashPassword_ShouldReturnDifferentHashFromOriginalPassword()
    {
      var password = "MySecurePassword123!";

      var hashedPassword = _passwordService.HashPassword(password);

      Assert.NotEqual(password, hashedPassword);
    }

    [Fact]
    public void HashPassword_ShouldGenerateDifferentHashesForSamePassword()
    {
      var password = "MySecurePassword123!";

      var hash1 = _passwordService.HashPassword(password);
      var hash2 = _passwordService.HashPassword(password);

      Assert.NotEqual(hash1, hash2); // BCrypt gera salt único para cada hash
    }

    [Fact]
    public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
    {
      var password = "MySecurePassword123!";
      var hashedPassword = _passwordService.HashPassword(password);

      var result = _passwordService.VerifyPassword(password, hashedPassword);

      Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_WithIncorrectPassword_ShouldReturnFalse()
    {
      var correctPassword = "MySecurePassword123!";
      var wrongPassword = "WrongPassword456!";
      var hashedPassword = _passwordService.HashPassword(correctPassword);

      var result = _passwordService.VerifyPassword(wrongPassword, hashedPassword);

      Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_WithEmptyPassword_ShouldReturnFalse()
    {
      var password = "MySecurePassword123!";
      var hashedPassword = _passwordService.HashPassword(password);

      var result = _passwordService.VerifyPassword("", hashedPassword);

      Assert.False(result);
    }
  }
}
