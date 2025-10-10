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

    public async Task<ServiceResponse<UserModel>> GetUserById(int id)
    {
      ServiceResponse<UserModel> serviceResponse = new ServiceResponse<UserModel>();

      try 
      {
        UserModel? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if(user == null)
        {
          serviceResponse.Data = null;
          serviceResponse.Message = "User not found!";
          serviceResponse.Success = false;
          return serviceResponse;
        }

        serviceResponse.Data = user;
      } catch (Exception ex)
      {
        serviceResponse.Message = ex.Message;
        serviceResponse.Success = false;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<UserModel>> UpdateUser(UserModel updatedUser)
    {
      ServiceResponse<UserModel> serviceResponse = new ServiceResponse<UserModel>();

      try 
      {
        UserModel? existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

        if (existingUser == null)
        {
          serviceResponse.Data = null;
          serviceResponse.Message = "User not found!";
          serviceResponse.Success = false;
          return serviceResponse;
        }

        existingUser.Name = updatedUser.Name;
        existingUser.LastName = updatedUser.LastName;
        existingUser.Department = updatedUser.Department;
        existingUser.Active = updatedUser.Active;
        existingUser.Shift = updatedUser.Shift;
        existingUser.UpdatedAt = DateTime.Now.ToLocalTime();

        await _context.SaveChangesAsync();

        serviceResponse.Data = existingUser;
        serviceResponse.Message = "User updated successfully!";
      } catch (Exception ex)
      {
        serviceResponse.Message = ex.Message;
        serviceResponse.Success = false;
      }

      return serviceResponse;
    }

    public Task<ServiceResponse<List<UserModel>>> DeleteUser(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<ServiceResponse<UserModel>> DeactivateUser(int id)
    {
      ServiceResponse<UserModel> serviceResponse = new ServiceResponse<UserModel>();

      try 
      {
        UserModel? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if(user == null)
        {
          serviceResponse.Data = null;
          serviceResponse.Message = "User not found!";
          serviceResponse.Success = false;
          return serviceResponse;
        }

        user.Active = false;
        user.UpdatedAt = DateTime.Now.ToLocalTime();

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        serviceResponse.Data = user;
        serviceResponse.Message = "User deactivated successfully!";
      } catch (Exception ex)
      {
        serviceResponse.Message = ex.Message;
        serviceResponse.Success = false;
      }

      return serviceResponse;
    }
  }
}
