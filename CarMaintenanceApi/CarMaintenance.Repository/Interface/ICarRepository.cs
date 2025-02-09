using CarMaintenance.Database.Entities;

namespace CarMaintenance.Repository.Interface;

public interface ICarRepository
{
	Task<List<Car>> GetUserCarsAsync(int userId);
	Task<Car> GetCarDetailsAsync(int userId, int carId);
	Task AddCarAsync(Car car);
}
