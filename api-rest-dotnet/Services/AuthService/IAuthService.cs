using api_rest_dotnet.DTOs;
using api_rest_dotnet.Models;

namespace api_rest_dotnet.Services.AuthService
{
  public interface IAuthService
  {
    Task<ServiceResponse<AuthResponseDto>> SignUp(SignupDto signupDto);
    Task<ServiceResponse<AuthResponseDto>> Login(LoginDto loginDto);
    Task<ServiceResponse<string>> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
    Task<ServiceResponse<bool>> ResetPassword(ResetPasswordDto resetPasswordDto);
  }
}
