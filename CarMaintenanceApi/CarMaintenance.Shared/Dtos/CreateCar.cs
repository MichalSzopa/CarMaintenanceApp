namespace CarMaintenance.Shared.Dtos;

public class CreateCar
{
  public string Make { get; set; }
  public string Model { get; set; }
  public int Year { get; set; }
  public string VIN { get; set; }
  public int Mileage { get; set; }
  public CarInsuranceBasicData? Insurance { get; set; }
}
