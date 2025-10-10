
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using api_rest_dotnet.Service.UserService;
using System.Threading.Tasks;
using api_rest_dotnet.Models;

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

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<UserModel>>> CreateUser(UserModel newUser)
    {
      try
      {
        var serviceResponse = await _userInterface.CreateUser(newUser);
        
        if (!serviceResponse.Success)
        {
            return BadRequest(serviceResponse);
        }
        
        return StatusCode(201, serviceResponse); // todo: return CreatedAtAction(...) with GetUserById to ensure RESTful practices
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
