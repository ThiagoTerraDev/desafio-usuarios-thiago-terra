using api_rest_dotnet.DataContext;
using api_rest_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_rest_dotnet.Service.UserService
{
  public class UserService : IUserService
  { 
    private readonly ApplicationDbContext _context;
    public UserService(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<ServiceResponse<List<UserModel>>> GetUsers()
    {
      ServiceResponse<List<UserModel>> serviceResponse = new ServiceResponse<List<UserModel>>();

      try {
        serviceResponse.Data = await _context.Users.ToListAsync();

        if(serviceResponse.Data.Count == 0)
        {
          serviceResponse.Message = "No users found!";
        }
      } catch(Exception ex)
      {
        serviceResponse.Message = ex.Message;
        serviceResponse.Success = false;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<UserModel>> CreateUser(UserModel newUser)
    {
      ServiceResponse<UserModel> serviceResponse = new ServiceResponse<UserModel>();

      try 
      {
        if(newUser == null)
        {
          serviceResponse.Data = null;
          serviceResponse.Message = "Please provide user data!";
          serviceResponse.Success = false;
          return serviceResponse;
        }

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        serviceResponse.Data = newUser;
        serviceResponse.Message = "User created successfully!";
      } catch (Exception ex)
      {
        serviceResponse.Message = ex.Message;
        serviceResponse.Success = false;
      }

      return serviceResponse;
    }

    public Task<ServiceResponse<UserModel>> GetUserById(int id)
    {
      throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<UserModel>>> UpdateUser(UserModel updatedUser)
    {
      throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<UserModel>>> DeleteUser(int id)
    {
      throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<UserModel>>> DeactivateUser(int id)
    {
      throw new NotImplementedException();
    }
  }
}
