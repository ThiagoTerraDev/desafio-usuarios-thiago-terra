using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using api_rest_dotnet.Services.TokenService;
using api_rest_dotnet.Models;
using api_rest_dotnet.Enums;

namespace api_rest_dotnet.Tests
{
  public class TokenServiceTests
  {
    private readonly ITokenService _tokenService;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private const string TestTokenKey = "ThisIsAVerySecureTestKeyThatIsAtLeast64CharactersLongForHmacSha512Algorithm";

    public TokenServiceTests()
    {
      _mockConfiguration = new Mock<IConfiguration>();
      _mockConfiguration.Setup(x => x["AppSettings:Token"]).Returns(TestTokenKey);
      _tokenService = new TokenService(_mockConfiguration.Object);
    }

    [Fact]
    public void GenerateJwtToken_ShouldReturnNonEmptyString()
    {
      var user = CreateTestUser();

      var token = _tokenService.GenerateJwtToken(user);

      Assert.NotNull(token);
      Assert.NotEmpty(token);
    }

    [Fact]
    public void GenerateJwtToken_ShouldContainUserIdClaim()
    {
      var user = CreateTestUser();

      var token = _tokenService.GenerateJwtToken(user);
      var claims = DecodeToken(token);

      var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

      Assert.NotNull(userIdClaim);
      Assert.Equal(user.Id.ToString(), userIdClaim.Value);
    }

    [Fact]
    public void GenerateJwtToken_ShouldContainEmailClaim()
    {
      var user = CreateTestUser();

      var token = _tokenService.GenerateJwtToken(user);
      var claims = DecodeToken(token);

      var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

      Assert.NotNull(emailClaim);
      Assert.Equal(user.Email, emailClaim.Value);
    }

    [Fact]
    public void GenerateJwtToken_ShouldGenerateValidJwtFormat()
    {
      var user = CreateTestUser();

      var token = _tokenService.GenerateJwtToken(user);

      // JWT tem formato: header.payload.signature (3 partes separadas por ponto)
      var parts = token.Split('.');
      Assert.Equal(3, parts.Length);
    }

    private UserModel CreateTestUser()
    {
      return new UserModel
      {
        Id = 1,
        Name = "Test",
        LastName = "User",
        Email = "test@example.com",
        PasswordHash = "hashedPassword",
        PasswordSalt = "salt",
        Department = DepartmentEnum.RH,
        Shift = ShiftEnum.Manha,
        Active = true,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      };
    }

    // Helper method para decodificar token e extrair claims
    private IEnumerable<Claim> DecodeToken(string token)
    {
      var handler = new JwtSecurityTokenHandler();
      var jwtToken = handler.ReadJwtToken(token);
      return jwtToken.Claims;
    }
  }
}
