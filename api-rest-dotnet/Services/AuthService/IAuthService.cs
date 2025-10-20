using api_rest_dotnet.DTOs;
using api_rest_dotnet.Models;

namespace api_rest_dotnet.Services.AuthService
{
  public interface IAuthService
  {
    Task<ServiceResponse<AuthResponseDto>> SignUp(SignupDto signupDto);
    Task<ServiceResponse<AuthResponseDto>> Login(LoginDto loginDto);
    // Task<string> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    // Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
  }
}
