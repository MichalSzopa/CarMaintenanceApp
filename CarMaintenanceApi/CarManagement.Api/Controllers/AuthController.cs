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
  [HttpPost("Login")]
  public async Task<IActionResult> Login(LoginUserModel model)
  {
    try
    {
      var token = await authService.LoginAsync(model);
      return Ok(token);
    }
    catch (Exception ex) // TODO
    {
      return Unauthorized();
    }
  }

  // logout


  //[Authorize]
  //[HttpGet("TestAuthEndpoint")]
  //public async Task<IActionResult> TestAuthMethod()
  //{
  //  return Ok("You are authorized");
  //}
}
