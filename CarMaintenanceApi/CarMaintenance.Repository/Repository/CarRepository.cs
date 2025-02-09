using CarMaintenance.Database;
using CarMaintenance.Database.Entities;
using CarMaintenance.Repository.Interface;
using CarMaintenance.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenance.Repository.Repository;

public class CarRepository(ICarMaintenanceDbContext dbContext) : ICarRepository
{
	// TODO add asNoTracking to queries where tracking is not necessary
	public async Task AddCarAsync(Car car)
	{

		var carAccess = new CarAccess
		{
			CarId = car.Id,
			UserId = car.OwnerId,
			GrantedDate = DateTime.Now,
			ExpiryDate = null,
		};

		var initialService = new CarServicing
		{
			CarId = car.Id,
			ServiceDate = DateTime.Now,
			Description = "Car added to system",
			Mileage = car.Mileage,
			Type = EServiceType.Other,
		};

		car.GrantedAccesses = new List<CarAccess> { carAccess };
		car.CarServices = new List<CarServicing> { initialService };

		await dbContext.Cars.AddAsync(car);
	}

	public async Task<Car> GetCarDetailsAsync(int userId, int carId)
	{
		var car = await dbContext.Cars
								 .Where(c => c.Id == carId && c.GrantedAccesses.Any(ga => ga.UserId == userId))
								 .Include(c => c.Owner)
								 .Include(c => c.CarInsurances.Where(ci => ci.StartDate <= DateTime.Now && ci.EndDate >= DateTime.Now))
								 .FirstOrDefaultAsync();

		return car;
	}

	public async Task<List<Car>> GetUserCarsAsync(int userId)
	{
		var cars = await dbContext.CarAccesses
		  .Where(c => c.UserId == userId)
		  .Select(c => c.Car)
		  .ToListAsync();
		return cars;
	}
}
