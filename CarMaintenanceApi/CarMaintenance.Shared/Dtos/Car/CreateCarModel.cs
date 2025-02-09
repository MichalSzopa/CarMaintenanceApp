namespace CarMaintenance.Shared.Dtos.Car;

public class CreateCarModel
{
  public string Make { get; set; }
  public string Model { get; set; }
  public int Year { get; set; }
  public string VIN { get; set; }
  public int Mileage { get; set; }
  public CarInsuranceBasicDataModel? Insurance { get; set; }
}
