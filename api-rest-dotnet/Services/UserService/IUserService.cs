using api_rest_dotnet.Models;
using api_rest_dotnet.DTOs;

namespace api_rest_dotnet.Services.UserService
{
  public interface IUserService
  {
    Task<ServiceResponse<List<UserModel>>> GetUsers();
    Task<ServiceResponse<UserModel>> CreateUserWithDto(CreateUserDto createUserDto);
    Task<ServiceResponse<UserModel>> GetUserById(int id);
    Task<ServiceResponse<UserModel>> UpdateUserWithDto(UpdateUserDto updatedUser);
    Task<ServiceResponse<UserModel>> DeleteUser(int id);
    Task<ServiceResponse<UserModel>> DeactivateUser(int id);
  }
}
