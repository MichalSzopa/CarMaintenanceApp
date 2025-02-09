using CarMaintenance.Database;
using CarMaintenance.Database.Entities;
using CarMaintenance.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenance.Repository.Repository;

public class CarRepository(ICarMaintenanceDbContext dbContext) : ICarRepository
{
  public async Task AddCarAsync(Car car)
  {
    await dbContext.Cars.AddAsync(car);
    await dbContext.SaveChangesAsync();

    var carAccess = new CarAccess
    {
      CarId = car.Id,
      UserId = car.OwnerId,
      GrantedDate = DateTime.Now,
      ExpiryDate = null,
    };

    await dbContext.CarAccesses.AddAsync(carAccess);
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
