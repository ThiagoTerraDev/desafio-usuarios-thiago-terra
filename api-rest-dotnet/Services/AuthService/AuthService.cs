using Microsoft.EntityFrameworkCore;
using api_rest_dotnet.DTOs;
using api_rest_dotnet.Models;
using api_rest_dotnet.DataContext;
using api_rest_dotnet.Services.PasswordService;
using api_rest_dotnet.Services.TokenService;

namespace api_rest_dotnet.Services.AuthService
{
  public class AuthService : IAuthService
  {
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordInterface;
    private readonly ITokenService _tokenService;

    public AuthService(ApplicationDbContext context, IPasswordService passwordInterface, ITokenService tokenService)
    {
      _context = context;
      _passwordInterface = passwordInterface;
      _tokenService = tokenService;
    }

    public async Task<ServiceResponse<AuthResponseDto>> SignUp(SignupDto signupDto)
    {
      ServiceResponse<AuthResponseDto> serviceResponseAuth = new ServiceResponse<AuthResponseDto>();

      try
      {
        if (await UserAlreadyExists(signupDto.Email))
        {
          serviceResponseAuth.Data = null;
          serviceResponseAuth.Message = "Email já cadastrado!";
          serviceResponseAuth.Success = false;
          return serviceResponseAuth;
        }

        string passwordHash = _passwordInterface.HashPassword(signupDto.Password);
        
        var newUser = new UserModel
        {
          Name = signupDto.Name,
          LastName = signupDto.LastName,
          Email = signupDto.Email,
          PasswordHash = passwordHash,
          PasswordSalt = passwordHash, // atualmente uso BCrypt, que já gerencia internamente o salt, porém isso pode mudar no futuro
          Department = signupDto.Department,
          Shift = signupDto.Shift,
          Active = true,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow,
          TokenCreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var token = _tokenService.GenerateJwtToken(newUser);

        serviceResponseAuth.Data = new AuthResponseDto
        {
          UserId = newUser.Id,
          Name = newUser.Name,
          LastName = newUser.LastName,
          Email = newUser.Email,
          Token = token
        };
        serviceResponseAuth.Message = "User created successfully!";

      } catch (Exception ex)
      {
        serviceResponseAuth.Data = null;
        serviceResponseAuth.Message = ex.Message;
        serviceResponseAuth.Success = false;
      }

      return serviceResponseAuth;
    }

    public async Task<bool> UserAlreadyExists(string email)
    {
      return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<ServiceResponse<AuthResponseDto>> Login(LoginDto loginDto)
    {
      ServiceResponse<AuthResponseDto> serviceResponseAuth = new ServiceResponse<AuthResponseDto>();

      try 
      {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (existingUser == null || !_passwordInterface.VerifyPassword(loginDto.Password, existingUser.PasswordHash))
        {
          serviceResponseAuth.Data = null;
          serviceResponseAuth.Message = "Email ou senha inválidos";
          serviceResponseAuth.Success = false;
          return serviceResponseAuth;
        }

        var token = _tokenService.GenerateJwtToken(existingUser);

        serviceResponseAuth.Data = new AuthResponseDto
        {
          UserId = existingUser.Id,
          Name = existingUser.Name,
          LastName = existingUser.LastName,
          Email = existingUser.Email,
          Token = token
        };
        
        serviceResponseAuth.Message = "Login successful!";
      } catch (Exception ex) 
      {
        serviceResponseAuth.Data = null;
        serviceResponseAuth.Message = ex.Message;
        serviceResponseAuth.Success = false;
      }

      return serviceResponseAuth;
    }

    // public async Task<string> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    // {
    //   throw new NotImplementedException("ForgotPassword será implementado no próximo passo");
    // }

    // public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    // {
    //   throw new NotImplementedException("ResetPassword será implementado no próximo passo");
    // }
  }
}
