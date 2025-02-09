using CarMaintenance.Service.Interface;
using CarMaintenance.Shared.Dtos.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMainenance.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CarsController(ICarService carService) : ControllerBase
{
	// get car list
	[Authorize]
	[HttpGet("GetAllCarsForUser")]
	public async Task<IActionResult> GetAllCarsForUser()
	{
		var userId = HttpContext.GetUserId();
		var result = await carService.GetCarsAsync(userId);
		return Ok(result);
	}

	// get car details
	[Authorize]
	[HttpGet("GetCarDetails")]
	public async Task<IActionResult> GetCarDetails(int carId)
	{
		var userId = HttpContext.GetUserId();
		var result = await carService.GetCarDetailsAsync(userId, carId);
		return Ok(result);
	}

	// create car
	[Authorize]
	[HttpPost("CreateCar")]
	public async Task<IActionResult> CreateCar(CreateCarModel createCar)
	{
		var userId = HttpContext.GetUserId();
		var result = await carService.CreateCarAsync(createCar, userId);
		return Ok(result);
	}

	// grant access to car

	// revoke access to car

	// archive car

	// get car history - perhaps db procedure case
	// get report for car - perhaps db procedure case
}
