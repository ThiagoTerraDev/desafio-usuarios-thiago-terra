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

    // [ABAIXO] Pendente de implementação!
    /// <summary>
    /// Inicia o processo de recuperação de senha
    /// </summary>
    /// <param name="forgotPasswordDto">Email do usuário</param>
    /// <returns>Link para reset de senha (será logado no console)</returns>
    /// <response code="200">Email de recuperação enviado</response>
    /// <response code="404">Email não encontrado</response>
    // [HttpPost("forgot-password")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    // {
    //   try
    //   {
    //     if (!ModelState.IsValid)
    //     {
    //       return BadRequest(ModelState);
    //     }

    //     var resetLink = await _authService.ForgotPasswordAsync(forgotPasswordDto);
    //     
    //     // Loga o link no console em vez de enviar email
    //     Console.WriteLine("===========================================");
    //     Console.WriteLine("LINK DE RESET DE SENHA:");
    //     Console.WriteLine(resetLink);
    //     Console.WriteLine("===========================================");

    //     return Ok(new { message = "Link de reset de senha gerado (verifique o console)" });
    //   }
    //   catch (Exception ex)
    //   {
    //     return NotFound(new { message = ex.Message });
    //   }
    // }

    /// <summary>
    /// Finaliza o processo de reset de senha
    /// </summary>
    /// <param name="resetPasswordDto">Token e nova senha</param>
    /// <returns>Confirmação de senha alterada</returns>
    /// <response code="200">Senha alterada com sucesso</response>
    /// <response code="400">Token inválido ou expirado</response>
    // [HttpPost("reset-password")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    // {
    //   try
    //   {
    //     if (!ModelState.IsValid)
    //     {
    //       return BadRequest(ModelState);
    //     }

    //     var result = await _authService.ResetPasswordAsync(resetPasswordDto);
    //     
    //     if (result)
    //     {
    //       return Ok(new { message = "Senha alterada com sucesso!" });
    //     }
    //     
    //     return BadRequest(new { message = "Falha ao alterar senha" });
    //   }
    //   catch (Exception ex)
    //   {
    //     return BadRequest(new { message = ex.Message });
    //   }
    // }
  }
}
