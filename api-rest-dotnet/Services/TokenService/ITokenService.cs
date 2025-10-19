using api_rest_dotnet.Models;

namespace api_rest_dotnet.Services.TokenService
{
  public interface ITokenService
  {
    string GenerateJwtToken(UserModel user);
  }
}

