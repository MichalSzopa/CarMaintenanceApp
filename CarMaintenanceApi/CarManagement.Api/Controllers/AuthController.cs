using CarMaintenance.Service.Interface;
using CarMaintenance.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CarMainenance.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
  // register
  [HttpPost("RegisterUser")]
  public async Task<IActionResult> RegisterUser(RegisterUserModel model)
  {
    await authService.RegisterUserAsync(model);
    return Ok();
  }

  // login

  // logout
}
