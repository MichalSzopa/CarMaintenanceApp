using CarMaintenance.Shared.Dtos.Auth;

namespace CarMaintenance.Service.Interface;

public interface IAuthService
{
  Task RegisterUserAsync(RegisterUserModel model);
  Task<string> LoginAsync(LoginUserModel model);
  Task Logout(int userId);
}
