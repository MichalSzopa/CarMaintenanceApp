using CarMaintenance.Shared.Codes;

namespace CarMainenance.Api;

public static class HttpContextHelper
{
	public static int GetUserId(this HttpContext httpContext)
	{
		string userId = httpContext.User.FindFirst(c => c.Type == Claims.UserId)?.Value;
		return int.Parse(userId);
	}
}
