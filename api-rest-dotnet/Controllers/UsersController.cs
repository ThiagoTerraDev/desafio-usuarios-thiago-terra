
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
      var serviceResponse = await _userInterface.GetUsers();

      return serviceResponse.Success 
        ? Ok(serviceResponse) 
        : BadRequest(serviceResponse);
    }
  }
}
