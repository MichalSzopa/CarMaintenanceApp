namespace CarMaintenance.Service.Interface;

using CarMaintenance.Shared.Dtos.Car;

public interface ICarService
{
  Task<List<CarListModel>> GetCarsAsync(int userId);
  Task<CarDetailsModel> GetCarDetailsAsync(int userId, int carId);
  Task<int> CreateCarAsync(CreateCarModel model, int ownerId);
}
