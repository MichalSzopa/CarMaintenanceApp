using CarMaintenance.Database.Entities;
using CarMaintenance.Repository;
using CarMaintenance.Service.Interface;
using CarMaintenance.Shared.Dtos.Auth;

namespace CarMaintenance.Service.Service;

public class AuthService(IUnitOfWork unitOfWork) : IAuthService
{
  public Task<string> Login(LoginUserModel model)
  {
    throw new NotImplementedException();
  }

  public Task Logout(int userId)
  {
    throw new NotImplementedException();
  }

  public async Task RegisterUserAsync(RegisterUserModel model)
  {
    ValidateEmail(model.Email);
    ValidatePasswordStrength(model.Password);

    var user = new User
    {
      Email = model.Email.ToUpper(),
      FirstName = model.FirstName.ToUpper(),
      LastName = model.LastName.ToUpper(),
      Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
    };

    await unitOfWork.Users.AddUserAsync(user);
    await unitOfWork.SaveChangesAsync();
  }

  private void ValidatePasswordStrength(string password)
  {
    // TODO
  }

  private void ValidateEmail(string email)
  {
    // TODO
  }
}
