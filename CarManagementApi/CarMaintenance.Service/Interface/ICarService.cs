namespace CarMaintenance.Service.Interface;

using CarMaintenance.Shared.Dtos;

public interface ICarService
{
  Task<List<CarList>> GetCarsAsync(int userId);
  Task<CarDetails> GetCarDetailsAsync(int userId, int carId);
  Task<int> CreateCarAsync(CreateCar model, int ownerId);
}
