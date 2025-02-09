using CarMaintenance.Database.Entities;
using CarMaintenance.Repository;
using CarMaintenance.Service.Interface;
using CarMaintenance.Shared.Dtos;

namespace CarMaintenance.Service.Service;

public class CarService(IUnitOfWork unitOfWork) : ICarService
{
  public async Task<int> CreateCarAsync(CreateCar model, int ownerId)
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

  public async Task<CarDetails> GetCarDetailsAsync(int userId, int carId)
  {
    var car = await unitOfWork.Cars.GetCarDetailsAsync(userId, carId);

    var result = new CarDetails
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
      result.Insurance = new CarInsuranceBasicData
      {
        StartDate = insurance.StartDate,
        EndDate = insurance.EndDate,
        Provider = insurance.Provider,
        PolicyNumber = insurance.PolicyNumber,
      };
    }

    return result;
  }

  public async Task<List<CarList>> GetCarsAsync(int userId)
  {
    var cars = await unitOfWork.Cars.GetUserCarsAsync(userId);

    var result = cars.Select(c => new CarList
    {
      Id = c.Id,
      Make = c.Make,
      Model = c.Model,
      Year = c.Year,
    }).ToList();

    return result;
  }
}
