using Microsoft.AspNetCore.Mvc;
using api_rest_dotnet.DTOs;
using api_rest_dotnet.Services.AuthService;
using api_rest_dotnet.Models;

namespace api_rest_dotnet.Controllers
{
  [Route("api/auth")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authInterface)
    {
      _authService = authInterface;
    }

    /// <summary>
    /// Registra um novo usuário no sistema
    /// </summary>
    /// <param name="signupDto">Dados do novo usuário</param>
    /// <returns>Dados do usuário criado e token JWT</returns>
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Dados inválidos ou email já cadastrado</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ServiceResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Signup(SignupDto signupDto)
    {
      try
      {
        var serviceResponseAuth = await _authService.SignUp(signupDto);

        if (!serviceResponseAuth.Success)
        {
          return BadRequest(serviceResponseAuth);
        }
        
        return Ok(serviceResponseAuth);
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<AuthResponseDto>
        {
          Data = null,
          Message = "Internal server error",
          Success = false
        });
      }
    }

    /// <summary>
    /// Autentica um usuário existente (Login)
    /// </summary>
    /// <param name="loginDto">Credenciais do usuário</param>
    /// <returns>Dados do usuário e token JWT</returns>
    /// <response code="200">Login realizado com sucesso</response>
    /// <response code="401">Credenciais inválidas</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ServiceResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
      try
      {
        var response = await _authService.Login(loginDto);
        return Ok(response);
      } catch (Exception ex)
      {
        return Unauthorized(new { message = ex.Message });
      }
    }

    /// <summary>
    /// Inicia o processo de recuperação de senha
    /// </summary>
    /// <param name="forgotPasswordDto">Email do usuário</param>
    /// <returns>Link para reset de senha (será logado no console)</returns>
    /// <response code="200">Link de recuperação gerado (verifique o console do servidor)</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
      try
      {
        var serviceResponse = await _authService.ForgotPassword(forgotPasswordDto);
        
        if (!serviceResponse.Success)
        {
          return BadRequest(serviceResponse);
        }
        
        return Ok(serviceResponse);
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<string>
        {
          Data = null,
          Message = "Internal server error",
          Success = false
        });
      }
    }

    /// <summary>
    /// Finaliza o processo de reset de senha
    /// </summary>
    /// <param name="resetPasswordDto">Token e nova senha</param>
    /// <returns>Confirmação de senha alterada</returns>
    /// <response code="200">Senha alterada com sucesso</response>
    /// <response code="400">Token inválido ou expirado</response>
    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
      try
      {
        var serviceResponse = await _authService.ResetPassword(resetPasswordDto);
        
        if (!serviceResponse.Success)
        {
          return BadRequest(serviceResponse);
        }
        
        return Ok(serviceResponse);
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<bool>
        {
          Data = false,
          Message = "Internal server error",
          Success = false
        });
      }
    }
  }
}
