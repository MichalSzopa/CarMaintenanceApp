using CarMaintenance.Shared.Dtos.Auth;

namespace CarMaintenance.Service.Interface;

public interface IAuthService
{
	Task RegisterUserAsync(RegisterUserModel model);
	Task<AccessTokensModel> LoginAsync(LoginUserModel model);
	Task LogoutAsync(int userId);
	Task<string?> RefreshJwtToken(string refreshToken);
}
