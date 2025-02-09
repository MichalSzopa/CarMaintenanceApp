using CarMaintenance.Database.Entities;
using CarMaintenance.Repository;
using CarMaintenance.Service.Interface;
using CarMaintenance.Shared.Codes;
using CarMaintenance.Shared.Dtos.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CarMaintenance.Service.Service;

public class AuthService(IUnitOfWork unitOfWork, IConfiguration configuration) : IAuthService
{
	public async Task<AccessTokensModel> LoginAsync(LoginUserModel model)
	{
		var user = await unitOfWork.Users.GetUserByEmailAsync(model.Email.ToUpper());

		if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
		{
			throw new Exception("Wrong email or password"); // TODO
		}

		string token = GenerateJwtToken(user);

		var refreshToken = GenerateRefreshToken();
		user.RefreshToken = refreshToken;
		user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
		await unitOfWork.SaveChangesAsync();

		var result = new AccessTokensModel
		{
			AccessToken = token,
			RefreshToken = refreshToken
		};

		return result;
	}

	public async Task LogoutAsync(int userId)
	{
		var user = await unitOfWork.Users.GetUserById(userId);
		user.RefreshToken = null;
		user.RefreshTokenExpiry = null;
		await unitOfWork.SaveChangesAsync();
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

	public async Task<string?> RefreshJwtToken(string refreshToken)
	{
		var user = await unitOfWork.Users.GetUserByRefreshTokenAsync(refreshToken);

		if (user == null)
		{
			return null; // Invalid or expired refresh token
		}

		// Generate new JWT
		var newAccessToken = GenerateJwtToken(user);
		var newRefreshToken = GenerateRefreshToken();

		user.RefreshToken = newRefreshToken;
		user.RefreshTokenExpiry = DateTime.Now.AddDays(7);

		await unitOfWork.SaveChangesAsync();

		return newAccessToken;
	}

	private void ValidatePasswordStrength(string password)
	{
		string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

		if (!Regex.IsMatch(password, pattern))
		{
			throw new Exception("Password must contain at least 1 uppercase letter, 1 lowercase letter, 1 digit and be at least 8 characters long"); // TODO
		}
	}

	private void ValidateEmail(string email)
	{
		string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
		if (!Regex.IsMatch(email, pattern))
		{
			throw new Exception("Invalid email address"); // TODO
		}
	}

	private string GenerateRefreshToken()
	{
		var randomBytes = new byte[64];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomBytes);
		return Convert.ToBase64String(randomBytes);
	}

	private string GenerateJwtToken(User user)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var claims = new[]
			{
			new Claim(Claims.UserEmail, user.Email),
			new Claim(Claims.TokenIdentifier, Guid.NewGuid().ToString()),
			new Claim(Claims.UserId, user.Id.ToString())
		};

		var token = new JwtSecurityToken(
			issuer: configuration["Jwt:Issuer"],
			audience: configuration["Jwt:Audience"],
			expires: DateTime.UtcNow.AddMinutes(30),
			claims: claims,
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
