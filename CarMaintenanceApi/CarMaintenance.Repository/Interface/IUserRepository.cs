using CarMaintenance.Database.Entities;

namespace CarMaintenance.Repository.Interface;

public interface IUserRepository
{
	Task AddUserAsync(User user);
	Task<User> GetUserById(int userId);
	Task<User> GetUserByEmailAsync(string email);
	Task<User> GetUserByRefreshTokenAsync(string refreshToken);

}
