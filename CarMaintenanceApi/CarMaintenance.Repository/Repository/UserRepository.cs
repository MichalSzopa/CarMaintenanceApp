using CarMaintenance.Database;
using CarMaintenance.Database.Entities;
using CarMaintenance.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenance.Repository.Repository;

public class UserRepository(ICarMaintenanceDbContext dbContext) : IUserRepository
{
	public async Task AddUserAsync(User user)
	{
		var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
		if (existingUser != null)
		{
			throw new Exception("User with this email already exists"); // TODO specific exception
		}

		await dbContext.Users.AddAsync(user);
	}

	public async Task<User> GetUserByEmailAsync(string email)
	{
		var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
		return user;
	}

	public async Task<User> GetUserById(int userId)
	{
		var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
		return user;
	}

	public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
	{
		var user = await dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.Now);
		return user;
	}
}
