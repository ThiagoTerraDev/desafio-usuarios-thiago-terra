
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using api_rest_dotnet.Services.UserService;
using api_rest_dotnet.DTOs;
using System.Threading.Tasks;
using api_rest_dotnet.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace api_rest_dotnet.Controllers
{
  [Route("api/users")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _userInterface;
    public UsersController(IUserService userInterface)
    {
      _userInterface = userInterface;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<UserModel>>>> GetUsers()
    {
      try
      {
        var serviceResponse = await _userInterface.GetUsers();

        if (!serviceResponse.Success)
        {
          return BadRequest(serviceResponse);
        }

        return Ok(serviceResponse);
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<List<UserModel>>
        {
          Data = null,
          Message = "Internal server error",
          Success = false
        });
      }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<UserModel>>> CreateUser(CreateUserDto createUserDto)
    {
      try
      {
        var serviceResponse = await _userInterface.CreateUserWithDto(createUserDto);
        
        if (!serviceResponse.Success)
        {
          return BadRequest(serviceResponse);
        }
        
        return CreatedAtAction(
          nameof(GetUserById),
          new { id = serviceResponse.Data!.Id },
          serviceResponse
        );
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<UserModel>
        {
          Data = null,
          Message = "Internal server error",
          Success = false
        });
      }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<UserModel>>> GetUserById(int id)
    {
      try
      {
        var serviceResponse = await _userInterface.GetUserById(id);
        
        if (!serviceResponse.Success)
        {
          return serviceResponse.Message.Contains("not found", StringComparison.OrdinalIgnoreCase)
            ? NotFound(serviceResponse)
            : BadRequest(serviceResponse);
        }
        
        return Ok(serviceResponse);
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<UserModel>
        {
          Data = null,
          Message = "Internal server error",
          Success = false
        });
      }
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<UserModel>>> UpdateUser(UpdateUserDto updatedUser)
    {
      try
      {
        var serviceResponse = await _userInterface.UpdateUserWithDto(updatedUser);
        
        if (!serviceResponse.Success)
        {
          return serviceResponse.Message.Contains("not found", StringComparison.OrdinalIgnoreCase)
            ? NotFound(serviceResponse)
            : BadRequest(serviceResponse);
        }
        
        return Ok(serviceResponse);
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<UserModel>
        {
          Data = null,
          Message = "Internal server error",
          Success = false
        });
      }
    }

    [Authorize]
    [HttpPatch("{id}/deactivate")]
    public async Task<ActionResult<ServiceResponse<UserModel>>> DeactivateUser(int id)
    {
      try
      {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (currentUserId != null && int.Parse(currentUserId) == id)
        {
          return BadRequest(new ServiceResponse<UserModel>
          {
            Data = null,
            Message = "Você não pode desativar sua própria conta",
            Success = false
          });
        }

        var serviceResponse = await _userInterface.DeactivateUser(id);
        
        if (!serviceResponse.Success)
        {
          return serviceResponse.Message.Contains("not found", StringComparison.OrdinalIgnoreCase)
            ? NotFound(serviceResponse)
            : BadRequest(serviceResponse);
        }
        
        return Ok(serviceResponse);
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<UserModel>
        {
          Data = null,
          Message = "Internal server error",
          Success = false
        });
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<UserModel>>> DeleteUser(int id)
    {
      try
      {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (currentUserId != null && int.Parse(currentUserId) == id)
        {
          return BadRequest(new ServiceResponse<UserModel>
          {
            Data = null,
            Message = "Você não pode excluir sua própria conta",
            Success = false
          });
        }

        var serviceResponse = await _userInterface.DeleteUser(id);
        
        if (!serviceResponse.Success)
        {
          return serviceResponse.Message.Contains("not found", StringComparison.OrdinalIgnoreCase)
            ? NotFound(serviceResponse)
            : BadRequest(serviceResponse);
        }
        
        return Ok(serviceResponse);
      } catch (Exception)
      {
        return StatusCode(500, new ServiceResponse<UserModel>
        {
          Data = null,
          Message = "Internal server error",
          Success = false
        });
      }
    }
  }
}
