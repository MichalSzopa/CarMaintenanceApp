using CarMaintenance.Database.Entities;
using CarMaintenance.Repository;
using CarMaintenance.Service.Interface;
using CarMaintenance.Shared.Dtos.Car;

namespace CarMaintenance.Service.Service;

public class CarService(IUnitOfWork unitOfWork) : ICarService
{
  public async Task<int> CreateCarAsync(CreateCarModel model, int ownerId)
  {
    var car = new Car
    {
      Make = model.Make.ToUpper(),
      Model = model.Model.ToUpper(),
      Year = model.Year,
      VIN = model.VIN.ToUpper(),
      OwnerId = ownerId,
      Mileage = model.Mileage,
    };

    if (model.Insurance != null)
    {
      car.CarInsurances = new List<CarInsurance>
      {
        new()
        {
          StartDate = model.Insurance.StartDate.Date,
          EndDate = model.Insurance.EndDate.Date,
          Provider = model.Insurance.Provider.ToUpper(),
          PolicyNumber = model.Insurance.PolicyNumber.ToUpper(),
        },
      };
    }

    try
    {
      await unitOfWork.BeginTransactionAsync();
      await unitOfWork.Cars.AddCarAsync(car);
      await unitOfWork.CommitAsync();
      await unitOfWork.SaveChangesAsync();
      return car.Id;
    }
    catch
    {
      await unitOfWork.RollbackAsync();
      throw;
    }
  }

  public async Task<CarDetailsModel> GetCarDetailsAsync(int userId, int carId)
  {
    var car = await unitOfWork.Cars.GetCarDetailsAsync(userId, carId);

    var result = new CarDetailsModel
    {
      Make = car.Make,
      Model = car.Model,
      Year = car.Year,
      VIN = car.VIN,
      Mileage = car.Mileage,
      Owner = $"{car.Owner.FirstName} {car.Owner.LastName}",
    };

    if (car.CarInsurances.Count > 0)
    {
      var insurance = car.CarInsurances.FirstOrDefault();
      result.Insurance = new CarInsuranceBasicDataModel
      {
        StartDate = insurance.StartDate,
        EndDate = insurance.EndDate,
        Provider = insurance.Provider,
        PolicyNumber = insurance.PolicyNumber,
      };
    }

    return result;
  }

  public async Task<List<CarListModel>> GetCarsAsync(int userId)
  {
    var cars = await unitOfWork.Cars.GetUserCarsAsync(userId);

    var result = cars.Select(c => new CarListModel
    {
      Id = c.Id,
      Make = c.Make,
      Model = c.Model,
      Year = c.Year,
    }).ToList();

    return result;
  }
}
