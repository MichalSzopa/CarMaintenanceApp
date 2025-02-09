using CarMaintenance.Service.Interface;
using CarMaintenance.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMainenance.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
	[AllowAnonymous]
	[HttpPost("RegisterUser")]
	public async Task<IActionResult> RegisterUser(RegisterUserModel model)
	{
		await authService.RegisterUserAsync(model);
		return Ok();
	}

	[AllowAnonymous]
	[HttpPost("Login")]
	public async Task<IActionResult> Login(LoginUserModel model)
	{
		try
		{
			var tokens = await authService.LoginAsync(model);

			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = DateTime.UtcNow.AddDays(7)
			};

			Response.Cookies.Append("refreshToken", tokens.RefreshToken, cookieOptions);

			return Ok(tokens.AccessToken);
		}
		catch (Exception ex) // TODO
		{
			return Unauthorized();
		}
	}

	[AllowAnonymous]
	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken()
	{
		// Get refresh token from HTTP-only cookie
		var refreshToken = Request.Cookies["refreshToken"];
		if (string.IsNullOrEmpty(refreshToken))
		{
			return Unauthorized();
		}

		var newAccessToken = await authService.RefreshJwtToken(refreshToken);
		if (newAccessToken == null)
		{
			return Unauthorized();
		}

		return Ok(newAccessToken);
	}

	[Authorize]
	[HttpPatch("Logout")]
	public async Task<IActionResult> Logout()
	{
		var userId = HttpContext.GetUserId();
		await authService.LogoutAsync(userId);
		return Ok();
	}
}
