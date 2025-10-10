
using api_rest_dotnet.Models;

namespace api_rest_dotnet.Service.UserService
{
  public interface IUserService
  {
    Task<ServiceResponse<List<UserModel>>> GetUsers();
    Task<ServiceResponse<UserModel>> CreateUser(UserModel newUser);
    Task<ServiceResponse<UserModel>> GetUserById(int id);
    Task<ServiceResponse<List<UserModel>>> UpdateUser(UserModel updatedUser);
    Task<ServiceResponse<List<UserModel>>> DeleteUser(int id);
    Task<ServiceResponse<List<UserModel>>> DeactivateUser(int id);
  }
}
