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

    public async Task<ServiceResponse<string>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
      ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

      try
      {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);

        if (user == null)
        {
          serviceResponse.Data = null;
          serviceResponse.Message = "Se o email existir, um link de redefinição será enviado.";
          serviceResponse.Success = true;
          return serviceResponse;
        }

        var resetToken = Guid.NewGuid().ToString();
        
        user.ResetPasswordToken = resetToken;
        user.ResetPasswordTokenExpires = DateTime.UtcNow.AddHours(1);
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var resetLink = $"http://localhost:4200/reset-password?token={resetToken}";
        Console.WriteLine("========================================");
        Console.WriteLine("📧 RESET PASSWORD LINK (Console Output)");
        Console.WriteLine("========================================");
        Console.WriteLine($"User: {user.Email}");
        Console.WriteLine($"Link: {resetLink}");
        Console.WriteLine($"Token expira em: {user.ResetPasswordTokenExpires:dd/MM/yyyy HH:mm:ss} UTC");
        Console.WriteLine("========================================");

        serviceResponse.Data = resetToken;
        serviceResponse.Message = "Se o email existir, um link de redefinição será enviado.";
        serviceResponse.Success = true;
      } catch (Exception ex)
      {
        serviceResponse.Data = null;
        serviceResponse.Message = ex.Message;
        serviceResponse.Success = false;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
      ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

      try
      {
        var user = await _context.Users.FirstOrDefaultAsync(u => 
          u.ResetPasswordToken == resetPasswordDto.Token &&
          u.ResetPasswordTokenExpires > DateTime.UtcNow
        );

        if (user == null)
        {
          serviceResponse.Data = false;
          serviceResponse.Message = "Token inválido ou expirado.";
          serviceResponse.Success = false;
          return serviceResponse;
        }

        user.PasswordHash = _passwordInterface.HashPassword(resetPasswordDto.Password);
        user.PasswordSalt = user.PasswordHash;
        
        user.ResetPasswordToken = null;
        user.ResetPasswordTokenExpires = null;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        Console.WriteLine("========================================");
        Console.WriteLine("✅ PASSWORD RESET SUCCESSFUL");
        Console.WriteLine("========================================");
        Console.WriteLine($"User: {user.Email}");
        Console.WriteLine($"Reset at: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss} UTC");
        Console.WriteLine("========================================");

        serviceResponse.Data = true;
        serviceResponse.Message = "Senha redefinida com sucesso!";
        serviceResponse.Success = true;
      } catch (Exception ex)
      {
        serviceResponse.Data = false;
        serviceResponse.Message = ex.Message;
        serviceResponse.Success = false;
      }

      return serviceResponse;
    }
  }
}
