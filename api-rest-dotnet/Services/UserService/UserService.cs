using api_rest_dotnet.DataContext;
using api_rest_dotnet.Models;
using api_rest_dotnet.DTOs;
using api_rest_dotnet.Services.PasswordService;
using Microsoft.EntityFrameworkCore;

namespace api_rest_dotnet.Services.UserService
{
  public class UserService : IUserService
  { 
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordService;

    public UserService(ApplicationDbContext context, IPasswordService passwordService)
    {
      _context = context;
      _passwordService = passwordService;
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

    public async Task<ServiceResponse<UserModel>> CreateUserWithDto(CreateUserDto createUserDto)
    {
      ServiceResponse<UserModel> serviceResponse = new ServiceResponse<UserModel>();

      try
      {
        if (await _context.Users.AnyAsync(u => u.Email == createUserDto.Email))
        {
          serviceResponse.Data = null;
          serviceResponse.Message = "Email já cadastrado!";
          serviceResponse.Success = false;
          return serviceResponse;
        }

        string passwordHash = _passwordService.HashPassword(createUserDto.Password);

        var newUser = new UserModel
        {
          Name = createUserDto.Name,
          LastName = createUserDto.LastName,
          Email = createUserDto.Email,
          PasswordHash = passwordHash,
          PasswordSalt = passwordHash,
          Department = createUserDto.Department,
          Shift = createUserDto.Shift,
          Active = true,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow,
          TokenCreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        serviceResponse.Data = newUser;
        serviceResponse.Message = "User created successfully!";
      }
      catch (Exception ex)
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

    public async Task<ServiceResponse<UserModel>> UpdateUserWithDto(UpdateUserDto updatedUser)
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

        if (existingUser.Email != updatedUser.Email)
        {
          var emailExists = await _context.Users.AnyAsync(u => u.Email == updatedUser.Email && u.Id != updatedUser.Id);
          if (emailExists)
          {
            serviceResponse.Data = null;
            serviceResponse.Message = "Email/usuário já cadastrados!";
            serviceResponse.Success = false;
            return serviceResponse;
          }
        }

        existingUser.Name = updatedUser.Name;
        existingUser.LastName = updatedUser.LastName;
        existingUser.Email = updatedUser.Email;
        existingUser.Department = updatedUser.Department;
        existingUser.Shift = updatedUser.Shift;
        existingUser.UpdatedAt = DateTime.UtcNow;

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

    public async Task<ServiceResponse<UserModel>> DeleteUser(int id)
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

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        serviceResponse.Data = user;
        serviceResponse.Message = "User deleted successfully!";
      } catch (Exception ex)
      {
        serviceResponse.Message = ex.Message;
        serviceResponse.Success = false;
      }

      return serviceResponse;
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
        user.UpdatedAt = DateTime.UtcNow;

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
